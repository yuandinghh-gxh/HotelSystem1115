﻿namespace HotelSystem1115
{
    partial class FrmInhotoltime
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInhotoltime));
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.今日ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.昨天ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.前天ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.最近7天ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.最近一个月ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.本月ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.上月ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.所有时间ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "进店时间  起始：";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy年MM月dd日 hh时mm分";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(123, 31);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(158, 21);
            this.dateTimePicker1.TabIndex = 28;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 29;
            this.label2.Text = "终止：";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CustomFormat = "yyyy年MM月dd日 hh时mm分";
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(123, 81);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(158, 21);
            this.dateTimePicker2.TabIndex = 30;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(28, 143);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 31;
            this.button2.Text = "确定";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(185, 143);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 32;
            this.button1.Text = "取消";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.今日ToolStripMenuItem,
            this.昨天ToolStripMenuItem1,
            this.前天ToolStripMenuItem,
            this.toolStripSeparator1,
            this.最近7天ToolStripMenuItem,
            this.最近一个月ToolStripMenuItem,
            this.toolStripSeparator2,
            this.本月ToolStripMenuItem,
            this.上月ToolStripMenuItem,
            this.所有时间ToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 34);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            // 
            // 今日ToolStripMenuItem
            // 
            this.今日ToolStripMenuItem.Name = "今日ToolStripMenuItem";
            this.今日ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.今日ToolStripMenuItem.Text = "今日";
            this.今日ToolStripMenuItem.Click += new System.EventHandler(this.今日ToolStripMenuItem_Click);
            // 
            // 昨天ToolStripMenuItem1
            // 
            this.昨天ToolStripMenuItem1.Name = "昨天ToolStripMenuItem1";
            this.昨天ToolStripMenuItem1.Size = new System.Drawing.Size(136, 22);
            this.昨天ToolStripMenuItem1.Text = "昨天";
            this.昨天ToolStripMenuItem1.Click += new System.EventHandler(this.昨天ToolStripMenuItem1_Click);
            // 
            // 前天ToolStripMenuItem
            // 
            this.前天ToolStripMenuItem.Name = "前天ToolStripMenuItem";
            this.前天ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.前天ToolStripMenuItem.Text = "前天";
            this.前天ToolStripMenuItem.Click += new System.EventHandler(this.前天ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(133, 6);
            // 
            // 最近7天ToolStripMenuItem
            // 
            this.最近7天ToolStripMenuItem.Name = "最近7天ToolStripMenuItem";
            this.最近7天ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.最近7天ToolStripMenuItem.Text = "最近7天";
            this.最近7天ToolStripMenuItem.Click += new System.EventHandler(this.最近7天ToolStripMenuItem_Click);
            // 
            // 最近一个月ToolStripMenuItem
            // 
            this.最近一个月ToolStripMenuItem.Name = "最近一个月ToolStripMenuItem";
            this.最近一个月ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.最近一个月ToolStripMenuItem.Text = "最近一个月";
            this.最近一个月ToolStripMenuItem.Click += new System.EventHandler(this.最近一个月ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(133, 6);
            // 
            // 本月ToolStripMenuItem
            // 
            this.本月ToolStripMenuItem.Name = "本月ToolStripMenuItem";
            this.本月ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.本月ToolStripMenuItem.Text = "本月";
            this.本月ToolStripMenuItem.Click += new System.EventHandler(this.本月ToolStripMenuItem_Click);
            // 
            // 上月ToolStripMenuItem
            // 
            this.上月ToolStripMenuItem.Name = "上月ToolStripMenuItem";
            this.上月ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.上月ToolStripMenuItem.Text = "上月";
            this.上月ToolStripMenuItem.Click += new System.EventHandler(this.上月ToolStripMenuItem_Click);
            // 
            // 所有时间ToolStripMenuItem
            // 
            this.所有时间ToolStripMenuItem.Name = "所有时间ToolStripMenuItem";
            this.所有时间ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.所有时间ToolStripMenuItem.Text = "所有时间";
            this.所有时间ToolStripMenuItem.Click += new System.EventHandler(this.所有时间ToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(16, 30);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(284, 81);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(32, 37);
            this.toolStrip1.TabIndex = 35;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // FrmInhotoltime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 189);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label1);
            this.Name = "FrmInhotoltime";
            this.Text = "进店时间。。。。。";
            this.Load += new System.EventHandler(this.FrmInhotoltime_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem 今日ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 昨天ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 前天ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 最近7天ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 最近一个月ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 本月ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 上月ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 所有时间ToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
    }
}