using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epicode_U5_W2_D5_BenchMark.Models
{
    public class UtentiModel
    {
        [Required(ErrorMessage ="Inserire un username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Inserire una password")]
        public string Password { get; set; }
    }
}