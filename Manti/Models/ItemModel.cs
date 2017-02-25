using Manti.Classes.Settings;
using Manti.Classes.ItemTab;

namespace Manti.Models {
	public class ItemModel {
		private ItemModel() { }

		private static ItemModel model;

		public Item item { get; set; }
		public ItemLPMD[] loot { get; set; }
		public ItemLPMD[] prospect { get; set; }
		public ItemLPMD[] mill { get; set; }
		public ItemLPMD[] disenchant { get; set; }

		public void updateFullItemFromDatabase(uint id) {
			updateItemFromDatabase(id);
			updateItemLootFromDatabase(id);
			updateItemProspectFromDatabase(id);
			updateItemMillFromDatabase(id);
			updateItemDisenchantFromDatabase(id);
		}

		public void updateItemFromDatabase(uint id) {
			item = Settings.getWorldDB().getItem(id);
		}

		public void updateItemLootFromDatabase(uint id) {
			loot = Settings.getWorldDB().getItemLPMD(id, Classes.Database.DatabaseWorld.LPMD.LOOT);
		}

		public void updateItemProspectFromDatabase(uint id) {
			prospect = Settings.getWorldDB().getItemLPMD(id, Classes.Database.DatabaseWorld.LPMD.PROSPECTING);
		}

		public void updateItemMillFromDatabase(uint id) {
			mill = Settings.getWorldDB().getItemLPMD(id, Classes.Database.DatabaseWorld.LPMD.MILLING);
		}

		public void updateItemDisenchantFromDatabase(uint id) {
			disenchant = Settings.getWorldDB().getItemLPMD(id, Classes.Database.DatabaseWorld.LPMD.DISENCHANT);
		}

		public static ItemModel getInstance() {
			if(model == null) { model = new ItemModel(); }

			return model;
		}
	}
}
