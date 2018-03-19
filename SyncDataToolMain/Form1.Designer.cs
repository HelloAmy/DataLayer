namespace SyncDataToolMain
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.数据库同步ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.纯数据同步ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.数据库同步ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(597, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 数据库同步ToolStripMenuItem
            // 
            this.数据库同步ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.纯数据同步ToolStripMenuItem});
            this.数据库同步ToolStripMenuItem.Name = "数据库同步ToolStripMenuItem";
            this.数据库同步ToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.数据库同步ToolStripMenuItem.Text = "数据库同步";
            // 
            // 纯数据同步ToolStripMenuItem
            // 
            this.纯数据同步ToolStripMenuItem.Name = "纯数据同步ToolStripMenuItem";
            this.纯数据同步ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.纯数据同步ToolStripMenuItem.Text = "纯数据同步";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 341);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 数据库同步ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 纯数据同步ToolStripMenuItem;
    }
}

