using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Manti.Classes.AccountTab;

namespace Manti.Classes.Generate {
	public abstract class SqlGenerate {
		public string deleteFromDatabase(string table, string where, object value) {
			return $"DELETE FROM {table} WHERE {where} = '{value}';" + Environment.NewLine;
		}

		public string insertToDatabase(string table, string[] columns, object[] values) {
			if(columns.Length == values.Length) {
				string column = "", value = "";

				for(var i = 0; i < columns.Length; i++) {
					column += columns[i];
					column += (i == columns.Length - 1 ? "" : ", ");
				}

				for(var i = 0; i < values.Length; i++) {
					value += "'" + values[i].ToString() + "'";
					value += (i == values.Length - 1 ? ""  : ", ");
				}

				return $"INSERT INTO {table} ({column}) VALUES ({value});" + Environment.NewLine;
			}

			return null;
		}

		public string updateToDatabase(string table, string[] columns, object[] values, string idcolumn, string id) {
			if(columns.Length == values.Length) {
				string query = String.Format("UPDATE {0} SET ", table);

				for(var i = 0; i < columns.Length; i++) {
					query += String.Format("{0} = '{1}'", columns[i], values[i].ToString());
					query += (i == columns.Length - 1 ? String.Format(" WHERE {0} = '{1}';" + Environment.NewLine, idcolumn, id) : ", ");
				}

				return query;
			}

			return null;
		}

		public string insertOrUpdateToDatabase(string table, string[] columns, object[] values) {
			if(columns.Length == values.Length) {
				string column = "", value = "", update = "";

				for(var i = 0; i < columns.Length; i++) {
					column += columns[i];
					column += (i == columns.Length - 1 ? "" : ", ");
				}

				for(var i = 0; i < values.Length; i++) {
					value += "'" + values[i].ToString() + "'";
					value += (i == values.Length - 1 ? "" : ", ");
				}

				for(var i = 0; i < columns.Length; i++) {
					update += $"{columns[i]} = '{values[i].ToString()}'";
					update += (i == columns.Length - 1 ? "" : ", ");
				}

				return $"INSERT INTO {table} ({column}) VALUES ({value}) ON DUPLICATE KEY UPDATE {update};" + Environment.NewLine;
			}

			return null;
		}
	}
}
