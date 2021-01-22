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
    public partial class FrmToGo : Form
    {
        public string _a = "0005";//流水号后四位数
        public FrmMain _fm;
        public FrmToGo(FrmMain fm)
        {
            this._fm = fm;
            InitializeComponent();
        }

        private void FrmToGo_Load(object sender, EventArgs e)
        {
            this.txtnullah.Text = "PK" + DateTime.Now.ToString("yyyyMMdd")+this._a;
            //加载商品表
            string sql = "select * from Goods";
            DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
            foreach (DataRow row in dt.Rows)
            {
                ListViewItem item = new ListViewItem();
                this.listView2.Items.Add(item);
                item.Text = row["GoodsId"].ToString();
                item.SubItems.Add(row["GoodsName"].ToString());
                item.SubItems.Add(string.Format("{0:F2}",row["Price"]));
                item.SubItems.Add(row["StockNum"].ToString());
            }
        }
        /// <summary>
        /// 添加商品单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox2_Click(object sender, EventArgs e)
        {
            this.panel3.Visible = true;
        }
        /// <summary>
        /// listview2取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEit_Click(object sender, EventArgs e)
        {
            this.panel3.Visible = false;
            this.textBox2.Text = "";
        }
        /// <summary>
        /// listview2确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnteer_Click(object sender, EventArgs e)
        {
            double TotelMoney = Convert.ToDouble(this.lbAllMoney.Text);//总金额
            this.ListSend(TotelMoney);
        }
        /// <summary>
        /// listview间传递
        /// </summary>
        private void ListSend(double TotelMoney)
        { 
            if (this.listView2.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择一项商品", "提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            else
            {
                if (Convert.ToInt32(this.listView2.SelectedItems[0].SubItems[3].Text) <= 0)
                {
                    this.panel3.Visible = false;
                    this.textBox3.Text = "1";
                    MessageBox.Show("当前库存不足", "提示");
                    return;
                }
                else if (Convert.ToInt32(this.listView2.SelectedItems[0].SubItems[3].Text) < Convert.ToInt32(this.textBox3.Text))
                {
                    this.panel3.Visible = false;
                    this.textBox3.Text = "1";
                    MessageBox.Show("当前库存不足", "提示");
                    return;
                }
                else
                {
                    //遍历listview1中的项
                    foreach (ListViewItem lv in this.listView1.Items)
                    {
                        //判断listview1中是否存在其商品
                        if (this.listView2.SelectedItems[0].SubItems[1].Text == lv.Text)
                        {
                            //存在有此商品
                            lv.SubItems[3].Text = (Convert.ToDouble(lv.SubItems[3].Text) + Convert.ToDouble(this.textBox3.Text)).ToString();//数量增加
                            //金额增加
                            lv.SubItems[4].Text = string.Format("{0:F2}", Convert.ToDouble(lv.SubItems[3].Text) * Convert.ToDouble(lv.SubItems[1].Text));
                            //总金额增加
                            TotelMoney += Convert.ToDouble(this.listView2.SelectedItems[0].SubItems[2].Text)*Convert.ToDouble(this.textBox3.Text);
                            //减少库存
                            this.listView2.SelectedItems[0].SubItems[3].Text = (Convert.ToInt32(this.listView2.SelectedItems[0].SubItems[3].Text) - Convert.ToInt32(this.textBox3.Text)).ToString();
                            this.lbAllMoney.Text = string.Format("{0:F2}", TotelMoney);
                            this.textBox3.Text = "1";
                            this.textBox2.Text = "";
                            this.panel3.Visible = false;//商品传递完后隐藏listview1
                            return;
                        }
                    }
                    ListViewItem item = new ListViewItem();
                    this.listView1.Items.Add(item);
                    item.Tag = Convert.ToInt32(this.listView2.SelectedItems[0].Text);
                    item.Text = this.listView2.SelectedItems[0].SubItems[1].Text;
                    item.SubItems.Add(this.listView2.SelectedItems[0].SubItems[2].Text);//单价
                    item.SubItems.Add("1");//打折比例
                    item.SubItems.Add(this.textBox3.Text);//数量
                    //金额等于单价乘以数量
                    item.SubItems.Add(string.Format("{0:F2}", Convert.ToDouble(this.listView2.SelectedItems[0].SubItems[2].Text) * Convert.ToDouble(this.textBox3.Text)));
                    //总金额增加
                    TotelMoney += Convert.ToDouble(this.listView2.SelectedItems[0].SubItems[2].Text) * Convert.ToDouble(this.textBox3.Text);
                    item.SubItems.Add("admin(admin)");//记账人
                    item.SubItems.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));//入账时间
                    //减少库存
                    this.listView2.SelectedItems[0].SubItems[3].Text = (Convert.ToInt32(this.listView2.SelectedItems[0].SubItems[3].Text) - Convert.ToInt32(this.textBox3.Text)).ToString();
                    this.lbAllMoney.Text =string.Format("{0:F2}",TotelMoney);
                    this.textBox3.Text = "1";
                    this.textBox2.Text = "";
                    this.panel3.Visible = false;//商品传递完后隐藏listview1

                }
            }
        }
        /// <summary>
        /// listview双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView2_DoubleClick(object sender, EventArgs e)
        {
            double TotelMoney = Convert.ToDouble(this.lbAllMoney.Text);//总金额
            this.ListSend(TotelMoney);
        }
        /// <summary>
        /// 打印账单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("打印机设置出错,请重新设置打印机!", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            return;
        }
        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 0)
            {
            }
            else
            {
                DialogResult dr = MessageBox.Show("你真的要删除此商品吗？", "信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    //遍历商品表
                    foreach (ListViewItem item in this.listView2.Items)
                    {
                        //如果商品名称相等
                        if (this.listView1.SelectedItems[0].Text == item.SubItems[1].Text)
                        {
                            //库存增加
                            item.SubItems[3].Text = (Convert.ToInt32(item.SubItems[3].Text) + Convert.ToInt32(this.listView1.SelectedItems[0].SubItems[3].Text)).ToString();
                            //总金额减少
                            this.lbAllMoney.Text = string.Format("{0:F2}", Convert.ToDouble(this.lbAllMoney.Text) - Convert.ToDouble(this.listView1.SelectedItems[0].SubItems[4].Text));
                            this.listView1.SelectedItems[0].Remove();//移除选中项
                            return;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 修改商品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 0)
            {
            }
            else
            {
                FrmToGoUpdate tu = new FrmToGoUpdate(this);
                tu.ShowDialog();
            }
        }
        /// <summary>
        /// 取消退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 开始结账
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            if (this.listView1.Items.Count == 0)
            {
            }
            else
            {
                FrmToGoSettel tgs = new FrmToGoSettel(this);
                tgs.ShowDialog();
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            int key = (int)e.KeyChar;
            if (key == 8 || (key >= 48 && key <= 57))
            {
            }
            else
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// 简拼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //输入简拼时在消费项目中显示商品
            string sql = string.Format("select * from Goods where SpellBrief like '%{0}%'", this.textBox2.Text);
            DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
            this.listView2.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                ListViewItem item = new ListViewItem();
                this.listView2.Items.Add(item);
                item.Text = row["GoodsId"].ToString();
                item.SubItems.Add(row["GoodsName"].ToString());
                item.SubItems.Add(string.Format("{0:F2}", Convert.ToDouble(row["Price"])));
                item.SubItems.Add(row["StockNum"].ToString());
            }
        }
    }
}
