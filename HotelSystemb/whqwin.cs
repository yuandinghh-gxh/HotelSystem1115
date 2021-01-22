using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace MysqlHoverTree
{
    public partial class whqwin : Office2007Form {  //Form {
        private Point formPoint;    // 窗体的屏幕坐标
        private Point mousePoint;     // 鼠标光标的屏幕坐标

        public whqwin( )
        {
            InitializeComponent();
        }
        private void buttonX1_Click(object sender, EventArgs e) {
            System.Environment.Exit( 0 );  
        }

        private void whqwin_MouseDown(object sender, MouseEventArgs e) {
            mousePoint = Control.MousePosition;  //窗口移动
            formPoint = this.Location;
        }

        private void whqwin_MouseMove(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                Point mousePos = Control.MousePosition;
                this.Location = new Point( mousePos.X - mousePoint.X + formPoint.X, mousePos.Y - mousePoint.Y + formPoint.Y );
            }
        }

        private void buttonX6_Click(object sender, EventArgs e) {  //开始记录
  
        }

        private void timer1_Tick(object sender, EventArgs e) {
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
         //   labelX2.Text = currentTime.ToString();   //"f" ); //不显示秒
            labelX2.Text = currentTime.ToLongDateString();
            labelX2.Text = labelX2.Text + " "+ DateTime.Now.ToString( "dddd", new System.Globalization.CultureInfo( "zh-cn" ) ) + " "+ currentTime.ToString("T");  //中文星期显示
        }
        /// <summary>
        ///  设置通信口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonX2_Click(object sender, EventArgs e) {   //设置通信口
            FrmScanProt fm = new FrmScanProt();
            fm.ShowDialog();
        }

        private void textBoxX1_TextChanged(object sender, EventArgs e) {

        }
    }
}
