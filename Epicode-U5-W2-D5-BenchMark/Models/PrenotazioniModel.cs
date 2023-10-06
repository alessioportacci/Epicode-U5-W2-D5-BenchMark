using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epicode_U5_W2_D5_BenchMark.Models
{
    public class PrenotazioniModel 
    {
        public int PkPrenotazione { get; set; }

        public int FkCliente { get; set; }
        [Display(Name = "Nome Cliente")]

        public string NomeCliente { get; set; }

        [Display(Name = "Numero Camera")]
        public int FkCamera { get; set; }

        [Display(Name = "Data Prenotazione")]
        public DateTime DataPrenotazione { get; set; }

        [Display(Name = "Prenotazione dal")]
        public DateTime Dal {  get; set; }

        [Display(Name = "Prenotazione al")]
        public DateTime Al {  get; set; }
        public double Caparra { get; set; }
        public double Tariffa { get; set; }

        [Display(Name = "Con mezza pensione")]
        public bool MezzaPensione { get; set; }

        [Display(Name = "Con prima colazione")]
        public bool PrimaColazione { get; set; }
    }

    public class AddPrenotazioniModel
    {
        public int PkPrenotazione { get; set; }
        public int FkCliente { get; set; }

        [Display(Name = "Numero Camera")]
        //[Remote("CheckStanza", "Prenotazione", ErrorMessage = "Stanza non disponibile")]
        public int FkCamera { get; set; }

        [Display(Name = "Prenotazione dal")]
        [Required(ErrorMessage = "Inserire data")]
        public DateTime Dal { get; set; }

        [Display(Name = "Prenotazione al")]
        [Required(ErrorMessage = "Inserire data")]
        public DateTime Al { get; set; }

        [Required(ErrorMessage = "Inserire caparra")]
        public double Caparra { get; set; }

        [Required(ErrorMessage = "Inserire tariffa")]
        public double Tariffa { get; set; }


        [Display(Name = "Con mezza pensione")]
        public bool MezzaPensione { get; set; }


        [Display(Name = "Con prima colazione")]
        public bool PrimaColazione { get; set; }
    }
}