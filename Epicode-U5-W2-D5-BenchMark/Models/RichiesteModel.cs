using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epicode_U5_W2_D5_BenchMark.Models
{
    public class RichiesteModel
    {
        public int? PkRichiesta { get; set; }
        public int FkPrenotazione { get; set; }
        [Required(ErrorMessage = "Inserire una tipologia")]
        [Display(Name = "Tipologia")]
        public int FkTipologia { get; set; }
        public string Richiesta { get; set; }
        public double Prezzo { get; set; }
        public DateTime DataRichiesta { get; set; }

    }

    public class RichiesteTipologiaModel
    {
        public string Richiesta { get; set; }
        public double Prezzo { get; set; }
    }

    public class RichiesteTipologiaDropdownModel
    {
        public int value { get; set; }
        public string text { get; set; }
    }
}