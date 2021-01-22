using System;

namespace Serialexpample {
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
			this.components = new System.ComponentModel.Container();
			this.textBox = new System.Windows.Forms.TextBox();
			this.sendButton = new System.Windows.Forms.Button();
			this.readButton = new System.Windows.Forms.Button();
			this.baudRatelLabel = new System.Windows.Forms.Label();
			this.propertyButton = new System.Windows.Forms.Button();
			this.readTimeOutLabel = new System.Windows.Forms.Label();
			this.parityLabel = new System.Windows.Forms.Label();
			this.stopBitLabel = new System.Windows.Forms.Label();
			this.saveStatusButton = new System.Windows.Forms.Button();
			this.startCommButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// textBox
			// 
			this.textBox.Location = new System.Drawing.Point(12, 18);
			this.textBox.Multiline = true;
			this.textBox.Name = "textBox";
			this.textBox.Size = new System.Drawing.Size(210, 131);
			this.textBox.TabIndex = 0;
			this.textBox.Text = "textBox";
			// 
			// sendButton
			// 
			this.sendButton.Location = new System.Drawing.Point(264, 25);
			this.sendButton.Name = "sendButton";
			this.sendButton.Size = new System.Drawing.Size(148, 23);
			this.sendButton.TabIndex = 1;
			this.sendButton.Text = "sendButton";
			this.sendButton.UseVisualStyleBackColor = true;
			this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
			// 
			// readButton
			// 
			this.readButton.Location = new System.Drawing.Point(264, 69);
			this.readButton.Name = "readButton";
			this.readButton.Size = new System.Drawing.Size(163, 23);
			this.readButton.TabIndex = 2;
			this.readButton.Text = "readButton";
			this.readButton.UseVisualStyleBackColor = true;
			this.readButton.Click += new System.EventHandler(this.readButton_Click);
			// 
			// baudRatelLabel
			// 
			this.baudRatelLabel.AutoSize = true;
			this.baudRatelLabel.Location = new System.Drawing.Point(41, 187);
			this.baudRatelLabel.Name = "baudRatelLabel";
			this.baudRatelLabel.Size = new System.Drawing.Size(119, 15);
			this.baudRatelLabel.TabIndex = 3;
			this.baudRatelLabel.Text = "baudRatelLabel";
			// 
			// propertyButton
			// 
			this.propertyButton.Location = new System.Drawing.Point(264, 238);
			this.propertyButton.Name = "propertyButton";
			this.propertyButton.Size = new System.Drawing.Size(139, 23);
			this.propertyButton.TabIndex = 4;
			this.propertyButton.Text = "propertyButton";
			this.propertyButton.UseVisualStyleBackColor = true;
			this.propertyButton.Click += new System.EventHandler(this.propertyButton_Click);
			// 
			// readTimeOutLabel
			// 
			this.readTimeOutLabel.AutoSize = true;
			this.readTimeOutLabel.Location = new System.Drawing.Point(25, 230);
			this.readTimeOutLabel.Name = "readTimeOutLabel";
			this.readTimeOutLabel.Size = new System.Drawing.Size(135, 15);
			this.readTimeOutLabel.TabIndex = 5;
			this.readTimeOutLabel.Text = "readTimeOutLabel";
			// 
			// parityLabel
			// 
			this.parityLabel.AutoSize = true;
			this.parityLabel.Location = new System.Drawing.Point(41, 276);
			this.parityLabel.Name = "parityLabel";
			this.parityLabel.Size = new System.Drawing.Size(95, 15);
			this.parityLabel.TabIndex = 6;
			this.parityLabel.Text = "parityLabel";
			// 
			// stopBitLabel
			// 
			this.stopBitLabel.AutoSize = true;
			this.stopBitLabel.Location = new System.Drawing.Point(41, 326);
			this.stopBitLabel.Name = "stopBitLabel";
			this.stopBitLabel.Size = new System.Drawing.Size(103, 15);
			this.stopBitLabel.TabIndex = 7;
			this.stopBitLabel.Text = "stopBitLabel";
			// 
			// saveStatusButton
			// 
			this.saveStatusButton.Location = new System.Drawing.Point(264, 134);
			this.saveStatusButton.Name = "saveStatusButton";
			this.saveStatusButton.Size = new System.Drawing.Size(163, 23);
			this.saveStatusButton.TabIndex = 8;
			this.saveStatusButton.Text = "saveStatusButton";
			this.saveStatusButton.UseVisualStyleBackColor = true;
			this.saveStatusButton.Click += new System.EventHandler(this.saveStatusButton_Click);
			// 
			// startCommButton
			// 
			this.startCommButton.Location = new System.Drawing.Point(264, 176);
			this.startCommButton.Name = "startCommButton";
			this.startCommButton.Size = new System.Drawing.Size(176, 23);
			this.startCommButton.TabIndex = 9;
			this.startCommButton.Text = "startCommButton";
			this.startCommButton.UseVisualStyleBackColor = true;
			this.startCommButton.Click += new System.EventHandler(this.startCommButton_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(264, 307);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(55, 15);
			this.label1.TabIndex = 10;
			this.label1.Text = "label1";
			// 
			// timer1
			// 
			this.timer1.Interval = 5000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(513, 389);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.startCommButton);
			this.Controls.Add(this.saveStatusButton);
			this.Controls.Add(this.stopBitLabel);
			this.Controls.Add(this.parityLabel);
			this.Controls.Add(this.readTimeOutLabel);
			this.Controls.Add(this.propertyButton);
			this.Controls.Add(this.baudRatelLabel);
			this.Controls.Add(this.readButton);
			this.Controls.Add(this.sendButton);
			this.Controls.Add(this.textBox);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

    

        #endregion

        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.Button readButton;
        private System.Windows.Forms.Label baudRatelLabel;
        private System.Windows.Forms.Button propertyButton;
        private System.Windows.Forms.Label readTimeOutLabel;
        private System.Windows.Forms.Label parityLabel;
        private System.Windows.Forms.Label stopBitLabel;
        private System.Windows.Forms.Button saveStatusButton;
        private System.Windows.Forms.Button startCommButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
    }
}

