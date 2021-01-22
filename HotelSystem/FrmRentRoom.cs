using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
//using Microsoft.Synchronization;

namespace HotelSystem1115
{
    public partial class FrmRentRoom : Form
    {
        private readonly FrmMain _frmmain;
        private string _roomName;           //保存主单房间
        private int _count=2;               //在lvkaiRoom 中用于控制序号
  //      private int _VIPTypeId;
  //      private double _PriceOfDiscount;//存VIP开房的折后价格
        private int _vipGuestId;
        private double _deposit ;             //挂单 如果没挂单==0 否则为上次押金剩余金额
        private string sql;                     // 共有变量
        private double _rentRoomOrder;        //入住价
        private double _rentRoomOrdertrue;    //固定优惠 入住价
        private   bool  _changeUnit ;     // 操作人员 改变单价
        public  DataTable Vipguest = new DataTable(); //房间状态
        private bool lselhy ;            //在会员列表中选择了 会员
        private bool lfirst ;            //第一次 添加房间
        private DialogResult resault;
  //      private string txtDepositstr;           //押金备份
 
        public FrmRentRoom(FrmMain frmmain)
        {
            _frmmain = frmmain;
            InitializeComponent();
            cmbCardType.SelectedIndex = 0;           //初始化下拉表默认值
            cmbGusetsex.SelectedIndex = 0;
            cmbPayType.SelectedIndex = 0;
            cmbsellmember.SelectedIndex = 0;
            txtRentRoomOrder.Text = txtfactUnit.Text;   //实际 入住房屋单价
            label58.Text = "";  //挂单会员
            _changeUnit = false;
            panel1.Controls.Add(textBox14);
            panel1.Location = new Point(18, 128);
            panel1.Visible = false; 
            comboBox3.Text = "普通宾客";
            tabControl4.SelectedIndex = 1;      //在TabControl控件中将指定的选项卡设置为当前选项卡

        }
        private void FrmRentRoom_Load(object sender, EventArgs e)
        {
            var sql = "select * from Client";          //加载宾客来源  旅行社，团购，内部考核，协议个人，酒店管理，会员，
            DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
            cmbGusetsource.DataSource = dt;
            cmbGusetsource.DisplayMember = "clientName";
            cmbGusetsource.ValueMember = "clientId";
            sql = "select * from Sellperson";        //加载营销人员
            DataTable dt2 = SqlHelp.ExcuteAsAdapter(sql);
            DataRow dt3 = dt2.NewRow();
            dt3["SellPersonName"] = "无";
            dt3["SellpersonId"] = -1;
            dt2.Rows.InsertAt(dt3, 0);
            cmbsellmember.DataSource = dt2;
            cmbsellmember.DisplayMember = "SellPersonName";
            cmbsellmember.ValueMember = "SellpersonId";
            skinEngine1.SkinFile = "s (1).ssk";
     
            label60.Text = _frmmain.RoomName;
            label62.Text = _frmmain.RoomTypeName;   //房间类型
            sql = "select * from Vipguest";          //加载宾客来源  旅行社，团购，内部考核，协议个人，酒店管理，会员，
            Vipguest = SqlHelp.ExcuteAsAdapter(sql);
            if (FrmMain.bdestine)  // 有 预订进入
            {
                txtGusetName.Text = _frmmain.GuestName;
                comboBox3.Text=Frmdestine.Pguestsou;  //宾客来源
                txtGusetPhone.Text = Frmdestine.Pphone;
            }
            lselhy = false;
            FrmAdd();
        }
 
        public void FrmAdd()     // 窗体加载
        {
            sql = "select * from Room r inner join RoomType rt on r.RoomTypeId=rt.RoomTypeId where r.RoomId=" + _frmmain.RoomId;
            DataTable dt = SqlHelp.ExcuteAsAdapter(sql);

            _roomName = _frmmain.RoomName;   // = dt.Rows[0]["RoomName"].ToString().Trim();
    //        lbRoomType.Text = dt.Rows[0]["RoomTypeName"].ToString();  //         lbAddRoomType.Text = dt.Rows[0]["RoomTypeName"] + "可供房间";

            double priceOfToday = Convert.ToDouble(dt.Rows[0]["PriceOfToday"]);   // 挂牌单价
            txtfactUnit.Text = string.Format("{0:F2}", priceOfToday);           
            txtRentRoomOrder.Text = txtfactUnit.Text;    // 没优惠先赋值 挂牌价= 入住单价
            _rentRoomOrder = Convert.ToDouble(txtRentRoomOrder.Text); 
            if (FrmSystemMain.cht.onlypreferential == 'y')    //y,n 仅使用每间固定优惠：
            {
                _rentRoomOrder = _rentRoomOrder - FrmSystemMain.cht.preferential;  //固定优惠金额
                txtRentRoomOrder.Text = string.Format("{0:F2}", _rentRoomOrder);
            }
            _rentRoomOrdertrue = _rentRoomOrder;  // 不在改变入住价
            txtDeposit.Text = string.Format("{0:F2}", Convert.ToDouble(dt.Rows[0]["PricrOfForegift"]));   //实际设定押金
  //          txtDepositstr = txtDeposit.Text;
            //是否禁用钟点房复选框
            if (dt.Rows[0]["HoursCost"].ToString()=="N")
            {
                chbtimeRoom.Enabled = false;
            }
            AddRoom();         //添加追加房间
        }
 
        private void AddRoom()    // 追加房间ListView
        {    //添加开单房间
            if (!lselhy)
            {
                lvKaiRoom.Items.Clear();
                var item2 = new ListViewItem();
                lvKaiRoom.Items.Add(item2);
                item2.Text = "1";
                item2.SubItems.Add(_roomName);
                //添加追加房间
                string sql =string.Format("select r.RoomName,r.RoomId from Room r inner join RoomState rs on r.RoomStateId=rs.RoomStateId inner join dbo.RoomType rt on r.RoomTypeId=rt.RoomTypeId where rs.RoomStateName='{0}' and rt.RoomTypeName='{1}'",_frmmain.RoomStateId, _frmmain.RoomTypeName);
                DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
                lvAddRoom.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    if (_roomName != row["RoomName"].ToString()) //如果查询出来的可供房间不等于以选定的房间就加载
                    {
                        var item = new ListViewItem();
                        lvAddRoom.Items.Add(item);
                        item.Tag = row["RoomId"];
                        item.Text = row["RoomName"].ToString();
                    }
                }
            }
            else
            {
                if (lfirst)
                {
                    MessageBox.Show("您还没输入入住客人", "提示", MessageBoxButtons.OK);
                }
                else
                {
                    lfirst = true;
                }
            }
        }
 
        private void checkBox6_CheckedChanged(object sender, EventArgs e)    // 钟点房
        {
            sql = "select PricrOfHours from RoomType rt inner join Room r on r.RoomTypeId= rt.RoomTypeId where RoomId=" + _frmmain.RoomId;
            double pricrOfHours = Convert.ToDouble(SqlHelp.ExcuteScalar(sql));
            cmbtimemoney.Items.Clear();
            if (chbtimeRoom.Checked == true)  //选择 钟点房
            {
                txtdestineday.Text = "0";
                txtdestineday.Enabled = false;
                txtfactUnit.Text = pricrOfHours.ToString();
                checkBox4.Checked = false;
                checkBox4.Enabled = false;
                cmbtimemoney.Items.Add(string.Format("标准：{0}", pricrOfHours));
                cmbtimemoney.SelectedIndex = 0;
                cmbtimemoney.Enabled = true;
                txtDeposit.Text = Convert.ToString(FrmSystemMain.cht.timedep); //钟点房押金
            }
            else
            {
                txtdestineday.Text = "1";
                txtdestineday.Enabled = true;
                cmbtimemoney.Enabled = false;
                checkBox4.Checked = true;
                checkBox4.Enabled = true;
                FrmAdd();
            }
            txtRentRoomOrder.Text = txtfactUnit.Text;   //实际 入住房屋单价
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (lselhy)    //没选中会员
            {
                if (lvAddRoom.SelectedItems.Count > 0)
                {
                    string roomName = lvAddRoom.SelectedItems[0].Text.Trim();
                    var item = new ListViewItem();
                    lvKaiRoom.Items.Add(item);
                    item.Text = "" + (_count++);
                    item.SubItems.Add(roomName);
                    lvAddRoom.SelectedItems[0].Remove(); //从追加房间中移除该项
                }
            }
            else
            {
                MessageBox.Show("您还没输入入住客人", "提示", MessageBoxButtons.OK);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (lvKaiRoom.SelectedItems.Count > 0)
            {
                if (lvKaiRoom.SelectedItems[0].SubItems[1].Text == _roomName)
                {
                    MessageBox.Show("主单房间不能删除", "提示");
                }
                else
                {
                    string roomName = lvKaiRoom.SelectedItems[0].SubItems[1].Text;                 //把选中项的房间明存入变量中
                    int roomId = Convert.ToInt32(lvKaiRoom.SelectedItems[0].SubItems[0].Text);
                    var item = new ListViewItem();
                    lvAddRoom.Items.Add(item);
                    item.Text = roomName;
                    //移除选中项
                    lvKaiRoom.SelectedItems[0].Remove();
                    foreach (ListViewItem l in lvKaiRoom.Items)
                    {
                        if (roomId < Convert.ToInt32(l.Text))
                        {
                            l.Text=""+(roomId++);
                        }
                    }
                    roomId = 0;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)     //确认开房 保存
        {
            if (txtCardId.TextLength == 0 || txtGusetName.Text == null)
            {
                MessageBox.Show("请输入客人身份证姓名信息 ！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            string timestr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            sql = string.Format("select RentRoomInfoId from RentRoom  where GuestCardId ='{0}' and OrderState = '正常' ", txtCardId.Text);  //确认是否已经使用 本身份证
            _vipGuestId = Convert.ToInt32(SqlHelp.ExcuteScalar(sql));
             if (_vipGuestId != 0)
                {
                    MessageBox.Show("本身份证号已经开房，请勿重复使用 ！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
 
            if (comboBox3.Text == "普通客人")    //初次来点
            {
                string sql3 = string.Format("insert into VIPGuest values ('{0}','{1}','{2}',{3},'折扣卡',{4},'{5}',{6},'{7}','{8}','可用','{9}','{10}','{11}',{12},{13},'{14}')",
                txtGusetName.Text,   // 0 [GuestName]  姓名
                cmbGusetsex.Text,
                txtCardId.Text,         //{2}
                0,                 //VIPTypeId  怎么使用？
                    //折扣卡   
                0,                   // {4} ClipMoney  储值金额
                txtGusetCompany.Text,   // 公司
                0,    //  {6} SellMoney
                txtGusetPhone.Text,     //{7}   电话    
                timestr, // {8} 最后编辑 的时间 可以确定
                    //  Estate  可用
                txtRemark.Text,                   //      注释         ======
                textBox22.Text,       //  [GusetAddr]   会员地址，身份证 或者 手填   =
                timestr, // {11} 最后编辑 的时间 可以确定
                1,                     //  RentCount 入住次数
                1,   //RentDay  入住累计时间 单位：天
                FrmSystemMain.cht._fdiscout  //老会员 享受的打折
                );
                SqlHelp.ExcuteInsertUpdateDelete(sql3);
              }
            else    //  会员客户
            {
            }
            string sql1 = string.Format("select VIPGuestId from VIPGuest where CardId='{0}'", txtCardId.Text);//根据房间号取房间Id
            _vipGuestId = Convert.ToInt32(SqlHelp.ExcuteScalar(sql1));
            RentRoom();
         }
        private void RentRoom()      // 开房
        {
            if ((int)txtCardId.TextLength == 18 && txtGusetName.Text != null)
            {
                DateTime time = DateTime.Now;//把当前开房时间存入变量
               // string sql = "select * from RentRoom";
                sql = "select top 2 * from RentRoom order by RentRoomInfoId desc";   //去最后两行
                DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
                int[] array = new int[3];
                int c = 0;
                foreach (DataRow row in dt.Rows)
                {
                    if (time < Convert.ToDateTime(row["RentTime"]))
                    {
                        MessageBox.Show("当前时间差出错，请重新设置系统时间在进行开房~", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    array[c++] = Convert.ToInt32(row["RentRoomInfoId"]);           //取得主结账房间ID
                }
                string rentDurationUnit;//存储开房单位
                if (chbtimeRoom.Checked)
                {
                    rentDurationUnit = "小时";
                }
                else
                {
                    rentDurationUnit = "天";
                }
                if (_deposit != 0)                          //如果押金挂单 加上剩余 押金
                {
                    _deposit += Convert.ToDouble(txtDeposit.Text);
                    txtDeposit.Text = Convert.ToString(_deposit);
                }

                foreach (ListViewItem item in lvKaiRoom.Items)  //选择 几间房间 
                {
                    string sql1 = string.Format("select RoomId from Room where RoomName='{0}'", item.SubItems[1].Text);//根据房间号取房间Id
                    int roomId = Convert.ToInt32(SqlHelp.ExcuteScalar(sql1));
                    string sql3 = string.Format("insert into RentRoom values ('{0}','正常',{1},'{2}',{3},'{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}',0,0,0,'','n','n','n','','{14}','{15}','{16}',0)",
                    txtRentRoomOrder.Text,                     //1  RentRoomOrder  实际成交价
                    roomId,                                         //3    ==============1
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),  //4  [RentTime]
                    txtdestineday.Text,  //估计住宿天数      [RentDuration] ==========3
                    rentDurationUnit,     // 天  ， 小时                           
                    txtfactUnit.Text,    //7 [RentCost] [money]  酒店挂牌价 ===========5
                    txtDeposit.Text,    //   [Deposit] [money]   押金       
                    comboBox3.Text,     //  [GusetType] [nvarchar](20)  会员类型  ====7
                    txtGusetName.Text,   // 10 [GuestName]  姓名
                    cmbCardType.Text,    //   身份证  证件种类  cmbCardType     ======9
                    txtCardId.Text,    //  证件 ID
                    txtGusetPhone.Text,     //13  电话                        ======11
                    _vipGuestId,      //  会员表的id                     =========12
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),  // 最后编辑 的时间 可以确定
                    cmbPayType.Text,                         // 押金类型，现金，信用卡等
                    _roomName,                         //房间号  9-20
                    _frmmain.RoomTypeName

                    );
                    /*	[Receivable] [money] NOT NULL CONSTRAINT [DF_RentRoom_Receivable]  DEFAULT ((0)),
                        [Paid] [money] NOT NULL CONSTRAINT [DF_RentRoom_Paid]  DEFAULT ((0)),
                        [PhoneCost] [money] NOT NULL CONSTRAINT [DF_RentRoom_PhoneCost]  DEFAULT ((0)),
                        [Settlenum] [nvarchar](20) NULL,
                        [Receipt] [nchar](1) NULL,
                        [Free] [nchar](4) NULL,
                        [QuitD] [nchar](1) NULL,
                        [Settlenote] [nvarchar](50) NULL,
                     * DepositType  押金类型，现金，信用卡等 */
                    FrmSystemMain.cht.deposit += Convert.ToDouble(txtDeposit.Text);     //押金计入当前入住合计
                    FrmSystemMain.cht.Openroom++;                                       //开房间数
                    SqlHelp.ExcuteInsertUpdateDelete(sql3);
                    string sql2 = string.Format("update Room Set RoomStateId=2 where RoomName='{0}'", item.SubItems[1].Text); //改变房间状态
                    SqlHelp.ExcuteInsertUpdateDelete(sql2);
                    string syslogstr = string.Format("[主客房号]{0}[宾客姓名]{1}", item.SubItems[1].Text, txtGusetName.Text);

                    if (FrmMain.bdestine)
                    {
                        sql2 = string.Format("insert into SystemLog values ('{0}','{1}','预订开单','{2}','','')",
                            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), AppInfo.UserName, syslogstr); //  记录  开房事件
                    }
                    else
                    {
                        sql2 = string.Format("insert into SystemLog values ('{0}','{1}','散客开单','{2}','','')",
      DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), AppInfo.UserName, syslogstr); //  记录  开房事件
                    }
                    SqlHelp.ExcuteInsertUpdateDelete(sql2);
                    array[0]++; // +1 必然是当前记录的 序列号
                    sql2 = string.Format("insert into  Deposit values ('{0}','{1}','{2}',{3},{4},'开户押金')", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), item.SubItems[1].Text, txtGusetName.Text, Convert.ToDecimal(txtDeposit.Text), array[0]); // 记录 第一次 押金
                    SqlHelp.ExcuteInsertUpdateDelete(sql2);
                    if (_changeUnit)  // 收银员 改变入住单价了 ？
                    {
                        if (Convert.ToDouble(txtRentRoomOrder.Text) < _rentRoomOrder)  // 设定价格 高于 实际价格，所以收银员改小了
                        {   // 价格真的改变了
                            FrmSystemMain.cht.chaneUnit++;  //出现改变价格
                            syslogstr = syslogstr + txtRentRoomOrder.Text + "实际应该设定：" + (string)_rentRoomOrder.ToString();
                            sql = string.Format("insert into SystemLog values ('{0}','{1}','修改入住价格','{2}','','')", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), AppInfo.UserName, syslogstr);//  记录  开房事件
                            SqlHelp.ExcuteInsertUpdateDelete(sql);
                        }
                    }
                }
                MessageBox.Show(" 开房成功! ");
                _frmmain.AddRoomType();
                _frmmain.RoomStateId = "入住";
                _frmmain.GetTab2();
                _frmmain.GetTab2Tob();
                _frmmain.lbRoomType.Text = _frmmain.RoomTypeName;
                Close();
            }
            else
            {
                tabControl4.SelectedIndex = 1;  //选择 不同 选项卡
                MessageBox.Show("请输入客人身份证姓名信息！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
 
        private void txtdestineday_TextChanged(object sender, EventArgs e)   // 根据预住房天数改变实际单价
        {
            //实际单价等于预住天数乘以预设单价
            txtfactUnit.Text = string.Format("{0:F2}", Convert.ToDouble(txtdestineday.Text) * Convert.ToDouble(txtRentRoomOrder.Text));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmRentRoom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                button5_Click(null, null);
            }
            else if(e.KeyCode==Keys.F4)
            {
                button6_Click(null,null);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)  //打折
        {
            string str =(string) comboBox2.SelectedItem;
            double c = Convert.ToDouble(txtfactUnit.Text);
            double i;
            switch (str)
            {
                case "95折":
                    c = c *  0.95;
                     break;
                case "9折":
                    c = c *  0.9;
                    break;
                case "85折":
                    c = c * 0.85;
                    break;
                case "8折":
                    c = c * 0.8;
                    break;
                case "7折":
                    c = c * 0.7;
                    break;
                case "6折":
                    c = c * 0.6;
                    break;
                case "5折":
                    c = c * 0.5;
                    break;
            }
            i = c;
            textBox18.Text = str;
            if (_rentRoomOrdertrue > i)   // 打折价低于 固定优惠价 采用 低的
            {
                str = (string)i.ToString();
                txtRentRoomOrder.Text = str + ".00";
                _rentRoomOrder = i;
            }
            else
            {
                _rentRoomOrder = _rentRoomOrdertrue;
                txtRentRoomOrder.Text = string.Format("{0:F2}",_rentRoomOrder);
            }
        }

        private void tabControl4_Selecting(object sender, TabControlCancelEventArgs e) //选择 选项卡 光标输入点
        {
            txtCardId.Select();  //设定 文本输入框 光标
            ActiveControl = txtCardId;
            if (FrmMain.bdestine)
            {
                comboBox3.Text = Frmdestine.Pguestsou;
            }
            //else
            //{
            //    comboBox3.Text = "普通宾客";
            //}
            panel1.Visible = false;
        }

        private void txtCardId_KeyPress(object sender, KeyPressEventArgs e)   //身份证输入框 
        {
            if ((int)txtCardId.TextLength == 18)  //如果 已经到18位停止收入
                e.Handled = true;
            if ((int)e.KeyChar < 48 || (int)e.KeyChar > 57  )  //只允许输入数字
           {
                if ((int)e.KeyChar != 8 )       // 回格backspace       
                      e.Handled = true;
            }
            Pd();
       }

       private void  Pd()  //判断身份证 到18位 是否是数字
       {
           if ((int)txtCardId.TextLength == 18)
           {
               string c = txtCardId.Text.Substring(17);
               if (c == "0" || c =="2" || c=="4" || c=="6" || c=="8" )
                   cmbGusetsex.Text = "女";
               else
                   cmbGusetsex.Text = "男";
               if (cmbCardType.Text == "身份证")
               {
                   sql = string.Format("select * from VIPGuest where CardId='{0}'", txtCardId.Text);
                   DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
                   if (dt.Rows.Count == 0)
                   {
                       label45.Text = "本客人为首次入住我店";
                       comboBox3.Text = "普通客人";
                   }
                   else
                   {
                       comboBox3.Text = "会员客人";
                       foreach (DataRow row in dt.Rows)
                       {
                           txtGusetName.Text = row["GuestName"].ToString();
                           string s1 = row["RentCount"].ToString();
                           string s2 = row["RentDay"].ToString();
                           label45.Text = string.Format("本客人为我店会员，已经入住 {0} 次，累计入住 {1} 天", s1, s2);
                           txtGusetCompany.Text = row["GusetCompany"].ToString();
                           txtRemark.Text = row["Remark"].ToString();
                           textBox22.Text = row["GusetAddr"].ToString();
                           txtGusetPhone.Text = row["Phone"].ToString();
  
                           //item.SubItems.Add(Convert.ToDateTime(row["BookDatetime"]).ToString("yyyy-MM-dd"));
                           //item.SubItems.Add(row["Estate"].ToString());
                       }
                       sql = string.Format("select Deposit from RentRoom where  GuestCardId ='{0}' and Free='挂单'",  txtCardId.Text);  
                       DataTable dt1 = SqlHelp.ExcuteAsAdapter(sql);
             //????          int _depositint = (int) _deposit;
                       foreach (DataRow row in dt1.Rows)
                       {
                           sql = row["Deposit"].ToString();
                           label58.Text = "挂单押金：" + sql;
                           _deposit = Convert.ToInt32(sql);
                       }
                   }
               }
           }
       }

       private void txtCardId_Leave(object sender, EventArgs e)  // 离开 身份证输入框 事件
       {
           Pd();
       }
       private void button12_Click(object sender, EventArgs e)  //扫描身份证
       {

       }

       private void txtGusetPhone_KeyPress(object sender, KeyPressEventArgs e)  //输入电话号码
       {
           if ((int)e.KeyChar < 48 || (int)e.KeyChar > 57)  //只允许输入数字
           {
               if ((int)e.KeyChar != 8)       // 回格backspace       
                   e.Handled = true;
           }
       }

       private void txtRentRoomOrder_KeyPress(object sender, KeyPressEventArgs e)
       {
           if (FrmSystemMain.cht.hand == 'y')   // 允许改变单价
           {
               int a = (int) e.KeyChar;
               if (a == 8 || (a >= 48 && a <= 57))
               {
               }
               else
               {
                   e.Handled = true;
               }
               _changeUnit = true;
           }
       }

       private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)  //宾客类型 1 普通宾客2 会员宾客
       {
           if (comboBox3.Text == "会员宾客")
           {
               panel1.Visible = true;
               lselhy = false;                //没选中会员
               listView3.Items.Clear();         //          listView3.BeginUpdate();
               foreach (DataRow r in Vipguest.Rows) //取 一行 表数据
               {
                   var item = new ListViewItem
                   {
                       Tag = r["VIPGuestId"],
                       Text = string.Format(Convert.ToString(r["VIPGuestId"]))
                   };
                   item.SubItems.Add(r["GuestName"].ToString());
                   item.SubItems.Add(r["Sex"].ToString());
                   item.SubItems.Add(r["CardId"].ToString());
                   item.SubItems.Add(r["Phone"].ToString());
                   item.SubItems.Add(string.Format("{0:f2}", Convert.ToDouble(r["ClipMoney"])));
                   item.SubItems.Add(r["Discount"].ToString());
                   item.SubItems.Add(r["RentDay"].ToString());
                   listView3.Items.Add(item);
               }
           }
           else  //  if (comboBox3.Text == "普通宾客")
           {
               panel1.Visible = false; 
           }
       }

       private void button2_Click(object sender, EventArgs e)  //取消
       {
           panel1.Visible = false;
           txtGusetName.Text = "";
           cmbGusetsex.Text = "";
           txtCardId.Text = "";
           txtGusetPhone.Text = "";
           textBox18.Text = "";
           lselhy = false;
           comboBox3.Text = "普通宾客";
       }

       private void button1_Click(object sender, EventArgs e)  //确定 选择
       {
           if (lselhy != true)
           {
               resault = MessageBox.Show("您没有选择会员是否退出？", "提示", MessageBoxButtons.YesNo);        //.OK, MessageBoxIcon.Stop);
               if (resault == DialogResult.Yes)
               {
                   button2_Click(null, null);
               }
            }
           else
           {
               panel1.Visible = false;
           }
       }
       private void tabControl4_SelectedIndexChanged(object sender, EventArgs e)  //tab 选择不同的页面
       {
           panel1.Visible = false;
       }
       private void listView3_SelectedIndexChanged(object sender, EventArgs e)  //单击listview 行事件 选择行数据
       {
           if ( listView3.SelectedIndices.Count > 0)
           {
               ListView.SelectedIndexCollection c = listView3.SelectedIndices;
        //       string ss = listView3.Items[c[0]].Text;   // 编号
                txtGusetName.Text = listView3.FocusedItem.SubItems[1].Text;
                cmbGusetsex.Text = listView3.FocusedItem.SubItems[2].Text;
                txtCardId.Text = listView3.FocusedItem.SubItems[3].Text;
                txtGusetPhone.Text = listView3.FocusedItem.SubItems[4].Text;
                textBox18.Text = listView3.FocusedItem.SubItems[6].Text;
               cmbGusetsource.Text = "会员宾客";
               lselhy = true;
           }
       }
       private void button7_Click(object sender, EventArgs e)  //查询 会员  
       {
           lselhy = false;
           int result = 0; //result 
           try
           {        //如果位数超过4的话，请选用Convert.ToInt32() 和int.Parse()
               int resulttest = int.Parse(textBox14.Text);  //只是测试 是否我数字   result = Convert.ToInt16(message);
           }
           catch
           {
               result = 1;
           }
           if (button7.Text != null)
           {
               bool first = true;
               for (int i = 0; i < listView3.Items.Count; i++)
               {
                   if (listView3.Items[i].SubItems[result].Text == textBox14.Text.Trim()) 
                   {
                       if (first)
                       {
                           listView3.Items[i].Focused = true;
                           listView3.Items[i].Selected = true;
                           listView3.Items[i].EnsureVisible();
                           listView3.Items[i].BackColor = Color.DeepSkyBlue;//选定状态﹐为蓝色
                           first = false;
                           txtGusetName.Text = listView3.Items[i].SubItems[1].Text;
                           cmbGusetsex.Text= listView3.Items[i].SubItems[2].Text;
                           txtCardId.Text= listView3.Items[i].SubItems[3].Text;
                           txtGusetPhone.Text= listView3.Items[i].SubItems[4].Text;
                           textBox18.Text = listView3.Items[i].SubItems[1].Text;
                           cmbGusetsource.Text = "会员宾客";
                           lselhy = true;
                       }
                       else
                       {
                           listView3.Items[i].BackColor = Color.White;
                       }
                   }
               }
           }
       }

    }  //类  括号
}
