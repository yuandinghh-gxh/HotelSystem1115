using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Whq {
	public partial class disptemp : UserControl {

		DateTime Time;
		public string cabinetnum { get; set; }
		public string Highesttemp { get; set; }
		public string lowesttemp { get; set; }
		public string curtemp { get; set; }
		public disptemp( ) {
			InitializeComponent();
			labelX1.Text = cabinetnum;
			labelX1.Text = curtemp;
		}
		
		#region draw clock
		private void UserControl1_Paint(object sender, PaintEventArgs e) {
			//Graphics dc = e.Graphics;
			//Pen pn = new Pen( ForeColor );
			//SolidBrush br = new SolidBrush( ForeColor );
			//initCoordinates( dc );
			//DrawDots( dc, br );
			//DrawHourHand( dc, pn );
			//DrawSecondHand( dc, pn );
			//DrawMinuteHand( dc, pn );
		}

		public void initCoordinates(Graphics dc) {
			if (this.Width == 0 || this.Height == 0) return;
			dc.TranslateTransform( this.Width / 2, this.Height / 2 );
			dc.ScaleTransform( this.Height / 250F, this.Width / 250F );
		}
		public void DrawDots(Graphics dc, Brush brush) {
			int iSize;
			for (int i = 0; i <= 59; i++) {
				if (i % 5 == 0) {
					iSize = 15;
				} else {
					iSize = 5;
				}
				dc.FillEllipse( brush, -iSize / 2, -100 - iSize / 2, iSize, iSize );
				dc.RotateTransform( 6 );
			}
		}
		public virtual void DrawHourHand(Graphics grfx, Pen pn) {
			GraphicsState gs = grfx.Save();
			grfx.RotateTransform( 360.0F * Time.Minute / 12 + 30.0F * Time.Minute / 60 );
			grfx.DrawLine( pn, 0, 0, 0, -50 );
			grfx.Restore( gs );
		}
		public virtual void DrawMinuteHand(Graphics grfx, Pen pn) {
			GraphicsState gs = grfx.Save();
			grfx.RotateTransform( 360.0F * Time.Minute / 60 + 6.0F * Time.Second / 60 );
			grfx.DrawLine( pn, 0, 0, 0, -70 );
			grfx.Restore( gs );
		}
		public virtual void DrawSecondHand(Graphics grfx, Pen pn) {
			GraphicsState gs = grfx.Save();
			grfx.RotateTransform( 360.0F * Time.Second / 60 );
			grfx.DrawLine( pn, 0, 0, 0, -100 );
			grfx.Restore( gs );
		}
		#endregion

		private void timer_Tick(object sender, EventArgs e) {
			Time = DateTime.Now;
			        Refresh();
		}

		internal class UserControl1 {
			public UserControl1( ) {
			}
		}
	}
}
