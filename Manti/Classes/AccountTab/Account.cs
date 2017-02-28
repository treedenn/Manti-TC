namespace Manti.Classes.AccountTab {
	public class Account {
		public uint id { get; set; }
		public string username { get; set; }
		public string password { get; set; }
		public string email { get; set; }
		public string reqemail { get; set; }
		public string joindate { get; set; }
		public string lastIP { get; set; }
		public bool locked { get; set; }
		public bool online { get; set; }
		public byte expansion { get; set; }
		public AccountBanned banned { get; set; }
		public AccountMuted muted { get; set; }
		public AccountAccess[] access { get; set; }
	}
}
