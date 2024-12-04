using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    public class Service
    {
        public string StrConn = "Server=127.0.0.1;Database=Note;Uid=root;Pwd=12345";

        public DataTable Init() {
            using (MySqlConnection conn = new MySqlConnection(StrConn))
            {
                conn.Open();
                string strCommand = $"SELECT * FROM Notes";
                MySqlCommand cmd = new MySqlCommand(strCommand, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }
    }   
}
