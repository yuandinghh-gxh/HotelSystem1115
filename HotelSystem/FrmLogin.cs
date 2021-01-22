using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HotelSystem1115   {
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();

    //        skinEngine1.SkinFile = "s (1).ssk";
            if (AppInfo.IfPwd == "Y")
            {
                txtLoginName.Text = AppInfo.AdminName;
                txtLoginPassword.Text = AppInfo.AdminPwd;
                checkBox1.Checked = true;
            }
            else
            {
                txtLoginName.Text = AppInfo.AdminName;
                txtLoginPassword.Text = "";
                checkBox1.Checked = false;
            }
        }

        private void btnland_Click(object sender, EventArgs e)  // 登陆
        {
            if (txtLoginName.Text == "" || txtLoginPassword.Text == "")
            {
                MessageBox.Show("用户名密码不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLoginName.Text = "";
                txtLoginPassword.Text = "";
                return;
            }
            string sql=string.Format("select * from dbo.Users u inner join Admin a on u.AdminId=a.AdminId where u.LoginName='{0}' and u.PassWord='{1}'",txtLoginName.Text,txtLoginPassword.Text);
            DataTable dt=SqlHelp.ExcuteAsAdapter(sql);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("用户名密码错误", "提示");
                txtLoginName.Text = "";
                txtLoginPassword.Text = "";
            }
            else
            {
   //             Frmyanzheng fry = new Frmyanzheng();      // 图形验证
     //           fry.ShowDialog();
       //         if (AppInfo._YanZheng)
         //       {
                    AppInfo.IsLogin = true;
                    AppInfo.UserId = Convert.ToInt32(dt.Rows[0]["UserId"]);
                    AppInfo.UserName = dt.Rows[0]["LoginName"].ToString();
                    AppInfo.AdminName = dt.Rows[0]["AdminName"].ToString();
                    AppInfo.AdminPwd = dt.Rows[0]["PassWord"].ToString();
                    AppInfo.AdminId = Convert.ToInt32(dt.Rows[0]["AdminId"]);
      //              string sql2 = string.Format("Update Users set XH='N'where UserId !={0}", AppInfo.UserId);   //???????????
        //            SqlHelp.ExcuteInsertUpdateDelete(sql2);
                    string sql3 = string.Format("Update Users set XH='Y'where UserId ={0}", AppInfo.UserId);
                    SqlHelp.ExcuteInsertUpdateDelete(sql3);
                    timer1.Start();
          //      }
         /*       else
                {
                    Application.Exit();
                } */
            }
        }

        private void 退出XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

 /*       private void FrmLogin_Load(object sender, EventArgs e)
        {
            skinEngine1.SkinFile = "s (1).ssk";
            if (AppInfo.IfPwd == "Y")
            {
                txtLoginName.Text = AppInfo.AdminName;
                txtLoginPassword.Text = AppInfo.AdminPwd;
                checkBox1.Checked = true;
            }
            else
            {
                txtLoginName.Text = AppInfo.AdminName;
                txtLoginPassword.Text = "";
                checkBox1.Checked = false;
            }   
        }*/

        private void checkBox1_CheckedChanged(object sender, EventArgs e)  //记住密码
        {
            if (checkBox1.Checked == false)
            {
                txtLoginPassword.Text = "";
            }
            else
            { 
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Text != "正在登录请稍后. . ." && Text != "美萍酒店—用户登录")
            {
                Text += " .";
            }
            else
            {
                if (Text != "正在登录请稍后. . .")
                {
                    Text += " ";
                }
                else
                {
                    if (checkBox1.Checked == true)
                    {
                        string sql2 = string.Format("Update Users set IfPwd='Y' where UserId={0}",AppInfo.UserId);
                        SqlHelp.ExcuteInsertUpdateDelete(sql2);
                    }
                    else
                    {
                        string sql2 = string.Format("Update Users set IfPwd='N' where UserId={0}",AppInfo.UserId);
                        SqlHelp.ExcuteInsertUpdateDelete(sql2);
                    }
                    timer1.Stop();
                    string s=string.Format("{0}在{1}登陆本系统",AppInfo.UserName,DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    string sql = string.Format("insert into SystemLog values ('{0}','{1}','登陆系统','{2}','{3}','{4}')", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), AppInfo.UserName, s, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")); 
               
                    SqlHelp.ExcuteInsertUpdateDelete(sql);
                    Close();
                }
                Text = "正在登录请稍后. . .";
            }
        }
    }
}
