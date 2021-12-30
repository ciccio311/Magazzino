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
                try
                {
                    var wcf = new ServiceReference1.Service1Client();

                    //gli mandiamo le credenziali e lui controlla se 
                    //c'è un utente con quelle credenziali, se c'è ci invia i dati dell utente
                    if (wcf.Login(ut.id, ut.password) == null)
                    {

                        ViewBag.Message = "ID o password errati!";
                        return View();
                    }
                    else
                    {
                        ut.convertiServerToCLient(wcf.Login(ut.id, ut.password));
                        Session["ID"] = ut.id;
                        return View("MenuUtente", ut);
                    }

                    
                    
                }catch(Exception e)
                {
                    Console.WriteLine("ERRORE: " + e.ToString());
                    return View();
                }
            }
                return View(ut);
        }


        public ActionResult Logout()
        {
            try
            {
                Session["ID"] = null;
            }
            catch (Exception e)
            {
                Console.WriteLine("ERRORE: " + e);
            }
            //ritorno nella home
            return RedirectToAction("Index", "Home");
        }

    }
}