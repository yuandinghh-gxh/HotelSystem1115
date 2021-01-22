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
    public partial class FrmAffordserve : Form
    {
        private  FrmAddConsums _fsc;
        public FrmAffordserve(FrmAddConsums fsc)
        {
            _fsc = fsc;
            InitializeComponent();
        }

        private void FrmAffordserve_Load(object sender, EventArgs e)
        {
            AddListView(true);
        }
        private void AddListView(bool b)
        {
            string sql;
            if (b)
            {
                sql = "select * from ServeMember sm inner join ServeGrade sg on sm.ServeGradeId=sg.ServeGradeId";
            }
            else
            {
                if (textBox1.Text == "")
                {
                    sql = "select * from ServeMember sm inner join ServeGrade sg on sm.ServeGradeId=sg.ServeGradeId";
                }
                else
                {
                    sql = string.Format("select * from ServeMember sm inner join ServeGrade sg on sm.ServeGradeId=sg.ServeGradeId where sm.Spell='{0}' or sm.ServeNumber='{0}'", textBox1.Text);
                }
            }
            DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
            listView1.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                ListViewItem item = new ListViewItem();
                listView1.Items.Add(item);
                item.Tag = row["ServeMemberId"];
                item.Text = row["ServeNumber"].ToString();
                item.SubItems.Add(row["ServeName"].ToString());
                item.SubItems.Add(row["Sex"].ToString());
                item.SubItems.Add(row["GradeName"].ToString());
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            AddListView(false);
        }
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
            }
            else
            {
                AddConsums();
            }
        }
        private void AddConsums()
        {
            double totelmoney = 0;//保存总金额
            ListViewItem item = new ListViewItem();
            _fsc.lvSellBill.Items.Add(item);
            item.Tag = Convert.ToInt32(_fsc.lvSell.SelectedItems[0].Text);
            item.Text = _fsc.Frmmain.RoomName;
            item.SubItems.Add(_fsc.lvSell.SelectedItems[0].SubItems[1].Text);
            item.SubItems.Add(_fsc.lvSell.SelectedItems[0].SubItems[2].Text);
            item.SubItems.Add("1");
            item.SubItems.Add("1");
            item.SubItems.Add("" + string.Format("{0:F2}", (Convert.ToDouble(_fsc.lvSell.SelectedItems[0].SubItems[2].Text))));
            item.SubItems.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            item.SubItems.Add("");
            item.SubItems.Add("*");
            item.SubItems.Add("" + listView1.SelectedItems[0].Text);
            item.SubItems.Add("admin(admin)");

            foreach (ListViewItem it in _fsc.lvSellBill.Items)
            {
                totelmoney += Convert.ToDouble(it.SubItems[5].Text);
            }
            _fsc.lbChiefmoney.Text = string.Format("{0:F2}", totelmoney);
            Close();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            AddConsums();
        }
    }
}
