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
    public class StanzeController : Controller
    {

        static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionDb"].ConnectionString.ToString());

        // GET: Stanze
        public ActionResult Index()
        {
            List<StanzeModel> stanzeList = new List<StanzeModel>();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM T_Stanze", conn);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                    stanzeList.Add(new StanzeModel()
                    {
                        PkCamera = Int32.Parse(sqlDataReader["PkCamera"].ToString()),
                        Descrizione = sqlDataReader["Descrizione"].ToString(),
                        Tipologia = sqlDataReader["Tipologia"].ToString(),
                        Occupata = Boolean.Parse(sqlDataReader["Occupata"].ToString()),
                    });
            }
            catch
            { }
            finally
            {
                conn.Close();
            }

            return View(stanzeList);
        }

    }
}
