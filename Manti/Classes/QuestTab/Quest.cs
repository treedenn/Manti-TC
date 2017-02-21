using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manti.Classes.QuestTab {
	public class Quest {
		// Section 1
		public uint id { get; set; }
		public string title { get; set; }
		public string logDescription { get; set; }
		public string questDescription { get; set; }
		public string area { get; set; }
		public string completed { get; set; }
		public string objective1 { get; set; }
		public string objective2 { get; set; }
		public string objective3 { get; set; }
		public string objective4 { get; set; }
		public int requirePlayerKills { get; set; }
		public int timeAllowed { get; set; }
		public int questInfo { get; set; }
		public int questLevel { get; set; }
		public int suggestedPlayers { get; set; }
		public int exclusiveGroup { get; set; }
		public int prevQuest { get; set; }
		public int nextQuest { get; set; }
		public int races { get; set; }
		public int classes { get; set; }
		public int minLevel { get; set; }
		public int maxLevel { get; set; }
		public int reqFaction1 { get; set; }
		public int reqFaction2 { get; set; }
		public int reqValue1 { get; set; }
		public int reqValue2 { get; set; }
		public int minRepFaction { get; set; }
		public int maxRepFaction { get; set; }
		public int minRepValue { get; set; }
		public int maxRepValue { get; set; }
		public int zoneIdOrQuestSort { get; set; }
		public int skillId { get; set; }
		public int skillPoints { get; set; }
		public int type { get; set; }
		public int flags { get; set; }
		public int specialFlags { get; set; }
		public int sourceItemId { get; set; }
		public int sourceItemCount { get; set; }
		public int sourceSpellId { get; set; }
		// Section 2
		public uint requiredNpcOrGoId1 { get; set; }
		public uint requiredNpcOrGoId2 { get; set; }
		public uint requiredNpcOrGoId3 { get; set; }
		public uint requiredNpcOrGoId4 { get; set; }
		public int requiredNpcCount1 { get; set; }
		public int requiredNpcCount2 { get; set; }
		public int requiredNpcCount3 { get; set; }
		public int requiredNpcCount4 { get; set; }
		public uint requiredItemId1 { get; set; }
		public uint requiredItemId2 { get; set; }
		public uint requiredItemId3 { get; set; }
		public uint requiredItemId4 { get; set; }
		public uint requiredItemId5 { get; set; }
		public uint requiredItemId6 { get; set; }
		public int requiredItemCount1 { get; set; }
		public int requiredItemCount2 { get; set; }
		public int requiredItemCount3 { get; set; }
		public int requiredItemCount4 { get; set; }
		public int requiredItemCount5 { get; set; }
		public int requiredItemCount6 { get; set; }
		public uint rewardChoiceItemId1 { get; set; }
		public uint rewardChoiceItemId2 { get; set; }
		public uint rewardChoiceItemId3 { get; set; }
		public uint rewardChoiceItemId4 { get; set; }
		public uint rewardChoiceItemId5 { get; set; }
		public uint rewardChoiceItemId6 { get; set; }
		public int rewardChoiceItemCount1 { get; set; }
		public int rewardChoiceItemCount2 { get; set; }
		public int rewardChoiceItemCount3 { get; set; }
		public int rewardChoiceItemCount4 { get; set; }
		public int rewardChoiceItemCount5 { get; set; }
		public int rewardChoiceItemCount6 { get; set; }
		public uint rewardItemId1 { get; set; }
		public uint rewardItemId2 { get; set; }
		public uint rewardItemId3 { get; set; }
		public uint rewardItemId4 { get; set; }
		public int rewardItemCount1 { get; set; }
		public int rewardItemCount2 { get; set; }
		public int rewardItemCount3 { get; set; }
		public int rewardItemCount4 { get; set; }
		public uint rewardFactionId1 { get; set; }
		public uint rewardFactionId2 { get; set; }
		public uint rewardFactionId3 { get; set; }
		public uint rewardFactionId4 { get; set; }
		public uint rewardFactionId5 { get; set; }
		public int rewardFactionValue1 { get; set; }
		public int rewardFactionValue2 { get; set; }
		public int rewardFactionValue3 { get; set; }
		public int rewardFactionValue4 { get; set; }
		public int rewardFactionValue5 { get; set; }
		public int rewardOrRequiredMoney { get; set; }
		public int rewardOrRequiredMoneyML { get; set; } // ML = Max Level
		public int rewardOrRequiredArenaPoints { get; set; }
		public int rewardOrRequiredHonorPoints { get; set; }
		public int rewardOrRequiredHonorMultiplier { get; set; }
		public uint rewardTitleId { get; set; }
		public uint rewardTalent { get; set; }
		public uint mailTemplateId { get; set; }
		public uint mailDelay { get; set; }
		public uint rewardDisplaySpell { get; set; }
		public uint rewardSpell { get; set; }
	}
}
