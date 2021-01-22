namespace Whq {
	partial class panel2 {
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
			this.navigationBar1 = new DevComponents.DotNetBar.NavigationBar();
			((System.ComponentModel.ISupportInitialize)(this.navigationBar1)).BeginInit();
			this.SuspendLayout();
			// 
			// navigationBar1
			// 
			this.navigationBar1.BackgroundStyle.BackColor1.Color = System.Drawing.SystemColors.Control;
			this.navigationBar1.BackgroundStyle.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.navigationBar1.BackgroundStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.navigationBar1.ItemPaddingBottom = 2;
			this.navigationBar1.ItemPaddingTop = 2;
			this.navigationBar1.Location = new System.Drawing.Point(213, 484);
			this.navigationBar1.Name = "navigationBar1";
			this.navigationBar1.Size = new System.Drawing.Size(515, 34);
			this.navigationBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.navigationBar1.TabIndex = 0;
			this.navigationBar1.Text = "navigationBar1";
			this.navigationBar1.Click += new System.EventHandler(this.navigationBar1_Click);
			// 
			// panel2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1079, 1104);
			this.Controls.Add(this.navigationBar1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "panel2";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "panel2";
			((System.ComponentModel.ISupportInitialize)(this.navigationBar1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.NavigationBar navigationBar1;
	}
}