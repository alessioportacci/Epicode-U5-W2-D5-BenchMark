using Epicode_U5_W2_D5_BenchMark.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Epicode_U5_W2_D5_BenchMark.Controllers
{
    public class HomeController : Controller
    {

        static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionDb"].ConnectionString.ToString());

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UtentiModel utente)
        {
            if (ModelState.IsValid)
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT Username, Password FROM [T_Utenti] " +
                                                    "WHERE @Username = Username AND @Password = Password", conn);
                    cmd.Parameters.AddWithValue("Username", utente.Username);
                    cmd.Parameters.AddWithValue("Password", utente.Password);
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        FormsAuthentication.SetAuthCookie(utente.Username, false);
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch
                {
                    return View();
                }
                finally
                {
                    conn.Close();
                }

            return View();
        }


        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}