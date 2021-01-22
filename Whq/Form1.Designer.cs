namespace Whq {
    partial class wifi {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent( ) {
            this.wifiListOK3 = new System.Windows.Forms.ComboBox();
            this.wifiListOK5 = new System.Windows.Forms.ListBox();
            this.wifiListOK1 = new System.Windows.Forms.ListView();
            this.wifiListOK6 = new System.Windows.Forms.DataGridView();
            this.pictureBoxW = new System.Windows.Forms.PictureBox();
            this.connectWifiOK = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.wifiListOK6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxW)).BeginInit();
            this.SuspendLayout();
            // 
            // wifiListOK3
            // 
            this.wifiListOK3.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.wifiListOK3.FormattingEnabled = true;
            this.wifiListOK3.Location = new System.Drawing.Point(419, 731);
            this.wifiListOK3.Name = "wifiListOK3";
            this.wifiListOK3.Size = new System.Drawing.Size(180, 26);
            this.wifiListOK3.TabIndex = 0;
            // 
            // wifiListOK5
            // 
            this.wifiListOK5.FormattingEnabled = true;
            this.wifiListOK5.ItemHeight = 15;
            this.wifiListOK5.Location = new System.Drawing.Point(32, 697);
            this.wifiListOK5.Name = "wifiListOK5";
            this.wifiListOK5.Size = new System.Drawing.Size(258, 79);
            this.wifiListOK5.TabIndex = 1;
            // 
            // wifiListOK1
            // 
            this.wifiListOK1.Location = new System.Drawing.Point(3, 82);
            this.wifiListOK1.Name = "wifiListOK1";
            this.wifiListOK1.Size = new System.Drawing.Size(362, 515);
            this.wifiListOK1.TabIndex = 2;
            this.wifiListOK1.UseCompatibleStateImageBehavior = false;
            this.wifiListOK1.View = System.Windows.Forms.View.Details;
            // 
            // wifiListOK6
            // 
            this.wifiListOK6.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.wifiListOK6.Location = new System.Drawing.Point(392, 509);
            this.wifiListOK6.Name = "wifiListOK6";
            this.wifiListOK6.RowTemplate.Height = 27;
            this.wifiListOK6.Size = new System.Drawing.Size(240, 150);
            this.wifiListOK6.TabIndex = 3;
            // 
            // pictureBoxW
            // 
            this.pictureBoxW.Location = new System.Drawing.Point(392, 382);
            this.pictureBoxW.Name = "pictureBoxW";
            this.pictureBoxW.Size = new System.Drawing.Size(172, 96);
            this.pictureBoxW.TabIndex = 4;
            this.pictureBoxW.TabStop = false;
            // 
            // connectWifiOK
            // 
            this.connectWifiOK.AutoSize = true;
            this.connectWifiOK.Location = new System.Drawing.Point(32, 28);
            this.connectWifiOK.Name = "connectWifiOK";
            this.connectWifiOK.Size = new System.Drawing.Size(55, 15);
            this.connectWifiOK.TabIndex = 5;
            this.connectWifiOK.Text = "label1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(431, 82);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // wifi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 814);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.connectWifiOK);
            this.Controls.Add(this.pictureBoxW);
            this.Controls.Add(this.wifiListOK6);
            this.Controls.Add(this.wifiListOK1);
            this.Controls.Add(this.wifiListOK5);
            this.Controls.Add(this.wifiListOK3);
            this.Name = "wifi";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.wifiListOK6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxW)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox wifiListOK5;
        private System.Windows.Forms.ListView wifiListOK1;
        public System.Windows.Forms.ComboBox wifiListOK3;
        private System.Windows.Forms.DataGridView wifiListOK6;
        private System.Windows.Forms.PictureBox pictureBoxW;
        private System.Windows.Forms.Label connectWifiOK;
        private System.Windows.Forms.Button button1;
    }
}