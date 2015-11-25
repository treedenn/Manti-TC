namespace Manti.FormPopup
{
    partial class FormToolSelection
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxToolItemClassSearch = new System.Windows.Forms.TextBox();
            this.buttonToolItemClassOK = new System.Windows.Forms.Button();
            this.buttonToolItemClassClose = new System.Windows.Forms.Button();
            this.listViewToolItemClass = new System.Windows.Forms.ListView();
            this.columnHeaderTICID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTICValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // textBoxToolItemClassSearch
            // 
            this.textBoxToolItemClassSearch.Location = new System.Drawing.Point(12, 12);
            this.textBoxToolItemClassSearch.Name = "textBoxToolItemClassSearch";
            this.textBoxToolItemClassSearch.Size = new System.Drawing.Size(360, 20);
            this.textBoxToolItemClassSearch.TabIndex = 1;
            this.textBoxToolItemClassSearch.TextChanged += new System.EventHandler(this.textBoxToolItemClassSearch_TextChanged);
            // 
            // buttonToolItemClassOK
            // 
            this.buttonToolItemClassOK.Location = new System.Drawing.Point(12, 376);
            this.buttonToolItemClassOK.Name = "buttonToolItemClassOK";
            this.buttonToolItemClassOK.Size = new System.Drawing.Size(179, 23);
            this.buttonToolItemClassOK.TabIndex = 2;
            this.buttonToolItemClassOK.Text = "OK";
            this.buttonToolItemClassOK.UseVisualStyleBackColor = true;
            this.buttonToolItemClassOK.Click += new System.EventHandler(this.buttonToolItemClassOK_Click);
            // 
            // buttonToolItemClassClose
            // 
            this.buttonToolItemClassClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonToolItemClassClose.Location = new System.Drawing.Point(193, 376);
            this.buttonToolItemClassClose.Name = "buttonToolItemClassClose";
            this.buttonToolItemClassClose.Size = new System.Drawing.Size(179, 23);
            this.buttonToolItemClassClose.TabIndex = 2;
            this.buttonToolItemClassClose.Text = "Close";
            this.buttonToolItemClassClose.UseVisualStyleBackColor = true;
            this.buttonToolItemClassClose.Click += new System.EventHandler(this.buttonToolItemClassClose_Click);
            // 
            // listViewToolItemClass
            // 
            this.listViewToolItemClass.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewToolItemClass.BackColor = System.Drawing.SystemColors.Control;
            this.listViewToolItemClass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewToolItemClass.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderTICID,
            this.columnHeaderTICValue});
            this.listViewToolItemClass.FullRowSelect = true;
            this.listViewToolItemClass.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewToolItemClass.Location = new System.Drawing.Point(12, 38);
            this.listViewToolItemClass.MultiSelect = false;
            this.listViewToolItemClass.Name = "listViewToolItemClass";
            this.listViewToolItemClass.Size = new System.Drawing.Size(360, 332);
            this.listViewToolItemClass.TabIndex = 3;
            this.listViewToolItemClass.UseCompatibleStateImageBehavior = false;
            this.listViewToolItemClass.View = System.Windows.Forms.View.Details;
            this.listViewToolItemClass.DoubleClick += new System.EventHandler(this.listViewToolItemClass_DoubleClick);
            // 
            // columnHeaderTICID
            // 
            this.columnHeaderTICID.Tag = "";
            this.columnHeaderTICID.Text = "ID";
            // 
            // columnHeaderTICValue
            // 
            this.columnHeaderTICValue.Tag = "";
            this.columnHeaderTICValue.Text = "Value";
            this.columnHeaderTICValue.Width = 300;
            // 
            // FormToolSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 411);
            this.ControlBox = false;
            this.Controls.Add(this.listViewToolItemClass);
            this.Controls.Add(this.buttonToolItemClassClose);
            this.Controls.Add(this.buttonToolItemClassOK);
            this.Controls.Add(this.textBoxToolItemClassSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormToolSelection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Selection Mode";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormToolItemClass_FormClosed);
            this.Load += new System.EventHandler(this.FormToolItemClass_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxToolItemClassSearch;
        private System.Windows.Forms.Button buttonToolItemClassOK;
        private System.Windows.Forms.Button buttonToolItemClassClose;
        private System.Windows.Forms.ListView listViewToolItemClass;
        private System.Windows.Forms.ColumnHeader columnHeaderTICID;
        private System.Windows.Forms.ColumnHeader columnHeaderTICValue;
    }
}