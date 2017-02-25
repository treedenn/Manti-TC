using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Manti.Views.FormPopup {
	public partial class FormPopupEntities : Form {
		public FormPopupEntities() {
			InitializeComponent();
		}

		private DataSet tableEntity = new DataSet(); // containing items, creatures & game objects;

		private string selectionValue = "";
		private bool selectionOutput = true;
		private bool[] entities; // order: (item, creature, gameobject)

		#region Functions

		// Contain tables with all the entities;
		public DataSet setEntityTable {
			set { tableEntity = value; }
		}
		// Disables the radiobuttons
		public bool[] disableEntity {
			set { entities = value; }
		}

		// The selection value
		public string changeSelection {
			set { selectionValue = value; }
			get { return selectionValue; }
		}
		// Changes the output to either ID (true) or DisplayID (false)
		public bool changeOutput {
			set { selectionOutput = value; }
		}

		public void addItems(DataTable table) {
			dataGridViewPopupEntity.DataSource = null;
			bool isExact;

			string[] getSearch = // search text
            {
				textBoxPopupSearchID.Text.Trim(),
				textBoxPopupSearchDisplayID.Text.Trim(),
				textBoxPopupSearchValue.Text.Trim()
			};

			var searchTable = new DataTable();
			searchTable.Columns.Add("id", typeof(string));
			searchTable.Columns.Add("displayid", typeof(string));
			searchTable.Columns.Add("value", typeof(string));

			foreach(DataRow row in table.Rows) {
				for(var i = 0; i < getSearch.Length; i++) {
					if(getSearch[i] != "") {
						isExact = getSearch[i].StartsWith("=");

						if(isExact && getSearch[i].TrimStart('=').ToLower() == row[i].ToString().ToLower()) {
							searchTable.Rows.Add(row.ItemArray);
						} else if(row[i].ToString().ToLower().Contains(getSearch[i].ToLower())) {
							searchTable.Rows.Add(row.ItemArray);
						}

					}
				}

				if(getSearch[0] == "" && getSearch[1] == "" && getSearch[2] == "") {
					searchTable.Rows.Add(row.ItemArray);
				}
			}

			dataGridViewPopupEntity.DataSource = searchTable;

			if(selectionValue != "0") {
				foreach(DataGridViewRow row in dataGridViewPopupEntity.Rows) {
					if(selectionOutput) {
						if(row.Cells[0].Value.ToString() == selectionValue) {
							row.DefaultCellStyle.BackColor = Color.LightGreen;
							dataGridViewPopupEntity.FirstDisplayedScrollingRowIndex = row.Index;
						}
					} else {
						if(row.Cells[1].Value.ToString() == selectionValue) {
							row.DefaultCellStyle.BackColor = Color.LightGreen;
							dataGridViewPopupEntity.FirstDisplayedScrollingRowIndex = row.Index;
						}
					}
				}
			}


		}

		#endregion
		#region Events
		private void FormPopupEntities_Load(object sender, EventArgs e) {
			dataGridViewPopupEntity.AutoGenerateColumns = false;

			radioButtonPopupEntityItem.Checked = entities[0];
			radioButtonPopupEntityItem.Visible = entities[0];
			radioButtonPopupEntityCreature.Checked = entities[1];
			radioButtonPopupEntityCreature.Visible = entities[1];
			radioButtonPopupEntityGameObject.Checked = entities[2];
			radioButtonPopupEntityGameObject.Visible = entities[2];
		}
		private void buttonPopupEntitySearch_Click(object sender, EventArgs e) {
			if(radioButtonPopupEntityItem.Checked) {
				addItems(tableEntity.Tables[0]);
			} else if(radioButtonPopupEntityCreature.Checked) {
				addItems(tableEntity.Tables[1]);
			} else {
				addItems(tableEntity.Tables[2]);
			}
		}
		private void dataGridViewPopupEntity_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e) {
			buttonPopupOK_Click(sender, e);
		}
		private void buttonPopupOK_Click(object sender, EventArgs e) {
			if(dataGridViewPopupEntity.SelectedRows.Count > 0) {
				selectionValue = (selectionOutput) ?
					dataGridViewPopupEntity.SelectedRows[0].Cells[0].Value.ToString() :
					dataGridViewPopupEntity.SelectedRows[0].Cells[1].Value.ToString();
			}

			this.Close();
		}
		private void buttonPopupWowhead_Click(object sender, EventArgs e) {
			if(dataGridViewPopupEntity.SelectedRows.Count > 0) {
				string URL = "";

				URL = (radioButtonPopupEntityItem.Checked) ? "http://legion.wowhead.com/item=" : URL;
				URL = (radioButtonPopupEntityCreature.Checked) ? "http://legion.wowhead.com/npc=" : URL;
				URL = (radioButtonPopupEntityGameObject.Checked) ? "http://legion.wowhead.com/object=" : URL;

				System.Diagnostics.Process.Start(URL + dataGridViewPopupEntity.SelectedRows[0].Cells[0].Value.ToString());
			}

		}
		private void buttonPopupClose_Click(object sender, EventArgs e) {
			this.Close();
		}
		#endregion

	}
}
