using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manti.Classes.CreatureTab {
	public class CreatureVendor {
		public uint entry { get; set; }
		public int slot { get; set; }
		public uint item { get; set; }
		public uint maxCount { get; set; }
		public uint increaseTime { get; set; }
		public uint extendedCost { get; set; }
		public string name { get; set; }
	}
}
