using System;
using System.Windows.Forms;

using System.Data;

namespace Manti.Classes {
	public class UtilityHelper {
		/// <summary>
		/// Function looks for a specific .CSV extension file and turns it into a DataTable (used for helper buttons).
		/// </summary>
		/// <param name="csvName">The specific .CSV File</param>
		/// <param name="ID">What column the ID is</param>
		/// <param name="value">Where the value/name is in the CSV extension</param>
		/// <returns>DataTable with all the rows from the ID and Value</returns>
		public static DataTable readExcelCSV(string csvName, int ID, int value, int value2 = 0) {
			var reader = new System.IO.StreamReader(@".\CSV\" + csvName + ".dbc.csv");
			var forgetFirst = true;

			var newTable = new DataTable();

			if(reader != null) {
				newTable.Columns.Add("id", typeof(string));
				newTable.Columns.Add("value", typeof(string));
				if(value2 != 0) { newTable.Columns.Add("value2", typeof(string)); }

				string line; string[] words;

				while((line = reader.ReadLine()) != null) {
					words = line.Split(';');

					if(forgetFirst == false) {
						if(words.Length > value && words[value] != null) {
							DataRow newRow = newTable.NewRow();

							// adds the id and value to the row
							newRow["id"] = words[ID].Trim('"'); newRow["value"] = words[value].Trim('"');
							// if value2 is above 0, add another column value
							if(value2 != 0) { newRow["value2"] = words[value2].Trim('"'); }

							newTable.Rows.Add(newRow);
						}
					}

					forgetFirst = false;
				}

				reader.Close();
			} else {
				MessageBox.Show(csvName + " Could not been found in the CSV folder.\n It has to be same location as the program.", "File Directory : CSV ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			return newTable;
		}

		public static DateTime getDateTimeFromString(string date) {
			DateTime expetedDate;
			if(DateTime.TryParseExact(date, new string[] { "dd/MM/yyyy H:m:s", "MM/dd/yyyy H:m:s" }, null, System.Globalization.DateTimeStyles.None, out expetedDate)) {
				return expetedDate;
			}

			return new DateTime();
		}

		public static DateTime UnixStampToDateTime(string unixStamp) {
			var DateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			DateTime = DateTime.AddSeconds(Convert.ToDouble(unixStamp)).ToLocalTime();

			return DateTime;
		}

		public static ulong DateTimeToUnixStamp(DateTime dateTime) {
			return (ulong) (TimeZoneInfo.ConvertTimeToUtc(dateTime) - new DateTime(1970, 1, 1)).TotalSeconds;
		}

		public static bool checkEmptyControls(Control control) {
			foreach(Control ct in control.Controls) {
				if(ct is TextBox || ct is ComboBox) {
					if(!string.IsNullOrEmpty(ct.Text)) {
						return false;
					}
				}
			}

			return true;
		}

		public static string removeExactSign(string text) {
			return text.Substring(1, text.Length - 1);
		}
	}
}
