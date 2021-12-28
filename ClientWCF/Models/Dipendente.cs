using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClientWCF.Models
{
    public class Dipendente
    {
        [Required]
        [Display(Name = "ID")]
        public int id { get; set; }


        [Display(Name = "Nome")]
        public string nome { get; set; }

        [Display(Name = "Cognome")]
        public string cognome { get; set; }

        [Display(Name = "Telefono")]
        public string telefono { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string password { get; set; }

        public bool amministratore { get; set; }


    }
}