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
    public partial class FrmSystemAddServeGrade : Form
    {
        private FrmSystemMain _fsm;
        private bool _b;//选择增加还是修改,用于窗体重用
        public FrmSystemAddServeGrade(FrmSystemMain fsm,bool b)
        {
            _b = b;
            _fsm = fsm;
            InitializeComponent();
        }

        private void FrmSystemAddServeGrade_Load(object sender, EventArgs e)
        {
            skinEngine1.SkinFile = "s (1).ssk";
            if (_b)
            {
                Text = "修改服务生等级";
                txtGradeNumber.Enabled = false;
                txtGradeNumber.Text = _fsm.lvServeGrade.SelectedItems[0].Text;
                txtGrade.Text = _fsm.lvServeGrade.SelectedItems[0].SubItems[1].Text;
                txtGrade.Focus();
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (_b)
            {
                if (txtGrade.Text == "")
                {
                    MessageBox.Show("等级名称不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtGrade.Focus();
                    return;
                }
                else
                {
                    string sql2 = "select * from ServeGrade";
                    DataTable dt = SqlHelp.ExcuteAsAdapter(sql2);
                    foreach (DataRow row in dt.Rows)
                    {
                        if (txtGrade.Text == row["GradeName"].ToString() && txtGrade.Text != _fsm.lvServeGrade.SelectedItems[0].SubItems[1].Text)
                        {
                            MessageBox.Show("此服务生等级以存在!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            txtGrade.Focus();
                            return;
                        }
                    }
                    string sql = string.Format("Update ServeGrade set GradeName='{0}' where ServeGradeId={1}",
                        txtGrade.Text,
                        _fsm.lvServeGrade.SelectedItems[0].Tag);
                    SqlHelp.ExcuteInsertUpdateDelete(sql);
                    _fsm.AddServeGrade();
                    Close();
                }
            }
            else
            {
                AddEnter();
            }
        }
        /// <summary>
        /// 增加等级
        /// </summary>
        private void AddEnter()
        {
            if (txtGradeNumber.Text == "")
            {
                MessageBox.Show("等级编号不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtGradeNumber.Focus();
                return;
            }
            else if (txtGrade.Text == "")
            {
                MessageBox.Show("等级名称不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtGrade.Focus();
                return;
            }
            else
            {
                string sql2 = "select * from ServeGrade";
                DataTable dt = SqlHelp.ExcuteAsAdapter(sql2);
                foreach (DataRow row in dt.Rows)
                {
                    if (txtGradeNumber.Text == row["GradeNumber"].ToString() || txtGrade.Text == row["GradeName"].ToString())
                    {
                        MessageBox.Show("此等级编号或服务生等级以存在!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                }
                string sql = string.Format("insert into ServeGrade values ('{0}','{1}')",
                    txtGradeNumber.Text,
                    txtGrade.Text);
                SqlHelp.ExcuteInsertUpdateDelete(sql);
                _fsm.AddServeGrade();
                Close();
            }
        }
    }
}
