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
    public partial class FrmSystemAddRoom : Form
    {
        private bool _b;//用户是否选中修改房间
        private FrmSystemMain _fsm;
        public FrmSystemAddRoom(FrmSystemMain fsm,bool b)
        {
            _b = b;
            _fsm = fsm;
            InitializeComponent();
        }

        private void FrmSystemAddRoom_Load(object sender, EventArgs e)
        {
            skinEngine1.SkinFile = "s (1).ssk";
            if (_b)
            {
                //加载下拉列表
                string sql = "select * from RoomType";
                DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
                cboRoomType.DataSource = dt;
                cboRoomType.DisplayMember = "RoomTypeName";
                cboRoomType.ValueMember = "RoomTypeId";

                Text = "修改房间信息";
                cboRoomType.SelectedValue = _fsm.listView2.SelectedItems[0].Tag;
                txtRoomnumber.Text = _fsm.listView2.SelectedItems[0].Text;
                txtPosition.Text = _fsm.listView2.SelectedItems[0].SubItems[3].Text;
                txtRoomphoneNumber.Text = _fsm.listView2.SelectedItems[0].SubItems[4].Text;
                txtDoorlock.Text = _fsm.listView2.SelectedItems[0].SubItems[5].Text;
            }
            else
            {
                //加载下拉列表
                string sql = "select * from RoomType";
                DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
                cboRoomType.DataSource = dt;
                cboRoomType.DisplayMember = "RoomTypeName";
                cboRoomType.ValueMember = "RoomTypeId";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnEnder_Click(object sender, EventArgs e)
        {
            if (_b)
            {
                //修改房间
                UpdateRoom();
            }
            else
            {
                //增加房间
                AddRoom();
            }
          
        }
        /// <summary>
        /// 修改房间
        /// </summary>
        private void UpdateRoom()
        {
            bool b = true;//判断是否存在此房间
            if (txtRoomnumber.Text == "")
            {
                MessageBox.Show("请输入房间编号!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                string sql = "select RoomName from Room";
                DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
                foreach (DataRow row in dt.Rows)
                {
                    if (txtRoomnumber.Text == row["RoomName"].ToString()&&txtRoomnumber.Text!=_fsm.listView2.SelectedItems[0].Text)
                    {
                        MessageBox.Show("此房间正在使用，不能进行此项操作!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        b = false;
                        return;
                    }
                }
                if (b)
                {
                    if (txtPosition.Text == "")//如果所在位置为空，则用*号代替
                    {
                        string sql2 = string.Format("update Room set RoomName='{0}',RoomTypeId={1},Position='*',PhoneNumber='{2}',DoorLook='{3}' where RoomName='{4}'",
                            txtRoomnumber.Text,
                            cboRoomType.SelectedValue,
                            txtRoomphoneNumber.Text,
                            txtDoorlock.Text,
                            _fsm.listView2.SelectedItems[0].Text
                            );
                        SqlHelp.ExcuteInsertUpdateDelete(sql2);
                        _fsm.AddListView2();//调用刷新
                        Close();
                    }
                    else
                    {
                        string sql2 = string.Format("update Room set RoomName='{0}',RoomTypeId={1},Position='{2}',PhoneNumber='{3}',DoorLook='{4}' where RoomName='{5}'",
                            txtRoomnumber.Text,
                            cboRoomType.SelectedValue,
                            txtPosition.Text,
                            txtRoomphoneNumber.Text,
                            txtDoorlock.Text,
                            _fsm.listView2.SelectedItems[0].Text
                            );
                        SqlHelp.ExcuteInsertUpdateDelete(sql2);
                        _fsm.AddListView2();//调用刷新
                        Close();
                    }
                }
            }
        }
        /// <summary>
        /// 添加房间
        /// </summary>
        private void AddRoom()
        {
            bool b = true;//判断是否存在此房间
            if (txtRoomnumber.Text == "")
            {
                MessageBox.Show("请输入房间编号!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                string sql = "select RoomName from Room";
                DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
                foreach (DataRow row in dt.Rows)
                {
                    if (txtRoomnumber.Text == row["RoomName"].ToString())
                    {
                        MessageBox.Show("该房间号以存在，请重新编号!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        b = false;
                        return;
                    }
                }
                if (b)
                {
                    if (txtPosition.Text == "")//如果所在位置为空，则用*号代替
                    {
                        string sql2 = string.Format("insert into Room values ('{0}',{1},1,'*','40.00',null,'{2}','{3}')",
                            txtRoomnumber.Text,
                            cboRoomType.SelectedValue,
                            txtRoomphoneNumber.Text,
                            txtDoorlock.Text);//增加房间表中的房间编号,房间类型ID，房间电话，门码锁
                        SqlHelp.ExcuteInsertUpdateDelete(sql2);
                        _fsm.AddListView2();//调用刷新
                        Close();
                    }
                    else
                    {
                        string sql2 = string.Format("insert into Room values ('{0}',{1},1,'{2}','40.00',null,'{3}','{4}','','')",
                            txtRoomnumber.Text,
                            cboRoomType.SelectedValue,
                            txtPosition.Text,
                            txtRoomphoneNumber.Text,
                            txtDoorlock.Text);//增加房间表中的房间编号,房间类型ID，房间电话，门码锁
                        SqlHelp.ExcuteInsertUpdateDelete(sql2);
                        _fsm.AddListView2();//调用刷新
                        Close();
                    }
                    string s = string.Format("添加房间{0}", txtRoomnumber.Text);
                    string sql3 = string.Format("insert into SystemLog values ('{0}','{1}','单个添加','{2}','','')", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), AppInfo.UserName, s);
                    SqlHelp.ExcuteInsertUpdateDelete(sql3);
                }
            }
        }
    }
}
