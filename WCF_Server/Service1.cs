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

        public DipendenteServer Login(string id, string pswd)
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
    }
}
