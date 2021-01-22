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
    public partial class FrmSystemAddGoodsType : Form
    {
        private bool _b;//判断是增加还是修改，用于窗体重用
        private FrmSystemMain _fsm;
        public FrmSystemAddGoodsType(FrmSystemMain fsm,bool b)
        {
            _b = b;
            _fsm = fsm;
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (_b)
            {
                #region //修改类别
                if (radioButton1.Checked == true)//如果需要服务生
                {
                    string sql = string.Format("update GoodsClass set GoodsClass='{0}',Affordserve='需要' where GoodsClassId={1}",
                        txtGoodsType.Text,
                        _fsm.lvGoodsClass.SelectedItems[0].Tag);
                    SqlHelp.ExcuteInsertUpdateDelete(sql);
                    _fsm.AddGoodsType();
                    Close();
                }
                else
                {
                    string sql2 = "select * from GoodsClass";
                    DataTable dt = SqlHelp.ExcuteAsAdapter(sql2);
                    foreach (DataRow row in dt.Rows)
                    {
                        if (txtGoodsType.Text == row["GoodsClass"].ToString() && txtGoodsType.Text != _fsm.lvGoodsClass.SelectedItems[0].SubItems[1].Text)
                        {
                            MessageBox.Show("此商品类别以存在!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            txtGoodsType.Focus();
                            return;
                        }
                    }
                    string sql = string.Format("update GoodsClass set GoodsClass='{0}',Affordserve='不需要' where GoodsClassId={1}",
                        txtGoodsType.Text,
                        _fsm.lvGoodsClass.SelectedItems[0].Tag);
                    SqlHelp.ExcuteInsertUpdateDelete(sql);
                    _fsm.AddGoodsType();
                    Close();
                }
                #endregion
            }
            else
            {
                #region //增加类别
                if (txtGoodsNumber.Text == "")
                {
                    MessageBox.Show("类型编号不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                else if (txtGoodsType.Text == "")
                {
                    MessageBox.Show("商品类别不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                else
                {
                    string sql2 = "select * from GoodsClass";
                    DataTable dt = SqlHelp.ExcuteAsAdapter(sql2);
                    foreach (DataRow row in dt.Rows)
                    {
                        if (txtGoodsNumber.Text == row["GoodsClassNumber"].ToString() || txtGoodsType.Text == row["GoodsClass"].ToString())
                        {
                            MessageBox.Show("此商品编号或商品类别以存在!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }
                    }
                    if (radioButton1.Checked == true)//如果需要服务生
                    {
                        string sql = string.Format("insert into GoodsClass values ('{0}','需要','{1}')",
                            txtGoodsType.Text,
                            txtGoodsNumber.Text);
                        SqlHelp.ExcuteInsertUpdateDelete(sql);
                        _fsm.AddGoodsType();
                        Close();
                    }
                    else
                    {
                        string sql = string.Format("insert into GoodsClass values ('{0}','不需要','{1}')",
                            txtGoodsType.Text,
                            txtGoodsNumber.Text);
                        SqlHelp.ExcuteInsertUpdateDelete(sql);
                        _fsm.AddGoodsType();
                        Close();
                    }
                }
                #endregion
            }
        }

        private void FrmSystemAddGoodsType_Load(object sender, EventArgs e)
        {
            skinEngine1.SkinFile = "s (1).ssk";
            if (_b)
            {
                Text = "修改项目类别";
                txtGoodsNumber.Enabled = false;
                txtGoodsNumber.Text = _fsm.lvGoodsClass.SelectedItems[0].Text;
                txtGoodsType.Text = _fsm.lvGoodsClass.SelectedItems[0].SubItems[1].Text;
                if (_fsm.lvGoodsClass.SelectedItems[0].SubItems[2].Text == "需要")
                {
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;
                }
            }
        }
    }
}
