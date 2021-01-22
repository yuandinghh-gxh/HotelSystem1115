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
    public partial class FrmToGoUpdate : Form
    {
        private FrmToGo _TG;
        public FrmToGoUpdate(FrmToGo TG)
        {
            this._TG = TG;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmToGoUpdate_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = "1";
            this.label2.Text = this._TG.listView1.SelectedItems[0].Text;
        }
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            double db = Convert.ToDouble(this._TG.listView1.SelectedItems[0].SubItems[3].Text);
            foreach (ListViewItem item in this._TG.listView2.Items)
            {
                if (this.label2.Text == item.SubItems[1].Text)
                {
                    if (Convert.ToDouble(this.textBox1.Text) > Convert.ToDouble(item.SubItems[3].Text))
                    {
                        MessageBox.Show("当前库存不足", "提示");
                        this.Close();
                        return;
                    }
                    else
                    {
                        double TotelMoney = Convert.ToDouble(this._TG.lbAllMoney.Text);//总金额
                        //存在有此商品
                        this._TG.listView1.SelectedItems[0].SubItems[3].Text = Convert.ToDouble(this.textBox1.Text).ToString();//数量增加
                        //金额增加
                        this._TG.listView1.SelectedItems[0].SubItems[4].Text = string.Format("{0:F2}", Convert.ToDouble(this._TG.listView1.SelectedItems[0].SubItems[3].Text) * Convert.ToDouble(this._TG.listView1.SelectedItems[0].SubItems[1].Text));
                        //总金额增加
                        TotelMoney += Convert.ToDouble(this._TG.listView2.SelectedItems[0].SubItems[2].Text) * (Convert.ToDouble(this.textBox1.Text)-db);
                        //减少当前库存
                        this._TG.listView2.SelectedItems[0].SubItems[3].Text = (Convert.ToInt32(this._TG.listView2.SelectedItems[0].SubItems[3].Text) - Convert.ToInt32(this.textBox1.Text)).ToString();
                        this._TG.lbAllMoney.Text = string.Format("{0:F2}", TotelMoney);
                        this.Close();
                    }
                }
            }  
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
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
