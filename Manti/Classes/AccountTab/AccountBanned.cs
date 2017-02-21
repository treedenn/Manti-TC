using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manti.Classes.AccountTab {
	public class AccountBanned {
		public DateTime banDate { get; set; }
		public DateTime unbanDate { get; set; }
		public string by { get; set; }
		public string reason { get; set; }
		public bool isActive { get; set; }
	}
}
