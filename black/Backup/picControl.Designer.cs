namespace WinControls
{
    partial class picControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.msp = new System.Windows.Forms.MenuStrip();
            this.tsCurrent = new System.Windows.Forms.ToolStripMenuItem();
            this.ts1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBigger = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSmaller = new System.Windows.Forms.ToolStripMenuItem();
            this.ts2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.tsRight = new System.Windows.Forms.ToolStripMenuItem();
            this.ts3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsReplace = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSaved = new System.Windows.Forms.ToolStripMenuItem();
            this.ts4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.picBox1 = new WinControls.picBox();
            this.msp.SuspendLayout();
            this.SuspendLayout();
            // 
            // msp
            // 
            this.msp.AllowMerge = false;
            this.msp.BackColor = System.Drawing.Color.Transparent;
            this.msp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsCurrent,
            this.ts1,
            this.tsBigger,
            this.tsSmaller,
            this.ts2,
            this.tsLeft,
            this.tsRight,
            this.ts3,
            this.tsReplace,
            this.tsSaved,
            this.ts4,
            this.tsPrint});
            this.msp.Location = new System.Drawing.Point(0, 0);
            this.msp.MdiWindowListItem = this.tsBigger;
            this.msp.Name = "msp";
            this.msp.ShowItemToolTips = true;
            this.msp.Size = new System.Drawing.Size(564, 27);
            this.msp.TabIndex = 1;
            this.msp.Text = "menuStrip";
            // 
            // tsCurrent
            // 
            this.tsCurrent.Name = "tsCurrent";
            this.tsCurrent.Size = new System.Drawing.Size(65, 23);
            this.tsCurrent.Text = "實際大小";
            // 
            // ts1
            // 
            this.ts1.Name = "ts1";
            this.ts1.Size = new System.Drawing.Size(6, 23);
            // 
            // tsBigger
            // 
            this.tsBigger.Name = "tsBigger";
            this.tsBigger.Size = new System.Drawing.Size(41, 23);
            this.tsBigger.Text = "放大";
            // 
            // tsSmaller
            // 
            this.tsSmaller.Name = "tsSmaller";
            this.tsSmaller.Size = new System.Drawing.Size(41, 23);
            this.tsSmaller.Text = "縮小";
            // 
            // ts2
            // 
            this.ts2.Name = "ts2";
            this.ts2.Size = new System.Drawing.Size(6, 23);
            // 
            // tsLeft
            // 
            this.tsLeft.Name = "tsLeft";
            this.tsLeft.Size = new System.Drawing.Size(41, 23);
            this.tsLeft.Text = "左轉";
            // 
            // tsRight
            // 
            this.tsRight.Name = "tsRight";
            this.tsRight.Size = new System.Drawing.Size(41, 23);
            this.tsRight.Text = "右轉";
            // 
            // ts3
            // 
            this.ts3.Name = "ts3";
            this.ts3.Size = new System.Drawing.Size(6, 23);
            // 
            // tsReplace
            // 
            this.tsReplace.Name = "tsReplace";
            this.tsReplace.Size = new System.Drawing.Size(41, 23);
            this.tsReplace.Text = "替換";
            // 
            // tsSaved
            // 
            this.tsSaved.Name = "tsSaved";
            this.tsSaved.Size = new System.Drawing.Size(41, 23);
            this.tsSaved.Text = "另存";
            // 
            // ts4
            // 
            this.ts4.Name = "ts4";
            this.ts4.Size = new System.Drawing.Size(6, 23);
            // 
            // tsPrint
            // 
            this.tsPrint.Name = "tsPrint";
            this.tsPrint.Size = new System.Drawing.Size(41, 23);
            this.tsPrint.Text = "列印";
            // 
            // picBox1
            // 
            this.picBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBox1.Location = new System.Drawing.Point(0, 27);
            this.picBox1.Name = "picBox1";
            this.picBox1.Size = new System.Drawing.Size(564, 333);
            this.picBox1.TabIndex = 2;
            this.picBox1.Text = "picBox1";
            // 
            // picControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.picBox1);
            this.Controls.Add(this.msp);
            this.Name = "picControl";
            this.Size = new System.Drawing.Size(564, 360);
            this.msp.ResumeLayout(false);
            this.msp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msp;
        private System.Windows.Forms.ToolStripMenuItem tsCurrent;
        private System.Windows.Forms.ToolStripSeparator ts1;
        private System.Windows.Forms.ToolStripMenuItem tsBigger;
        private System.Windows.Forms.ToolStripMenuItem tsSmaller;
        private System.Windows.Forms.ToolStripSeparator ts2;
        private System.Windows.Forms.ToolStripMenuItem tsLeft;
        private System.Windows.Forms.ToolStripMenuItem tsRight;
        private System.Windows.Forms.ToolStripSeparator ts3;
        private System.Windows.Forms.ToolStripMenuItem tsReplace;
        private System.Windows.Forms.ToolStripMenuItem tsSaved;
        private System.Windows.Forms.ToolStripSeparator ts4;
        private System.Windows.Forms.ToolStripMenuItem tsPrint;
        private picBox picBox1;
    }
}
