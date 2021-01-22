namespace HotelSystem1115
{
    partial class FrmSystemAddGoods
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSystemAddGoods));
            this.label1 = new System.Windows.Forms.Label();
            this.cboGoodsType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGoodsNumber = new System.Windows.Forms.TextBox();
            this.txtGoodsName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPriceCost = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtStockNum = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtStockAlarm = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtencash = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cboencashBadge = new System.Windows.Forms.CheckBox();
            this.cboStock = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.btnEnter = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "项目类别：";
            // 
            // cboGoodsType
            // 
            this.cboGoodsType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGoodsType.FormattingEnabled = true;
            this.cboGoodsType.Location = new System.Drawing.Point(97, 18);
            this.cboGoodsType.Name = "cboGoodsType";
            this.cboGoodsType.Size = new System.Drawing.Size(102, 20);
            this.cboGoodsType.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "项目编码：";
            // 
            // txtGoodsNumber
            // 
            this.txtGoodsNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGoodsNumber.Location = new System.Drawing.Point(97, 57);
            this.txtGoodsNumber.Name = "txtGoodsNumber";
            this.txtGoodsNumber.Size = new System.Drawing.Size(100, 21);
            this.txtGoodsNumber.TabIndex = 3;
            // 
            // txtGoodsName
            // 
            this.txtGoodsName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGoodsName.Location = new System.Drawing.Point(97, 93);
            this.txtGoodsName.Name = "txtGoodsName";
            this.txtGoodsName.Size = new System.Drawing.Size(100, 21);
            this.txtGoodsName.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "项目名称：";
            // 
            // txtPrice
            // 
            this.txtPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrice.Location = new System.Drawing.Point(99, 131);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(100, 21);
            this.txtPrice.TabIndex = 7;
            this.txtPrice.Text = "0.00";
            this.txtPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrice_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "预设单价：";
            // 
            // txtPriceCost
            // 
            this.txtPriceCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPriceCost.Location = new System.Drawing.Point(301, 18);
            this.txtPriceCost.Name = "txtPriceCost";
            this.txtPriceCost.Size = new System.Drawing.Size(100, 21);
            this.txtPriceCost.TabIndex = 9;
            this.txtPriceCost.Text = "0";
            this.txtPriceCost.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPriceCost_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(230, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "单价成本：";
            // 
            // txtStockNum
            // 
            this.txtStockNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStockNum.Location = new System.Drawing.Point(301, 57);
            this.txtStockNum.Name = "txtStockNum";
            this.txtStockNum.Size = new System.Drawing.Size(100, 21);
            this.txtStockNum.TabIndex = 11;
            this.txtStockNum.Text = "0";
            this.txtStockNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtStockNum_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(230, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "库存数量：";
            // 
            // txtStockAlarm
            // 
            this.txtStockAlarm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStockAlarm.Location = new System.Drawing.Point(301, 94);
            this.txtStockAlarm.Name = "txtStockAlarm";
            this.txtStockAlarm.Size = new System.Drawing.Size(100, 21);
            this.txtStockAlarm.TabIndex = 13;
            this.txtStockAlarm.Text = "0";
            this.txtStockAlarm.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtStockAlarm_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(230, 98);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "报警库存：";
            // 
            // txtencash
            // 
            this.txtencash.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtencash.Location = new System.Drawing.Point(301, 131);
            this.txtencash.Name = "txtencash";
            this.txtencash.Size = new System.Drawing.Size(100, 21);
            this.txtencash.TabIndex = 15;
            this.txtencash.Text = "0";
            this.txtencash.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtencash_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(230, 135);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "兑换积分：";
            // 
            // cboencashBadge
            // 
            this.cboencashBadge.AutoSize = true;
            this.cboencashBadge.Location = new System.Drawing.Point(28, 158);
            this.cboencashBadge.Name = "cboencashBadge";
            this.cboencashBadge.Size = new System.Drawing.Size(156, 16);
            this.cboencashBadge.TabIndex = 16;
            this.cboencashBadge.Text = "允许该商品兑换会员积分";
            this.cboencashBadge.UseVisualStyleBackColor = true;
            this.cboencashBadge.CheckedChanged += new System.EventHandler(this.cboencashBadge_CheckedChanged);
            // 
            // cboStock
            // 
            this.cboStock.AutoSize = true;
            this.cboStock.Location = new System.Drawing.Point(28, 189);
            this.cboStock.Name = "cboStock";
            this.cboStock.Size = new System.Drawing.Size(132, 16);
            this.cboStock.TabIndex = 17;
            this.cboStock.Text = "允许参与进销存管理";
            this.cboStock.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(28, 220);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(96, 16);
            this.checkBox3.TabIndex = 18;
            this.checkBox3.Text = "保存后不关闭";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // btnEnter
            // 
            this.btnEnter.Location = new System.Drawing.Point(177, 203);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(75, 23);
            this.btnEnter.TabIndex = 19;
            this.btnEnter.Text = "保存";
            this.btnEnter.UseVisualStyleBackColor = true;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // btnclose
            // 
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.Location = new System.Drawing.Point(311, 203);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(75, 23);
            this.btnclose.TabIndex = 20;
            this.btnclose.Text = "取消";
            this.btnclose.UseVisualStyleBackColor = true;
            // 
            // skinEngine1
            // 
            this.skinEngine1.@__DrawButtonFocusRectangle = true;
            this.skinEngine1.DisabledButtonTextColor = System.Drawing.Color.Gray;
            this.skinEngine1.DisabledMenuFontColor = System.Drawing.SystemColors.GrayText;
            this.skinEngine1.InactiveCaptionColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.skinEngine1.SerialNumber = "";
            this.skinEngine1.SkinFile = null;
            // 
            // FrmSystemAddGoods
            // 
            this.AcceptButton = this.btnEnter;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnclose;
            this.ClientSize = new System.Drawing.Size(457, 265);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.cboStock);
            this.Controls.Add(this.cboencashBadge);
            this.Controls.Add(this.txtencash);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtStockAlarm);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtStockNum);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtPriceCost);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtGoodsName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtGoodsNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboGoodsType);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSystemAddGoods";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加商品项目";
            this.Load += new System.EventHandler(this.FrmSystemAddGoods_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboGoodsType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGoodsNumber;
        private System.Windows.Forms.TextBox txtGoodsName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPriceCost;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtStockNum;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtStockAlarm;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtencash;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox cboencashBadge;
        private System.Windows.Forms.CheckBox cboStock;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.Button btnclose;
        private Sunisoft.IrisSkin.SkinEngine skinEngine1;
    }
}