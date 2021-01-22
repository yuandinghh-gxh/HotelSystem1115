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
    public partial class FrmGuestInfo : Form
    {
        private FrmMain _fm;
        public FrmGuestInfo(FrmMain fm)
        {
            _fm = fm;
            InitializeComponent();
        }

        private void FrmGuestInfo_Load(object sender, EventArgs e)
        {
            skinEngine1.SkinFile = "s (1).ssk";
            cmbCardType.SelectedIndex = 0;
            cmbGusetsex.SelectedIndex = 0;
            cmbGusetsource.SelectedIndex = 0;
            cmbGusetType.SelectedIndex = 0;
            label2.Text = _fm.RoomName;
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //获取此房间最后一次开房的信息
            string sql = string.Format("select RentRoomInfoId from RentRoom where RoomId={0} and RentTime=(select max(RentTime) from RentRoom where RoomId={0})", _fm.RoomId);
            int RentId = Convert.ToInt32(SqlHelp.ExcuteScalar(sql));

            //根据开房Id进行修改
            string sql2 = string.Format("update RentRoom set GusetType='{0}',GuestName='{1}',GuestCardType ='{2}',GuestCardId='{3}',GusetPhone='{4}',GusetCompany='{5}',Remark='{6}' where RentRoomInfoId={7}",
                cmbGusetType.Text,
                txtGusetName.Text,
                cmbCardType.Text,
                txtCardId.Text,
                txtGusetPhone.Text,
                txtGusetCompany.Text,
                txtRemark.Text,
                RentId);
            SqlHelp.ExcuteInsertUpdateDelete(sql2);
            MessageBox.Show("修改成功!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }
    }
}
