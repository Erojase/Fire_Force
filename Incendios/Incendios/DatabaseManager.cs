using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace Incendios
{
    class DatabaseManager
    {
        private MySqlConnection conn;
        private MySqlDataAdapter adapter;
        private string connStr = "server=localhost;uid=admin-dam2;pwd=joyfe;database=incendios;";

        public DatabaseManager()
        {
            
        }


        public void GetTable(string query, string sortBy)
        {
            conn = new MySqlConnection(connStr);
            conn.Open();

            string sql = "SELECT * FROM usuarios";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            using (MySqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    var myString = rdr[1];
                }
            }
        }


        public object Login(string user, string passwd)
        {
            conn = new MySqlConnection(connStr);
            conn.Open();

            string sql = "call incendios.login('"+user+"', '"+passwd+"');";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            using (MySqlDataReader rdr = cmd.ExecuteReader())
            {
                rdr.Read();
                return rdr[0];
            }
        }
    }
    
}
