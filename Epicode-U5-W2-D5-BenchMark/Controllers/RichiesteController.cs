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
            double totale = 0;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM V_Richieste WHERE FkPrenotazione = " + id, conn);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    totale += Convert.ToDouble(sqlDataReader["Prezzo"]);
                    richiesteList.Add(new RichiesteModel()
                    {
                        PkRichiesta = Int32.Parse(sqlDataReader["PkRichiesta"].ToString()),
                        FkPrenotazione = Int32.Parse(sqlDataReader["FkPrenotazione"].ToString()),
                        FkTipologia = Int32.Parse(sqlDataReader["FkTipologia"].ToString()),
                        Richiesta = sqlDataReader["Richiesta"].ToString(),
                        Prezzo = Double.Parse(sqlDataReader["Prezzo"].ToString()),
                        DataRichiesta = DateTime.Parse(sqlDataReader["DataRichiesta"].ToString()),
                    });
                }

            }
            catch
            { }
            finally
            {
                conn.Close();
            }

            ViewBag.stanzaId = id;

            return PartialView(richiesteList);
        }

        public ActionResult Create(int id)
        {
            List<RichiesteTipologiaDropdownModel> richiesteList = new List<RichiesteTipologiaDropdownModel>();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM T_RichiesteTipologia", conn);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                    richiesteList.Add(new RichiesteTipologiaDropdownModel()
                    {
                        value = Int32.Parse(sqlDataReader["PkTipologia"].ToString()),
                        text = String.Concat(sqlDataReader["Prezzo"].ToString(), "€ - ", sqlDataReader["Richiesta"].ToString())
                    });
            }
            catch
            { }
            finally
            {
                conn.Close();
            }

            ViewBag.id = id;
            ViewBag.richieste = richiesteList;

            return View();
        }

        [HttpPost]
        public ActionResult Create(RichiesteModel richiesta)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO T_RichiesteAggiuntive VALUES(@Prenotazione, @Tipologia, @Data)", conn);
                cmd.Parameters.AddWithValue("Prenotazione", richiesta.FkPrenotazione);
                cmd.Parameters.AddWithValue("Tipologia", richiesta.FkTipologia);
                cmd.Parameters.AddWithValue("Data", DateTime.Now);
                cmd.ExecuteNonQuery();
            }
            catch
            { }
            finally
            {
                conn.Close();
            }
        return RedirectToAction("Details", "Prenotazione", new { id = richiesta.FkPrenotazione });
        }


        public static double GetTotale(int id) 
        {
            double totale = 0;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM V_Richieste WHERE FkPrenotazione = " + id, conn);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                    totale += Convert.ToDouble(sqlDataReader["Prezzo"]);
            }
            catch
            { }
            finally
            {
                conn.Close();
            }

            return totale;
        }
    }
}
