namespace HotelSystem1115
{
    partial class FrmSystemBatchAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSystemBatchAdd));
            this.btnClose = new System.Windows.Forms.Button();
            this.btnEnter = new System.Windows.Forms.Button();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txtPhontion = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboRoomType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdbAfter = new System.Windows.Forms.RadioButton();
            this.rdbFormer = new System.Windows.Forms.RadioButton();
            this.txtbadge = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRoomAfter = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRoomFormer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(171, 269);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 29;
            this.btnClose.Text = "取消";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnEnter
            // 
            this.btnEnter.Location = new System.Drawing.Point(42, 269);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(75, 23);
            this.btnEnter.TabIndex = 28;
            this.btnEnter.Text = "保存";
            this.btnEnter.UseVisualStyleBackColor = true;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(52, 224);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(180, 16);
            this.checkBox2.TabIndex = 27;
            this.checkBox2.Text = "将房间号作为门锁码同时添加";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(53, 189);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(204, 16);
            this.checkBox1.TabIndex = 26;
            this.checkBox1.Text = "将房间号作为房间电话号同时添加";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // txtPhontion
            // 
            this.txtPhontion.Location = new System.Drawing.Point(115, 145);
            this.txtPhontion.Name = "txtPhontion";
            this.txtPhontion.Size = new System.Drawing.Size(121, 21);
            this.txtPhontion.TabIndex = 25;
            this.txtPhontion.Text = "无";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(50, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 24;
            this.label5.Text = "所在区域:";
            // 
            // cboRoomType
            // 
            this.cboRoomType.FormattingEnabled = true;
            this.cboRoomType.Location = new System.Drawing.Point(115, 108);
            this.cboRoomType.Name = "cboRoomType";
            this.cboRoomType.Size = new System.Drawing.Size(121, 20);
            this.cboRoomType.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 22;
            this.label4.Text = "房间类型:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdbAfter);
            this.panel1.Controls.Add(this.rdbFormer);
            this.panel1.Location = new System.Drawing.Point(168, 70);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(99, 21);
            this.panel1.TabIndex = 21;
            // 
            // rdbAfter
            // 
            this.rdbAfter.AutoSize = true;
            this.rdbAfter.Location = new System.Drawing.Point(49, 2);
            this.rdbAfter.Name = "rdbAfter";
            this.rdbAfter.Size = new System.Drawing.Size(47, 16);
            this.rdbAfter.TabIndex = 1;
            this.rdbAfter.TabStop = true;
            this.rdbAfter.Text = "置后";
            this.rdbAfter.UseVisualStyleBackColor = true;
            // 
            // rdbFormer
            // 
            this.rdbFormer.AutoSize = true;
            this.rdbFormer.Location = new System.Drawing.Point(3, 2);
            this.rdbFormer.Name = "rdbFormer";
            this.rdbFormer.Size = new System.Drawing.Size(47, 16);
            this.rdbFormer.TabIndex = 0;
            this.rdbFormer.TabStop = true;
            this.rdbFormer.Text = "置前";
            this.rdbFormer.UseVisualStyleBackColor = true;
            // 
            // txtbadge
            // 
            this.txtbadge.Location = new System.Drawing.Point(115, 70);
            this.txtbadge.Name = "txtbadge";
            this.txtbadge.Size = new System.Drawing.Size(47, 21);
            this.txtbadge.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 19;
            this.label3.Text = "标记字符:";
            // 
            // txtRoomAfter
            // 
            this.txtRoomAfter.Location = new System.Drawing.Point(191, 27);
            this.txtRoomAfter.MaxLength = 3;
            this.txtRoomAfter.Name = "txtRoomAfter";
            this.txtRoomAfter.Size = new System.Drawing.Size(47, 21);
            this.txtRoomAfter.TabIndex = 18;
            this.txtRoomAfter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRoomAfter_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(168, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "至";
            // 
            // txtRoomFormer
            // 
            this.txtRoomFormer.Location = new System.Drawing.Point(115, 27);
            this.txtRoomFormer.MaxLength = 3;
            this.txtRoomFormer.Name = "txtRoomFormer";
            this.txtRoomFormer.Size = new System.Drawing.Size(47, 21);
            this.txtRoomFormer.TabIndex = 16;
            this.txtRoomFormer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRoomFormer_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "房间范围:";
            // 
            // FrmSystemBatchAdd
            // 
            this.AcceptButton = this.btnEnter;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(309, 318);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.txtPhontion);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cboRoomType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtbadge);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtRoomAfter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtRoomFormer);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSystemBatchAdd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "增加房间";
            this.Load += new System.EventHandler(this.FrmSystemBatchAdd_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox txtPhontion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboRoomType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdbAfter;
        private System.Windows.Forms.RadioButton rdbFormer;
        private System.Windows.Forms.TextBox txtbadge;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRoomAfter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRoomFormer;
        private System.Windows.Forms.Label label1;
    }
}