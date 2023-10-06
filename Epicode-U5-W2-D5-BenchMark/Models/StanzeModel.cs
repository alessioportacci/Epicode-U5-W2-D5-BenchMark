using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epicode_U5_W2_D5_BenchMark.Models
{
    public class StanzeModel 
    {
        public int PkCamera {  get; set; }
        public string Descrizione {  get; set; }
        public string Tipologia { get; set; }
        public bool Occupata { get; set; }
    }
}