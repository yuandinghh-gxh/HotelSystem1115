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
    public partial class FrmDestineregister : Form
    {
        private Frmdestine _fd;
        private bool _b,bfristin=true;
        private string lstr;
        private int li;
        private  DataTable DestineTable = new DataTable(); //房间预订表
  //      public  DataTable Room = new DataTable();

        public FrmDestineregister(Frmdestine fd,bool b)
        {
            _b = b;
            _fd = fd;
            InitializeComponent();
        }

        private void FrmDestineregister_Load(object sender, EventArgs e)
        {
            cmbsource.DataSource = FrmMain.Clientdt;  //加载宾客来源
            cmbsource.DisplayMember = "clientName";
            cmbsource.ValueMember = "clientId";
    //DataRow row = FrmMain.Roomlx.NewRow(); // 设想 好像不行  //comboBox2.Text = row["RoomTypeName"].ToString(); //comboBox2_SelectedIndexChanged(null, null);
            lstr = "select * from RoomType";     //不能 使用  静态 数据FrmMain.Roomlx;
            DataTable dt2 = SqlHelp.ExcuteAsAdapter(lstr);     // 房间类型 加载预订规格
            DataRow row = dt2.NewRow();                     //前面加一个空白 行
            row["RoomTypeId"] = 0;
            row["RoomTypeName"] = "";
            dt2.Rows.InsertAt(row, 0);
            comboBox2.DataSource = dt2;
            comboBox2.DisplayMember = "RoomTypeName";
            comboBox2.ValueMember = "RoomTypeId";

            dateTimePicker1.Text = DateTime.Now.ToString("yyyy年MM月dd日 HH时mm分");
            dateTimePicker2.Text = DateTime.Now.AddDays(1).ToString("yyyy年MM月dd日 12时00分");
            dateTimePicker3.Text = DateTime.Now.AddHours(1).ToString("yyyy年MM月dd日 HH时mm分");
    

            if (_b)  
            {
                Text = "修改预定登记";
                txtGuestName.Text = _fd.listView1.SelectedItems[0].SubItems[3].Text;
                txtPhone.Text = _fd.listView1.SelectedItems[0].SubItems[5].Text;
                cmbsource.Text = _fd.listView1.SelectedItems[0].SubItems[7].Text;
                comboBox2.Text = _fd.listView1.SelectedItems[0].SubItems[1].Text;
                cmbRoomId.Text = _fd.listView1.SelectedItems[0].SubItems[2].Text;
                dateTimePicker1.Text = _fd.listView1.SelectedItems[0].SubItems[8].Text;
                dateTimePicker2.Text = _fd.Pyltime;
                dateTimePicker3.Text = _fd.listView1.SelectedItems[0].SubItems[9].Text;
                textBox7.Text = _fd.listView1.SelectedItems[0].SubItems[6].Text;
                richTextBox1.Text = _fd.listView1.SelectedItems[0].SubItems[10].Text;
                button1.Enabled = false;
                var item = new ListViewItem();
                listView1.Items.Clear();
                listView1.Items.Add(item);
                item.Text = txtGuestName.Text;

                item.SubItems.Add(dateTimePicker1.Text);
                item.SubItems.Add(txtPhone.Text);
                bfristin = true;
                cmbRoomId.Text = _fd.listView1.SelectedItems[0].SubItems[2].Text;
            }
            else
            {      
                Showdestine();  //显示已经预订宾客
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)  // 根据预订规格加载房间类型
        {
            int ii;
            if (comboBox2.SelectedIndex != 0) //加载房间类型
            {
                lstr = comboBox2.Text;
                li = FrmMain.Getdatalxid(lstr);  //取得房间类型
                var cbi1 = new ComboBoxItem();
                cmbRoomId.Items.Clear();
  

                foreach (DataRow r in FrmMain .Room.Rows) //取 房间预订
                {
                    if (li == Convert.ToInt32(r["RoomTypeId"]))
                    {
                        ii = Convert.ToInt32(r["Roomstateid"]); //取得当前房间状态信息   
                        if (ii == 1) // 可供 
                        {
                            cbi1.Text = r["roomname"].ToString();
                            cbi1.Value = r["roomname"].ToString();
                            cmbRoomId.Items.Add(cbi1);
                        }
                    }
                }
                if (_b)
                {
                    cbi1.Text = _fd.listView1.SelectedItems[0].SubItems[2].Text;   // 修改 加上这个已经预订的
                    cbi1.Value = _fd.listView1.SelectedItems[0].SubItems[2].Text;
                    cmbRoomId.Items.Add(cbi1);
                    if (bfristin)
                    {
                        bfristin = false;
                        cmbRoomId.Text = _fd.listView1.SelectedItems[0].SubItems[2].Text;
                    }
                }
            }
        }

        private void cmbRoomId_SelectedIndexChanged(object sender, EventArgs e)   // 房间编号
        {
        }

        private void button2_Click(object sender, EventArgs e)    // 保存
        {
            ifDistine();  
        }
   
        private void ifDistine()   // 判断  保存 预订数据
        {
            if (Convert.ToDateTime(dateTimePicker1.Text )>=Convert.ToDateTime( dateTimePicker2.Text))
            {
                MessageBox.Show("时间错误，请检查！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else
            {

                if (_b)  // b = tree  为 编辑 预定房间
                {
                    if (cmbRoomId.SelectedIndex == 0 || comboBox2.SelectedIndex == 0)
                    {
                        MessageBox.Show("请先选择房间后在预定!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    UpdateDestine();  //修改
                }
                else
                {
                    AddDestine();  // 新预订
                }
            }
        }
     
        private void UpdateDestine()   // 修改预定
        {
                lstr = string.Format("Update Destine set RoomType='{0}',RoomId='{1}',GuestName='{2}',Phone='{3}',DestineNumber='{4}',ClientId={5},ydtime='{6}',yltime='{7}',bltime='{8}',Remak='{9}' where DestineId={10}",
                    comboBox2.Text,
                    cmbRoomId.Text,
                    txtGuestName.Text,
                    txtPhone.Text,
                    textBox7.Text,
                    cmbsource.SelectedValue,
                    Convert.ToDateTime(dateTimePicker1.Text),
                    Convert.ToDateTime(dateTimePicker2.Text),
                    Convert.ToDateTime(dateTimePicker3.Text),
                    richTextBox1.Text,
                    _fd.listView1.SelectedItems[0].Tag);
                   SqlHelp.ExcuteInsertUpdateDelete(lstr);
                if (cmbRoomId.Text != _fd.listView1.SelectedItems[0].SubItems[2].Text)   //如果 换房间要修改房间状态
                {
                    string sql2 = string.Format("Update Room set RoomStateId=4 where RoomName={0}", cmbRoomId.Text);  //预订
                    SqlHelp.ExcuteInsertUpdateDelete(sql2);
                    sql2 = string.Format("Update Room set RoomStateId=1 where RoomName='{0}'", _fd.listView1.SelectedItems[0].SubItems[2].Text);  //可供
                    SqlHelp.ExcuteInsertUpdateDelete(sql2);
                    FrmMain .Room = SqlHelp.ExcuteAsAdapter("select * from Room order by RoomName ");  //更新room数据
                }

                string s = string.Format("修改预定房间{0}--{1}", _fd.listView1.SelectedItems[0].SubItems[2].Text, cmbRoomId.Text);    //系统日志
                lstr = string.Format("insert into SystemLog values ('{0}','{1}','预定管理','{2}','','')", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), AppInfo.UserName, s);
                SqlHelp.ExcuteInsertUpdateDelete(lstr);
                _fd.Showdestine();      //调用刷新
                Close();
        }
  
        private void AddDestine()   //添加 预订 房间
        {
            int roomId = 0;
  
            if (listView2.Items.Count == 0)
            {
                MessageBox.Show("请先选择房间后在预定!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }
            else
            {
                foreach (ListViewItem item in listView2.Items)
                {
                    foreach (DataRow r in FrmMain.Room.Rows)     //根据房间名字（8801等），取得房间的状态，在从状态表roomstate 取得状态字符串 （可供入住等）
                    {
                        if (item.SubItems[1].Text == r["RoomName"].ToString())
                        {
                            int ii = Convert.ToInt32(r["RoomStateid"]);
                            roomId = Convert.ToInt32(r["RoomId"]);
                            lstr = FrmMain.Getdatazt(ii);
                            break;
                        }
                    }
                    if (lstr == "可供")
                    {                   //   int roomTypeId = FrmMain.Getdatalxid(item.Text);         //插入预定信息
                        string sql3 =string.Format("insert into Destine values('预定中','{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')",
                            comboBox2.Text,
                            cmbRoomId.Text,
                            txtGuestName.Text,
                            txtPhone.Text,
                            textBox7.Text,       //预订单号 
                            cmbsource.Text,           //客人来源 单位  id  ，旅行社、。。
                           Convert.ToDateTime(dateTimePicker1.Text),
                           Convert.ToDateTime(dateTimePicker2.Text),
                           Convert.ToDateTime(dateTimePicker3.Text),
                           DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),   //当前预订时间
                           richTextBox1.Text);
                        SqlHelp.ExcuteInsertUpdateDelete(sql3);              //修改房间状态为预定
                        sql3 = string.Format("Update Room set RoomStateId=4 where RoomId={0}", roomId);
                        SqlHelp.ExcuteInsertUpdateDelete(sql3);     //系统日志
                        string s = string.Format("[房间号]{0}以预定，[客人姓名]{1}", item.SubItems[1].Text,txtGuestName.Text);
                        sql3 = string.Format("insert into SystemLog values ('{0}','{1}','预定管理','{2}','','')", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), AppInfo.UserName, s);
                        SqlHelp.ExcuteInsertUpdateDelete(sql3);
                        FrmMain.Room = SqlHelp.ExcuteAsAdapter("select * from Room order by RoomName ");  //更新room数据
                        _fd.Showdestine();       //调用刷新
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("只有可供房可以预定!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

            if (txtGuestName.Text == "")
            {
                MessageBox.Show("请输入客人姓名 !", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }
            if (txtPhone.Text == "")
            {
                MessageBox.Show("请输入客人电话号码 !", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }
            if (cmbsource.Text == "")
            {
                MessageBox.Show("请输入客人来源 !", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }
            if (textBox7.Text == "")
            {
                MessageBox.Show("请输入预订单号 !", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }
            if (cmbRoomId.Text == "" || comboBox2.Text == "" )
            {
                MessageBox.Show("请选择预定规格和房间编号!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                foreach (ListViewItem item in listView2.Items)
                {
                    if (item.SubItems[1].Text == cmbRoomId.Text)
                    {
                        return;
                    }
                }
                ListViewItem item1 = new ListViewItem();
                listView2.Items.Add(item1);
                item1.Text = comboBox2.Text;
                item1.SubItems.Add(cmbRoomId.Text);
            }
        }

        private void listView2_DoubleClick(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count == 0)
            {
            }
            else
            {
                listView2.SelectedItems[0].Remove();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime t1 = Convert.ToDateTime(dateTimePicker1.Text);
            dateTimePicker3.Text = t1.AddHours(1).ToString("yyyy年MM月dd日 HH时mm分");
        }
          private  void  Showdestine()  //显示 已经预订 信息
            {
                DestineTable = SqlHelp.ExcuteAsAdapter("select * from Destine");
                foreach (DataRow r in DestineTable.Rows) //取 房间预订
                {
                    var item = new ListViewItem();
                    listView1.Items.Add(item);
                    item.Tag = r["DestineId"];
                    item.Text = r["GuestName"].ToString();
                    item.SubItems.Add(r["ydtime"].ToString());
                    item.SubItems.Add(r["Phone"].ToString());
                }
            }
    }
       public class ComboBoxItem
        {
            private string _text = null;
            private object _value = null;
            public string Text { get { return _text; } set { _text = value; } }
            public object Value { get { return _value; } set { _value = value; } }
            public override string ToString()
            {
                return _text;
            }
        }
}
