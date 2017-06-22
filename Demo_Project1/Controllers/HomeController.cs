using Demo_Project1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Demo_Project1.Controllers
{	
    public class HomeController : Controller
    {
		public ActionResult Index()
        {
            return View();
        }

		//For Login Controller
		public ActionResult ValidateUser(string User_Name, string Password)
		{
			Int32 validateuser = 0;
			Register Validate = new Register();
			validateuser = Validate.ValidateUser(User_Name, Password);
			if (validateuser == 1)
			{
				//return new FilePathResult("Instruction.cshtml", "text/html");
				//return View("~/Views/Home/Instruction.cshtml");
				//return RedirectToActionPermanent("Instruction");
				//var authenticate = FormsAuthentication.Authenticate(User_Name, Password);
				Session["UseerId"] = User_Name;
				return Json(new { status = "Success", message = "Success" });
			}
			else
				return Json(new { status = "Error", message = "Failed" });

		}

		//For Register Controller
		public JsonResult SaveDetailedInfo(string User_Name, DateTime DOB, string mailId, string Password, string Confirm_Password, string Phone_no, string Address)
		{

			Register Log = new Register();
			Log.Savedata(User_Name, DOB, mailId, Password, Confirm_Password, Phone_no, Address);
			return Json(new { status = "Success", message = "Success" });
		}

		public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

		public ActionResult Instruction()
		{
			
			return View();
		}

		public ActionResult Questionnaire()
		{
			Register Reg = new Register();
			List<questions> listids = new List<questions>();
			listids = Reg.Questionniare();
			return View(listids);
		}
		public ActionResult GenerateResult(List<SelectListItem> list)
		{
			int rightcount = 0;
			int wrongans = 0;

			Register Reg = new Register();
			List<questions> listids = new List<questions>();
			listids = Reg.Questionniare();
			for (int i = 0; i <= 19; i++)
			{
				if (listids[i].QuestionId == Convert.ToInt32(list[i].Text) && listids[i].Correct_Answer == list[i].Value)
				{
					rightcount += 1;
				}
				else
				{
					wrongans += 1;
				}
			}
			string UserID = Session["UseerId"].ToString();
			Reg.SaveDataFromUser(list, listids, UserID);
			//List<questions> listids = new List<questions>();
			//listids = Reg.Questionniare();
			//if (list == listids)
			//{
			//	return Json(new { status = "Success ", message = "Success" });
			//}
			//else
			//{
			//	return Json(new { status = "Error", message = "Error" });
			//}
			return Json(new { rAns = rightcount, wans = wrongans });
		}
		public ActionResult Logout()
		{
			FormsAuthentication.SignOut();
			return RedirectToAction("Index", "Home", null);

		}
	}
}