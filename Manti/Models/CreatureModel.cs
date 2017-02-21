using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Manti.Classes.Settings;
using Manti.Classes.CreatureTab;

namespace Manti.Models {
	public class CreatureModel {
		private CreatureModel() { }

		private static CreatureModel model;

		public Creature creature { get; set; } // creature
		public CreatureLocation[] location { get; set; } // location
		public CreatureVendor[] vendor { get; set; } // vendor
		public CreatureLPS[] loot { get; set; } // loot
		public CreatureLPS[] pickpocket { get; set; } // pickpocket
		public CreatureLPS[] skin { get; set; } // skin

		public void updateFullCreatureFromDatabase(uint entry) {
			updateCreatureFromDatabase(entry);
			updateCreatureLocationFromDatabase(entry);
			updateCreatureVendorFromDatabase(entry);
			updateCreatureLootFromDatabase(entry);
			updateCreaturePickpocketFromDatabase(entry);
			updateCreatureSkinFromDatabase(entry);
		}

		public void updateCreatureFromDatabase(uint entry) {
			creature = Settings.getWorldDB().getCreature(entry);
		}

		public void updateCreatureLocationFromDatabase(uint entry) {
			location = Settings.getWorldDB().getCreatureLocation(entry);
		}

		public void updateCreatureVendorFromDatabase(uint entry) {
			vendor = Settings.getWorldDB().getCreatureVendor(entry);
		}

		public void updateCreatureLootFromDatabase(uint entry) {
			loot = Settings.getWorldDB().getCreatureLoot(entry);
		}

		public void updateCreaturePickpocketFromDatabase(uint entry) {
			pickpocket = Settings.getWorldDB().getCreaturePickpocket(entry);
		}

		public void updateCreatureSkinFromDatabase(uint entry) {
			skin = Settings.getWorldDB().getCreatureSkin(entry);
		}

		public static CreatureModel getInstance() {
			if(model == null) { model = new CreatureModel(); }

			return model;
		}
	}
}
