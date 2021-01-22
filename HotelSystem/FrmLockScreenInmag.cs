using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace HotelSystem1115
{
    public partial class FrmLockScreenInmag : Form
    {
        public FrmLockscreen _fls;
        public FrmLockScreenInmag(FrmLockscreen fls)
        {
            _fls = fls;
            InitializeComponent();
        }

        private void FrmLockScreenInmag_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != _fls._Pwd)
            {
                textBox1.Text = "";
                textBox1.Focus();
                panel3.Enabled = true;
                label3.Enabled = true;
                Thread.Sleep(2000);
                panel3.Enabled = false;
                label3.Enabled = false;
            }
            else
            {
                _fls.Close();
                Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Process[] p = Process.GetProcesses();

            foreach (Process p1 in p)
            {
                try
                {
                    if (p1.ProcessName.ToLower().Trim() == "taskmgr")//这里判断是任务管理器   
                    {
                        p1.Kill();
                        return;
                    }
                }
                catch
                {
                    return;
                }
            }
        }
    }
}
