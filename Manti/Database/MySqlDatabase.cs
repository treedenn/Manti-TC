using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace Manti.Database
{
    class MySqlDatabase
    {

        public string address { get; set; } = FormMySQL.Address;
        public string username { get; set; }
        public string password { get; set; }
        public int port { get; set; }
        public string database { get; set; }

        public string connectionString { get; private set; }

        private MySqlDatabase(string address, string username, string password, int port, string dbName)
        {
            this.address = address;
            this.username = username;
            this.password = password;
            this.port = port;
            this.database = dbName;

            var builder = new MySqlConnectionStringBuilder();
            builder.Server = address;
            builder.UserID = username;
            builder.Password = password;
            builder.Port = (uint)port;
            builder.Database = dbName;

            connectionString = builder.ToString();
        }

        private void ExecuteNonQuery(string query)
        {

            using (var connect = new MySqlConnection(connectionString))
            {
                connect.Open();

                using (var cmd = new MySqlCommand(query, connect))
                {
                    cmd.ExecuteNonQuery();
                }

                connect.Close();
            }

        }

        private DataSet ExecuteQuery(string query)
        {
            DataSet ds = new DataSet();

            using (var connect = new MySqlConnection(connectionString))
            {
                connect.Open();

                using (var da = new MySqlDataAdapter(query, connect))
                {
                    da.Fill(ds);
                }

                connect.Close();
            }

            return ds;
        }

    }
}
