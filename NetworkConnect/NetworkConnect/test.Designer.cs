namespace NetworkConnect
{
    partial class test
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
            this.button1 = new System.Windows.Forms.Button();
            this.btnFind = new System.Windows.Forms.Button();
            this.btnJoin = new System.Windows.Forms.Button();
            this.lstAvail = new System.Windows.Forms.ListBox();
            this.btnHost = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStat = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(17, 178);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 32);
            this.button1.TabIndex = 0;
            this.button1.Text = "SinglePlayer";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(93, 12);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(88, 23);
            this.btnFind.TabIndex = 7;
            this.btnFind.Text = "Find Sessions";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // btnJoin
            // 
            this.btnJoin.Location = new System.Drawing.Point(192, 203);
            this.btnJoin.Name = "btnJoin";
            this.btnJoin.Size = new System.Drawing.Size(75, 23);
            this.btnJoin.TabIndex = 6;
            this.btnJoin.Text = "Join Game";
            this.btnJoin.UseVisualStyleBackColor = true;
            // 
            // lstAvail
            // 
            this.lstAvail.FormattingEnabled = true;
            this.lstAvail.Location = new System.Drawing.Point(27, 50);
            this.lstAvail.Name = "lstAvail";
            this.lstAvail.Size = new System.Drawing.Size(240, 108);
            this.lstAvail.TabIndex = 5;
            // 
            // btnHost
            // 
            this.btnHost.Location = new System.Drawing.Point(17, 229);
            this.btnHost.Name = "btnHost";
            this.btnHost.Size = new System.Drawing.Size(95, 23);
            this.btnHost.TabIndex = 4;
            this.btnHost.Text = "Host Game";
            this.btnHost.UseVisualStyleBackColor = true;
            this.btnHost.Click += new System.EventHandler(this.btnHost_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStat});
            this.statusStrip1.Location = new System.Drawing.Point(0, 260);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(284, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStat
            // 
            this.lblStat.Name = "lblStat";
            this.lblStat.Size = new System.Drawing.Size(95, 17);
            this.lblStat.Text = "Click Host Game";
            // 
            // test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 282);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.btnJoin);
            this.Controls.Add(this.lstAvail);
            this.Controls.Add(this.btnHost);
            this.Controls.Add(this.button1);
            this.Name = "test";
            this.Text = "test";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Button btnJoin;
        private System.Windows.Forms.ListBox lstAvail;
        private System.Windows.Forms.Button btnHost;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStat;
    }
}