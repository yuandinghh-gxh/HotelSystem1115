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
    public partial class FrmSystemVIPType : Form
    {
        private FrmSystemMain _fsm;
        private bool _b;
        public FrmSystemVIPType(FrmSystemMain fsm,bool b)
        {
            _fsm = fsm;
            _b = b;
            InitializeComponent();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            IF();
        }
        /// <summary>
        /// 判断
        /// </summary>
        private void IF()
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("会员等级不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return;
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("打折比率有误！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return;
            }
            else
            {
                if (_b)
                {
                    foreach (ListViewItem item in _fsm.lvVIPType.Items)
                    {
                        if (textBox1.Text == item.Text.Trim()&&textBox1.Text!=_fsm.lvVIPType.SelectedItems[0].Text.Trim())
                        {
                            MessageBox.Show("此会员等级以存在！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textBox1.Focus();
                            return;
                        }
                    }
                    UpdateVIPType();
                }
                else
                {
                    foreach (ListViewItem item in _fsm.lvVIPType.Items)
                    {
                        if (textBox1.Text == item.Text.Trim())
                        {
                            MessageBox.Show("此会员等级以存在！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textBox1.Focus();
                            return;
                        }
                    }
                    AddVIPType();
                }
            }
        }
        /// <summary>
        /// 修改VIP类型
        /// </summary>
        private void UpdateVIPType()
        {
            string sql = string.Format("Update VIPType set VIPName='{0}',VIPAbate={1} where VIPTypeId={2}",
                textBox1.Text,
                textBox2.Text,
                _fsm.lvVIPType.SelectedItems[0].Tag);
            SqlHelp.ExcuteInsertUpdateDelete(sql);
            _fsm.VIPType();
            Close();
        }
        /// <summary>
        /// 增加VIP类型
        /// </summary>
        private void AddVIPType()
        {
            string sql = string.Format("insert into VIPType values ('{0}',{1})", textBox1.Text, textBox2.Text);
            SqlHelp.ExcuteInsertUpdateDelete(sql);
            _fsm.VIPType();
            Close();
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            int a = (int)e.KeyChar;
            if (a == 8 || (a >= 47 && a <= 58))
            {
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            int a = (int)e.KeyChar;
            if (a == 8 || (a >= 47 && a <= 58))
            {
            }
            else
            {
                e.Handled = true;
            }
        }

        private void FrmSystemVIPType_Load(object sender, EventArgs e)
        {
            if (_b)
            {
                Text = "修改信息";
                textBox1.Text = _fsm.lvVIPType.SelectedItems[0].Text.Trim();
                textBox2.Text = _fsm.lvVIPType.SelectedItems[0].SubItems[1].Text;
            }
        }
    }
}
