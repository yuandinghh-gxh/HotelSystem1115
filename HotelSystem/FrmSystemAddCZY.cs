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
    public partial class FrmSystemAddCZY : Form
    {
        private FrmSystemMain _fsm;
        private bool _b;
        public FrmSystemAddCZY(FrmSystemMain fsm, bool b)
        {
            _fsm = fsm;
            _b = b;
            InitializeComponent();
            comboBox2.SelectedIndex = 0;
        }

        private void FrmSystemAddCZY_Load(object sender, EventArgs e)
        {
            //加载权限组
            string sql = "select * from Admin";
            DataTable dt = SqlHelp.ExcuteAsAdapter(sql);

            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "AdminName";
            comboBox1.ValueMember = "AdminId";
            if (_b)
            {
                string sql2 = "select * from Users where UserId="+_fsm.lvUsers.SelectedItems[0].Tag;
                DataTable dt2 = SqlHelp.ExcuteAsAdapter(sql2);
                Text = "编辑操作员";
                textBox1.Text = _fsm.lvUsers.SelectedItems[0].SubItems[1].Text;
                textBox1.Enabled = false;
                textBox2.Text = _fsm.lvUsers.SelectedItems[0].SubItems[2].Text;
                textBox3.Text = dt2.Rows[0]["PassWord"].ToString();
                textBox4.Text = textBox3.Text;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                comboBox1.Text = _fsm.lvUsers.SelectedItems[0].Text;
                comboBox2.Text = _fsm.lvUsers.SelectedItems[0].SubItems[3].Text;
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
                MessageBox.Show("操作员编号不能为空", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return;
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("操作员姓名不能为空", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return;
            }
            else if (textBox3.Text.Trim() != textBox4.Text.Trim())
            {
                MessageBox.Show("两次密码输入不一致", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox3.Focus();
                return;
            }
            else
            {
                if (_b)
                {
                    foreach (ListViewItem item in _fsm.lvUsers.Items)
                    {
                        if (textBox1.Text.Trim() == item.SubItems[1].Text && textBox1.Text.Trim()!=_fsm.lvUsers.SelectedItems[0].SubItems[1].Text)
                        {
                            MessageBox.Show("此操作员编号以存在", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textBox1.Focus();
                            return;
                        }
                    }
                    string sql = string.Format("Update Users set LoginName='{0}',AdminId={1},State='{2}' where UserId={3}",
                        textBox2.Text.Trim(),
                        comboBox1.SelectedValue,
                        comboBox2.Text,
                        _fsm.lvUsers.SelectedItems[0].Tag);
                    SqlHelp.ExcuteInsertUpdateDelete(sql);
                    _fsm.AddUsers();
                    Close();
                }
                else
                {
                    foreach (ListViewItem item in _fsm.lvUsers.Items)
                    {
                        if (textBox1.Text.Trim() == item.SubItems[1].Text)
                        {
                            MessageBox.Show("此操作员编号以存在", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textBox1.Focus();
                            return;
                        }
                    }
                    string sql = string.Format("insert into Users values('{0}','{1}','{2}','N',{3},'{4}','N','','')",
                        textBox1.Text.Trim(),
                        textBox2.Text.Trim(),
                        textBox4.Text.Trim(),
                        comboBox1.SelectedValue,
                        comboBox2.Text);
                    SqlHelp.ExcuteInsertUpdateDelete(sql);
                    _fsm.AddUsers();
                    Close();
                }
            }
        }
    }
}
