using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Whq {
    public partial class Login : Form {
        const int WM_NCLBUTTONDOWN = 0xA1;
        const int HT_CAPTION = 0x2;
        private string sql;
        private bool first = true;
        private DataTable dt;
        //     public static char comset;
   
        [DllImport( "user32.dll" )]
        static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        public Login()        {
            InitializeComponent();            sql = "select COUNT(*)  from Users ";           dt = SQLiteDBHelper.ExecQuery( sql );
            if (dt.Rows.Count == 0) {
                MessageBox.Show( "数据库连接异常，请检查是否建立数据库连接！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop );             labelX2.Text = "出错"; // Application.Exit();
            } else {
                AppInfo.Login = true;
            }
            sql = "select  *  from parameters  ";
            dt = SQLiteDBHelper.ExecQuery( sql );
            foreach (DataRow row in dt.Rows) {
                AppInfo.frontstr = row["frontstr"].ToString();
                AppInfo.supply = row["supply"].ToString();
                AppInfo.lowtemp = Convert.ToInt32( row["lowtem"].ToString() );
                AppInfo.uptemp = Convert.ToInt32( row["uptemp"].ToString() );
                AppInfo.startnum = Convert.ToInt32( row["startnum"].ToString() );
                AppInfo.nodecount = Convert.ToInt32( row["nodecount"].ToString() );
                AppInfo.samplingcyc = Convert.ToInt32( row["samplingcyc"].ToString() );
     //           AppInfo.controlnum = Convert.ToBoolean( row["controlnum"].ToString() );
                AppInfo.controltemp = Convert.ToBoolean( row["controltemp"].ToString() );
                AppInfo.comset = Convert.ToBoolean( row["comset"].ToString() );
                AppInfo.comparison = Convert.ToBoolean( row["comparison"].ToString() );
            }
  //          dt = SQLiteDBHelper.ExecQuery( "Select * From comparison" );
          }
        private void Login_Load(object sender, EventArgs e)        {
            buttonX2_Click( null, null );
        }
        private void buttonX1_Click_1(object sender, EventArgs e) {
            System.Environment.Exit( 0 );
        }

        private void timer1_Tick(object sender, EventArgs e) {
            //    = new System.DateTime(); 
            DateTime currentTime = DateTime.Now;
            labelX6.Text = currentTime.ToLongDateString();
            labelX6.Text = labelX6.Text + " " + DateTime.Now.ToString( "dddd", new System.Globalization.CultureInfo( "zh-cn" ) ) + " " + currentTime.ToString( "T" );  //中文星期显示
        }

        private void Login_MouseMove(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left & this.WindowState == FormWindowState.Normal) {             // 移动窗体
                Capture = false;
                SendMessage( Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0 );
            }
        }
        private void buttonX2_Click(object sender, EventArgs e) {
            if (txtLoginName.Text == "" || txtLoginPassword.Text == "") {
                MessageBox.Show( "用户名密码不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                txtLoginName.Text = "";
                txtLoginPassword.Text = "";
            }
              string password  =    Md5.getMd5Hash( txtLoginPassword.Text );
              sql = string.Format( "select * from Users  where LoginName='{0}' and PassWord='{1}'", txtLoginName.Text, password );
            DataTable dt = SQLiteDBHelper.ExecQuery( sql ); 
            if (dt.Rows.Count == 0) {
                MessageBox.Show( "用户名密码错误", "提示" );
                txtLoginName.Text = "";
                txtLoginPassword.Text = "";
            } else {    //   Frmyanzheng fry = new Frmyanzheng();   // 图形验证     fry.ShowDialog();            //         if (AppInfo._YanZheng)        //       {
       //         AppInfo.IsLogin = true;
         //       AppInfo.UserId = Convert.ToInt32( dt.Rows[0]["UserId"] );
                AppInfo.UserName = dt.Rows[0]["UserName"].ToString();
                AppInfo.LoginName = dt.Rows[0]["LoginName"].ToString();
                AppInfo.AdminId = Convert.ToInt32( dt.Rows[0]["AdminId"] );
                timer2.Start();
                AppInfo.Login = true;
                main frmmain = new main();              frmmain.ShowDialog();
            }
        }
        private void timer2_Tick_1(object sender, EventArgs e) {
            timer1.Stop();
            if (first) {
                first = false;
                string s = string.Format( "{0}在{1}登陆本系统", AppInfo.UserName, DateTime.Now.ToString( "yyyy-MM-dd HH:mm:ss" ) );
                string sql = string.Format( "insert into SystemLog values ('{0}','{1}','登陆系统','{2}')", DateTime.Now.ToString( "yyyy-MM-dd HH:mm:ss" ), AppInfo.UserName, s );
                SQLiteDBHelper.ExecQuery( sql ); 
            }
         Close();    //????
        }
        private void button1_Click(object sender, EventArgs e) {
            var System = new set(); System.ShowDialog();
        }
    }
}
