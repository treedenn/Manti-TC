using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace Manti.Classes.Database {
	public class MySqlAssist {
		public static bool TestConnection(MySqlDatabase db) {
			using(var connect = new MySqlConnection(db.connectionString)) {
				try {
					connect.Open();
					connect.Close();
					return true;
				} catch(Exception) {
					return false;
				}
			}
		}
	}
}
