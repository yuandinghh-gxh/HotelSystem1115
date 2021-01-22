namespace Modbus {
	partial class Form1 {
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent( ) {
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.txt_Slave = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.button3 = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.txt_Start = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txt_Length = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.com = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.button1.Location = new System.Drawing.Point(89, 41);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(103, 36);
			this.button1.TabIndex = 0;
			this.button1.Text = "连接";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.button2.Location = new System.Drawing.Point(272, 41);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(160, 36);
			this.button2.TabIndex = 1;
			this.button2.Text = "Close Com";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// txt_Slave
			// 
			this.txt_Slave.Location = new System.Drawing.Point(219, 102);
			this.txt_Slave.Name = "txt_Slave";
			this.txt_Slave.Size = new System.Drawing.Size(124, 25);
			this.txt_Slave.TabIndex = 2;
			this.txt_Slave.Text = "1";
			this.txt_Slave.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(89, 107);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(89, 20);
			this.label1.TabIndex = 3;
			this.label1.Text = "从站地址";
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.button3.Location = new System.Drawing.Point(389, 102);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(103, 30);
			this.button3.TabIndex = 4;
			this.button3.Text = "读取";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label2.Location = new System.Drawing.Point(89, 143);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(89, 20);
			this.label2.TabIndex = 6;
			this.label2.Text = "开始地址";
			// 
			// txt_Start
			// 
			this.txt_Start.Location = new System.Drawing.Point(219, 138);
			this.txt_Start.Name = "txt_Start";
			this.txt_Start.Size = new System.Drawing.Size(124, 25);
			this.txt_Start.TabIndex = 5;
			this.txt_Start.Text = "0";
			this.txt_Start.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label3.Location = new System.Drawing.Point(89, 181);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(89, 20);
			this.label3.TabIndex = 8;
			this.label3.Text = "数据长度";
			// 
			// txt_Length
			// 
			this.txt_Length.Location = new System.Drawing.Point(219, 176);
			this.txt_Length.Name = "txt_Length";
			this.txt_Length.Size = new System.Drawing.Size(124, 25);
			this.txt_Length.TabIndex = 7;
			this.txt_Length.Text = "10";
			this.txt_Length.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label4.ForeColor = System.Drawing.Color.Red;
			this.label4.Location = new System.Drawing.Point(385, 159);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(242, 30);
			this.label4.TabIndex = 9;
			this.label4.Text = "无COM连接";
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point(93, 236);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(308, 117);
			this.richTextBox1.TabIndex = 10;
			this.richTextBox1.Text = "";
			// 
			// com
			// 
			this.com.FormattingEnabled = true;
			this.com.Items.AddRange(new object[] {
            "COM5",
            "COM4",
            "COM1",
            "COM2",
            "COM3",
            "COM6"});
			this.com.Location = new System.Drawing.Point(485, 50);
			this.com.Name = "com";
			this.com.Size = new System.Drawing.Size(121, 23);
			this.com.TabIndex = 11;
			this.com.Text = "COM5";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(677, 417);
			this.Controls.Add(this.com);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txt_Length);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txt_Start);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txt_Slave);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TextBox txt_Slave;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txt_Start;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txt_Length;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.ComboBox com;
	}
}

