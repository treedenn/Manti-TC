using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Manti.Classes.Settings;
using Manti.Classes.GameObjectTab;

namespace Manti.Models {
	public class GameObjectModel {
		private GameObjectModel() { }

		private static GameObjectModel model;

		public GameObject gameObject { get; set; }

		public void updateGameObjectFromDatabase(uint id) {
			gameObject = Settings.getWorldDB().getGameObject(id);
		}

		public static GameObjectModel getInstance() {
			if(model == null) { model = new GameObjectModel(); }

			return model;
		}
	}
}
