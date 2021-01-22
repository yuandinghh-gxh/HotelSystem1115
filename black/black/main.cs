using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace black {
	public partial class main : Form {
		public main( ) {
			InitializeComponent();
		//	this.Location = new Point(0, 1000 );
			this.BackColor = Color.White; this.TransparencyKey = Color.White;
			label3.BringToFront(); label5.BringToFront();
		}

		private void timer1_Tick(object sender, EventArgs e) {
			DateTime dt = DateTime.Now.AddMilliseconds( 0 - Environment.TickCount );   //计算 开机时间
			TimeSpan m_WorkTimeTemp = new TimeSpan( Convert.ToInt64( Environment.TickCount ) * 10000 );
			label5.Text = m_WorkTimeTemp.Hours + ": " + m_WorkTimeTemp.Minutes;
			label3.Text = A.deltime.ToString(); 
		}
	}
}
