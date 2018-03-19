namespace SyncDataToolMain
{
    partial class DataSyncForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.FromTableNames = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.FromDBName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.FromPwd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.FromAccount = new System.Windows.Forms.TextBox();
            this.FromDBServer = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ToTableNames = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.ToDBName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ToPwd = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ToAccount = new System.Windows.Forms.TextBox();
            this.ToDBServer = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ResultMsg = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "DB Server";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Account";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.FromTableNames);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.FromDBName);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.FromPwd);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.FromAccount);
            this.groupBox1.Controls.Add(this.FromDBServer);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(251, 200);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "From DB";
            // 
            // FromTableNames
            // 
            this.FromTableNames.Location = new System.Drawing.Point(83, 123);
            this.FromTableNames.Multiline = true;
            this.FromTableNames.Name = "FromTableNames";
            this.FromTableNames.Size = new System.Drawing.Size(152, 71);
            this.FromTableNames.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 126);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Table Names";
            // 
            // FromDBName
            // 
            this.FromDBName.Location = new System.Drawing.Point(84, 99);
            this.FromDBName.Name = "FromDBName";
            this.FromDBName.Size = new System.Drawing.Size(151, 20);
            this.FromDBName.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 102);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "DB Name";
            // 
            // FromPwd
            // 
            this.FromPwd.Location = new System.Drawing.Point(84, 75);
            this.FromPwd.Name = "FromPwd";
            this.FromPwd.PasswordChar = '*';
            this.FromPwd.Size = new System.Drawing.Size(151, 20);
            this.FromPwd.TabIndex = 6;
            this.FromPwd.Text = "P@ss1234";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Password";
            // 
            // FromAccount
            // 
            this.FromAccount.Location = new System.Drawing.Point(84, 51);
            this.FromAccount.Name = "FromAccount";
            this.FromAccount.Size = new System.Drawing.Size(151, 20);
            this.FromAccount.TabIndex = 4;
            this.FromAccount.Text = "sa";
            // 
            // FromDBServer
            // 
            this.FromDBServer.Location = new System.Drawing.Point(84, 26);
            this.FromDBServer.Name = "FromDBServer";
            this.FromDBServer.Size = new System.Drawing.Size(151, 20);
            this.FromDBServer.TabIndex = 3;
            this.FromDBServer.Text = "HLIU114SEC0,1801";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ToTableNames);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.ToDBName);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.ToPwd);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.ToAccount);
            this.groupBox2.Controls.Add(this.ToDBServer);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(269, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(264, 199);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "To DB";
            // 
            // ToTableNames
            // 
            this.ToTableNames.Location = new System.Drawing.Point(83, 117);
            this.ToTableNames.Multiline = true;
            this.ToTableNames.Name = "ToTableNames";
            this.ToTableNames.Size = new System.Drawing.Size(163, 76);
            this.ToTableNames.TabIndex = 9;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 114);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 13);
            this.label10.TabIndex = 8;
            this.label10.Text = "Table Names";
            // 
            // ToDBName
            // 
            this.ToDBName.Location = new System.Drawing.Point(84, 95);
            this.ToDBName.Name = "ToDBName";
            this.ToDBName.Size = new System.Drawing.Size(162, 20);
            this.ToDBName.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(22, 98);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "DB Name";
            // 
            // ToPwd
            // 
            this.ToPwd.Location = new System.Drawing.Point(84, 71);
            this.ToPwd.Name = "ToPwd";
            this.ToPwd.PasswordChar = '*';
            this.ToPwd.Size = new System.Drawing.Size(162, 20);
            this.ToPwd.TabIndex = 5;
            this.ToPwd.Text = "P@ss1234";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Password";
            // 
            // ToAccount
            // 
            this.ToAccount.Location = new System.Drawing.Point(84, 47);
            this.ToAccount.Name = "ToAccount";
            this.ToAccount.Size = new System.Drawing.Size(162, 20);
            this.ToAccount.TabIndex = 3;
            this.ToAccount.Text = "sa";
            // 
            // ToDBServer
            // 
            this.ToDBServer.Location = new System.Drawing.Point(83, 22);
            this.ToDBServer.Name = "ToDBServer";
            this.ToDBServer.Size = new System.Drawing.Size(163, 20);
            this.ToDBServer.TabIndex = 2;
            this.ToDBServer.Text = "CNSHASQLUWV040,1801";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Account";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "DB Server";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ResultMsg);
            this.groupBox3.Location = new System.Drawing.Point(13, 219);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(647, 322);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Result";
            // 
            // ResultMsg
            // 
            this.ResultMsg.Location = new System.Drawing.Point(6, 19);
            this.ResultMsg.Name = "ResultMsg";
            this.ResultMsg.Size = new System.Drawing.Size(635, 297);
            this.ResultMsg.TabIndex = 0;
            this.ResultMsg.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(548, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // DataSyncForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 553);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "DataSyncForm";
            this.Text = "DataSyncForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox FromPwd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox FromAccount;
        private System.Windows.Forms.TextBox FromDBServer;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox ToPwd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox ToAccount;
        private System.Windows.Forms.TextBox ToDBServer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox FromTableNames;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox FromDBName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox ToTableNames;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox ToDBName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox ResultMsg;
        private System.Windows.Forms.Button button1;
    }
}