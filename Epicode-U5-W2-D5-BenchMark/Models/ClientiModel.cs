using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epicode_U5_W2_D5_BenchMark.Models
{
    public class ClientiModel
    {
        public int PkCliente { get; set; }
        [Required(ErrorMessage = "Inserire il Codice Fiscale")]
        [Display(Name = "Codice Fiscale")]
        public string CF { get; set; }

        [Required(ErrorMessage = "Inserire un nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Inserire una città")]
        [Display(Name = "Città")]
        public string Citta { get; set; }
        [Required(ErrorMessage = "Inserire la provincia")]
        [StringLength(2, ErrorMessage = "Massimo due caratteri")]
        public string Provincia { get; set; }

        [Required(ErrorMessage = "Inserire l'email")]
        [Display(Name = "E-mail")]
        public string email { get; set; }

        [Required(ErrorMessage = "Inserire il telefono")]
        public string Telefono { get; set; }
    }
}