using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manti.Classes.AccountTab {
	public class AccountMuted {
		public DateTime muteDate { get; set; }
		public double duration { get; set; }
		public string by { get; set; }
		public string reason { get; set; }

		public DateTime getUnmuteDate() {
			return muteDate.AddMinutes(duration);
		}
	}
}
