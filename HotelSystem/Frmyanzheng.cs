using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HotelSystem1115
{
    public partial class Frmyanzheng : Form     //验证输入框
    {
        private string _s;
        public Frmyanzheng()
        {
            InitializeComponent();
        }

        private void Frmyanzheng_Load(object sender, EventArgs e)
        {
            this.ChanShengYanZheng();
            this.textBox1.Focus();
        }
        private void ChanShengYanZheng()
        {
            Bitmap iamge = new Bitmap(80, 30);//创建一张图片
            Graphics g = Graphics.FromImage(iamge);
            g.Clear(Color.White);

            _s= this.ChanShengChar();
            Color c = this.ChanShengColor();
            g.DrawString(this._s, new Font("宋体", 20), new SolidBrush(c), 10, 0);

            //绘制干扰线
            for (int i = 0; i < 4; i++)
            {
                Random rd = new Random(Guid.NewGuid().GetHashCode());
                Pen pen = new Pen(this.ChanShengColor());
                g.DrawLine(pen, 20, rd.Next(2, 28), 60, rd.Next(2, 28));
            }
            //this.Dispose();
            this.pictureBox1.Image = iamge;
        }
        private Color ChanShengColor()
        {
            Random rd = new Random(Guid.NewGuid().GetHashCode());
            Color c = Color.FromArgb(255, rd.Next(0, 100), rd.Next(0, 100), rd.Next(0, 100));
            return c;
        }
        private string ChanShengChar()
        {
            string s1 = "0123456789abcdefghijklmnopqrstuvwxyz";
            string s = "";
            for (int i = 0; i < 4; i++)
            {
                Random rd = new Random(Guid.NewGuid().GetHashCode());
                int a = rd.Next(0, 36);
                s += s1[a].ToString();
            }

            return s.ToUpper();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ChanShengYanZheng();
        }

        private void button2_Click(object sender, EventArgs e)  //确认
        {
            string s = this.textBox1.Text;
                if (s.ToUpper() == this._s)
                {
                    AppInfo.YanZheng = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("输入错误，请重新输入！", "              提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    this.textBox1.Text = "";
                    this.textBox1.Focus();
                    this.ChanShengYanZheng();
                        return;
                }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AppInfo.YanZheng = false;
            this.Close();
        }
    }
}
