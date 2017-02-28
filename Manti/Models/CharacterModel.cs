using Manti.Classes.Settings;
using Manti.Classes.CharacterTab;

namespace Manti.Models {
	public class CharacterModel {
		private CharacterModel() { character = new Character(); }

		private static CharacterModel model;

		public Character character { get; set; }
		public CharacterInventory[] inventory { get; set; }

		public void updateFullCharacterFromDatabase(uint guid) {
			updateCharacterFromDatabase(guid);
			updateCharacterInventoryFromDatabase(guid);
		}

		public void updateCharacterFromDatabase(uint guid) {
			character = Settings.getCharsDB().getCharacter(guid);
		}

		public void updateCharacterInventoryFromDatabase(uint guid) {
			inventory = Settings.getCharsDB().getCharacterInventory(guid);
		}

		public static CharacterModel getInstance() {
			if(model == null) { model = new CharacterModel(); }

			return model;
		}
	}
}
