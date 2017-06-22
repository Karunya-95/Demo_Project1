using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using System.Web.Mvc;

namespace Demo_Project1.Models
{
	public class Register
	{
		//For Login Model
		public int ValidateUser(string User_Name, string Password)
		{
			string strcon = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
			int UserTypeId;
			SqlConnection con = new SqlConnection(strcon);
			SqlCommand cmd = new SqlCommand();
			try
			{
				cmd.Connection = con;
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.CommandText = "LoginUser";
				cmd.Parameters.Add("@User_Name", SqlDbType.VarChar).Value = User_Name;
				cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = Password;
				cmd.Parameters.Add("@result", SqlDbType.Int);
				cmd.Parameters["@result"].Direction = ParameterDirection.Output;
				con.Open();
				cmd.ExecuteNonQuery();
				UserTypeId = Convert.ToInt32(cmd.Parameters["@result"].Value);
			}
			catch (Exception e)
			{
				throw e;
			}
			finally
			{
				con.Close();
			}
			return UserTypeId;
		}

		//For Register Model
		public void Savedata(string User_Name, DateTime DOB, string mailId, string Password, string Confirm_Password, string Phone_no, string Address)
		{

			string strcon = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
			SqlConnection DbConnection = new SqlConnection(strcon);
			DbConnection.Open();
			SqlCommand command = new SqlCommand("INSERT into Registration_Table (User_Name,DOB,mailId,Password,Confirm_Password,Phone_no,Address) VALUES (@User_Name,@DOB,@mailId,@Password,@Confirm_Password,@Phone_no,@Address)", DbConnection);
			command.Parameters.Add(new SqlParameter("@User_Name", User_Name));
			command.Parameters.Add(new SqlParameter("@DOB", DOB));
			command.Parameters.Add(new SqlParameter("@mailId", mailId));
			command.Parameters.Add(new SqlParameter("@Password", Password));
			command.Parameters.Add(new SqlParameter("@Confirm_Password", Confirm_Password));
			command.Parameters.Add(new SqlParameter("@Phone_no", Phone_no));
			command.Parameters.Add(new SqlParameter("@Address", Address));
			command.ExecuteNonQuery();
		}

		//For Questionniare Model
		public List<questions> Questionniare()
		{
			List<questions> listid = new List<questions>();
			
			string strcon = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
			SqlConnection conn = new SqlConnection(strcon);
			string queryString = " select * from QuestionsTable";
			conn.Open();
			SqlCommand cmd = new SqlCommand(queryString, conn);
			SqlDataReader dr = cmd.ExecuteReader();
			while (dr.Read())
			{
				questions question = new questions();
				question.QuestionId = Convert.ToInt32(dr["QuestionId"]);
				question.Questions = dr["Questions"].ToString();
				question.OptionA = dr["OptionA"].ToString();
				question.OptionB = dr["OptionB"].ToString();
				question.OptionC = dr["OptionC"].ToString();
				question.OptionD = dr["OptionD"].ToString();
				question.OptionE = dr["OptionE"].ToString();
				question.Correct_Answer = dr["Correct_Answer"].ToString();
				listid.Add(question);
			}
			return listid;
		}

		public void SaveDataFromUser(List<SelectListItem> list, List<questions> listids,string UserID)
		{

			string strcon = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
			SqlConnection DbConnection = new SqlConnection(strcon);
			DbConnection.Open();

			for (int i = 0; i <= 19; i++)
			{
				SqlCommand command = new SqlCommand("insert into Save_Table (User_Id , QuestionId ,Questions, Correct_Answer) values('" + UserID + "',"+ list[i].Text + ",'" + listids[i].Questions + "','" + list[i].Value + "')", DbConnection);
				
				command.ExecuteNonQuery();
			}
		}

		//For Save Table
		//public void getresults(string QuestionId, string Questions, string Correct_Answer)
		//{
		//	string strcon = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
		//	SqlConnection DbConnection = new SqlConnection(strcon);
		//	DbConnection.Open();
		//	SqlCommand command = new SqlCommand("INSERT into Save_Table (QuestionId,Questions,Correct_Answer) VALUES (@QuestionId,@Questions,@Correct_Answer", DbConnection);
		//	command.Parameters.Add(new SqlParameter("@QuestionId", QuestionId));
		//	command.Parameters.Add(new SqlParameter("@Questions", Questions));
		//	command.Parameters.Add(new SqlParameter("@Correct_Answer", Correct_Answer));
		//	command.ExecuteNonQuery();
		//}
	}
}

	
				

