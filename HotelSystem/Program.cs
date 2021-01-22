using System;
using System.Windows.Forms;

namespace HotelSystem1115 {
    static class Program      {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Login login = new Login();
            login.ShowDialog();
            if (AppInfo.Login)   
            {
                var frmlogin = new FrmLogin();
                Application.Run(frmlogin);
                if (AppInfo.IsLogin)    //成功登陆。。。。。。。。。
                {
                    FrmMain frmmain = new FrmMain();
                    Application.Run(frmmain);
                }
            }
        }
    }
}
