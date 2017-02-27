using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manti.Models {
	public class WorldManagerModel {
		private WorldManagerModel() { }

		private static WorldManagerModel model;

		public worldObject lastGeneratedWorldObject;

		public static WorldManagerModel getInstance() {
			if(model == null) { model = new WorldManagerModel(); }

			return model;
		}

		public enum worldObject {
			ITEM,
			CREATURE,
			GAMEOBJECT,
			QUEST
		}
	}
}
