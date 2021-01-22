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
    public partial class FrmSystemAddServeMember : Form
    {
        private FrmSystemMain _fsm;
        private bool _b;//选中增加还是修改,用于代码重用
        public FrmSystemAddServeMember(FrmSystemMain fsm,bool b)
        {
            _fsm = fsm;
            _b = b;
            InitializeComponent();
            cboSex.SelectedIndex = 0;
        }

        private void FrmSystemAddServeMember_Load(object sender, EventArgs e)
        {
            skinEngine1.SkinFile = "s (1).ssk";
            //加载服务区域下拉表
            string sql = "select * from RoomType";
            DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
            cboRoomType.DataSource = dt;
            cboRoomType.DisplayMember = "RoomTypeName";
            cboRoomType.ValueMember = "RoomTypeId";
            //加载服务等级下拉表
            string sql2 = "select * from ServeGrade";
            DataTable dt2 = SqlHelp.ExcuteAsAdapter(sql2);
            cboServeGrade.DataSource = dt2;
            cboServeGrade.DisplayMember = "GradeName";
            cboServeGrade.ValueMember = "ServeGradeId";

            if (_b)
            {
                string sql3 = "select * from ServeMember where ServeMemberId="+_fsm.lvServeMember.SelectedItems[0].Tag;
                DataTable dt3 = SqlHelp.ExcuteAsAdapter(sql3);
                foreach (DataRow row in dt3.Rows)
                {
                    rtbPositionQuality.Text = row["ReMark"].ToString();
                    cboRoomType.SelectedValue = row["RoomTypeId"].ToString();
                    cboServeGrade.SelectedValue = row["ServeGradeId"].ToString();
                }
                Text = "信息修改";
                txtNumber.Enabled = false;
                txtNumber.Text = _fsm.lvServeMember.SelectedItems[0].Text;
                txtName.Text = _fsm.lvServeMember.SelectedItems[0].SubItems[1].Text;
                txtSpell.Text = _fsm.lvServeMember.SelectedItems[0].SubItems[2].Text;
                cboSex.SelectedItem = _fsm.lvServeMember.SelectedItems[0].SubItems[3].Text;
                txtPhone.Text = _fsm.lvServeMember.SelectedItems[0].SubItems[7].Text;
                txtCard.Text = _fsm.lvServeMember.SelectedItems[0].SubItems[8].Text;
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            EsTimaTion();
        }
        /// <summary>
        /// 判断
        /// </summary>
        private void EsTimaTion()
        {
            if (txtNumber.Text == "")
            {
                MessageBox.Show("服务生编号不能为空!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtNumber.Focus();
                return;
            }
            else if (txtName.Text == "")
            {
                MessageBox.Show("服务生名称不能为空!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtName.Focus();
                return;
            }
            else if (txtSpell.Text == "")
            {
                MessageBox.Show("服务生简拼不能为空!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtSpell.Focus();
                return;
            }
            else
            {
                if (_b)
                {
                    string sql = "select * from ServeMember";
                    DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
                    foreach (DataRow row in dt.Rows)
                    {
                        if (txtNumber.Text == row["ServeNumber"].ToString()&&txtNumber.Text!=_fsm.lvServeMember.SelectedItems[0].Text)
                        {
                            MessageBox.Show("此编号的服务生以存在！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            txtNumber.Focus();
                            return;
                        }
                    }
                    UpdateServeMember();
                }
                else
                {
                    string sql = "select * from ServeMember";
                    DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
                    foreach (DataRow row in dt.Rows)
                    {
                        if (txtNumber.Text == row["ServeNumber"].ToString())
                        {
                            MessageBox.Show("此编号的服务生以存在！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            txtNumber.Focus();
                            return;
                        }
                    }
                    AddServeMember();
                }
            }
        }
        /// <summary>
        /// 增加服务生
        /// </summary>
        private void AddServeMember()
        {
            int count = 0;
            string sql = string.Format("insert into ServeMember values('{0}','{1}','{2}','{3}',{4},'无',{6},'{7}','{8}')",
                txtNumber.Text,
                txtName.Text,
                (txtSpell.Text).ToUpper(),
                cboSex.SelectedItem,
                cboRoomType.SelectedValue,
                cboServeGrade.SelectedValue,
                txtPhone.Text,
                txtCard.Text,
                rtbPositionQuality.Text);
            SqlHelp.ExcuteInsertUpdateDelete(sql);
            count=_fsm.AddServeMember(count);
            _fsm.FWStxtWaiterAnnal.Text = string.Format("共{0}条记录", count);
            Close();
        }
        /// <summary>
        /// 修改服务生
        /// </summary>
        private void UpdateServeMember()
        {
            int count = 0;
            string sql = string.Format("update ServeMember set ServeNumber='{0}',ServeName='{1}',Spell='{2}',Sex='{3}',RoomTypeId={4},ServeGradeId={5},PhoneNumber='{6}',CardId='{7}',ReMark='{8}' where ServeMemberId={9}",
                txtNumber.Text,
                txtName.Text,
                (txtSpell.Text).ToUpper(),
                cboSex.SelectedItem,
                cboRoomType.SelectedValue,
                cboServeGrade.SelectedValue,
                txtPhone.Text,
                txtCard.Text,
                rtbPositionQuality.Text,
                _fsm.lvServeMember.SelectedItems[0].Tag);
            SqlHelp.ExcuteInsertUpdateDelete(sql);
            count = _fsm.AddServeMember(count);
            _fsm.FWStxtWaiterAnnal.Text = string.Format("共{0}条记录", count);
            Close();
        }
    }
}
