namespace Serialexpample {
    partial class PropertyPage {
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
            this.BaudRateComboBox = new System.Windows.Forms.ComboBox();
            this.stopBitComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // BaudRateComboBox
            // 
            this.BaudRateComboBox.FormattingEnabled = true;
            this.BaudRateComboBox.Location = new System.Drawing.Point(102, 45);
            this.BaudRateComboBox.Name = "BaudRateComboBox";
            this.BaudRateComboBox.Size = new System.Drawing.Size(216, 23);
            this.BaudRateComboBox.TabIndex = 0;
            this.BaudRateComboBox.Text = "BaudRateComboBox";
            // 
            // stopBitComboBox
            // 
            this.stopBitComboBox.FormattingEnabled = true;
            this.stopBitComboBox.Location = new System.Drawing.Point(102, 104);
            this.stopBitComboBox.Name = "stopBitComboBox";
            this.stopBitComboBox.Size = new System.Drawing.Size(206, 23);
            this.stopBitComboBox.TabIndex = 1;
            this.stopBitComboBox.Text = "stopBitComboBox";
            // 
            // PropertyPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 383);
            this.Controls.Add(this.stopBitComboBox);
            this.Controls.Add(this.BaudRateComboBox);
            this.Name = "PropertyPage";
            this.Text = "PropertyPage";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox BaudRateComboBox;
        private System.Windows.Forms.ComboBox stopBitComboBox;
    }
}