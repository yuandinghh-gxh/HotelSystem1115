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
    public partial class FrmSystemAddBm : Form
    {
        private FrmSystemMain _fsm;
        private bool _b;
        public FrmSystemAddBm(FrmSystemMain fsm ,bool b)
        {
            _fsm = fsm;
            _b = b;
            InitializeComponent();
        }

        private void FrmSystemAddBm_Load(object sender, EventArgs e)
        {
            if (_b)
            {
                Text = "修改部门";
                textBox1.Text = _fsm.lvBM.SelectedItems[0].Text;
                textBox2.Text = _fsm.lvBM.SelectedItems[0].SubItems[1].Text;
            }
        }

        private void button1_Click(object sender, EventArgs e)
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
                MessageBox.Show("部门编码不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return;
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("部门名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return;
            }
            else
            {
                if (_b)
                {
                    foreach (ListViewItem item in _fsm.lvBM.Items)
                    {
                        if ((textBox1.Text.Trim() == item.Text && textBox1.Text.Trim() != _fsm.lvBM.SelectedItems[0].Text) || (textBox2.Text.Trim() == item.SubItems[1].Text && textBox2.Text.Trim() != _fsm.lvBM.SelectedItems[0].SubItems[1].Text))
                        {
                            MessageBox.Show("此部门以存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textBox1.Focus();
                            return;
                        }
                    }
                    string sql = string.Format("Update BM set BMNumber='{0}',BMName='{1}' where BMId={2}", textBox1.Text.Trim(), textBox2.Text.Trim(), _fsm.lvBM.SelectedItems[0].Tag);
                    SqlHelp.ExcuteInsertUpdateDelete(sql);
                    _fsm.AddBM();
                    Close();
                }
                else
                {
                    foreach (ListViewItem item in _fsm.lvBM.Items)
                    {
                        if (textBox1.Text.Trim() == item.Text || textBox2.Text.Trim() == item.SubItems[1].Text)
                        {
                            MessageBox.Show("此部门以存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textBox1.Focus();
                            return;
                        }
                    }
                    string sql = string.Format("insert into BM values('{0}','{1}')", textBox1.Text.Trim(), textBox2.Text.Trim());
                    SqlHelp.ExcuteInsertUpdateDelete(sql);
                    _fsm.AddBM();
                    Close();
                }
            }
        }
    }
}
