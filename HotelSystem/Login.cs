using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HotelSystem1115 {
    public partial class Login : Form    {
        public Login()        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)        {

        }
        private void Login_Load(object sender, EventArgs e)      {      label1.Text = "准备启动. . .";            timer1.Start();        }
        private void timer1_Tick(object sender, EventArgs e)         {            Show();        }
        private new void Show()
        {
            if (label1.Text != "准备启动. . ." && label1.Text != "检查数据库是否连接. . ." && label1.Text != "连接正常，准备启动. . .")
            {
                label1.Text += ".";
            }
            else
            {
                if (label1.Text != "检查数据库是否连接. . ." && label1.Text != "连接正常，准备启动. . .")
                {
                    label1.Text += " .";
                }
                else
                {
                    if (label1.Text != "连接正常，准备启动. . .")
                    {
                        label1.Text += " .";
                    }
                    else
                    {
                        string sql = "select * from Users where UserId=2";
                        DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
                        if (dt.Rows.Count == 0)
                        {
                            timer1.Stop();
                            MessageBox.Show("数据库连接异常，请检查是否建立数据库连接！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            Application.Exit();
                        }
                        else
                        {
                            string sql1 = "select * from Users where XH='Y'";
                            DataTable dt1 = SqlHelp.ExcuteAsAdapter(sql1);
                            AppInfo.IfPwd = dt1.Rows[0]["IfPwd"].ToString();
                            AppInfo.AdminName = dt1.Rows[0]["LoginName"].ToString();
                            AppInfo.AdminPwd = dt1.Rows[0]["PassWord"].ToString();
                            AppInfo.Login = true;
                            Close(); 
                        }
                    }
                    label1.Text = "连接正常，准备启动. . .";
                    return;
                }
                label1.Text = "检查数据库是否连接. . .";
            }
        }
    }
}
