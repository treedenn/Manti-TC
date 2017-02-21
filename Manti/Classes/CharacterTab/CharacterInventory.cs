using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manti.Classes.CharacterTab {
	public class CharacterInventory {
		public uint guid { get; set; }
		public uint bag { get; set; }
		public uint slot { get; set; }
		public uint item { get; set; }
		public string name { get; set; }
	}
}
