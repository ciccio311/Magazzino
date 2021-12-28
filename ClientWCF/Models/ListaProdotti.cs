using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientWCF.Models
{
    public class ListaProdotti
    {
        public ListaProdotti()
        {
            this.listaProducts = new List<Prodotto>();
        }
        public List<Prodotto> listaProducts { get; set; } 


        
        
    }
}