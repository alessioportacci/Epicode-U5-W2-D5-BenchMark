using Epicode_U5_W2_D5_BenchMark.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epicode_U5_W2_D5_BenchMark.Controllers
{
    [Authorize]
    public class ClientiController : Controller
    {

        static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionDb"].ConnectionString.ToString());

        public ActionResult Index(string email, string telefono)
        {
            List<ClientiModel> clientiList = new List<ClientiModel>();

            //TODO: filtri dinamici
            string cmdText = "SELECT * FROM [T_Clienti] WHERE 1=1";
            if (email != "" && email != null)
                cmdText += String.Concat("AND LIKE CONCAT('%',",email,", '%')");
            if (telefono != "" && email != null)
                cmdText += String.Concat("AND telefono = '", telefono, "'");

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                    clientiList.Add(new ClientiModel()
                    {
                        PkCliente = Int32.Parse(sqlDataReader["PkCliente"].ToString()),
                        CF = sqlDataReader["CF"].ToString(),
                        Nome = sqlDataReader["Nome"].ToString(),
                        Citta = sqlDataReader["Citta"].ToString(),
                        Provincia = sqlDataReader["Provincia"].ToString(),
                        email = sqlDataReader["email"].ToString(),
                        Telefono = sqlDataReader["Telefono"].ToString(),
                    });
            }
            catch
            { }
            finally
            {
                conn.Close();
            }

            return View(clientiList);
        }

        public ActionResult Details(int id)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM [T_Clienti] WHERE PkCliente = @PkCliente", conn);
                cmd.Parameters.AddWithValue("PkCliente", id);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                    return View(new ClientiModel
                    {
                        PkCliente = Int32.Parse(sqlDataReader["PkCliente"].ToString()),
                        CF = sqlDataReader["CF"].ToString(),
                        Nome = sqlDataReader["Nome"].ToString(),
                        Citta = sqlDataReader["Citta"].ToString(),
                        Provincia = sqlDataReader["Provincia"].ToString(),
                        email = sqlDataReader["email"].ToString(),
                        Telefono = sqlDataReader["Telefono"].ToString(),
                    });
            }
            catch
            { }
            finally
            {
                conn.Close();
            }

            return RedirectToAction("Index");
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ClientiModel cliente)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO [T_Clienti] VALUES (@CF, @Nome, @Citta, @Provincia, @Email, @Telefono)", conn);
                cmd.Parameters.AddWithValue("CF", cliente.CF);
                cmd.Parameters.AddWithValue("Nome", cliente.Nome);
                cmd.Parameters.AddWithValue("Citta", cliente.Citta);
                cmd.Parameters.AddWithValue("Provincia", cliente.Provincia);
                cmd.Parameters.AddWithValue("Email", cliente.email);
                cmd.Parameters.AddWithValue("Telefono", cliente.Telefono);
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


        public ActionResult Edit(int id)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM [T_Clienti] WHERE PkCliente = @PkCliente", conn);
                cmd.Parameters.AddWithValue("PkCliente", id);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                    return View(new ClientiModel
                    {
                        PkCliente = Int32.Parse(sqlDataReader["PkCliente"].ToString()),
                        CF = sqlDataReader["CF"].ToString(),
                        Nome = sqlDataReader["Nome"].ToString(),
                        Citta = sqlDataReader["Citta"].ToString(),
                        Provincia = sqlDataReader["Provincia"].ToString(),
                        email = sqlDataReader["email"].ToString(),
                        Telefono = sqlDataReader["Telefono"].ToString(),
                    });
            }
            catch
            { }
            finally
            {
                conn.Close();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(ClientiModel cliente)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(String.Concat("UPDATE [T_Clienti] SET " +
                                                                "CF = @CF, " +
                                                                "Nome = @Nome, " +
                                                                "Citta = @Citta, " +
                                                                "Provincia = @Provincia, " +
                                                                "Email = @Email, " +
                                                                "Telefono = @Telefono " +
                                                               "WHERE PkCliente = ", cliente.PkCliente)
                                                , conn);

                cmd.Parameters.AddWithValue("CF", cliente.CF);
                cmd.Parameters.AddWithValue("Nome", cliente.Nome);
                cmd.Parameters.AddWithValue("Citta", cliente.Citta);
                cmd.Parameters.AddWithValue("Provincia", cliente.Provincia);
                cmd.Parameters.AddWithValue("Email", cliente.email);
                cmd.Parameters.AddWithValue("Telefono", cliente.Telefono);
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

    }
}
