namespace FtpScan
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtSearchKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnScan = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtDomianListFile = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDomainCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSearchKey
            // 
            this.txtSearchKey.BackColor = System.Drawing.SystemColors.MenuText;
            this.txtSearchKey.ForeColor = System.Drawing.Color.Gold;
            this.txtSearchKey.Location = new System.Drawing.Point(101, 22);
            this.txtSearchKey.Name = "txtSearchKey";
            this.txtSearchKey.Size = new System.Drawing.Size(430, 21);
            this.txtSearchKey.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "关键词";
            // 
            // btnScan
            // 
            this.btnScan.BackColor = System.Drawing.SystemColors.MenuText;
            this.btnScan.ForeColor = System.Drawing.Color.Gold;
            this.btnScan.Location = new System.Drawing.Point(537, 20);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(100, 23);
            this.btnScan.TabIndex = 4;
            this.btnScan.Text = "获取域名列表";
            this.btnScan.UseVisualStyleBackColor = false;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.BackColor = System.Drawing.SystemColors.InfoText;
            this.txtLog.ForeColor = System.Drawing.Color.Gold;
            this.txtLog.Location = new System.Drawing.Point(15, 92);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(887, 381);
            this.txtLog.TabIndex = 6;
            this.txtLog.Text = "";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.SystemColors.MenuText;
            this.button1.ForeColor = System.Drawing.Color.Gold;
            this.button1.Location = new System.Drawing.Point(830, 479);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Scan";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtDomianListFile
            // 
            this.txtDomianListFile.BackColor = System.Drawing.SystemColors.MenuText;
            this.txtDomianListFile.ForeColor = System.Drawing.Color.Gold;
            this.txtDomianListFile.Location = new System.Drawing.Point(99, 63);
            this.txtDomianListFile.Name = "txtDomianListFile";
            this.txtDomianListFile.Size = new System.Drawing.Size(303, 21);
            this.txtDomianListFile.TabIndex = 9;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.MenuText;
            this.button2.ForeColor = System.Drawing.Color.Gold;
            this.button2.Location = new System.Drawing.Point(408, 63);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(29, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "..";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.MenuText;
            this.button3.ForeColor = System.Drawing.Color.Gold;
            this.button3.Location = new System.Drawing.Point(830, 66);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(72, 23);
            this.button3.TabIndex = 11;
            this.button3.Text = "停止滚屏";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown1.BackColor = System.Drawing.SystemColors.MenuText;
            this.numericUpDown1.ForeColor = System.Drawing.Color.Gold;
            this.numericUpDown1.Location = new System.Drawing.Point(758, 482);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            75,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(45, 21);
            this.numericUpDown1.TabIndex = 12;
            this.numericUpDown1.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(711, 487);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "线程数";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "选择列表文件";
            // 
            // lblDomainCount
            // 
            this.lblDomainCount.AutoSize = true;
            this.lblDomainCount.Location = new System.Drawing.Point(461, 68);
            this.lblDomainCount.Name = "lblDomainCount";
            this.lblDomainCount.Size = new System.Drawing.Size(89, 12);
            this.lblDomainCount.TabIndex = 15;
            this.lblDomainCount.Text = "域名数量{0}个:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(917, 512);
            this.Controls.Add(this.lblDomainCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtDomianListFile);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSearchKey);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSearchKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtDomianListFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblDomainCount;
    }
}

