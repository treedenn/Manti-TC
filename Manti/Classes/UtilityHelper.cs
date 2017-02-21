using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Manti.Classes {
	public class UtilityHelper {
		public static DateTime UnixStampToDateTime(string unixStamp) {
			var DateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			DateTime = DateTime.AddSeconds(Convert.ToDouble(unixStamp)).ToLocalTime();

			return DateTime;
		}

		public static ulong DateTimeToUnixStamp(DateTime dateTime) {
			return (ulong) (TimeZoneInfo.ConvertTimeToUtc(dateTime) - new DateTime(1970, 1, 1)).TotalSeconds;
		}

		public bool CheckEmptyControls(Control control) {
			foreach(Control ct in control.Controls) {
				if(ct is TextBox || ct is ComboBox) {
					if(ct.Text != "") {
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
