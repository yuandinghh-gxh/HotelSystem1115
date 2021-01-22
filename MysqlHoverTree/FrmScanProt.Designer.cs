

namespace MysqlHoverTree {
    partial class FrmScanProt {
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
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.lblScan = new DevComponents.DotNetBar.LabelX();
            this.buttonX3 = new DevComponents.DotNetBar.ButtonX();
            this.button2 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX5 = new DevComponents.DotNetBar.ButtonX();
            this.listBox1 = new DevComponents.DotNetBar.ListBoxAdv();
            this.txtCode = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.SuspendLayout();
            // 
            // cboPortName
            // 
            this.cboPortName.DisplayMember = "Text";
            this.cboPortName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboPortName.FormattingEnabled = true;
            this.cboPortName.ItemHeight = 19;
            this.cboPortName.Location = new System.Drawing.Point(141, 287);
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
            this.labelX1.Location = new System.Drawing.Point(26, 289);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(93, 23);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "串口名称：";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(26, 336);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(93, 23);
            this.labelX2.TabIndex = 3;
            this.labelX2.Text = "波特率：";
            // 
            // cboBaudRate
            // 
            this.cboBaudRate.DisplayMember = "Text";
            this.cboBaudRate.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboBaudRate.FormattingEnabled = true;
            this.cboBaudRate.ItemHeight = 19;
            this.cboBaudRate.Location = new System.Drawing.Point(141, 334);
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
            this.labelX3.Location = new System.Drawing.Point(26, 388);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(93, 23);
            this.labelX3.TabIndex = 5;
            this.labelX3.Text = "数据位数：";
            // 
            // cboDataBit
            // 
            this.cboDataBit.DisplayMember = "Text";
            this.cboDataBit.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboDataBit.FormattingEnabled = true;
            this.cboDataBit.ItemHeight = 19;
            this.cboDataBit.Location = new System.Drawing.Point(141, 386);
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
            this.labelX4.Location = new System.Drawing.Point(26, 442);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(93, 23);
            this.labelX4.TabIndex = 7;
            this.labelX4.Text = "停止位：";
            // 
            // cboParityBit
            // 
            this.cboParityBit.DisplayMember = "Text";
            this.cboParityBit.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboParityBit.FormattingEnabled = true;
            this.cboParityBit.ItemHeight = 19;
            this.cboParityBit.Location = new System.Drawing.Point(141, 440);
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
            this.labelX5.Location = new System.Drawing.Point(26, 485);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(93, 23);
            this.labelX5.TabIndex = 9;
            this.labelX5.Text = "奇偶校验：";
            // 
            // cboStopBit
            // 
            this.cboStopBit.DisplayMember = "Text";
            this.cboStopBit.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboStopBit.FormattingEnabled = true;
            this.cboStopBit.ItemHeight = 19;
            this.cboStopBit.Location = new System.Drawing.Point(141, 483);
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
            this.button1.Location = new System.Drawing.Point(12, 205);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 33);
            this.button1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.button1.TabIndex = 10;
            this.button1.Text = "开始扫描";
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, ((byte)(134)));
            this.buttonX2.Location = new System.Drawing.Point(222, 205);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(123, 33);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 11;
            this.buttonX2.Text = "开始扫描";
            // 
            // lblScan
            // 
            this.lblScan.BackColor = System.Drawing.SystemColors.AppWorkspace;
            // 
            // 
            // 
            this.lblScan.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblScan.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblScan.ForeColor = System.Drawing.Color.Red;
            this.lblScan.Location = new System.Drawing.Point(410, 205);
            this.lblScan.Name = "lblScan";
            this.lblScan.Size = new System.Drawing.Size(142, 33);
            this.lblScan.TabIndex = 12;
            this.lblScan.Text = "未开启采集程序";
            // 
            // buttonX3
            // 
            this.buttonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX3.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX3.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, ((byte)(134)));
            this.buttonX3.Location = new System.Drawing.Point(602, 205);
            this.buttonX3.Name = "buttonX3";
            this.buttonX3.Size = new System.Drawing.Size(135, 33);
            this.buttonX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX3.TabIndex = 13;
            this.buttonX3.Text = "启动手动输入";
            // 
            // button2
            // 
            this.button2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.button2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.button2.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(794, 205);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(114, 33);
            this.button2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.button2.TabIndex = 14;
            this.button2.Text = "清空列表";
            // 
            // buttonX5
            // 
            this.buttonX5.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX5.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX5.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, ((byte)(134)));
            this.buttonX5.Location = new System.Drawing.Point(947, 205);
            this.buttonX5.Name = "buttonX5";
            this.buttonX5.Size = new System.Drawing.Size(114, 33);
            this.buttonX5.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX5.TabIndex = 15;
            this.buttonX5.Text = "添加";
            // 
            // listBox1
            // 
            this.listBox1.AutoScroll = true;
            // 
            // 
            // 
            this.listBox1.BackgroundStyle.Class = "ListBoxAdv";
            this.listBox1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.listBox1.CheckStateMember = null;
            this.listBox1.ContainerControlProcessDialogKey = true;
            this.listBox1.DragDropSupport = true;
            this.listBox1.Location = new System.Drawing.Point(26, 13);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(1035, 167);
            this.listBox1.TabIndex = 16;
            this.listBox1.ItemClick += new System.EventHandler(this.listBox1_ItemClick);
            // 
            // txtCode
            // 
            this.txtCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtCode.Border.Class = "TextBoxBorder";
            this.txtCode.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCode.Location = new System.Drawing.Point(322, 286);
            this.txtCode.Multiline = true;
            this.txtCode.Name = "txtCode";
            this.txtCode.PreventEnterBeep = true;
            this.txtCode.Size = new System.Drawing.Size(716, 257);
            this.txtCode.TabIndex = 17;
            // 
            // FrmScanProt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 599);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.buttonX5);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonX3);
            this.Controls.Add(this.lblScan);
            this.Controls.Add(this.buttonX2);
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
            this.Name = "FrmScanProt";
            this.Text = "FrmScanProt";
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
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private DevComponents.DotNetBar.LabelX lblScan;
        private DevComponents.DotNetBar.ButtonX buttonX3;
        private DevComponents.DotNetBar.ButtonX button2;
        private DevComponents.DotNetBar.ButtonX buttonX5;
        private DevComponents.DotNetBar.ListBoxAdv listBox1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCode;
    }
}