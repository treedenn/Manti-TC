namespace Manti.FormPopup
{
    partial class FormPopupEntities
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPopupEntities));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBoxPopupEntity = new System.Windows.Forms.GroupBox();
            this.radioButtonPopupEntityGameObject = new System.Windows.Forms.RadioButton();
            this.radioButtonPopupEntityCreature = new System.Windows.Forms.RadioButton();
            this.radioButtonPopupEntityItem = new System.Windows.Forms.RadioButton();
            this.textBoxPopupSearchID = new System.Windows.Forms.TextBox();
            this.textBoxPopupSearchValue = new System.Windows.Forms.TextBox();
            this.buttonPopupClose = new System.Windows.Forms.Button();
            this.buttonPopupOK = new System.Windows.Forms.Button();
            this.buttonPopupWowhead = new System.Windows.Forms.Button();
            this.textBoxPopupSearchDisplayID = new System.Windows.Forms.TextBox();
            this.buttonPopupEntitySearch = new System.Windows.Forms.Button();
            this.dataGridViewPopupEntity = new System.Windows.Forms.DataGridView();
            this.ColumnPopupEntityID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPopupEntityDisplayID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPopupEntityValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBoxPopupEntity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPopupEntity)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxPopupEntity
            // 
            this.groupBoxPopupEntity.Controls.Add(this.radioButtonPopupEntityGameObject);
            this.groupBoxPopupEntity.Controls.Add(this.radioButtonPopupEntityCreature);
            this.groupBoxPopupEntity.Controls.Add(this.radioButtonPopupEntityItem);
            this.groupBoxPopupEntity.Location = new System.Drawing.Point(12, 12);
            this.groupBoxPopupEntity.Name = "groupBoxPopupEntity";
            this.groupBoxPopupEntity.Size = new System.Drawing.Size(306, 48);
            this.groupBoxPopupEntity.TabIndex = 0;
            this.groupBoxPopupEntity.TabStop = false;
            this.groupBoxPopupEntity.Text = "Entities";
            // 
            // radioButtonPopupEntityGameObject
            // 
            this.radioButtonPopupEntityGameObject.AutoSize = true;
            this.radioButtonPopupEntityGameObject.Location = new System.Drawing.Point(208, 19);
            this.radioButtonPopupEntityGameObject.Name = "radioButtonPopupEntityGameObject";
            this.radioButtonPopupEntityGameObject.Size = new System.Drawing.Size(92, 17);
            this.radioButtonPopupEntityGameObject.TabIndex = 2;
            this.radioButtonPopupEntityGameObject.Text = "Game Objects";
            this.radioButtonPopupEntityGameObject.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonPopupEntityGameObject.UseVisualStyleBackColor = true;
            // 
            // radioButtonPopupEntityCreature
            // 
            this.radioButtonPopupEntityCreature.AutoSize = true;
            this.radioButtonPopupEntityCreature.Location = new System.Drawing.Point(97, 19);
            this.radioButtonPopupEntityCreature.Name = "radioButtonPopupEntityCreature";
            this.radioButtonPopupEntityCreature.Size = new System.Drawing.Size(70, 17);
            this.radioButtonPopupEntityCreature.TabIndex = 1;
            this.radioButtonPopupEntityCreature.Text = "Creatures";
            this.radioButtonPopupEntityCreature.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonPopupEntityCreature.UseVisualStyleBackColor = true;
            // 
            // radioButtonPopupEntityItem
            // 
            this.radioButtonPopupEntityItem.AutoSize = true;
            this.radioButtonPopupEntityItem.Checked = true;
            this.radioButtonPopupEntityItem.Location = new System.Drawing.Point(6, 19);
            this.radioButtonPopupEntityItem.Name = "radioButtonPopupEntityItem";
            this.radioButtonPopupEntityItem.Size = new System.Drawing.Size(50, 17);
            this.radioButtonPopupEntityItem.TabIndex = 0;
            this.radioButtonPopupEntityItem.TabStop = true;
            this.radioButtonPopupEntityItem.Text = "Items";
            this.radioButtonPopupEntityItem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonPopupEntityItem.UseVisualStyleBackColor = true;
            // 
            // textBoxPopupSearchID
            // 
            this.textBoxPopupSearchID.Location = new System.Drawing.Point(12, 66);
            this.textBoxPopupSearchID.Name = "textBoxPopupSearchID";
            this.textBoxPopupSearchID.Size = new System.Drawing.Size(60, 20);
            this.textBoxPopupSearchID.TabIndex = 7;
            // 
            // textBoxPopupSearchValue
            // 
            this.textBoxPopupSearchValue.Location = new System.Drawing.Point(144, 66);
            this.textBoxPopupSearchValue.Name = "textBoxPopupSearchValue";
            this.textBoxPopupSearchValue.Size = new System.Drawing.Size(228, 20);
            this.textBoxPopupSearchValue.TabIndex = 5;
            // 
            // buttonPopupClose
            // 
            this.buttonPopupClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPopupClose.Location = new System.Drawing.Point(262, 376);
            this.buttonPopupClose.Name = "buttonPopupClose";
            this.buttonPopupClose.Size = new System.Drawing.Size(110, 23);
            this.buttonPopupClose.TabIndex = 8;
            this.buttonPopupClose.Text = "Close";
            this.buttonPopupClose.UseVisualStyleBackColor = true;
            this.buttonPopupClose.Click += new System.EventHandler(this.buttonPopupClose_Click);
            // 
            // buttonPopupOK
            // 
            this.buttonPopupOK.Location = new System.Drawing.Point(12, 376);
            this.buttonPopupOK.Name = "buttonPopupOK";
            this.buttonPopupOK.Size = new System.Drawing.Size(110, 23);
            this.buttonPopupOK.TabIndex = 9;
            this.buttonPopupOK.Text = "OK";
            this.buttonPopupOK.UseVisualStyleBackColor = true;
            this.buttonPopupOK.Click += new System.EventHandler(this.buttonPopupOK_Click);
            // 
            // buttonPopupWowhead
            // 
            this.buttonPopupWowhead.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPopupWowhead.Location = new System.Drawing.Point(137, 376);
            this.buttonPopupWowhead.Name = "buttonPopupWowhead";
            this.buttonPopupWowhead.Size = new System.Drawing.Size(110, 23);
            this.buttonPopupWowhead.TabIndex = 10;
            this.buttonPopupWowhead.Text = "Search Wowhead";
            this.buttonPopupWowhead.UseVisualStyleBackColor = true;
            this.buttonPopupWowhead.Click += new System.EventHandler(this.buttonPopupWowhead_Click);
            // 
            // textBoxPopupSearchDisplayID
            // 
            this.textBoxPopupSearchDisplayID.Location = new System.Drawing.Point(78, 66);
            this.textBoxPopupSearchDisplayID.Name = "textBoxPopupSearchDisplayID";
            this.textBoxPopupSearchDisplayID.Size = new System.Drawing.Size(60, 20);
            this.textBoxPopupSearchDisplayID.TabIndex = 11;
            // 
            // buttonPopupEntitySearch
            // 
            this.buttonPopupEntitySearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPopupEntitySearch.BackgroundImage = global::Manti.Properties.Resources.iconSearch;
            this.buttonPopupEntitySearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonPopupEntitySearch.Image = ((System.Drawing.Image)(resources.GetObject("buttonPopupEntitySearch.Image")));
            this.buttonPopupEntitySearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonPopupEntitySearch.Location = new System.Drawing.Point(324, 12);
            this.buttonPopupEntitySearch.Name = "buttonPopupEntitySearch";
            this.buttonPopupEntitySearch.Size = new System.Drawing.Size(48, 48);
            this.buttonPopupEntitySearch.TabIndex = 12;
            this.buttonPopupEntitySearch.UseVisualStyleBackColor = true;
            this.buttonPopupEntitySearch.Click += new System.EventHandler(this.buttonPopupEntitySearch_Click);
            // 
            // dataGridViewPopupEntity
            // 
            this.dataGridViewPopupEntity.AllowUserToAddRows = false;
            this.dataGridViewPopupEntity.AllowUserToDeleteRows = false;
            this.dataGridViewPopupEntity.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            this.dataGridViewPopupEntity.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewPopupEntity.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewPopupEntity.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewPopupEntity.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewPopupEntity.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridViewPopupEntity.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataGridViewPopupEntity.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            this.dataGridViewPopupEntity.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewPopupEntity.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPopupEntity.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnPopupEntityID,
            this.ColumnPopupEntityDisplayID,
            this.ColumnPopupEntityValue});
            this.dataGridViewPopupEntity.GridColor = System.Drawing.SystemColors.HotTrack;
            this.dataGridViewPopupEntity.Location = new System.Drawing.Point(12, 89);
            this.dataGridViewPopupEntity.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridViewPopupEntity.MultiSelect = false;
            this.dataGridViewPopupEntity.Name = "dataGridViewPopupEntity";
            this.dataGridViewPopupEntity.ReadOnly = true;
            this.dataGridViewPopupEntity.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            this.dataGridViewPopupEntity.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewPopupEntity.RowHeadersVisible = false;
            this.dataGridViewPopupEntity.RowHeadersWidth = 99;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            this.dataGridViewPopupEntity.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewPopupEntity.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            this.dataGridViewPopupEntity.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewPopupEntity.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dataGridViewPopupEntity.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            this.dataGridViewPopupEntity.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            this.dataGridViewPopupEntity.RowTemplate.Height = 15;
            this.dataGridViewPopupEntity.RowTemplate.ReadOnly = true;
            this.dataGridViewPopupEntity.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewPopupEntity.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewPopupEntity.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewPopupEntity.ShowEditingIcon = false;
            this.dataGridViewPopupEntity.Size = new System.Drawing.Size(360, 284);
            this.dataGridViewPopupEntity.TabIndex = 13;
            this.dataGridViewPopupEntity.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPopupEntity_CellContentDoubleClick);
            // 
            // ColumnPopupEntityID
            // 
            this.ColumnPopupEntityID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColumnPopupEntityID.DataPropertyName = "id";
            this.ColumnPopupEntityID.Frozen = true;
            this.ColumnPopupEntityID.HeaderText = "ID";
            this.ColumnPopupEntityID.Name = "ColumnPopupEntityID";
            this.ColumnPopupEntityID.ReadOnly = true;
            this.ColumnPopupEntityID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnPopupEntityID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnPopupEntityID.Width = 24;
            // 
            // ColumnPopupEntityDisplayID
            // 
            this.ColumnPopupEntityDisplayID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColumnPopupEntityDisplayID.DataPropertyName = "displayid";
            this.ColumnPopupEntityDisplayID.Frozen = true;
            this.ColumnPopupEntityDisplayID.HeaderText = "DisplayID";
            this.ColumnPopupEntityDisplayID.Name = "ColumnPopupEntityDisplayID";
            this.ColumnPopupEntityDisplayID.ReadOnly = true;
            this.ColumnPopupEntityDisplayID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnPopupEntityDisplayID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnPopupEntityDisplayID.Width = 58;
            // 
            // ColumnPopupEntityValue
            // 
            this.ColumnPopupEntityValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnPopupEntityValue.DataPropertyName = "value";
            this.ColumnPopupEntityValue.HeaderText = "Value";
            this.ColumnPopupEntityValue.Name = "ColumnPopupEntityValue";
            this.ColumnPopupEntityValue.ReadOnly = true;
            this.ColumnPopupEntityValue.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnPopupEntityValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FormPopupEntities
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 411);
            this.ControlBox = false;
            this.Controls.Add(this.dataGridViewPopupEntity);
            this.Controls.Add(this.buttonPopupEntitySearch);
            this.Controls.Add(this.textBoxPopupSearchDisplayID);
            this.Controls.Add(this.buttonPopupWowhead);
            this.Controls.Add(this.buttonPopupClose);
            this.Controls.Add(this.buttonPopupOK);
            this.Controls.Add(this.textBoxPopupSearchID);
            this.Controls.Add(this.textBoxPopupSearchValue);
            this.Controls.Add(this.groupBoxPopupEntity);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormPopupEntities";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Entity Selection";
            this.Load += new System.EventHandler(this.FormPopupEntities_Load);
            this.groupBoxPopupEntity.ResumeLayout(false);
            this.groupBoxPopupEntity.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPopupEntity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxPopupEntity;
        private System.Windows.Forms.RadioButton radioButtonPopupEntityGameObject;
        private System.Windows.Forms.RadioButton radioButtonPopupEntityCreature;
        private System.Windows.Forms.RadioButton radioButtonPopupEntityItem;
        private System.Windows.Forms.TextBox textBoxPopupSearchID;
        private System.Windows.Forms.TextBox textBoxPopupSearchValue;
        private System.Windows.Forms.Button buttonPopupClose;
        private System.Windows.Forms.Button buttonPopupOK;
        private System.Windows.Forms.Button buttonPopupWowhead;
        private System.Windows.Forms.TextBox textBoxPopupSearchDisplayID;
        private System.Windows.Forms.Button buttonPopupEntitySearch;
        private System.Windows.Forms.DataGridView dataGridViewPopupEntity;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPopupEntityID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPopupEntityDisplayID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPopupEntityValue;
    }
}