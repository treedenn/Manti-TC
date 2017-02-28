using System.Security.Cryptography;

using Manti.Classes.Settings;
using Manti.Classes.AccountTab;
using System.Text;

namespace Manti.Models {
	public class AccountModel {
		private AccountModel() { account = new Account(); }

		private static AccountModel model;

		public Account account { get; set; }

		public void getAccountFromDatabase(uint entry) {
			account = Settings.getAuthDB().getAccount(entry);
		}

		public string generateShaHashPass(string username, string password) {
			SHA1 sha = SHA1.Create();
			string s = username + ":" + password;
			byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(s.ToUpper()));

			return bytesToHexString(bytes);
		}

		private string bytesToHexString(byte[] bytes) {
			var sb = new StringBuilder();
			foreach(byte b in bytes) {
				var hex = b.ToString("X2");
				sb.Append(hex);
			}

			return sb.ToString();
		}

		public static AccountModel getInstance() {
			if(model == null) { model = new AccountModel(); }

			return model;
		}
	}
}