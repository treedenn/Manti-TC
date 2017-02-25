namespace Manti.Classes.ItemTab {
	public class ItemLPMD { // loot, prospecting, milling and disenchant template
		public uint entry { get; set; }
		public uint item { get; set; }
		public int reference { get; set; }
		public float chance { get; set; }
		public byte questRequired { get; set; }
		public ushort lootMode { get; set; }
		public ushort groupId { get; set; }
		public ushort minCount { get; set; }
		public ushort maxCount { get; set; }
		public string name { get; set; }
	}
}
