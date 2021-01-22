using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Whq;

namespace Whq
{
    public partial class UserControl1: UserControl
    {
		public string Ss1 {
			set { labelX1.Text = value; }
		}
		public UserControl1()
        {
            InitializeComponent();
        }

	public    void   set1 ( string ss) {
			labelX1.Text = ss;
		}

    }
}
