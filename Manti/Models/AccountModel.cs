using Manti.Classes.Settings;
using Manti.Classes.AccountTab;

namespace Manti.Models {
	public class AccountModel {
		private AccountModel() { }

		private static AccountModel model;

		public Account account { get; set; }

		public void getAccountFromDatabase(uint entry) {
			account = Settings.getAuthDB().getAccount(entry);
		}

		public static AccountModel getInstance() {
			if(model == null) { model = new AccountModel(); }

			return model;
		}
	}
}
