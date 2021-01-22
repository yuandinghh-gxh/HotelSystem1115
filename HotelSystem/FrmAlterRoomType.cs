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
    public partial class FrmAlterRoomType : Form
    {
        private FrmMain _fm;
        private string roomlx;
        public FrmAlterRoomType(FrmMain fm)
        {
            _fm = fm;
            InitializeComponent();
        }

        private void FrmAlterRoomType_Load(object sender, EventArgs e)
        {
            skinEngine1.SkinFile = "s (1).ssk";
            string sql="select * from Room r inner join RoomType rt on r.RoomTypeId=rt.RoomTypeId inner join RoomState rs on r.RoomStateId=rs.RoomStateId where r.RoomId="+_fm.RoomId;
            DataTable dt = SqlHelp.ExcuteAsAdapter(sql);

            foreach (DataRow row in dt.Rows)
            {
                label2.Text = row["RoomName"].ToString();
                if (_fm.RoomStateId == "入住")
                {
                    label3.Text = string.Format("{0:F2}", row["PricrOfForegift"]);
                }
                label5.Text = row["RoomstateName"].ToString();
                roomlx = row["RoomTypeName"].ToString();  // 房间类型名称
                if (_fm.RoomStateId == "入住")
                {
                    textBox1.Text = row["Remark"].ToString();
                }

                if (_fm.RoomStateId == "预定")
                {
                    DialogResult dr = MessageBox.Show("预定状态改变请到 《预订管理》", "提示信息", MessageBoxButtons.OK);
                    if (dr == DialogResult.OK)
                    {
                       Close();
                    }
                }
            }

            if (_fm.RoomStateId == "入住")
            {
                checkBox1.Checked = true;
                label7.Text = string.Format("{0:F2}",_fm.Sellmoney);
            }
            else
            {
                checkBox1.Checked = false;
            }
        }

        private void UpdateRoomState(int State, string a)      // 修改房间状态方法
        {
            string sql1 = string.Format("select RentRoomInfoid from RentRoom where RoomId={0} and RentTime=(select max(RentTime) from RentRoom where RoomId={0})", _fm.RoomId);
            int a1 = Convert.ToInt32(SqlHelp.ExcuteScalar(sql1));//取最近一次开房记录
            if (a1 == 0)
            {
                string sql = string.Format("Update Room set RoomStateId={0} where RoomId={1}", State, _fm.RoomId);
                SqlHelp.ExcuteInsertUpdateDelete(sql);
                _fm.Refresh();//调用刷新显示方法

                string s = string.Format("将房间{0}从[{1}]修改为[{2}]", _fm.RoomName, _fm.RoomStateId, a);        //系统日志
                string sql4 = string.Format("insert into SystemLog values ('{0}','{1}','修改房态','{2}','','')", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), AppInfo.UserName, s);  //14-8-20
                SqlHelp.ExcuteInsertUpdateDelete(sql4);
                Close();
            }
            else
            {
                string sql2 = string.Format("select OrderState from RentRoom where RentRoomInfoid={0}", a1);
                string state1 = SqlHelp.ExcuteScalar(sql2).ToString();
                if (_fm.RoomStateId == "入住" && state1 == "正常")
                {
                    MessageBox.Show("没有可寄托的主单,不能改变其状态!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                else
                {
                    string sql = string.Format("Update Room set RoomStateId={0} where RoomId={1}", State, _fm.RoomId);
                    SqlHelp.ExcuteInsertUpdateDelete(sql);
                    _fm.Refresh();//调用刷新显示方法
                    Close();
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)        // 变为可供
        {
            int a = 1;
            string State = "可供";
            UpdateRoomState(a,State);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)       // 变为预定
        {
            int a = 4;
            string State = "预定";
            UpdateRoomState(a, State);
            string sql3 = string.Format("insert into Destine values('强制预定','{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')",
                roomlx, label2.Text, "","", "", "",           //客人来源 单位  id  ，旅行社、。。
               DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),   //当前预订时间
               DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),   //当前预订时间
               DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),   //当前预订时间
               DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),   //当前预订时间
             "");
            SqlHelp.ExcuteInsertUpdateDelete(sql3);              //修改房间状态为预定
            sql3 = string.Format("Update Room set RoomStateId=4 where RoomId={0}", _fm.RoomId);
            SqlHelp.ExcuteInsertUpdateDelete(sql3);   
            FrmMain.Room = SqlHelp.ExcuteAsAdapter("select * from Room order by RoomName ");  //更新room数据
    
            //DialogResult dr = MessageBox.Show("预定状态改变请到 《预订管理》", "提示信息", MessageBoxButtons.OK);
            //if (dr == DialogResult.OK)
            //{
            //    Close();
            //}
        }

        private void toolStripButton3_Click(object sender, EventArgs e)        // 变为停用
        {
            int a = 6;
            string State = "停用";
            UpdateRoomState(a, State);
        }
   
        private void toolStripButton4_Click(object sender, EventArgs e)   // 变为清理
        {
            int a = 3;
            string State = "清理";
            UpdateRoomState(a, State);
        }
    
        private void toolStripButton5_Click(object sender, EventArgs e)    // 变为占用
        {
            int a = 2;
            string State = "占用";
            UpdateRoomState(a, State);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)  //长包
        {
            int a = 5;
            string State = "长包";
            UpdateRoomState(a, State);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
