namespace Vcom {
    partial class FrmSetProt {
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
			this.components = new System.ComponentModel.Container();
			this.com = new DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.labelX1 = new DevComponents.DotNetBar.LabelX();
			this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
			this.lblScan = new DevComponents.DotNetBar.LabelX();
			this.textBoxX1 = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.labelX6 = new DevComponents.DotNetBar.LabelX();
			this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
			this.timer3 = new System.Windows.Forms.Timer(this.components);
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// com
			// 
			this.com.DisplayMember = "Text";
			this.com.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.com.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.com.ForeColor = System.Drawing.Color.Black;
			this.com.FormattingEnabled = true;
			this.com.ItemHeight = 24;
			this.com.Location = new System.Drawing.Point(240, 11);
			this.com.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.com.Name = "com";
			this.com.Size = new System.Drawing.Size(116, 30);
			this.com.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.com.TabIndex = 0;
			// 
			// labelX1
			// 
			// 
			// 
			// 
			this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX1.Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, ((byte)(134)));
			this.labelX1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.labelX1.Location = new System.Drawing.Point(12, 11);
			this.labelX1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(182, 34);
			this.labelX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
			this.labelX1.TabIndex = 1;
			this.labelX1.Text = "选择串口：";
			// 
			// buttonX2
			// 
			this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonX2.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, ((byte)(134)));
			this.buttonX2.Location = new System.Drawing.Point(463, 446);
			this.buttonX2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.buttonX2.Name = "buttonX2";
			this.buttonX2.Size = new System.Drawing.Size(160, 52);
			this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonX2.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.buttonX2.TabIndex = 3;
			this.buttonX2.Text = "退出设置";
			this.buttonX2.TextColor = System.Drawing.Color.Red;
			this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
			// 
			// lblScan
			// 
			// 
			// 
			// 
			this.lblScan.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.lblScan.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblScan.ForeColor = System.Drawing.Color.Red;
			this.lblScan.Location = new System.Drawing.Point(3, 114);
			this.lblScan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.lblScan.Name = "lblScan";
			this.lblScan.Size = new System.Drawing.Size(620, 49);
			this.lblScan.TabIndex = 19;
			this.lblScan.Text = "无法连接电压测试仪！";
			// 
			// textBoxX1
			// 
			this.textBoxX1.BackColor = System.Drawing.Color.White;
			// 
			// 
			// 
			this.textBoxX1.Border.Class = "TextBoxBorder";
			this.textBoxX1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.textBoxX1.DisabledBackColor = System.Drawing.Color.White;
			this.textBoxX1.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.textBoxX1.ForeColor = System.Drawing.Color.Black;
			this.textBoxX1.Location = new System.Drawing.Point(412, 194);
			this.textBoxX1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.textBoxX1.Multiline = true;
			this.textBoxX1.Name = "textBoxX1";
			this.textBoxX1.PreventEnterBeep = true;
			this.textBoxX1.Size = new System.Drawing.Size(211, 34);
			this.textBoxX1.TabIndex = 21;
			// 
			// labelX6
			// 
			// 
			// 
			// 
			this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX6.Font = new System.Drawing.Font("宋体", 21F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, ((byte)(134)));
			this.labelX6.FontBold = true;
			this.labelX6.ForeColor = System.Drawing.Color.DarkGreen;
			this.labelX6.Location = new System.Drawing.Point(3, 66);
			this.labelX6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.labelX6.Name = "labelX6";
			this.labelX6.Size = new System.Drawing.Size(620, 44);
			this.labelX6.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
			this.labelX6.TabIndex = 22;
			this.labelX6.Text = "开始连接电压测试仪";
			// 
			// buttonX1
			// 
			this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonX1.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, ((byte)(134)));
			this.buttonX1.Location = new System.Drawing.Point(463, 11);
			this.buttonX1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.buttonX1.Name = "buttonX1";
			this.buttonX1.Size = new System.Drawing.Size(160, 44);
			this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeMobile2014;
			this.buttonX1.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.buttonX1.TabIndex = 23;
			this.buttonX1.Text = "确认串行口";
			this.buttonX1.TextColor = System.Drawing.Color.DarkCyan;
			this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click_1);
			// 
			// timer3
			// 
			this.timer3.Enabled = true;
			this.timer3.Interval = 300;
			this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point(3, 371);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(353, 126);
			this.richTextBox1.TabIndex = 26;
			this.richTextBox1.Text = "";
			// 
			// FrmSetProt
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(635, 509);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.buttonX1);
			this.Controls.Add(this.labelX6);
			this.Controls.Add(this.textBoxX1);
			this.Controls.Add(this.lblScan);
			this.Controls.Add(this.buttonX2);
			this.Controls.Add(this.labelX1);
			this.Controls.Add(this.com);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Name = "FrmSetProt";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "设置串行口";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ComboBoxEx com;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private DevComponents.DotNetBar.LabelX lblScan;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX1;
        private DevComponents.DotNetBar.LabelX labelX6;
		private DevComponents.DotNetBar.ButtonX buttonX1;
		private System.Windows.Forms.Timer timer3;
		private System.Windows.Forms.RichTextBox richTextBox1;
	}
}