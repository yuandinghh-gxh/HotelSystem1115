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
    public partial class FrmToGoSettel : Form
    {
        private FrmToGo _tg;
        public FrmToGoSettel(FrmToGo tg)
        {
            this._tg = tg;
            InitializeComponent();
        }

        private void FrmToGoSettel_Load(object sender, EventArgs e)
        {
            //流水号
            this.lbNullah.Text = this._tg.txtnullah.Text;
            //消费金额
            this.lbConsume.Text = this._tg.lbAllMoney.Text;
            //应收金额
            this.label6.Text = this.lbConsume.Text;
            //实收金额
            this.label9.Text = this.lbConsume.Text;
            //宾客支付
            this.txtRentPay.Text = this.lbConsume.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 宾客支付
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtRentPay_TextChanged(object sender, EventArgs e)
        {
            if (this.txtRentPay.Text == "")
            {
                this.label14.Text = "-" + this.lbConsume.Text;
                return;
            }
            else 
            {
                this.label14.Text = string.Format("{0:F2}", Convert.ToDouble(this.txtRentPay.Text) - Convert.ToDouble(this.lbConsume.Text));
                return;
            }
        }
        /// <summary>
        /// 确定结账
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(this.label14.Text) < 0)
            {
                MessageBox.Show("支付金额不足！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else
            {
                foreach(ListViewItem item in this._tg.listView1.Items)
                {
                    foreach (ListViewItem i in this._tg.listView2.Items)
                    {
                        if (item.Text == i.SubItems[1].Text)
                        {
                            string sql = string.Format("Update Goods set StockNum={0} where GoodsId={1}", Convert.ToInt32(i.SubItems[3].Text), item.Tag);
                            SqlHelp.ExcuteInsertUpdateDelete(sql);
                            item.Remove();
                        }
                    }
                }
                this._tg._a ="000"+(Convert.ToDouble(this._tg._a) + 1).ToString();
                this._tg.txtnullah.Text = "PK" + DateTime.Now.ToString("yyyyMMdd") + this._tg._a;
                this._tg._fm.Alarm();
                this.Close();
            }
        }

        private void txtRentPay_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
