using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace Whq {
    partial class Form1 {
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
            this.sideNav1 = new DevComponents.DotNetBar.Controls.SideNav();
            this.sideNavPanel2 = new DevComponents.DotNetBar.Controls.SideNavPanel();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.buttonX8 = new DevComponents.DotNetBar.ButtonX();
            this.textBoxX9 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX12 = new DevComponents.DotNetBar.LabelX();
            this.line2 = new DevComponents.DotNetBar.Controls.Line();
            this.textBoxX8 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX11 = new DevComponents.DotNetBar.LabelX();
            this.textBoxX7 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.buttonX7 = new DevComponents.DotNetBar.ButtonX();
            this.templist = new System.Windows.Forms.ListView();
            this.机柜编号 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.当前温度 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.当前状态 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.累计运行时间 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.超温次数 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.故障次数 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textBoxX6 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.textBoxX5 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.textBoxX4 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.sideNavPanel4 = new DevComponents.DotNetBar.Controls.SideNavPanel();
            this.labelX13 = new DevComponents.DotNetBar.LabelX();
            this.comboBoxEx1 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.sideNavPanel1 = new DevComponents.DotNetBar.Controls.SideNavPanel();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.buttonX5 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX4 = new DevComponents.DotNetBar.ButtonX();
            this.textBoxX3 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.textBoxX2 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX9 = new DevComponents.DotNetBar.LabelX();
            this.textBoxX1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.buttonX3 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.userlistView = new System.Windows.Forms.ListView();
            this.真实姓名 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.登陆账号 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.登陆密码 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.权限 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sideNavItem1 = new DevComponents.DotNetBar.Controls.SideNavItem();
            this.separator1 = new DevComponents.DotNetBar.Separator();
            this.sideNavItem2 = new DevComponents.DotNetBar.Controls.SideNavItem();
            this.sideNavItem3 = new DevComponents.DotNetBar.Controls.SideNavItem();
            this.sideNavItem4 = new DevComponents.DotNetBar.Controls.SideNavItem();
            this.sideNavItem5 = new DevComponents.DotNetBar.Controls.SideNavItem();
            this.buttonX6 = new DevComponents.DotNetBar.ButtonX();
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.line1 = new DevComponents.DotNetBar.Controls.Line();
            this.sideNav1.SuspendLayout();
            this.sideNavPanel2.SuspendLayout();
            this.sideNavPanel4.SuspendLayout();
            this.sideNavPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sideNav1
            // 
            this.sideNav1.Controls.Add(this.sideNavPanel4);
            this.sideNav1.Controls.Add(this.sideNavPanel2);
            this.sideNav1.Controls.Add(this.sideNavPanel1);
            this.sideNav1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sideNav1.EnableClose = false;
            this.sideNav1.EnableMaximize = false;
            this.sideNav1.EnableSplitter = false;
            this.sideNav1.ForeColor = System.Drawing.Color.Black;
            this.sideNav1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.sideNavItem1,
            this.separator1,
            this.sideNavItem2,
            this.sideNavItem3,
            this.sideNavItem4,
            this.sideNavItem5});
            this.sideNav1.Location = new System.Drawing.Point(0, 0);
            this.sideNav1.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.sideNav1.Name = "sideNav1";
            this.sideNav1.Padding = new System.Windows.Forms.Padding(1);
            this.sideNav1.Size = new System.Drawing.Size(759, 471);
            this.sideNav1.TabIndex = 0;
            this.sideNav1.Text = "sideNav1";
            // 
            // sideNavPanel2
            // 
            this.sideNavPanel2.BackColor = System.Drawing.Color.DarkGray;
            this.sideNavPanel2.Controls.Add(this.labelX2);
            this.sideNavPanel2.Controls.Add(this.buttonX8);
            this.sideNavPanel2.Controls.Add(this.textBoxX9);
            this.sideNavPanel2.Controls.Add(this.labelX12);
            this.sideNavPanel2.Controls.Add(this.line2);
            this.sideNavPanel2.Controls.Add(this.textBoxX8);
            this.sideNavPanel2.Controls.Add(this.labelX11);
            this.sideNavPanel2.Controls.Add(this.textBoxX7);
            this.sideNavPanel2.Controls.Add(this.labelX8);
            this.sideNavPanel2.Controls.Add(this.buttonX7);
            this.sideNavPanel2.Controls.Add(this.templist);
            this.sideNavPanel2.Controls.Add(this.textBoxX6);
            this.sideNavPanel2.Controls.Add(this.textBoxX5);
            this.sideNavPanel2.Controls.Add(this.labelX5);
            this.sideNavPanel2.Controls.Add(this.textBoxX4);
            this.sideNavPanel2.Controls.Add(this.labelX6);
            this.sideNavPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sideNavPanel2.Location = new System.Drawing.Point(111, 32);
            this.sideNavPanel2.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.sideNavPanel2.Name = "sideNavPanel2";
            this.sideNavPanel2.Size = new System.Drawing.Size(647, 438);
            this.sideNavPanel2.TabIndex = 6;
            this.sideNavPanel2.Visible = false;
            this.sideNavPanel2.Paint += new System.Windows.Forms.PaintEventHandler(this.sideNavPanel2_Paint);
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.labelX2.Location = new System.Drawing.Point(6, 9);
            this.labelX2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(132, 23);
            this.labelX2.TabIndex = 49;
            this.labelX2.Text = "监控节点数：";
            // 
            // buttonX8
            // 
            this.buttonX8.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX8.BackColor = System.Drawing.Color.Silver;
            this.buttonX8.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground;
            this.buttonX8.Location = new System.Drawing.Point(505, 100);
            this.buttonX8.Name = "buttonX8";
            this.buttonX8.Size = new System.Drawing.Size(120, 27);
            this.buttonX8.Symbol = "";
            this.buttonX8.SymbolColor = System.Drawing.Color.Red;
            this.buttonX8.TabIndex = 47;
            this.buttonX8.Text = "确认";
            this.buttonX8.TextColor = System.Drawing.Color.Green;
            this.buttonX8.Click += new System.EventHandler(this.buttonX8_Click);
            // 
            // textBoxX9
            // 
            this.textBoxX9.AcceptsReturn = true;
            this.textBoxX9.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.textBoxX9.Border.Class = "TextBoxBorder";
            this.textBoxX9.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX9.DisabledBackColor = System.Drawing.Color.White;
            this.textBoxX9.ForeColor = System.Drawing.Color.Black;
            this.textBoxX9.Location = new System.Drawing.Point(404, 102);
            this.textBoxX9.MaxLength = 5;
            this.textBoxX9.Name = "textBoxX9";
            this.textBoxX9.PreventEnterBeep = true;
            this.textBoxX9.Size = new System.Drawing.Size(59, 21);
            this.textBoxX9.TabIndex = 46;
            // 
            // labelX12
            // 
            // 
            // 
            // 
            this.labelX12.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX12.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX12.ForeColor = System.Drawing.Color.Maroon;
            this.labelX12.Location = new System.Drawing.Point(253, 102);
            this.labelX12.Name = "labelX12";
            this.labelX12.Size = new System.Drawing.Size(159, 23);
            this.labelX12.TabIndex = 45;
            this.labelX12.Text = "温高于报警：";
            // 
            // line2
            // 
            this.line2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.line2.AutoSize = true;
            this.line2.EndLineCapSize = new System.Drawing.Size(0, 0);
            this.line2.ForeColor = System.Drawing.Color.Red;
            this.line2.Location = new System.Drawing.Point(1, 80);
            this.line2.Margin = new System.Windows.Forms.Padding(0);
            this.line2.Name = "line2";
            this.line2.Size = new System.Drawing.Size(2369, 2);
            this.line2.TabIndex = 44;
            this.line2.Text = "line2";
            this.line2.Thickness = 2;
            // 
            // textBoxX8
            // 
            this.textBoxX8.AcceptsReturn = true;
            this.textBoxX8.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.textBoxX8.Border.Class = "TextBoxBorder";
            this.textBoxX8.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX8.DisabledBackColor = System.Drawing.Color.White;
            this.textBoxX8.ForeColor = System.Drawing.Color.Black;
            this.textBoxX8.Location = new System.Drawing.Point(172, 100);
            this.textBoxX8.MaxLength = 5;
            this.textBoxX8.Name = "textBoxX8";
            this.textBoxX8.PreventEnterBeep = true;
            this.textBoxX8.Size = new System.Drawing.Size(59, 21);
            this.textBoxX8.TabIndex = 43;
            // 
            // labelX11
            // 
            // 
            // 
            // 
            this.labelX11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX11.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelX11.Location = new System.Drawing.Point(6, 102);
            this.labelX11.Name = "labelX11";
            this.labelX11.Size = new System.Drawing.Size(183, 23);
            this.labelX11.TabIndex = 42;
            this.labelX11.Text = "温度低于报警：";
            // 
            // textBoxX7
            // 
            this.textBoxX7.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.textBoxX7.Border.Class = "TextBoxBorder";
            this.textBoxX7.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX7.DisabledBackColor = System.Drawing.Color.White;
            this.textBoxX7.ForeColor = System.Drawing.Color.Black;
            this.textBoxX7.Location = new System.Drawing.Point(153, 42);
            this.textBoxX7.Name = "textBoxX7";
            this.textBoxX7.PreventEnterBeep = true;
            this.textBoxX7.Size = new System.Drawing.Size(59, 21);
            this.textBoxX7.TabIndex = 41;
            this.textBoxX7.Text = "1";
            // 
            // labelX8
            // 
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.labelX8.Location = new System.Drawing.Point(18, 45);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(120, 23);
            this.labelX8.TabIndex = 40;
            this.labelX8.Text = "编号起始：";
            // 
            // buttonX7
            // 
            this.buttonX7.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX7.BackColor = System.Drawing.Color.Silver;
            this.buttonX7.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground;
            this.buttonX7.Location = new System.Drawing.Point(505, 45);
            this.buttonX7.Name = "buttonX7";
            this.buttonX7.Size = new System.Drawing.Size(120, 25);
            this.buttonX7.Symbol = "";
            this.buttonX7.SymbolColor = System.Drawing.Color.Red;
            this.buttonX7.TabIndex = 39;
            this.buttonX7.Text = "确认修改";
            this.buttonX7.TextColor = System.Drawing.Color.Green;
            this.buttonX7.Click += new System.EventHandler(this.buttonX7_Click);
            // 
            // templist
            // 
            this.templist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.templist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.机柜编号,
            this.当前温度,
            this.当前状态,
            this.累计运行时间,
            this.超温次数,
            this.故障次数});
            this.templist.HideSelection = false;
            this.templist.Location = new System.Drawing.Point(1, 146);
            this.templist.Name = "templist";
            this.templist.Size = new System.Drawing.Size(646, 293);
            this.templist.TabIndex = 38;
            this.templist.UseCompatibleStateImageBehavior = false;
            this.templist.View = System.Windows.Forms.View.Details;
            // 
            // 机柜编号
            // 
            this.机柜编号.Text = "机柜编号";
            this.机柜编号.Width = 137;
            // 
            // 当前温度
            // 
            this.当前温度.Text = "当前温度";
            this.当前温度.Width = 100;
            // 
            // 当前状态
            // 
            this.当前状态.Text = "当前状态";
            this.当前状态.Width = 97;
            // 
            // 累计运行时间
            // 
            this.累计运行时间.Text = "运行时间";
            this.累计运行时间.Width = 103;
            // 
            // 超温次数
            // 
            this.超温次数.Text = "超温次数";
            this.超温次数.Width = 84;
            // 
            // 故障次数
            // 
            this.故障次数.Text = "故障次数";
            this.故障次数.Width = 90;
            // 
            // textBoxX6
            // 
            this.textBoxX6.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.textBoxX6.Border.Class = "TextBoxBorder";
            this.textBoxX6.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX6.DisabledBackColor = System.Drawing.Color.White;
            this.textBoxX6.ForeColor = System.Drawing.Color.Black;
            this.textBoxX6.Location = new System.Drawing.Point(548, 8);
            this.textBoxX6.Name = "textBoxX6";
            this.textBoxX6.PreventEnterBeep = true;
            this.textBoxX6.Size = new System.Drawing.Size(77, 21);
            this.textBoxX6.TabIndex = 37;
            // 
            // textBoxX5
            // 
            this.textBoxX5.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.textBoxX5.Border.Class = "TextBoxBorder";
            this.textBoxX5.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX5.DisabledBackColor = System.Drawing.Color.White;
            this.textBoxX5.ForeColor = System.Drawing.Color.Black;
            this.textBoxX5.Location = new System.Drawing.Point(357, 8);
            this.textBoxX5.Name = "textBoxX5";
            this.textBoxX5.PreventEnterBeep = true;
            this.textBoxX5.Size = new System.Drawing.Size(84, 21);
            this.textBoxX5.TabIndex = 35;
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.labelX5.Location = new System.Drawing.Point(217, 9);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(134, 23);
            this.labelX5.TabIndex = 34;
            this.labelX5.Text = "编号前字符：";
            // 
            // textBoxX4
            // 
            this.textBoxX4.AcceptsReturn = true;
            this.textBoxX4.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.textBoxX4.Border.Class = "TextBoxBorder";
            this.textBoxX4.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX4.DisabledBackColor = System.Drawing.Color.White;
            this.textBoxX4.ForeColor = System.Drawing.Color.Black;
            this.textBoxX4.Location = new System.Drawing.Point(141, 10);
            this.textBoxX4.MaxLength = 5;
            this.textBoxX4.Name = "textBoxX4";
            this.textBoxX4.PreventEnterBeep = true;
            this.textBoxX4.Size = new System.Drawing.Size(59, 21);
            this.textBoxX4.TabIndex = 33;
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.labelX6.Location = new System.Drawing.Point(451, 8);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(91, 23);
            this.labelX6.TabIndex = 36;
            this.labelX6.Text = "后字符：";
            // 
            // sideNavPanel4
            // 
            this.sideNavPanel4.AutoScroll = true;
            this.sideNavPanel4.Controls.Add(this.labelX13);
            this.sideNavPanel4.Controls.Add(this.comboBoxEx1);
            this.sideNavPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sideNavPanel4.Location = new System.Drawing.Point(111, 32);
            this.sideNavPanel4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sideNavPanel4.Name = "sideNavPanel4";
            this.sideNavPanel4.Size = new System.Drawing.Size(647, 438);
            this.sideNavPanel4.TabIndex = 14;
            // 
            // labelX13
            // 
            // 
            // 
            // 
            this.labelX13.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX13.Location = new System.Drawing.Point(30, 12);
            this.labelX13.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelX13.Name = "labelX13";
            this.labelX13.Size = new System.Drawing.Size(75, 22);
            this.labelX13.TabIndex = 3;
            this.labelX13.Text = "App styling:";
            // 
            // comboBoxEx1
            // 
            this.comboBoxEx1.DisplayMember = "Text";
            this.comboBoxEx1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxEx1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEx1.ForeColor = System.Drawing.Color.Black;
            this.comboBoxEx1.FormattingEnabled = true;
            this.comboBoxEx1.ItemHeight = 14;
            this.comboBoxEx1.Location = new System.Drawing.Point(196, 12);
            this.comboBoxEx1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxEx1.Name = "comboBoxEx1";
            this.comboBoxEx1.Size = new System.Drawing.Size(155, 20);
            this.comboBoxEx1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxEx1.TabIndex = 2;
            this.comboBoxEx1.SelectedIndexChanged += new System.EventHandler(this.comboBoxEx1_SelectedIndexChanged);
            // 
            // sideNavPanel1
            // 
            this.sideNavPanel1.AutoScroll = true;
            this.sideNavPanel1.BackColor = System.Drawing.Color.DarkGray;
            this.sideNavPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sideNavPanel1.Controls.Add(this.labelX4);
            this.sideNavPanel1.Controls.Add(this.comboBox1);
            this.sideNavPanel1.Controls.Add(this.labelX3);
            this.sideNavPanel1.Controls.Add(this.buttonX5);
            this.sideNavPanel1.Controls.Add(this.buttonX4);
            this.sideNavPanel1.Controls.Add(this.textBoxX3);
            this.sideNavPanel1.Controls.Add(this.labelX10);
            this.sideNavPanel1.Controls.Add(this.textBoxX2);
            this.sideNavPanel1.Controls.Add(this.labelX9);
            this.sideNavPanel1.Controls.Add(this.textBoxX1);
            this.sideNavPanel1.Controls.Add(this.labelX7);
            this.sideNavPanel1.Controls.Add(this.buttonX3);
            this.sideNavPanel1.Controls.Add(this.buttonX2);
            this.sideNavPanel1.Controls.Add(this.buttonX1);
            this.sideNavPanel1.Controls.Add(this.labelX1);
            this.sideNavPanel1.Controls.Add(this.userlistView);
            this.sideNavPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sideNavPanel1.ForeColor = System.Drawing.Color.Green;
            this.sideNavPanel1.Location = new System.Drawing.Point(111, 32);
            this.sideNavPanel1.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.sideNavPanel1.Name = "sideNavPanel1";
            this.sideNavPanel1.Size = new System.Drawing.Size(647, 438);
            this.sideNavPanel1.TabIndex = 2;
            this.sideNavPanel1.Visible = false;
            // 
            // labelX4
            // 
            this.labelX4.AutoSize = true;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX4.ForeColor = System.Drawing.Color.Green;
            this.labelX4.Location = new System.Drawing.Point(241, 321);
            this.labelX4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(111, 21);
            this.labelX4.TabIndex = 41;
            this.labelX4.Text = "选择账号权限：";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "普通用户",
            "超级管理员"});
            this.comboBox1.Location = new System.Drawing.Point(370, 327);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(125, 20);
            this.comboBox1.TabIndex = 40;
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX3.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelX3.Location = new System.Drawing.Point(446, 8);
            this.labelX3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(146, 29);
            this.labelX3.TabIndex = 39;
            this.labelX3.Text = "您是超级管理员";
            // 
            // buttonX5
            // 
            this.buttonX5.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX5.BackColor = System.Drawing.Color.Silver;
            this.buttonX5.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground;
            this.buttonX5.Location = new System.Drawing.Point(446, 381);
            this.buttonX5.Name = "buttonX5";
            this.buttonX5.Size = new System.Drawing.Size(178, 36);
            this.buttonX5.Symbol = "";
            this.buttonX5.SymbolColor = System.Drawing.Color.Red;
            this.buttonX5.TabIndex = 38;
            this.buttonX5.Text = "退出";
            this.buttonX5.TextColor = System.Drawing.Color.Green;
            this.buttonX5.Click += new System.EventHandler(this.buttonX5_Click);
            // 
            // buttonX4
            // 
            this.buttonX4.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX4.BackColor = System.Drawing.Color.Silver;
            this.buttonX4.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground;
            this.buttonX4.Location = new System.Drawing.Point(110, 381);
            this.buttonX4.Name = "buttonX4";
            this.buttonX4.Size = new System.Drawing.Size(179, 36);
            this.buttonX4.Symbol = "";
            this.buttonX4.SymbolColor = System.Drawing.Color.Red;
            this.buttonX4.TabIndex = 37;
            this.buttonX4.Text = "确认添加";
            this.buttonX4.TextColor = System.Drawing.Color.Green;
            this.buttonX4.Click += new System.EventHandler(this.buttonX4_Click);
            // 
            // textBoxX3
            // 
            this.textBoxX3.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.textBoxX3.Border.Class = "TextBoxBorder";
            this.textBoxX3.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX3.DisabledBackColor = System.Drawing.Color.White;
            this.textBoxX3.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxX3.ForeColor = System.Drawing.Color.Black;
            this.textBoxX3.Location = new System.Drawing.Point(110, 323);
            this.textBoxX3.Name = "textBoxX3";
            this.textBoxX3.PreventEnterBeep = true;
            this.textBoxX3.Size = new System.Drawing.Size(125, 24);
            this.textBoxX3.TabIndex = 36;
            // 
            // labelX10
            // 
            // 
            // 
            // 
            this.labelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX10.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX10.ForeColor = System.Drawing.Color.Green;
            this.labelX10.Location = new System.Drawing.Point(13, 323);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(91, 30);
            this.labelX10.TabIndex = 35;
            this.labelX10.Text = "设置密码：";
            // 
            // textBoxX2
            // 
            this.textBoxX2.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.textBoxX2.Border.Class = "TextBoxBorder";
            this.textBoxX2.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX2.DisabledBackColor = System.Drawing.Color.White;
            this.textBoxX2.ForeColor = System.Drawing.Color.Black;
            this.textBoxX2.Location = new System.Drawing.Point(370, 277);
            this.textBoxX2.Name = "textBoxX2";
            this.textBoxX2.PreventEnterBeep = true;
            this.textBoxX2.Size = new System.Drawing.Size(125, 21);
            this.textBoxX2.TabIndex = 34;
            // 
            // labelX9
            // 
            // 
            // 
            // 
            this.labelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX9.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX9.ForeColor = System.Drawing.Color.Green;
            this.labelX9.Location = new System.Drawing.Point(241, 277);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new System.Drawing.Size(123, 30);
            this.labelX9.TabIndex = 33;
            this.labelX9.Text = "添加登陆账号：";
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
            this.textBoxX1.ForeColor = System.Drawing.Color.Black;
            this.textBoxX1.Location = new System.Drawing.Point(110, 277);
            this.textBoxX1.Name = "textBoxX1";
            this.textBoxX1.PreventEnterBeep = true;
            this.textBoxX1.Size = new System.Drawing.Size(125, 21);
            this.textBoxX1.TabIndex = 32;
            // 
            // labelX7
            // 
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX7.ForeColor = System.Drawing.Color.Green;
            this.labelX7.Location = new System.Drawing.Point(13, 277);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(91, 30);
            this.labelX7.TabIndex = 31;
            this.labelX7.Text = "添加姓名：";
            // 
            // buttonX3
            // 
            this.buttonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX3.BackColor = System.Drawing.Color.Silver;
            this.buttonX3.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground;
            this.buttonX3.Location = new System.Drawing.Point(446, 197);
            this.buttonX3.Name = "buttonX3";
            this.buttonX3.Size = new System.Drawing.Size(157, 36);
            this.buttonX3.Symbol = "";
            this.buttonX3.SymbolColor = System.Drawing.Color.Red;
            this.buttonX3.TabIndex = 13;
            this.buttonX3.Text = "删除管理员";
            this.buttonX3.TextColor = System.Drawing.Color.Green;
            this.buttonX3.Click += new System.EventHandler(this.buttonX3_Click);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.BackColor = System.Drawing.Color.Silver;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground;
            this.buttonX2.Location = new System.Drawing.Point(446, 145);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(157, 36);
            this.buttonX2.Symbol = "";
            this.buttonX2.SymbolColor = System.Drawing.Color.Red;
            this.buttonX2.TabIndex = 12;
            this.buttonX2.Text = "修改密码";
            this.buttonX2.TextColor = System.Drawing.Color.Green;
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.BackColor = System.Drawing.Color.DarkGray;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground;
            this.buttonX1.Location = new System.Drawing.Point(446, 88);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(157, 36);
            this.buttonX1.Symbol = "";
            this.buttonX1.SymbolColor = System.Drawing.Color.Red;
            this.buttonX1.TabIndex = 11;
            this.buttonX1.Text = "添加管理员";
            this.buttonX1.TextColor = System.Drawing.Color.Green;
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("楷体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX1.ForeColor = System.Drawing.Color.Green;
            this.labelX1.Location = new System.Drawing.Point(68, 7);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(240, 37);
            this.labelX1.TabIndex = 10;
            this.labelX1.Text = "已经注册的管理员";
            // 
            // userlistView
            // 
            this.userlistView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.真实姓名,
            this.登陆账号,
            this.登陆密码,
            this.权限});
            this.userlistView.HideSelection = false;
            this.userlistView.Location = new System.Drawing.Point(-1, 50);
            this.userlistView.Name = "userlistView";
            this.userlistView.ShowGroups = false;
            this.userlistView.Size = new System.Drawing.Size(403, 211);
            this.userlistView.TabIndex = 9;
            this.userlistView.UseCompatibleStateImageBehavior = false;
            this.userlistView.View = System.Windows.Forms.View.Details;
            // 
            // 真实姓名
            // 
            this.真实姓名.Text = "真实姓名";
            this.真实姓名.Width = 85;
            // 
            // 登陆账号
            // 
            this.登陆账号.Text = "登陆账号";
            this.登陆账号.Width = 102;
            // 
            // 登陆密码
            // 
            this.登陆密码.Text = "登陆密码";
            this.登陆密码.Width = 99;
            // 
            // 权限
            // 
            this.权限.Text = "权限";
            this.权限.Width = 89;
            // 
            // sideNavItem1
            // 
            this.sideNavItem1.IsSystemMenu = true;
            this.sideNavItem1.Name = "sideNavItem1";
            this.sideNavItem1.Symbol = "";
            this.sideNavItem1.Text = "Menu";
            this.sideNavItem1.Visible = false;
            // 
            // separator1
            // 
            this.separator1.FixedSize = new System.Drawing.Size(3, 1);
            this.separator1.Name = "separator1";
            this.separator1.Padding.Bottom = 2;
            this.separator1.Padding.Left = 6;
            this.separator1.Padding.Right = 6;
            this.separator1.Padding.Top = 2;
            this.separator1.SeparatorOrientation = DevComponents.DotNetBar.eDesignMarkerOrientation.Vertical;
            // 
            // sideNavItem2
            // 
            this.sideNavItem2.Name = "sideNavItem2";
            this.sideNavItem2.Panel = this.sideNavPanel1;
            this.sideNavItem2.Symbol = "";
            this.sideNavItem2.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.sideNavItem2.Text = "账号管理";
            // 
            // sideNavItem3
            // 
            this.sideNavItem3.Name = "sideNavItem3";
            this.sideNavItem3.Panel = this.sideNavPanel2;
            this.sideNavItem3.Symbol = "";
            this.sideNavItem3.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.sideNavItem3.Tag = "";
            this.sideNavItem3.Text = "监控设置";
            // 
            // sideNavItem4
            // 
            this.sideNavItem4.Name = "sideNavItem4";
            this.sideNavItem4.Symbol = "";
            this.sideNavItem4.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.sideNavItem4.Text = "系统初始";
            // 
            // sideNavItem5
            // 
            this.sideNavItem5.Checked = true;
            this.sideNavItem5.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.sideNavItem5.Name = "sideNavItem5";
            this.sideNavItem5.Panel = this.sideNavPanel4;
            this.sideNavItem5.Symbol = "";
            this.sideNavItem5.Text = "帮助美化";
            // 
            // buttonX6
            // 
            this.buttonX6.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX6.Location = new System.Drawing.Point(0, 0);
            this.buttonX6.Name = "buttonX6";
            this.buttonX6.Size = new System.Drawing.Size(0, 0);
            this.buttonX6.TabIndex = 0;
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Metro;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
            // 
            // line1
            // 
            this.line1.Location = new System.Drawing.Point(0, 0);
            this.line1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(0, 0);
            this.line1.TabIndex = 0;
            this.line1.Text = "line1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 471);
            this.Controls.Add(this.sideNav1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "DotNetBar SideNav Control";
            this.TopLeftCornerSize = 0;
            this.TopRightCornerSize = 0;
            this.sideNav1.ResumeLayout(false);
            this.sideNav1.PerformLayout();
            this.sideNavPanel2.ResumeLayout(false);
            this.sideNavPanel2.PerformLayout();
            this.sideNavPanel4.ResumeLayout(false);
            this.sideNavPanel1.ResumeLayout(false);
            this.sideNavPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.SideNav sideNav1;
        private DevComponents.DotNetBar.Controls.SideNavPanel sideNavPanel1;
        private DevComponents.DotNetBar.Controls.SideNavItem sideNavItem1;
        private DevComponents.DotNetBar.Separator separator1;
        private DevComponents.DotNetBar.Controls.SideNavItem sideNavItem2;
        private DevComponents.DotNetBar.Controls.SideNavPanel sideNavPanel2;
        private DevComponents.DotNetBar.Controls.SideNavPanel sideNavPanel4;
        private DevComponents.DotNetBar.Controls.SideNavItem sideNavItem3;
        private DevComponents.DotNetBar.Controls.SideNavItem sideNavItem4;
        private DevComponents.DotNetBar.Controls.SideNavItem sideNavItem5;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.StyleManager styleManager1;
        private DevComponents.DotNetBar.ButtonX buttonX6;

      //  private Separator separator2;
        private LabelX labelX1;
        private ListView userlistView;
        private ColumnHeader 登陆账号;
        private ColumnHeader 登陆密码;
        private ButtonX buttonX3;
        private ButtonX buttonX2;
        private ButtonX buttonX1;
        private ButtonX buttonX5;
        private ButtonX buttonX4;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX3;
        private LabelX labelX10;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX2;
        private LabelX labelX9;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX1;
        private LabelX labelX7;
        private LabelX labelX3;
        private ColumnHeader 真实姓名;
        private ColumnHeader 权限;
        private LabelX labelX4;
        private ComboBox comboBox1;
        private ListView templist;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX6;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX5;
        private LabelX labelX5;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX4;
        private ButtonX buttonX7;
  //      private ButtonX buttonX6;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX7;
  //    private LabelX labelX8;
        private ColumnHeader 机柜编号;
        private ColumnHeader 当前温度;
        private ColumnHeader 当前状态;
        private ColumnHeader 累计运行时间;
        private ColumnHeader 超温次数;
        private ColumnHeader 故障次数;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX8;
        private LabelX labelX11;
        private DevComponents.DotNetBar.Controls.Line line2;
        private DevComponents.DotNetBar.Controls.Line line1;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX9;
        private LabelX labelX12;
        private ButtonX buttonX8;
        private LabelX labelX13;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxEx1;
        private LabelX labelX2;
        private LabelX labelX6;
    }
}

