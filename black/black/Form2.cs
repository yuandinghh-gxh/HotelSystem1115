using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace black {
	public partial class Form2 : Form {
		private Form1 form1;

		public Form2( ) {
			InitializeComponent();
		}

		public Form2(Form1 form1) {
			this.form1 = form1;
		}
	}
}
