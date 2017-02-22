using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manti.Models {
	public class ServerModel {
		private ServerModel() { isWorldOnline = false; isAuthOnline = false; }

		private static ServerModel model;

		public bool isWorldOnline { get; set; }
		public bool isAuthOnline { get; set; }

		public static ServerModel getInstance() {
			if(model == null) { model = new ServerModel(); }

			return model;
		}
	}
}
