using Epicode_U5_W2_D5_BenchMark.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Epicode_U5_W2_D5_BenchMark.Controllers
{
    [Authorize]
    public class RichiesteController : Controller
    {

        static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionDb"].ConnectionString.ToString());

        public ActionResult Index(int id)
        {
            List<RichiesteModel> richiesteList = new List<RichiesteModel>();

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM V_Richieste WHERE FkPrenotazione = " + id, conn);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                    richiesteList.Add(new RichiesteModel()
                    {
                        PkRichiesta = Int32.Parse(sqlDataReader["PkRichiesta"].ToString()),
                        FkPrenotazione = Int32.Parse(sqlDataReader["FkPrenotazione"].ToString()),
                        DataRichiesta = DateTime.Parse(sqlDataReader["DataRichiesta"].ToString()),
                        Richiesta = sqlDataReader["Richiesta"].ToString(),
                        Prezzo = Double.Parse(sqlDataReader["Richiesta"].ToString())
                    });
            }
            catch
            { }
            finally
            {
                conn.Close();
            }

            return PartialView(richiesteList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
