namespace Manti
{
    partial class FormSettings
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Account");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Character");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Creature");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Quest");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Game Object");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Item");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Tab Settings", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6});
            this.treeViewSettings = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // treeViewSettings
            // 
            this.treeViewSettings.BackColor = System.Drawing.SystemColors.Control;
            this.treeViewSettings.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeViewSettings.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeViewSettings.FullRowSelect = true;
            this.treeViewSettings.HideSelection = false;
            this.treeViewSettings.HotTracking = true;
            this.treeViewSettings.ImeMode = System.Windows.Forms.ImeMode.Close;
            this.treeViewSettings.Location = new System.Drawing.Point(0, 0);
            this.treeViewSettings.Name = "treeViewSettings";
            treeNode1.Name = "Node0";
            treeNode1.Text = "Account";
            treeNode2.Name = "Node1";
            treeNode2.Text = "Character";
            treeNode3.Name = "Node2";
            treeNode3.Text = "Creature";
            treeNode4.Name = "Node3";
            treeNode4.Text = "Quest";
            treeNode5.Name = "Node4";
            treeNode5.Text = "Game Object";
            treeNode6.Name = "Node5";
            treeNode6.Text = "Item";
            treeNode7.Name = "Node6";
            treeNode7.Text = "Tab Settings";
            this.treeViewSettings.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode7});
            this.treeViewSettings.Size = new System.Drawing.Size(121, 461);
            this.treeViewSettings.TabIndex = 0;
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.treeViewSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewSettings;
    }
}