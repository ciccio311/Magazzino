using ClientWCF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientWCF.Controllers
{
    public class UtenteController : Controller
    {
        // GET: Utente
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            var ut = new Dipendente();
            return View(ut);
        }

        [HttpPost]
        public ActionResult Login(Dipendente ut)
        {
            if (ModelState.IsValid)
            {
                //connessione col service
                //gli mandiamo le credenziali e lui controlla se 
                //c'è un utente con quelle credenziali, se c'è ci invia i dati dell utente

                return View("MenuUtente");
            }
                return View(ut);
        }



    }
}