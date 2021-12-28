using ClientWCF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientWCF.Controllers
{
    public class ProdottoController : Controller
    {
        // GET: Prodotto
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Prodotti()
        {
            ListaProdotti LP = new ListaProdotti();
            Prodotto p1 = new Prodotto();
            p1.nome = "cpu inel";
            p1.prezzo = 159.9F;
            p1.produttore = 1;
            p1.categoria = 1;
            p1.quantità = 10;
            Prodotto p2 = new Prodotto();
            p2.nome = "cpu amd";
            p2.prezzo = 149.9F;
            p2.produttore = 2;
            p2.categoria = 1;
            p2.quantità = 12;

            LP.listaProducts.Add(p1);
            LP.listaProducts.Add(p2);


            return View(LP);
        }
    }
}