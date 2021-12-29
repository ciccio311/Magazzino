using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCF_Server
{
    class DB
    {
        private string port = "3306;";
        private string name = "magazzino";
        private string user = "root;";
        private string passw = ";";
        private string address = "127.0.0.1;";


        //stringa di connessione al DB
        public string connectstring()
        {
            string con = "datasource=" + address + "port=" + port + "username=" + user + "password=" + passw + "database=" + name;

            return con;
        }

        //connessione a MysqlConnection
        public MySqlConnection getsqlconnect(string c)
        {
            MySqlConnection dbconnect = new MySqlConnection(c);
            return dbconnect;
        }

        public DipendenteServer accessoutenti(MySqlConnection x, int n, string p)
        {
            try
            {
                DipendenteServer ds1 = new DipendenteServer();

                //apertura connessione al DB
                x.Open(); 

                using (MySqlCommand command1 = x.CreateCommand())
                {

                    command1.CommandText = "SELECT * FROM `dipendente` WHERE dipendente.Password='" + p + "' AND dipendente.IDDipendente=" + n + ";";

                    using (MySqlDataReader reader = command1.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //legge i risultati ottenuti dalla query, in questo caso ritorna solo l id
                            //di chi ha fatto l accesso 
                            ds1.id = reader.GetInt32(0);
                            ds1.nome = reader.GetString(1);
                            ds1.cognome = reader.GetString(2);
                            ds1.telefono = reader.GetString(3);
                            ds1.password = reader.GetString(4);
                            ds1.amministratore = false;

                            x.Close(); //chiudiamo la connessione al DB
                            return ds1;

                        }
                        x.Close();
                        return null;
                    }

                }

            }
            catch (Exception e)
            {
                Console.WriteLine("ERRORE: " + e.ToString());
                x.Close();
                return null;
            }
        }

        public ListaProdottiServer getListaProdotti(MySqlConnection x)
        {
            try
            {
                ListaProdottiServer lps = new ListaProdottiServer();
                x.Open();              
                using (MySqlCommand command1 = x.CreateCommand())
                {

                    command1.CommandText = "SELECT * " +
                    "FROM PRODOTTO ;";

                    using (MySqlDataReader reader = command1.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //legge i risultati ottenuti dalla query, in questo caso ritorna i prodotti
                           
                            var id = reader.GetInt32(0);
                            var nome = reader.GetString(1);
                            var idProduttore = reader.GetInt32(2);
                            var idCat = reader.GetInt32(3);
                            var prezzo = reader.GetFloat(4);
                            var quantita = reader.GetInt32(5);
                            var posizione = reader.GetString(6);

                            ProdottoServer ps = new ProdottoServer(id, nome, idProduttore,prezzo, idCat, quantita, posizione);
                            lps.listaProducts.Add(ps);

                        }
                        x.Close();
                        return lps;
                    }

                    x.Close();
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERRORE: " + e.ToString());
                return null;
            }
        }
    }
}
