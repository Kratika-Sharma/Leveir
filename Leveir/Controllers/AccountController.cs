using Leveir.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Leveir.Controllers
{
    [CustomAuthorize]
    public class AccountController : Controller
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(Users user)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM tbl_Users WHERE Email = @Email AND Password = @Password AND RoleId = 1;";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", user.Email);
                        cmd.Parameters.AddWithValue("@Password", user.Password);
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Session["UserId"] = Convert.ToInt32(reader["UserId"]);
                                Session["Username"] = Convert.ToString(reader["Username"]);
                                Session["Email"] = Convert.ToString(reader["Email"]);
                                Session["Password"] = Convert.ToString(reader["Password"]);
                                return RedirectToAction("Dashboard", "Admin");
                            }
                            else
                            {
                                ViewBag.ErrorMessage = "Invalid Credentials";
                            }
                        }
                    }
                }
            }

            catch (Exception Ex)
            {
                ViewBag.ErrorMessage = "Error" + Ex.Message;
            }
            return View();
        }
        public ActionResult AdminLogout()
        {
           Session.Clear();
            return View();
        }


    }
}