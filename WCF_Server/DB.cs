﻿using MySqlConnector;
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
                            var ceo = reader.GetInt32(5);
                            if (ceo == 1)
                                ds1.amministratore = true;
                            else
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


        public ProdottoServer getProdottoById(MySqlConnection x, int n)
        {
            try
            {
                x.Open();
                using (MySqlCommand command1 = x.CreateCommand())
                {

                    command1.CommandText = "SELECT * " +
                    "FROM PRODOTTO " +
                    "WHERE PRODOTTO.IDPRODOTTO =" + n + ";";

                    using (MySqlDataReader reader = command1.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //legge i risultati ottenuti dalla query, in questo caso ritorna il prodotto cercato con id N
                            var id = reader.GetInt32(0);
                            var nome = reader.GetString(1);
                            var idProduttore = reader.GetInt32(2);
                            var idCat = reader.GetInt32(3);
                            var prezzo = reader.GetFloat(4);
                            var quantita = reader.GetInt32(5);
                            var posizione = reader.GetString(6);

                            ProdottoServer ps = new ProdottoServer(id, nome, idProduttore, prezzo, idCat, quantita, posizione);
                            return ps;
                        }
                        x.Close();
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


        public List<String> getFreePosition(MySqlConnection x)
        {
            List<String> posti = new List<string>();
            try
            {
                x.Open();
                using (MySqlCommand command1 = x.CreateCommand())
                {

                    command1.CommandText = "SELECT * " +
                    "FROM POSIZIONE " +
                    "WHERE POSIZIONE.DISPONIBILE = 1;";

                    using (MySqlDataReader reader = command1.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //legge i risultati ottenuti dalla query, in questo caso ritorna i posti disponibili
                            posti.Add(reader.GetString(0));
                        }
                        x.Close();
                        return posti;
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

        public bool ProductUpdate(MySqlConnection x, int id, int quant, string pos, int idDip, string desc, string date)
        {
            //dichiariamo la transazione e la facciamo partire
            MySqlTransaction transaction;
            x.Open();
            transaction = x.BeginTransaction();

            try
            {
                using (MySqlCommand command1 = x.CreateCommand())
                {
                    //Mettiamo la vecchia posizione del prodotto come disponibile
                    command1.CommandText = "UPDATE posizione SET Disponibile=1 " +
                              "WHERE posizione.IDPosizione = (SELECT posizione.IDPosizione " +
                              "FROM posizione, prodotto " +
                              "WHERE posizione.IDPosizione = prodotto.IDPosizione AND prodotto.IDProdotto =" + id + ");";
                    command1.ExecuteNonQuery();

                    //aggiorniamo la posizione e la quantità
                    command1.CommandText = "UPDATE prodotto SET Quantita=@Quantita,IDPosizione=@Posizione WHERE IDProdotto=@IDProdotto;";
                    command1.Parameters.AddWithValue("@Quantita", quant);
                    command1.Parameters.AddWithValue("@Posizione", pos);
                    command1.Parameters.AddWithValue("@IDProdotto", id);
                    command1.ExecuteNonQuery();

                    //mettiamo la nuova posizione del prodotto non disponibile
                    command1.CommandText = "UPDATE posizione SET Disponibile=0 " +
                       "WHERE posizione.IDPosizione = (SELECT posizione.IDPosizione " +
                                                        "FROM posizione, prodotto " +
                                                        "WHERE posizione.IDPosizione = prodotto.IDPosizione AND prodotto.IDProdotto =" + id + ");";
                    command1.ExecuteNonQuery();

                    command1.CommandText = "INSERT INTO operazione(IDOperazione, IDDipendente, Data, Descrizione, IDProdotto)VALUES(null," + idDip + ", '" + date + "', '" + desc + "'," + id + ");";
                    command1.ExecuteNonQuery();


                    x.Close();

                    return true;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("ERRORE: " + e.ToString());
                // In caso di errore chiamiamo la Rollback
                try
                {
                    //vengono annulate le modifiche in caso di errore e si ripristina il db a prima che si effettuasse la query
                    transaction.Rollback();
                    return false;
                }
                catch (Exception ex2)
                {
                    Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                    Console.WriteLine("  Message: {0}", ex2.Message);
                    return false;
                }
                return false;
            }
        }
    }
}
