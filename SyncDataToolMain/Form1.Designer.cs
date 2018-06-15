namespace SyncDataToolMain
{
    partial class readFileData
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
            this.BWEncrypt = new System.Windows.Forms.Button();
            this.BWDecrypt = new System.Windows.Forms.Button();
            this.resultText = new System.Windows.Forms.RichTextBox();
            this.inputText = new System.Windows.Forms.RichTextBox();
            this.btn_DesLog = new System.Windows.Forms.Button();
            this.btn_desDecrypt = new System.Windows.Forms.Button();
            this.btn_desAndRemark = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BWEncrypt
            // 
            this.BWEncrypt.Location = new System.Drawing.Point(780, 39);
            this.BWEncrypt.Name = "BWEncrypt";
            this.BWEncrypt.Size = new System.Drawing.Size(75, 23);
            this.BWEncrypt.TabIndex = 3;
            this.BWEncrypt.Text = "加密";
            this.BWEncrypt.UseVisualStyleBackColor = true;
            this.BWEncrypt.Click += new System.EventHandler(this.BWEncrypt_Click);
            // 
            // BWDecrypt
            // 
            this.BWDecrypt.Location = new System.Drawing.Point(780, 110);
            this.BWDecrypt.Name = "BWDecrypt";
            this.BWDecrypt.Size = new System.Drawing.Size(75, 23);
            this.BWDecrypt.TabIndex = 4;
            this.BWDecrypt.Text = "解密";
            this.BWDecrypt.UseVisualStyleBackColor = true;
            this.BWDecrypt.Click += new System.EventHandler(this.BWDecrypt_Click);
            // 
            // resultText
            // 
            this.resultText.Location = new System.Drawing.Point(33, 139);
            this.resultText.Name = "resultText";
            this.resultText.Size = new System.Drawing.Size(741, 338);
            this.resultText.TabIndex = 5;
            this.resultText.Text = "";
            // 
            // inputText
            // 
            this.inputText.Location = new System.Drawing.Point(33, 12);
            this.inputText.Name = "inputText";
            this.inputText.Size = new System.Drawing.Size(741, 108);
            this.inputText.TabIndex = 6;
            this.inputText.Text = "";
            // 
            // btn_DesLog
            // 
            this.btn_DesLog.Location = new System.Drawing.Point(781, 170);
            this.btn_DesLog.Name = "btn_DesLog";
            this.btn_DesLog.Size = new System.Drawing.Size(75, 23);
            this.btn_DesLog.TabIndex = 7;
            this.btn_DesLog.Text = "解密日志文件";
            this.btn_DesLog.UseVisualStyleBackColor = true;
            this.btn_DesLog.Click += new System.EventHandler(this.btn_DesLog_Click);
            // 
            // btn_desDecrypt
            // 
            this.btn_desDecrypt.Location = new System.Drawing.Point(781, 223);
            this.btn_desDecrypt.Name = "btn_desDecrypt";
            this.btn_desDecrypt.Size = new System.Drawing.Size(75, 23);
            this.btn_desDecrypt.TabIndex = 8;
            this.btn_desDecrypt.Text = "输入解密";
            this.btn_desDecrypt.UseVisualStyleBackColor = true;
            this.btn_desDecrypt.Click += new System.EventHandler(this.btn_desDecrypt_Click);
            // 
            // btn_desAndRemark
            // 
            this.btn_desAndRemark.Location = new System.Drawing.Point(781, 267);
            this.btn_desAndRemark.Name = "btn_desAndRemark";
            this.btn_desAndRemark.Size = new System.Drawing.Size(75, 23);
            this.btn_desAndRemark.TabIndex = 9;
            this.btn_desAndRemark.Text = "解密航信发票和备注";
            this.btn_desAndRemark.UseVisualStyleBackColor = true;
            this.btn_desAndRemark.Click += new System.EventHandler(this.btn_desAndRemark_Click);
            // 
            // readFileData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(877, 489);
            this.Controls.Add(this.btn_desAndRemark);
            this.Controls.Add(this.btn_desDecrypt);
            this.Controls.Add(this.btn_DesLog);
            this.Controls.Add(this.inputText);
            this.Controls.Add(this.resultText);
            this.Controls.Add(this.BWDecrypt);
            this.Controls.Add(this.BWEncrypt);
            this.Name = "readFileData";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BWEncrypt;
        private System.Windows.Forms.Button BWDecrypt;
        private System.Windows.Forms.RichTextBox resultText;
        private System.Windows.Forms.RichTextBox inputText;
        private System.Windows.Forms.Button btn_DesLog;
        private System.Windows.Forms.Button btn_desDecrypt;
        private System.Windows.Forms.Button btn_desAndRemark;
    }
}

