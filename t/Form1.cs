using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace t {
	public partial class Form1 : Form {
		private string llss ="ll";
		private string lls = "ll";
		private int j = 0;
		public Form1( ) {
			InitializeComponent();
	//		Label ll1 = new Label();
			Label llss = new Label();
			for (int j = 0; j < 5; j++) {
				lls = lls + j.ToString();
				load( 50*j, 100, lls, llss);
				//Label ll2 = new Label();
				//	lls = lls + j.ToString();
				//	load( 150 , 100, lls, ll2);
				Dictionary<string, string> openWith =	   new Dictionary<string, string>();
				// Add some elements to the dictionary. There are no 
				// duplicate keys, but some of the values are duplicates.
				openWith.Add( "txt", "notepad.exe" );		openWith.Add( "bmp", "paint.exe" );
				openWith.Add( "dib", "paint.exe" );				openWith.Add( "rtf", "wordpad.exe" );
				Dictionary<string, string>.ValueCollection valueColl =    openWith.Values;
				// The elements of the ValueCollection are strongly typed				// with the type that was specified for dictionary values.
				foreach (string s in valueColl)   { 			Console.WriteLine( "Value = {0}", s );				}
				// When you use foreach to enumerate dictionary elements,  				// the elements are retrieved as KeyValuePair objects.
				Console.WriteLine();
				foreach (KeyValuePair<string, string> kvp in openWith) {
					Console.WriteLine( "Key = {0}, Value = {1}",	    kvp.Key, kvp.Value );
				}
			}
		}

		private void button2_Click(object sender, EventArgs e)   			{ 
	// 	panel2.Visible = true;
		}

		private void button3_Click(object sender, EventArgs e) {
			panel2.Visible = false;
		}
		private void  load(int xx,int yy, string lls, Label ll) {

			ll.AutoSize = true;
			ll.BackColor = System.Drawing.SystemColors.AppWorkspace;
			ll.Font = new System.Drawing.Font( "仿宋", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)) );
			ll.ForeColor = System.Drawing.Color.FromArgb( ((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(100)))) );
			ll.Location = new System.Drawing.Point( xx, yy );
			ll.Margin = new System.Windows.Forms.Padding( 4, 0, 4, 0 );
			ll.Name = lls;
			ll.Size = new System.Drawing.Size( 48, 29 );
	//		ll.TabIndex = 5;
			ll.Text = "嵇康";
			ll.UseCompatibleTextRendering = true;
			Controls.Add( ll );
		}
	}
}
