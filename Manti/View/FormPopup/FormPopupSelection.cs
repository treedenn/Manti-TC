using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Manti.Views.FormPopup {
	public partial class FormPopupSelection : Form {
		public FormPopupSelection() {
			InitializeComponent();
		}

		private string csvName;
		private int csvId;
		private int csvValue;
		private bool loadImmediately;

		private DataTable data;
		private string selectionValue = "";

		public DataTable setDataTable {
			set { data = value; }
		}
		public string setCsvName {
			set { csvName = value; }
		}
		public int setCsvId {
			set { csvId = value; }
		}
		public int setCsvValue {
			set { csvValue = value; }
		}
		public bool setLoad {
			set { loadImmediately = value; }
		}
		public string setFormTitle {
			set { this.Text = value; }
		}
		public string changeSelection {
			set { selectionValue = value; }
			get { return selectionValue; }
		}

		private void addAllRows() {
			foreach(DataRow row in data.Rows) {
				var item = new ListViewItem(row[0].ToString());
				item.SubItems.Add(row[1].ToString());
				listViewPopupSelection.Items.Add(item);
			}
		}
		private void searchRows(string searchValue, bool byId) {
			foreach(DataRow row in data.Rows) {
				if(!string.IsNullOrEmpty(searchValue)) {
					if(row[(byId ? 0 : 1)].ToString().ToLower().Contains(searchValue.ToLower())) {
						var item = new ListViewItem(row[0].ToString());
						item.SubItems.Add(row[1].ToString());
						listViewPopupSelection.Items.Add(item);
					}
				}
			}
		}

		private void loadCsvFile() {
			data = Classes.UtilityHelper.readExcelCSV(csvName, csvId, csvValue);
		}
		private void loadSelectedValue() {
			foreach(ListViewItem item in listViewPopupSelection.Items) {
				if(item.SubItems[0].Text.ToString() == selectionValue) {
					item.BackColor = Color.LightGreen;
					//item.Selected = true;
					listViewPopupSelection.EnsureVisible(item.Index);

					return;
				}
			}
		}

		private void FormPopupSelection_Load(object sender, EventArgs e) {
			if(loadImmediately) {
				loadCsvFile();
				addAllRows();
				loadSelectedValue();
			}
		}

		private void buttonSearch_Click(object sender, EventArgs e) {
			listViewPopupSelection.Items.Clear();

			if(data == null) { loadCsvFile(); }

			string sId = textBoxPopupSearchID.Text;
			string sVal = textBoxPopupSearchValue.Text;

			if(!string.IsNullOrEmpty(sId)) {
				searchRows(sId, true);
			} else if(!string.IsNullOrEmpty(sVal)) {
				searchRows(sVal, false);
			} else {
				if(!loadImmediately) {
					DialogResult dr = MessageBox.Show("You sure you want to load them all?", "Warning - This may take a while...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

					if(dr == DialogResult.No) { return; }
				}

				addAllRows();
			}

			loadSelectedValue();
		}
		private void buttonPopupOK_Click(object sender, EventArgs e) {
			if(listViewPopupSelection.SelectedItems.Count > 0) {
				selectionValue = listViewPopupSelection.SelectedItems[0].Text;
			} else {
				selectionValue = "0";
			}

			this.Close();
		}
		private void buttonPopupClose_Click(object sender, EventArgs e) {
			this.Close();
		}

		private void listViewPopupSelection_DoubleClick(object sender, EventArgs e) {
			buttonPopupOK_Click(sender, e);
		}

		
	}
}