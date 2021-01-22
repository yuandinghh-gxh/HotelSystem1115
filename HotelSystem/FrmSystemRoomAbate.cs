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
    public partial class FrmSystemRoomAbate : Form
    {
        private FrmSystemMain _fsm;
        private int _Abate;
        public double _PriceOfDiscount1;
        public double _DiscountScale1;
        public string IFDiscount;//存放是否打折
        public FrmSystemRoomAbate(FrmSystemMain fsm,int Abate)
        {
            _Abate = Abate;
            _fsm = fsm;
            InitializeComponent();
            cboabate.SelectedIndex = 0;
        }

        private void FrmSystemRoomAbate_Load(object sender, EventArgs e)
        {
            if (_Abate == 3)//说明用户在VIP设置中弹出的窗体
            {
                label2.Text = _fsm.lvVIPDiscount.SelectedItems[0].Text.Trim();
                label4.Text = string.Format("{0:F2}", _fsm.lvVIPDiscount.SelectedItems[0].SubItems[1].Text);
                textBox1.Text = label4.Text;
                textBox2.Text = "1.0000";
                AddListView1();
            }
            else if (_Abate == 1)
            {
                label2.Text = _fsm.listView1.SelectedItems[0].Text.Trim();
                label4.Text = string.Format("{0:F2}", _fsm.listView1.SelectedItems[0].SubItems[1].Text);
                textBox1.Text = label4.Text;
                textBox2.Text = "1.0000";
                AddListView1();
            }
        }
        /// <summary>
        /// 加载listView1
        /// </summary>
        public void AddListView1()
        {
            if (_Abate == 3)
            {
                string sql = "select * from VIPDiscount vd inner join VIPType vt on vd.VIPTypeId=vt.VIPTypeId where vd.RoomTypeId=" + _fsm.lvVIPDiscount.SelectedItems[0].Tag;
                DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
                listView1.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    ListViewItem item = new ListViewItem();
                    listView1.Items.Add(item);
                    item.Tag = row["VIPDiscountId"];
                    item.Text = row["VIPName"].ToString();
                    item.SubItems.Add(string.Format("{0:F2}", row["DiscountScale"]));
                    item.SubItems.Add(string.Format("{0:F2}", row["PriceOfDiscount"]));
                }
            }
            else if (_Abate == 1)
            {
                string sql = "select * from VIPDiscount vd inner join VIPType vt on vd.VIPTypeId=vt.VIPTypeId where vd.RoomTypeId=" + _fsm.listView1.SelectedItems[0].Tag;
                DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
                listView1.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    ListViewItem item = new ListViewItem();
                    listView1.Items.Add(item);
                    item.Tag = row["VIPDiscountId"];
                    item.Text = row["VIPName"].ToString();
                    item.SubItems.Add(string.Format("{0:F2}", row["DiscountScale"]));
                    item.SubItems.Add(string.Format("{0:F2}", row["PriceOfDiscount"]));
                }
            }
        }
        private void cboabate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboabate.SelectedIndex == 0)
            {
                label6.Enabled = true;
                label7.Enabled = true;
                textBox1.Enabled = true;
                textBox2.Enabled = true;
            }
            else
            {
                label6.Enabled = false;
                label7.Enabled = false;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
            }
        }
        /// <summary>
        /// listView1的单机事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
            }
            else
            {
                FrmSystemUpdateDiscount fsud = new FrmSystemUpdateDiscount(this);
                fsud.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmSystemUpdateDiscount fsud = new FrmSystemUpdateDiscount(this);
            fsud.BtnEnter();
            Close();
        }
        /// <summary>
        /// 清除打折
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            string sql = string.Format("Update VIPDiscount set IfDiscount='N',PriceOfDiscount='{0}',DiscountScale='1' where RoomTypeId={1}",
                Convert.ToDouble(label4.Text),
                _fsm.lvVIPDiscount.SelectedItems[0].Tag);
            SqlHelp.ExcuteInsertUpdateDelete(sql);
            MessageBox.Show("清除打折成功，请重新打开窗口进行设置！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Close();
        }
    }
}
