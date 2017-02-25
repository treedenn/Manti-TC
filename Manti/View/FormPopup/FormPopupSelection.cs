using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Manti.Views.FormPopup {
	public partial class FormPopupSelection : Form {
		public FormPopupSelection() {
			InitializeComponent();
		}

		private DataTable data = new DataTable();
		private string selectionValue = "";

		#region Functions

		public DataTable setDataTable {
			set { data = value; }
		}
		public string setFormTitle {
			set { this.Text = value; }
		}
		public string changeSelection {
			set { selectionValue = value; }
			get { return selectionValue; }
		}
		private void addItems(TextBox search, int rowValue) {
			listViewPopupSelection.Items.Clear();

			foreach(DataRow row in data.Rows) {
				if(search.Text.Trim() != "") {
					if(row[rowValue].ToString().ToLower().Contains(search.Text.ToLower())) {
						var item = new ListViewItem( row[0].ToString() );
						item.SubItems.Add(row[1].ToString());
						listViewPopupSelection.Items.Add(item);
					}
				} else {
					var item = new ListViewItem(row[0].ToString());
					item.SubItems.Add(row[1].ToString());
					listViewPopupSelection.Items.Add(item);
				}
			}
		}

		#endregion
		#region Events
		// FormLoad
		private void FormPopupSelection_Load(object sender, EventArgs e) {
			addItems(textBoxPopupSearchValue, 1);

			foreach(ListViewItem item in listViewPopupSelection.Items) {
				if(item.SubItems[0].Text.ToString() == selectionValue) {
					item.BackColor = Color.LightGreen;
					item.Selected = true;
					listViewPopupSelection.EnsureVisible(item.Index);
				}
			}
		}
		// TextChanged for Value
		private void textBoxPopupSearchValue_TextChanged(object sender, EventArgs e) {
			addItems(textBoxPopupSearchValue, 1);
		}
		// TextChanged for ID
		private void textBoxPopupSearchID_TextChanged(object sender, EventArgs e) {
			addItems(textBoxPopupSearchID, 0);
		}
		// Button Click OK
		private void buttonPopupOK_Click(object sender, EventArgs e) {
			if(listViewPopupSelection.SelectedItems.Count > 0) {
				selectionValue = listViewPopupSelection.SelectedItems[0].Text;
			} else {
				selectionValue = "0";
			}

			this.Close();
		}
		// Button Click Close
		private void buttonPopupClose_Click(object sender, EventArgs e) {
			this.Close();
		}
		// Double Click Grid
		private void listViewPopupSelection_DoubleClick(object sender, EventArgs e) {
			buttonPopupOK_Click(sender, e);
		}

		#endregion

	}
}