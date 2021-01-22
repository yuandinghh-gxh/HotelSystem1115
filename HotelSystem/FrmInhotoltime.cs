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
    public partial class FrmInhotoltime : Form
    {
        public DateTime NowDateTime,d;
        public FrmInhotoltime()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmDayinquire.d1 = dateTimePicker1.Value;
            FrmDayinquire.d2 = dateTimePicker2.Value;
            FrmDayinquire.bjc = true;
            Close();
        }

        private void FrmInhotoltime_Load(object sender, EventArgs e)
        {
            Today();
            if (FrmDayinquire.bjc)   //进店
            {
                this.Text = "进店时间。。。。。";
                label1.Text = "进店时间  起始：";
            }
            else
            {
                this.Text = "离店时间。。。。。";
                label1.Text = "离店时间  起始：";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           FrmDayinquire.bjc = false;
            Close();
        }

        private void 今日ToolStripMenuItem_Click(object sender, EventArgs e)  //今天
        {
            Today();
        }

        void Today()
        {
            dateTimePicker1.Text = DateTime.Now.ToString("yyyy年MM月dd日 HH时mm分");
            dateTimePicker2.Text = DateTime.Now.ToString("yyyy年MM月dd日 hh时mm分");
        }

        private void 昨天ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            d = DateTime.Now;
            d = d.AddDays(-1);
            dateTimePicker1.Value = d; dateTimePicker2.Value = d;
        }

        private void 前天ToolStripMenuItem_Click(object sender, EventArgs e)  //前天
        {
            d = DateTime.Now;
            d = d.AddDays(-2);
            dateTimePicker1.Value = d; dateTimePicker2.Value = d;
        }

        private void 最近7天ToolStripMenuItem_Click(object sender, EventArgs e)  // 最近7天
        {
            d = DateTime.Now;
            d = d.AddDays(-7);
            dateTimePicker1.Value = d; dateTimePicker2.Value = DateTime.Now;
        }

        private void 最近一个月ToolStripMenuItem_Click(object sender, EventArgs e)  //最近一个月
        {
            d = DateTime.Now;
            d = d.AddMonths(-1);
            dateTimePicker1.Value = d; dateTimePicker2.Value = DateTime.Now;
        }

        private void 本月ToolStripMenuItem_Click(object sender, EventArgs e)  //本月
        {
            d = DateTime.Now;
            d = d.AddDays(1 - d.Day);
            dateTimePicker1.Value = d; dateTimePicker2.Value = DateTime.Now;
        }

        private void 上月ToolStripMenuItem_Click(object sender, EventArgs e)  //上月
        {
            d = DateTime.Now;
            d = d.AddMonths(-1);
            d = d.AddDays(1 - d.Day);
            dateTimePicker1.Value = d;
            d = DateTime.Now;
            d = d.AddDays(1 - d.Day);
            d = d.AddDays(-1);
            dateTimePicker2.Value = d;

        }

        private void 所有时间ToolStripMenuItem_Click(object sender, EventArgs e)  //所有时间
        {
            d = DateTime.Now;
            d = d.AddMonths(1 - d.Month);
            d = d.AddYears(-1); d = d.AddDays(1 - d.Day);
            dateTimePicker1.Value = d; dateTimePicker2.Value = DateTime.Now;
        }

  
    }
}
