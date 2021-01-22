using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MysqlHoverTree {
    public partial class Ttldisp : Form {
        [DllImport( "user32.dll", EntryPoint = "GetKeyboardState" )]
        public static extern int GetKeyboardState(byte[] pbKeyState);
        [DllImport( "user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi )]
        public static extern short GetKeyState(int keyCode);
        const int WM_NCLBUTTONDOWN = 0xA1;
        const int HT_CAPTION = 0x2;

        [DllImport( "user32.dll" )]
        static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        public Ttldisp( ) {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e) {
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            //   labelX2.Text = currentTime.ToString();   //"f" ); //不显示秒
            labelX2.Text = currentTime.ToLongDateString();
            labelX2.Text = labelX2.Text + " " + DateTime.Now.ToString( "dddd", new System.Globalization.CultureInfo( "zh-cn" ) ) + " " + currentTime.ToString( "T" );  //中文星期显示
        }

        private void button1_Click(object sender, EventArgs e) {
            this.Close();
        }


        private void Ttldisp_MouseMove(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left & this.WindowState == FormWindowState.Normal) {                  // 移动窗体
                this.Capture = false;
                SendMessage( Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0 );
            }
        }
    }
}