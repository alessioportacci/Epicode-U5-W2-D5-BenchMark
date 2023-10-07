using Epicode_U5_W2_D5_BenchMark.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epicode_U5_W2_D5_BenchMark.Controllers
{
    public class PrenotazioneController : Controller
    {

        static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionDb"].ConnectionString.ToString());

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM V_Prenotazioni WHERE PkPrenotazione = " + id, conn);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                    return View(new PrenotazioniModel()
                    {
                        PkPrenotazione = Int32.Parse(sqlDataReader["PkPrenotazione"].ToString()),
                        NomeCliente = sqlDataReader["Nome"].ToString(),
                        FkCliente = Int32.Parse(sqlDataReader["FkCliente"].ToString()),
                        FkCamera = Int32.Parse(sqlDataReader["FkCamera"].ToString()),
                        DataPrenotazione = sqlDataReader["DataPrenotazione"].ToString(),
                        Dal = sqlDataReader["Dal"].ToString(),
                        Al =sqlDataReader["Al"].ToString(),
                        Caparra = Double.Parse(sqlDataReader["Caparra"].ToString()),
                        Tariffa = Double.Parse(sqlDataReader["Tariffa"].ToString()),
                        MezzaPensione = Boolean.Parse(sqlDataReader["MezzaPensione"].ToString()),
                        PrimaColazione = Boolean.Parse(sqlDataReader["PrimaColazione"].ToString()),
                    }); ;
            }
            catch
            { }
            finally
            {
                conn.Close();
            }

            return View();
        }


        public ActionResult Create(int id)
        {
            ViewBag.id = id;
            return View();
        }

        [HttpPost]
        public ActionResult Create(AddPrenotazioniModel prenotazione)
        {
            if(ModelState.IsValid)
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO [T_Prenotazioni] VALUES (@FkCliente, @FkCamera, @DataPrenotazione, @Dal, @Al, @Caparra, @Tariffa, @MezzaPensione, @PrimaColazione)", conn);
                    cmd.Parameters.AddWithValue("FkCliente", prenotazione.FkCliente);
                    cmd.Parameters.AddWithValue("FkCamera", prenotazione.FkCamera);
                    cmd.Parameters.AddWithValue("DataPrenotazione", DateTime.Now);
                    cmd.Parameters.AddWithValue("Dal", prenotazione.Dal);
                    cmd.Parameters.AddWithValue("Al", prenotazione.Al);
                    cmd.Parameters.AddWithValue("Caparra", prenotazione.Caparra);
                    cmd.Parameters.AddWithValue("Tariffa", prenotazione.Tariffa);
                    cmd.Parameters.AddWithValue("MezzaPensione", prenotazione.MezzaPensione);
                    cmd.Parameters.AddWithValue("PrimaColazione", prenotazione.PrimaColazione);

                    cmd.ExecuteNonQuery();
                    
                    return RedirectToAction("Index");
                }
                catch
                { 
                }
                finally
                {
                    conn.Close();
                }

            return View(prenotazione);
        }

        [AllowAnonymous]
        public JsonResult CheckStanza(int pkStanza)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM T_Stanze WHERE Occupata = 1 AND PkCamera = " + pkStanza, conn);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                if (sqlDataReader.HasRows)
                    return Json(true);
            }
            catch
            { }
            finally
            {
                conn.Close();
            }

            return Json(false);
        }


        public ActionResult Edit(int id)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM V_Prenotazioni WHERE PkPrenotazione = " + id, conn);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                    return View(new AddPrenotazioniModel()
                    {
                        PkPrenotazione = id,
                        FkCamera = Int32.Parse(sqlDataReader["FkCamera"].ToString()),
                        Dal = DateTime.Parse(sqlDataReader["Dal"].ToString()),
                        Al = DateTime.Parse(sqlDataReader["Al"].ToString()),
                        Caparra = Double.Parse(sqlDataReader["Caparra"].ToString()),
                        Tariffa = Double.Parse(sqlDataReader["Tariffa"].ToString()),
                        MezzaPensione = Boolean.Parse(sqlDataReader["MezzaPensione"].ToString()),
                        PrimaColazione = Boolean.Parse(sqlDataReader["PrimaColazione"].ToString()),
                    }); ;
            }
            catch
            { }
            finally
            {
                conn.Close();
            }

            return View();
        }

        [HttpPost]
        public ActionResult Edit(AddPrenotazioniModel prenotazione)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(String.Concat("UPDATE [T_Prenotazioni] SET " +
                                                                "FkCamera = @FkCamera, " +
                                                                "Dal = @Dal, " +
                                                                "Al = @Al, " +
                                                                "Caparra = @Caparra, " +
                                                                "Tariffa = @Tariffa, " +
                                                                "MezzaPensione = @MezzaPensione, " +
                                                                "PrimaColazione = @PrimaColazione " +
                                                               "WHERE PkPrenotazione = ", prenotazione.PkPrenotazione)
                                                , conn);

                cmd.Parameters.AddWithValue("FkCamera", prenotazione.FkCamera);
                cmd.Parameters.AddWithValue("Dal", prenotazione.Dal);
                cmd.Parameters.AddWithValue("Al", prenotazione.Al);
                cmd.Parameters.AddWithValue("Caparra", prenotazione.Caparra);
                cmd.Parameters.AddWithValue("Tariffa", prenotazione.Tariffa);
                cmd.Parameters.AddWithValue("MezzaPensione", prenotazione.MezzaPensione);
                cmd.Parameters.AddWithValue("PrimaColazione", prenotazione.PrimaColazione);
                cmd.ExecuteNonQuery();
            }
            catch
            { }
            finally
            {
                conn.Close();
            }

            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM [T_Prenotazioni] WHERE PkPrenotazione = @PkPrenotazione", conn);
                cmd.Parameters.AddWithValue("PkPrenotazione", id);
                cmd.ExecuteNonQuery();
            }
            catch
            { }
            finally
            {
                conn.Close();
            }
            return RedirectToAction("Index");
        }


        public ActionResult Checkout(int id) 
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM T_Prenotazioni WHERE PkPrenotazione = " + id, conn);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                    return View(new CheckoutModel()
                    {
                        PkStanza = Int32.Parse(sqlDataReader["FkCamera"].ToString()),
                        PkPrenotazione = Int32.Parse(sqlDataReader["PkPrenotazione"].ToString()),
                        Preiodo = String.Concat(sqlDataReader["Dal"].ToString(), " - ", sqlDataReader["Al"].ToString()),
                        Tariffa = Double.Parse(sqlDataReader["Tariffa"].ToString()),
                        Caparra = Double.Parse(sqlDataReader["Caparra"].ToString()),
                        Totale = Double.Parse(sqlDataReader["Tariffa"].ToString()) - Double.Parse(sqlDataReader["Caparra"].ToString()) + RichiesteController.GetTotale(id)
                    });
            }
            catch
            { }
            finally
            {
                conn.Close();
            }

            return View();
        }


        public JsonResult PrenotazioniFilter(string CF = "", string Pensione = "")
        {
            List<PrenotazioniModel> prenotazioniList = new List<PrenotazioniModel>();

            Pensione = Pensione.ToLower() == "false" || Pensione.ToLower() == "" ? "" : "1";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(String.Concat("SELECT * FROM V_Prenotazioni ",
                                                "WHERE CF LIKE CONCAT('%', '", CF, "', '%') ",
                                                       "AND MezzaPensione LIKE CONCAT('%', '", Pensione, "', '%') "), conn);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                    prenotazioniList.Add(new PrenotazioniModel()
                    {
                        PkPrenotazione = Int32.Parse(sqlDataReader["PkPrenotazione"].ToString()),
                        NomeCliente = sqlDataReader["Nome"].ToString(),
                        FkCliente = Int32.Parse(sqlDataReader["FkCliente"].ToString()),
                        FkCamera = Int32.Parse(sqlDataReader["FkCamera"].ToString()),
                        DataPrenotazione = sqlDataReader["DataPrenotazione"].ToString(),
                        Dal = sqlDataReader["Dal"].ToString(),
                        Al = sqlDataReader["Al"].ToString(),
                        Caparra = Double.Parse(sqlDataReader["Caparra"].ToString()),
                        Tariffa = Double.Parse(sqlDataReader["Tariffa"].ToString()),
                        MezzaPensione = Boolean.Parse(sqlDataReader["MezzaPensione"].ToString()),
                        PrimaColazione = Boolean.Parse(sqlDataReader["PrimaColazione"].ToString()),
                    }); ;
            }
            catch
            { }
            finally
            {
                conn.Close();
            }

            return Json(prenotazioniList, JsonRequestBehavior.AllowGet);
        }


    }
}
