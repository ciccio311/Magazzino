using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF_Server
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "Service1" nel codice e nel file di configurazione contemporaneamente.
    public class Service1 : IService1
    {
        DB databse1 = new DB();
        public void DoWork()
        {
        }
        public void DoWork2()
        {
            Console.WriteLine("Ciao, sono dowork2!");
        }

        public DipendenteServer Login(int id, string pswd)
        {
            DipendenteServer ds = new DipendenteServer();

            try
            {

                //ci connettiamo al DB
                var x = databse1.getsqlconnect(databse1.connectstring());

                //ritorna l utente loggato
                ds = databse1.accessoutenti(x, id, pswd);



                return ds;

            }catch(Exception e)
            {
                Console.WriteLine("Errore: " + e.ToString());
                return null;
            }
        }

        public ListaProdottiServer getListaProdotti()
        {
            ListaProdottiServer lsp = new ListaProdottiServer();

            try
            {

                //ci connettiamo al DB
                var x = databse1.getsqlconnect(databse1.connectstring());

                //ritorna la lista di prodotti
                lsp = databse1.getListaProdotti(x);



                return lsp;

            }
            catch (Exception e)
            {
                Console.WriteLine("Errore: " + e.ToString());
                return null;
            }

        }


        public ProdottoServer getProdById(int n)
        {
            ProdottoServer ps = new ProdottoServer();
            try
            {
                //ci connettiamo al DB
                var x = databse1.getsqlconnect(databse1.connectstring());

                //ritorna la lista di prodotti
                
                ps = databse1.getProdottoById(x, n);

                return ps;

            }
            catch (Exception e)
            {
                Console.WriteLine("Errore: " + e.ToString());
                return null;
            }
        }


        public List<String> getFreePos()
        {
            List<String> postiDisponibili = new List<string>();
            try
            {
                //ci connettiamo al DB
                var x = databse1.getsqlconnect(databse1.connectstring());

                //ritorna la lista di prodotti
                postiDisponibili = databse1.getFreePosition(x);

                return postiDisponibili;
            }
            catch (Exception e)
            {
                Console.WriteLine("Errore: " + e.ToString());
                return null;
            }
        }

        public bool updateProduct(int id, int quant, string pos,int idDip, string desc, string date)
        {
            try
            {
                //ci connettiamo al DB
                var x = databse1.getsqlconnect(databse1.connectstring());

                //ritorna la lista di prodotti
                if (databse1.ProductUpdate(x, id, quant, pos,idDip,desc,date))
                {
                    return true;
                }
                else
                    return false;

                
            }
            catch (Exception e)
            {
                Console.WriteLine("Errore: " + e.ToString());
                return false;
            }
        }
    }
}
