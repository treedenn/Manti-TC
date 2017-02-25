using System;
using MySql.Data.MySqlClient;

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
