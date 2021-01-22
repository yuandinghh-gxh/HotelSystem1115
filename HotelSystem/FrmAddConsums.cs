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
    public partial class FrmAddConsums : Form
    {
        public FrmMain Frmmain;
        double _totelmoney = 0;//保存总金额
        public FrmAddConsums(FrmMain frmain)  //本地类 的实例化
        {
            Frmmain = frmain;
            InitializeComponent();
        }

        private void FrmAddConsums_Load(object sender, EventArgs e)
        {
  ///          skinEngine1.SkinFile = "s (1).ssk";
            lbSellRoom.Text = "消费房间："+Frmmain.RoomName;
            lbSellbill.Text = string.Format("{0}  消费清单", Frmmain.RoomName);

            //窗体加载时在消费项目中显示商品
            string sql = "select * from Goods";
            DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
            foreach (DataRow row in dt.Rows)
            {
                ListViewItem item = new ListViewItem();
                lvSell.Items.Add(item);
                item.Tag = row["GoodsId"];
                item.Text = row["GoodsId"].ToString();
                item.SubItems.Add(row["GoodsName"].ToString());
                item.SubItems.Add(string.Format("{0:F2}", Convert.ToDouble(row["Price"])));
                item.SubItems.Add(row["StockNum"].ToString());
            }
        }

        private void AddSell()
        {
            double totelmoney = 0;//保存总金额
            if (Convert.ToInt32(lvSell.SelectedItems[0].SubItems[3].Text) <= 0)
            {
                MessageBox.Show("当前库存不足", "提示");
                return;
            }
            else if (Convert.ToInt32(txtSellamount.Text) > Convert.ToInt32(lvSell.SelectedItems[0].SubItems[3].Text))
            {
                MessageBox.Show("当前库存不足", "提示");
                txtSellamount.Text = "1";
                return;
            }
            foreach (ListViewItem i in lvSellBill.Items)
            {
                //如果消费清单中有此商品，则在原有的基础上数量加1
                if (lvSell.SelectedItems[0].SubItems[1].Text == i.SubItems[1].Text)
                {
                    i.SubItems[4].Text = (Convert.ToInt32(i.SubItems[4].Text) + Convert.ToInt32(txtSellamount.Text)).ToString();//数量加1
                    //金额增加
                    i.SubItems[5].Text = string.Format("{0:F2}", (Convert.ToDouble(i.SubItems[5].Text) + (Convert.ToDouble(lvSell.SelectedItems[0].SubItems[2].Text) * Convert.ToInt32(txtSellamount.Text))));
                    //总金额增加
                    foreach (ListViewItem it in lvSellBill.Items)
                    {
                        totelmoney += Convert.ToDouble(it.SubItems[5].Text);
                    }
                    lbChiefmoney.Text = string.Format("{0:F2}", totelmoney);
                    //减少库存
                    lvSell.SelectedItems[0].SubItems[3].Text = (Convert.ToInt32(lvSell.SelectedItems[0].SubItems[3].Text) - Convert.ToInt32(txtSellamount.Text)).ToString();
                    txtSellamount.Text = "1";

                    _totelmoney = totelmoney;
                    return;
                }
            }
            ListViewItem item = new ListViewItem();
            lvSellBill.Items.Add(item);
            item.Tag = Convert.ToInt32(lvSell.SelectedItems[0].Text);
            item.Text = Frmmain.RoomName;
            item.SubItems.Add(lvSell.SelectedItems[0].SubItems[1].Text);
            item.SubItems.Add(lvSell.SelectedItems[0].SubItems[2].Text);
            item.SubItems.Add("1");
            item.SubItems.Add("" + txtSellamount.Text);
            item.SubItems.Add("" + string.Format("{0:F2}", (Convert.ToDouble(lvSell.SelectedItems[0].SubItems[2].Text) * Convert.ToInt32(txtSellamount.Text))));
            item.SubItems.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            item.SubItems.Add("");
            item.SubItems.Add("*");
            item.SubItems.Add("*");
            item.SubItems.Add("admin(admin)");
            //减少库存
            lvSell.SelectedItems[0].SubItems[3].Text = (Convert.ToInt32(lvSell.SelectedItems[0].SubItems[3].Text) - Convert.ToInt32(txtSellamount.Text)).ToString();
            foreach (ListViewItem it in lvSellBill.Items)
            {
                totelmoney += Convert.ToDouble(it.SubItems[5].Text);
            }
            lbChiefmoney.Text = string.Format("{0:F2}", totelmoney);
            txtSellamount.Text = "1";
            _totelmoney = totelmoney;
        }

        private void lvSell_DoubleClick(object sender, EventArgs e)
        {
            string sql = "select gc.Affordserve from Goods g inner join GoodsClass gc on g.GoodsClassId=gc.GoodsClassId where GoodsId=" + lvSell.SelectedItems[0].Tag;
            string Affordserve = SqlHelp.ExcuteScalar(sql).ToString();
            if (Affordserve == "需要")
            {
                FrmAffordserve fad = new FrmAffordserve(this);
                fad.ShowDialog();
            }
            else
            {
                AddSell();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lvSell.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择要消费的商品", "提示");
                return;
            }
            else
            {
                lvSell_DoubleClick(null, null);
            }
        }
        /// <summary>
        /// 消费退单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (lvSellBill.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择您需要退单的商品", "提示");
                return;
            }
            foreach (ListViewItem item in lvSell.Items)
            {
                //如果商品名相等，则库存曾加
                if (item.SubItems[1].Text == lvSellBill.SelectedItems[0].SubItems[1].Text)
                {
                    item.SubItems[3].Text = (Convert.ToInt32(item.SubItems[3].Text) + Convert.ToInt32(lvSellBill.SelectedItems[0].SubItems[4].Text)).ToString();
                }
            }
            //减少总金额
            lbChiefmoney.Text = string.Format("{0:F2}", _totelmoney - Convert.ToDouble(lvSellBill.SelectedItems[0].SubItems[5].Text)).ToString();
            _totelmoney = Convert.ToDouble(lbChiefmoney.Text);
            //移除选定项
            lvSellBill.SelectedItems[0].Remove();
        }
        /// <summary>
        /// 关闭保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            //如果消费清单中没有商品则退出
            if (lvSellBill.Items.Count == 0)
            {
                Close();
            }
            //获取房间ID
            int roomId = Convert.ToInt32(Frmmain.RoomId);
            //根据房间ID得取最近一次开房记录
            string sql = string.Format("select RentRoomInfoId from RentRoom where RoomId={0} and RentTime=(select max(RentTime) from RentRoom where RoomId={0})", roomId);
            int RentRoomInfoId = Convert.ToInt32(SqlHelp.ExcuteScalar(sql));

            foreach (ListViewItem item in lvSellBill.Items)
            {
                //增加消费信息
                string sql2 = string.Format("insert into GoodsSell values ({0},{1},{2},'{3}','','')", RentRoomInfoId, Convert.ToInt32(item.Tag), Convert.ToInt32(item.SubItems[4].Text), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                SqlHelp.ExcuteInsertUpdateDelete(sql2);

                //日志
                string s = string.Format("[房间号]{0}[消费项目]{1}[消费数量]{2}[消费金额]{3}", Frmmain.RoomName, item.SubItems[1].Text,item.SubItems[4].Text,item.SubItems[5].Text);
                string sql4 = string.Format("insert into SystemLog values ('{0}','{1}','增加消费','{2}','','')", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), AppInfo.UserName, s);
                SqlHelp.ExcuteInsertUpdateDelete(sql4);


                foreach (ListViewItem i in lvSell.Items)
                {
                    if (item.SubItems[1].Text == i.SubItems[1].Text)
                    {
                        //减少库存
                        string sql3 = string.Format("Update Goods set StockNum={0} where GoodsId={1}", Convert.ToInt32(i.SubItems[3].Text), Convert.ToInt32(i.Text));
                        SqlHelp.ExcuteInsertUpdateDelete(sql3);
                    }
                }
            }
            MessageBox.Show("保存成功!");
            Frmmain.GetGoodSellInfo();
            Frmmain.GetTab2();
            Frmmain.GetTab2Tob();
            Frmmain.lbRoomType.Text = "标准单人间";
            Frmmain.Refresh();
            Close();
        }
        /// <summary>
        /// 打印此次消费
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("''Is not a valid floating point value", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }
        /// <summary>
        /// 消费转单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("此功能未完善！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            return;
        }
        /// <summary>
        /// 简拼编码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSpell_TextChanged(object sender, EventArgs e)
        {
            //输入简拼时在消费项目中显示商品
            string sql = string.Format("select * from Goods where SpellBrief like '%{0}%'",txtSpell.Text);
            DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
            lvSell.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                ListViewItem item = new ListViewItem();
                lvSell.Items.Add(item);
                item.Text = row["GoodsId"].ToString();
                item.SubItems.Add(row["GoodsName"].ToString());
                item.SubItems.Add(string.Format("{0:F2}", Convert.ToDouble(row["Price"])));
                item.SubItems.Add(row["StockNum"].ToString());
            }
        }

        private void FrmAddConsums_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                button1_Click(null, null);
            }
            else if (e.KeyCode == Keys.F5)
            {
                button5_Click(null, null);
            }
        }
    }
}
