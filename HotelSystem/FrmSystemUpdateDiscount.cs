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
    public partial class FrmSystemUpdateDiscount : Form
    {
        private FrmSystemRoomAbate _fsra;
        public double _PriceOfDiscount;
        public bool _b = false;
        public double _DiscountScale;
        public double _PriceOfToday;//原价
        
        public FrmSystemUpdateDiscount(FrmSystemRoomAbate fsra)
        {
            _fsra = fsra;
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void FrmSystemUpdateDiscount_Load(object sender, EventArgs e)
        {
            label2.Text = _fsra.listView1.SelectedItems[0].Text;
            textBox1.Text = _fsra.listView1.SelectedItems[0].SubItems[2].Text; 
            textBox2.Text = _fsra.listView1.SelectedItems[0].SubItems[1].Text;
            _PriceOfToday = Convert.ToDouble(_fsra.label4.Text);
            _PriceOfDiscount = Convert.ToDouble(textBox1.Text);
            _DiscountScale = Convert.ToDouble(textBox2.Text);
            _b = true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int a = (int)e.KeyChar;
            if (a == 8 || (a >= 47 && a <= 58) || a == 46)
            {
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            int a = (int)e.KeyChar;
            if (a == 8 || (a >= 47 && a <= 58)||a==46)
            {
            }
            else
            {
                e.Handled = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                label5.Enabled = true;
                textBox2.Enabled = true;
            }
            else
            {
                label5.Enabled = false;
                textBox2.Enabled = false;
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            
            if (textBox1.Text == "")
            {
                MessageBox.Show("折后单价有误，请重新输入", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return;
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("打折比例有误，请重新输入", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return;
            }
            else
            {
                if (comboBox1.SelectedIndex == 0)
                    _fsra.IFDiscount = "Y";
                else
                    _fsra.IFDiscount = "N";
                _fsra.listView1.SelectedItems[0].SubItems[2].Text = _PriceOfDiscount.ToString();
                _fsra.listView1.SelectedItems[0].SubItems[1].Text = _DiscountScale.ToString();
                _fsra._DiscountScale1 = _DiscountScale;
                _fsra._PriceOfDiscount1 = _PriceOfDiscount;
                Close();
            }
        }
        /// <summary>
        /// 保存写入数据库
        /// </summary>
        public void BtnEnter()
        {
            string sql = string.Format("Update VIPDiscount set IfDiscount='{0}',PriceOfDiscount='{1}',DiscountScale='{2}' where VIPDiscountId={3}",
                    _fsra.IFDiscount,
                _fsra._PriceOfDiscount1,
                _fsra._DiscountScale1,
                _fsra.listView1.SelectedItems[0].Tag);
            SqlHelp.ExcuteInsertUpdateDelete(sql);
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (_b)
            {
                if (textBox1.Text == "")
                {
                    return;
                }
                _PriceOfDiscount = Convert.ToDouble(textBox1.Text);
                _DiscountScale = _PriceOfDiscount / _PriceOfToday;//打折比例等于今日价格除以折后价格
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (_b)
            {
                if (textBox2.Text == "")
                {
                    return;
                }
                _DiscountScale = Convert.ToDouble(textBox2.Text);
                _PriceOfDiscount = _PriceOfToday * _DiscountScale;//折后价等于今日价格乘以打折比例
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = _PriceOfDiscount.ToString();
            _PriceOfDiscount = Convert.ToDouble(textBox1.Text);
            
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Text = _DiscountScale.ToString();
            _DiscountScale = Convert.ToDouble(textBox2.Text);
        }
    }
}
