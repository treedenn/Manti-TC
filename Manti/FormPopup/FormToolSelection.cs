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
    public partial class FormToolSelection : Form
    {
        public FormToolSelection()
        {
            InitializeComponent();
        }

        private DataTable data = new DataTable();
        private string id = "";

        public DataTable setDataTable
        {
            set { data = value; }
        }

        public string getSelection
        {
            get { return id; }
        }

        public string setFormText
        {
            set { this.Text = value; }
        }

        private void addItems()
        {
            listViewToolItemClass.Items.Clear();

            foreach (DataRow row in data.Rows)
            {
                if (textBoxToolItemClassSearch.Text.Trim() != "")
                {
                    if (row["value"].ToString().ToLower().Contains(textBoxToolItemClassSearch.Text.ToLower()))
                    {
                        var item = new ListViewItem( row["id"].ToString() );
                        item.SubItems.Add( row["value"].ToString() );
                        listViewToolItemClass.Items.Add(item);
                    }
                } else
                {
                    var item = new ListViewItem(row["id"].ToString());
                    item.SubItems.Add(row["value"].ToString());
                    listViewToolItemClass.Items.Add(item);
                }
            }
        }

        private void FormToolItemClass_Load(object sender, EventArgs e)
        {
            addItems();
        }

            // FormClosed
        private void FormToolItemClass_FormClosed(object sender, FormClosedEventArgs e)
        {
            (this.Owner as FormMain).EnableTabs = true;
        }

            // TextChanged
        private void textBoxToolItemClassSearch_TextChanged(object sender, EventArgs e)
        {
            addItems();
        }

            // Button Click OK
        private void buttonToolItemClassOK_Click(object sender, EventArgs e)
        {
            if (listViewToolItemClass.SelectedItems.Count > 0)
            {
                id = listViewToolItemClass.SelectedItems[0].Text;

                this.Close();
            }
        }

            // Button Click Close
        private void buttonToolItemClassClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

            // Double Click Grid
        private void listViewToolItemClass_DoubleClick(object sender, EventArgs e)
        {
            buttonToolItemClassOK_Click(sender, e);
        }

    }
}