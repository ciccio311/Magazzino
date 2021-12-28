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

        public string connectstring()
        {
            string con = "datasource=" + address + "port=" + port + "username=" + user + "password=" + passw + "database=" + name;

            return con;
        }

        public MySqlConnection getsqlconnect(string c)
        {
            MySqlConnection dbconnect = new MySqlConnection(c);
            return dbconnect;
        }

        public int accessoutenti(MySqlConnection x, int n, string p)
        {
            try
            {

                x.Open();

                using (MySqlCommand command1 = x.CreateCommand())
                {

                    command1.CommandText = "SELECT DIPENDENTE.ID FROM UTENTE WHERE DIPENDENTE.ID='" + n + "' AND DIPENDENTE.Password='" + p + "';";

                    using (MySqlDataReader reader = command1.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var id = reader.GetInt32(0);

                            x.Close();
                            return id;

                        }
                        x.Close();
                        return 0;
                    }

                }

            }
            catch (Exception e)
            {

                x.Close();
                return 0;
            }
        }
    }
}
