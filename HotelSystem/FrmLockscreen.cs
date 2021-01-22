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
    public partial class FrmLockscreen : Form
    {
        public string _Pwd;//保存锁定密码
        public FrmLockscreen()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _Pwd = textBox1.Text;
            FrmLockScreenInmag flsi = new FrmLockScreenInmag(this);
            flsi.Show();
        }
    }
}
