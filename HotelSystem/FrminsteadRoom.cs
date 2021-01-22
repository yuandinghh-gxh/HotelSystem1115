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
    public partial class FrminsteadRoom : Form
    {
        private FrmMain _fm;
        private bool _b=false;//判断用户输入的房间是否为可供
        private int _RoomId;
        public FrminsteadRoom(FrmMain fm)
        {
            _fm = fm;
            InitializeComponent();
        }

        private void FrminsteadRoom_Load(object sender, EventArgs e)
        {
            skinEngine1.SkinFile = "s (1).ssk";
            label2.Text = _fm.RoomName;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                label4.Enabled = true;
                textBox2.Enabled = true;
            }
            else
            {
                label4.Enabled = false;
                textBox2.Enabled = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            bool b = true;
            string sql = string.Format("select * from Room r inner join RoomType rt on r.RoomTypeId=rt.RoomTypeId inner join RoomState rs on r.RoomStateId=rs.RoomStateId where r.RoomName='{0}'", textBox1.Text);
            DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
            foreach (DataRow row in dt.Rows)
            {
                if (row["RoomStateName"].ToString() == "可供")
                {
                    _RoomId = Convert.ToInt32(row["RoomId"]);
                    _b = true;
                }
                if (_fm.RoomTypeName == row["RoomTypeName"].ToString())
                {
                    b = false;
                    radioButton1.Checked = true;
                    radioButton1.Enabled = true;
                    radioButton2.Enabled = false;
                }
                else
                {
                    b = false;
                    radioButton2.Checked = true;
                    radioButton2.Enabled = true;
                    radioButton1.Enabled = false;
                }
            }
            if (b)
            {
                radioButton2.Checked = true;
                radioButton2.Enabled = true;
                radioButton1.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool b = true;
            if (_b)
            {
                if (textBox2.Text == "")
                {
                    b = true;
                }
                else
                {
                    b = false;
                }
                //取得需要转房的房间最近一次开房记录
                string sql = string.Format("select RentRoomInfoid from RentRoom where RoomId={0} and RentTime=(select max(RentTime) from RentRoom where RoomId={0})", _fm.RoomId);
                int a = Convert.ToInt32(SqlHelp.ExcuteScalar(sql));
                //修改需要转房的房间状态为可供
                string sql2 = string.Format("Update Room set RoomStateId=1 where RoomId={0}", _fm.RoomId);
                SqlHelp.ExcuteInsertUpdateDelete(sql2);
                //修改目标房的状态为入住
                string sql3 = string.Format("Update Room set RoomStateId=2 where RoomId={0}", _RoomId);
                SqlHelp.ExcuteInsertUpdateDelete(sql3);
                //把需要转房的房间开房记录修改为目标房的开房记录
                if (b)
                {
                    string sql4 = string.Format("Update RentRoom set RoomId={0} where RentRoomInfoid={1}", _RoomId, a);
                    SqlHelp.ExcuteInsertUpdateDelete(sql4);
                }
                else
                {
                    string sql4 = string.Format("Update RentRoom set RoomId={0},RentCost='{1}' where RentRoomInfoid={2}", _RoomId, textBox2.Text, a);
                    SqlHelp.ExcuteInsertUpdateDelete(sql4);
                }
                //系统日志
                string s = string.Format("将[{0}]调换至[{1}]", _fm.RoomName, textBox1.Text);
                string sql5 = string.Format("insert into SystemLog values ('{0}','{1}','调换房间','{2}')", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), AppInfo.UserName, s);
                SqlHelp.ExcuteInsertUpdateDelete(sql5);

                _fm.Refresh();//调用刷新
                Close();
            }
            else
            {
                MessageBox.Show("没有找到目标房间或目标房间处于非可供状态！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                textBox1.Focus();
                textBox1.Text = "";
                return;
            }
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
    }
}
