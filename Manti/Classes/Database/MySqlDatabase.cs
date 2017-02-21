using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace Manti.Classes.Database {
	public abstract class MySqlDatabase {
		public static bool runOffline = false;

		private string address { get; set; }
		private string username { get; set; }
		private string password { get; set; }
		private uint port { get; set; }
		private string database { get; set; }

		public string connectionString { get; private set; }

		protected MySqlDatabase(string address, string username, string password, uint port, string dbName) {
			this.address     = address;
			this.username    = username;
			this.password    = password;
			this.port        = port;
			this.database    = dbName;

			var builder      = new MySqlConnectionStringBuilder();
			builder.Server   = address;
			builder.UserID   = username;
			builder.Password = password;
			builder.Port     = port;
			builder.Database = dbName;

			connectionString = builder.ToString();
		}

		protected void executeNonQuery(string query, params MySqlParameter[] paras) {
			using(var connect = new MySqlConnection(connectionString)) {
				connect.Open();

				using(var cmd = new MySqlCommand(query, connect)) {
					foreach(var para in paras) {
						cmd.CommandText = cmd.CommandText.Replace(para.ParameterName, para.Value.ToString());
					}

					cmd.ExecuteNonQuery();
				}

				connect.Close();
			}
		}

		protected DataTable executeQuery(string query, params MySqlParameter[] paras) {
			using(var connect = new MySqlConnection(connectionString)) {
				connect.Open();

				using(var cmd = new MySqlCommand(query, connect)) {
					foreach(var para in paras) {
						cmd.CommandText = cmd.CommandText.Replace(para.ParameterName, para.Value.ToString());
					}

				var reader = cmd.ExecuteReader();

					var dt = new DataTable();
					dt.Load(reader);

					connect.Close();

					if(dt.Rows.Count == 0) { return null; }

					return dt;
				}
			}
		}
	}
}
