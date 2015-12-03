using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Manti.FormPopup
{
    public partial class FormPopupSelection : Form
    {
        public FormPopupSelection()
        {
            InitializeComponent();
        }

        #region Functions

        private DataTable data = new DataTable();
        private string id = "";

        public DataTable setDataTable
        {
            set { data = value; }
        }
        public string setFormTitle
        {
            set { this.Text = value; }
        }
        public string getSelection
        {
            get { return id; }
        }
        private void addItems()
        {
            listViewPopupSelection.Items.Clear();

            foreach (DataRow row in data.Rows)
            {
                if (textBoxPopupSearch.Text.Trim() != "")
                {
                    if (row["value"].ToString().ToLower().Contains(textBoxPopupSearch.Text.ToLower()))
                    {
                        var item = new ListViewItem( row["id"].ToString() );
                        item.SubItems.Add( row["name"].ToString() );
                        listViewPopupSelection.Items.Add(item);
                    }
                } else
                {
                    var item = new ListViewItem(row["id"].ToString());
                    item.SubItems.Add(row["name"].ToString());
                    listViewPopupSelection.Items.Add(item);
                }
            }
        }

        #endregion
        #region Events
            // FormLoad
        private void FormPopupSelection_Load(object sender, EventArgs e)
        {
            addItems();
        }
            // TextChanged
        private void textBoxPopupSearch_TextChanged(object sender, EventArgs e)
        {
            addItems();
        }
            // Button Click OK
        private void buttonPopupOK_Click(object sender, EventArgs e)
        {
            if (listViewPopupSelection.SelectedItems.Count > 0)
            {
                id = listViewPopupSelection.SelectedItems[0].Text;

                this.Close();
            }
        }
            // Button Click Close
        private void buttonPopupClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
            // Double Click Grid
        private void listViewPopupSelection_DoubleClick(object sender, EventArgs e)
        {
            buttonPopupOK_Click(sender, e);
        }

        #endregion

    }
}