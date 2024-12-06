using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        public void CreatNewTag(string Name) {
            using (MySqlConnection conn = new MySqlConnection(StrConn))
            {
                conn.Open();
                string strCommand = $"INSERT INTO Tags(name) VALUES ('{Name}')";
                MySqlCommand cmd = new MySqlCommand(strCommand, conn);
                cmd.ExecuteNonQuery();
            }
        }

        public void ReTag(Tag tag) {
            using (MySqlConnection conn = new MySqlConnection(StrConn))
            {
                conn.Open();
                string strCommand = $"UPDATE Tags SET Name = '{tag.Name}' WHERE Id = {tag.Id}";
                MySqlCommand cmd = new MySqlCommand(strCommand, conn);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Tag> SelTags()
        {
            List<Tag> result = new List<Tag>();
            using (MySqlConnection conn = new MySqlConnection(StrConn))
            {
                conn.Open();
                string strCommand = $"SELECT * FROM Tags";
                MySqlCommand cmd = new MySqlCommand(strCommand, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                var tempResult = from t in dataTable.AsEnumerable() select new Tag(t.Field<int>("Id"), t.Field<string>("Name"));
                foreach (var t in tempResult) {
                result.Add(t);
                }
            }
            return result;
        }
        public DataTable SelTagNote()
        {
            using (MySqlConnection conn = new MySqlConnection(StrConn))
            {
                conn.Open();
                string strCommand = $"SELECT * FROM NoteTags";
                MySqlCommand cmd = new MySqlCommand(strCommand, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }
        public void DelTag(Tag tag)
        {
            using (MySqlConnection conn = new MySqlConnection(StrConn))
            {
                conn.Open();
                string strCommand = $"DELETE FROM Tags SET WHERE Id = {tag.Id}";
                MySqlCommand cmd = new MySqlCommand(strCommand, conn);
                cmd.ExecuteNonQuery();
            }
        }
    }   
}
