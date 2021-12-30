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


        public ActionResult AzioniProdotto(int id)
        {
            Prodotto p = new Prodotto();
            if (ModelState.IsValid)
            {
                //connessione col service
                try
                {
                    var wcf = new ServiceReference1.Service1Client();
                    //controllo che ritorni un prodotto
                    if (wcf.getProdById(id) == null)
                    {
                        return Content("Non ci sono prodotti con ID = " + id);
                    }
                    else
                    {
                        //converto il prodotto da server a client e chiamo la view con il prodotto client
                        p.convertiServerToCLient(wcf.getProdById(id));

                        //Creo una lista di stringhe ed aggiungo posizione corrente del prodotto + quelle disponibili
                        List<String> posizioni = new List<string>();
                        posizioni.Add(p.posizione);
                        foreach(var z in wcf.getFreePos())
                        {
                            posizioni.Add(z);
                        }

                        ViewBag.data = posizioni;
                        ViewBag.Message = p.nome;
                        return View(p);
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("ERRORE: " + e.ToString());
                    return View();
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult AzioniProdotto(Prodotto p1)
        {
            if (ModelState.IsValid)
            {
                //connessione col service
                try
                {
                    var wcf = new ServiceReference1.Service1Client();
                    int i = (int)Session["ID"];
                    string date = DateTime.UtcNow.ToString("yyyy-MM-dd");
                    //controllo che ritorni un prodotto
                    if (wcf.updateProduct(p1.id, p1.quantità, p1.posizione,i,"Aggiornamento",date))
                    {
                        //ritorno alla view prodotti tramite redirectToAction
                        return RedirectToAction("Prodotti");
                    }
                    else
                    {
                        return Content("CAZZO");
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("ERRORE: " + e.ToString());
                    return View();
                }
            }
            return View();
        }

        public ActionResult Prodotti()
        {
            ListaProdotti LP = new ListaProdotti();
            if (ModelState.IsValid)
            {
                //connessione col service
                try
                {
                    var wcf = new ServiceReference1.Service1Client();

                    if (wcf.getListaProdotti() == null)
                    {
                        return Content("Non ci sono prodotti nel db!");
                    }
                    else
                    {
                       LP.ConvertServerList(wcf.getListaProdotti());
                        return View(LP);
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("ERRORE: " + e.ToString());
                    return View();
                }
            }
        
            return View(LP);
        }

        public ActionResult CreaProdotto()
        {
            Prodotto p1 = new Prodotto();
            return View(p1);
        }

        [HttpPost]
        public ActionResult CreaProdotto(Prodotto p1)
        {
            if (ModelState.IsValid)
            {
                //connessione col service
                try
                {
                    var wcf = new ServiceReference1.Service1Client();

                    var ProductToServer = p1.convertiClientToServer();


                    if (wcf.CreaProdotto(ProductToServer))
                    {
                        return Content("CREATO");
                    }
                    else
                        return Content("CAZZO");
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERRORE: " + e.ToString());
                    return View();
                }
            }
            return View("CAZZO");
        }

    }
}