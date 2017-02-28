using Manti.Classes.Settings;
using Manti.Classes.QuestTab;

namespace Manti.Models {
	public class QuestModel {
		private QuestModel() { quest = new Quest(); }

		private static QuestModel model;

		public Quest quest { get; set; } // quest
		public QuestGT[] giver { get; set; } // quest giver(s)
		public QuestGT[] taker { get; set; } // quest taker(s)

		public void updateFullQuestFromDatabase(uint id) {
			updateQuestFromDatabase(id);
			updateQuestGiversFromDatabase(id);
			updateQuestTakersFromDatabase(id);
		}

		public void updateQuestFromDatabase(uint id) {
			quest = Settings.getWorldDB().getQuest(id);
		}

		public void updateQuestGiversFromDatabase(uint id) {
			giver = Settings.getWorldDB().getQuestGT(id, true);
		}

		public void updateQuestTakersFromDatabase(uint id) {
			taker = Settings.getWorldDB().getQuestGT(id, false);
		}

		public static QuestModel getInstance() {
			if(model == null) { model = new QuestModel(); }

			return model;
		}
	}
}
