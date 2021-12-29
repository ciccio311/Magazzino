﻿using ClientWCF.Models;
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
                        ViewBag.data = wcf.getFreePos();
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
    }
}