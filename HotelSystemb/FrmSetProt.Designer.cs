namespace MysqlHoverTree {
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
            this.cboPortName = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.cboBaudRate = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.cboDataBit = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.cboParityBit = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.cboStopBit = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.button1 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.lblScan = new DevComponents.DotNetBar.LabelX();
            this.txtCode = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.ReadData = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // cboPortName
            // 
            this.cboPortName.DisplayMember = "Text";
            this.cboPortName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboPortName.FormattingEnabled = true;
            this.cboPortName.ItemHeight = 19;
            this.cboPortName.Location = new System.Drawing.Point(178, 206);
            this.cboPortName.Name = "cboPortName";
            this.cboPortName.Size = new System.Drawing.Size(121, 25);
            this.cboPortName.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboPortName.TabIndex = 0;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, ((byte)(134)));
            this.labelX1.Location = new System.Drawing.Point(12, 203);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(128, 23);
            this.labelX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "串口名称：";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, ((byte)(134)));
            this.labelX2.Location = new System.Drawing.Point(12, 252);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(138, 23);
            this.labelX2.TabIndex = 3;
            this.labelX2.Text = "波特率：";
            // 
            // cboBaudRate
            // 
            this.cboBaudRate.DisplayMember = "Text";
            this.cboBaudRate.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboBaudRate.FormattingEnabled = true;
            this.cboBaudRate.ItemHeight = 19;
            this.cboBaudRate.Location = new System.Drawing.Point(178, 252);
            this.cboBaudRate.Name = "cboBaudRate";
            this.cboBaudRate.Size = new System.Drawing.Size(121, 25);
            this.cboBaudRate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboBaudRate.TabIndex = 2;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, ((byte)(134)));
            this.labelX3.Location = new System.Drawing.Point(12, 304);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(138, 23);
            this.labelX3.TabIndex = 5;
            this.labelX3.Text = "数据位数：";
            // 
            // cboDataBit
            // 
            this.cboDataBit.DisplayMember = "Text";
            this.cboDataBit.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboDataBit.FormattingEnabled = true;
            this.cboDataBit.ItemHeight = 19;
            this.cboDataBit.Location = new System.Drawing.Point(178, 304);
            this.cboDataBit.Name = "cboDataBit";
            this.cboDataBit.Size = new System.Drawing.Size(121, 25);
            this.cboDataBit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboDataBit.TabIndex = 4;
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, ((byte)(134)));
            this.labelX4.Location = new System.Drawing.Point(12, 356);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(128, 23);
            this.labelX4.TabIndex = 7;
            this.labelX4.Text = "停止位：";
            // 
            // cboParityBit
            // 
            this.cboParityBit.DisplayMember = "Text";
            this.cboParityBit.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboParityBit.FormattingEnabled = true;
            this.cboParityBit.ItemHeight = 19;
            this.cboParityBit.Location = new System.Drawing.Point(178, 358);
            this.cboParityBit.Name = "cboParityBit";
            this.cboParityBit.Size = new System.Drawing.Size(121, 25);
            this.cboParityBit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboParityBit.TabIndex = 6;
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, ((byte)(134)));
            this.labelX5.Location = new System.Drawing.Point(12, 399);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(128, 23);
            this.labelX5.TabIndex = 9;
            this.labelX5.Text = "奇偶校验：";
            // 
            // cboStopBit
            // 
            this.cboStopBit.DisplayMember = "Text";
            this.cboStopBit.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboStopBit.FormattingEnabled = true;
            this.cboStopBit.ItemHeight = 19;
            this.cboStopBit.Location = new System.Drawing.Point(178, 401);
            this.cboStopBit.Name = "cboStopBit";
            this.cboStopBit.Size = new System.Drawing.Size(121, 25);
            this.cboStopBit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboStopBit.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.button1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.button1.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(405, 250);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(199, 36);
            this.button1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.button1.TabIndex = 1;
            this.button1.Text = "选择确认";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, ((byte)(134)));
            this.buttonX1.Location = new System.Drawing.Point(405, 350);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(199, 42);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 2;
            this.buttonX1.Text = "测试接收数据";
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, ((byte)(134)));
            this.buttonX2.Location = new System.Drawing.Point(456, 419);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(148, 35);
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
            this.lblScan.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblScan.ForeColor = System.Drawing.Color.Red;
            this.lblScan.Location = new System.Drawing.Point(405, 206);
            this.lblScan.Name = "lblScan";
            this.lblScan.Size = new System.Drawing.Size(199, 23);
            this.lblScan.TabIndex = 19;
            this.lblScan.Text = "目前没接收到数据";
            // 
            // txtCode
            // 
            // 
            // 
            // 
            this.txtCode.Border.Class = "TextBoxBorder";
            this.txtCode.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCode.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCode.Location = new System.Drawing.Point(22, 13);
            this.txtCode.Multiline = true;
            this.txtCode.Name = "txtCode";
            this.txtCode.PreventEnterBeep = true;
            this.txtCode.Size = new System.Drawing.Size(582, 171);
            this.txtCode.TabIndex = 20;
            // 
            // ReadData
            // 
            // 
            // 
            // 
            this.ReadData.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ReadData.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ReadData.ForeColor = System.Drawing.Color.Green;
            this.ReadData.Location = new System.Drawing.Point(405, 307);
            this.ReadData.Name = "ReadData";
            this.ReadData.Size = new System.Drawing.Size(199, 23);
            this.ReadData.TabIndex = 21;
            // 
            // FrmSetProt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 509);
            this.Controls.Add(this.ReadData);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.lblScan);
            this.Controls.Add(this.buttonX2);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.cboStopBit);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.cboParityBit);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.cboDataBit);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.cboBaudRate);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.cboPortName);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.Name = "FrmSetProt";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置串行口";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ComboBoxEx cboPortName;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboBaudRate;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboDataBit;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboParityBit;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboStopBit;
        private DevComponents.DotNetBar.ButtonX button1;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private DevComponents.DotNetBar.LabelX lblScan;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCode;
        private DevComponents.DotNetBar.LabelX ReadData;
    }
}