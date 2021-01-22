using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace datatime
{
    public partial class Form1 : Form
    {
        private DateTime d;
        public DateTime NowDateTime;
        public Form1()
        {
            InitializeComponent();
            NowDateTime = DateTime.Now;
            dateTimePicker1.Text = DateTime.Now.ToString("yyyy年MM月dd日 HH时mm分");
            dateTimePicker2.Text = DateTime.Now.ToString("yyyy年MM月dd日 hh时mm分");
            //year = NowDateTime.Year.ToString(CultureInfo.InvariantCulture);
            //mon = NowDateTime.Month.ToString(CultureInfo.InvariantCulture);
            //day = NowDateTime.Day.ToString(CultureInfo.InvariantCulture);
        }

        private void button2_Click(object sender, EventArgs e)
        {
           d =  dateTimePicker1.Value;
            d= d.AddDays(-1);
     
            dateTimePicker1.Value = d;

         label3.Text = dateTimePicker1.Value.AddDays(-2).ToLongDateString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 最近7天ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            d = DateTime.Now;
            d = d.AddDays(-7);
            dateTimePicker1.Value = d;
        }

        private void 最近一个月ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            d = DateTime.Now;
            d = d.AddMonths(-1);
            dateTimePicker1.Value = d;
        }

        private void 本月ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            d = DateTime.Now;
            d = d.AddDays(1 - d.Day);
            dateTimePicker1.Value = d;
        }

        private void 上月ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            d = DateTime.Now;
            d = d.AddMonths(-1);
            d =  d.AddDays(1 - d.Day);
            dateTimePicker1.Value = d;
        }

        private void 所有时间ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            d = DateTime.Now;
            d = d.AddMonths(1-d.Month);
            d = d.AddYears(-1); d = d.AddDays(1 - d.Day);
            dateTimePicker1.Value = d;
        }

        private void 昨天ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            d = DateTime.Now;
            d = d.AddDays(-1);
            dateTimePicker1.Value = d;
        }

        private void 前天ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            d = DateTime.Now;
            d = d.AddDays(-2);
            dateTimePicker1.Value = d;
        }

        private void 今日ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            d = DateTime.Now;
            dateTimePicker1.Value = d;
        }
    }
}
