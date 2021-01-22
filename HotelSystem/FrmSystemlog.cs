using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace HotelSystem1115
{
    public partial class FrmSystemlog : Form
    {
        public FrmSystemlog()
        {
            InitializeComponent();
        }

        private void FrmSystemlog_Load(object sender, EventArgs e)
        {
            string sql = string.Format("select * from SystemLog where HandleTime >='{0}' and HandleTime <'{1}'",
                DateTime.Today,
                DateTime.Today.AddDays(1));
            Add(sql);
        }
        private void Add(string sql)
        {
            DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
            listView1.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                ListViewItem item = new ListViewItem();
                listView1.Items.Add(item);
                item.Tag = row["SystemLogId"];
                item.Text = row["HandleTime"].ToString();
                item.SubItems.Add(row["operator"].ToString());
                item.SubItems.Add(row["Abstract"].ToString());
                item.SubItems.Add(row["opertorContent"].ToString());
            }
        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string sql = string.Format("select * from SystemLog where operator like '%{0}%' or Abstract like '%{0}%'",
                textBox3.Text);
            Add(sql);
        }

        private void 今天ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sql = string.Format("select * from SystemLog where HandleTime >='{0}' and HandleTime <'{1}'",
                DateTime.Today,
                DateTime.Today.AddDays(1));
            Add(sql);
        }

        private void 昨天ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sql = string.Format("select * from SystemLog where HandleTime >='{0}' and HandleTime <'{1}'",
                DateTime.Today.AddDays(-1),
                DateTime.Today);
            Add(sql);
        }

        private void 前天ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sql = string.Format("select * from SystemLog where HandleTime >='{0}' and HandleTime <'{1}'",
                DateTime.Today.AddDays(-2),
                DateTime.Today.AddDays(-1));
            Add(sql);
        }

        private void 最近七天ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sql = string.Format("select * from SystemLog where HandleTime >='{0}' and HandleTime <'{1}'",
                DateTime.Today.AddDays(-7),
                DateTime.Today.AddDays(1));
            Add(sql);
        }

        private void 最近一月ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sql = string.Format("select * from SystemLog where HandleTime >='{0}' and HandleTime <'{1}'",
                DateTime.Today.AddDays(-30),
                DateTime.Today.AddDays(1));
            Add(sql);
        }

        private void 本月ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Day = Convert.ToInt32(DateTime.Now.Day);
            string sql = string.Format("select * from SystemLog where HandleTime >='{0}' and HandleTime <'{1}'",
                DateTime.Today.AddDays(-(Day)),
                DateTime.Today.AddDays(1));
            Add(sql);
        }

        private void 上月ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Month = Convert.ToInt32(DateTime.Now.Month);
            int Day = Convert.ToInt32(DateTime.Now.Day);
            string sql = string.Format("select * from SystemLog where HandleTime >='{0}' and HandleTime <'{1}'",
                (DateTime.Today.AddDays(-(Day)).AddMonths(-(Month))),
                DateTime.Today.AddDays(-(Day)));
            Add(sql);
        }

        private void 所有时间ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sql = string.Format("select * from SystemLog");
            Add(sql);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DialogResult dr= MessageBox.Show("你确定要删除指定时间段的系统日记吗？", "                  提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                if (Convert.ToDateTime(dateTimePicker1.Text) >= Convert.ToDateTime(dateTimePicker2.Text).AddDays(1))
                {
                    MessageBox.Show("时间错误，请检查！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                else
                {
                    string sql = string.Format("Delete SystemLog where HandleTime >='{0}' and HandleTime <'{1}'",
                        dateTimePicker1.Text,
                    Convert.ToDateTime(dateTimePicker2.Text).AddDays(1));
                    Add(sql);

                    FrmSystemlog_Load(null, null);
                }
            }
        }

        private void 导出为文本文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK && saveFileDialog1.FileName.Length > 0)
            {
                RichTextBox rtb = new RichTextBox();
                rtb.Text += "                                              系统日记";
                foreach (ListViewItem item in listView1.Items)
                {
                    rtb.Text += item.Text + "     ";
                    rtb.Text += item.SubItems[1].Text + "     ";
                    rtb.Text += item.SubItems[2].Text + "     ";
                    rtb.Text += item.SubItems[3].Text + "     ";
                }
                rtb.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
            }
        }
    }
}
