 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
namespace HotelSystem1115  {
    public partial class FrmSystemMain : Form      {
        private FrmMain _fm;
   //     private Array Roomtype;
         ArrayList  Roomtype = new ArrayList();  //?????几种房屋类型
         private DataTable roomtypedt = new DataTable();  //房间类型 
         private DataTable roomdt = new DataTable();//实际房间 总数
         public static Charging cht = new Charging();  // 结构也要实例化
        public FrmSystemMain(FrmMain fm)         {
            _fm = fm;
            InitializeComponent();
            SettleRoomTypeName.SelectedIndex = 0;
        }
        private void FrmSystemintercalate_Load(object sender, EventArgs e)
        {
            readsys();
             Readjfset();          //设置 读出的 计费设置数据
            AddListView1();   //加载ListView1   房间类型  
            AddListView2();             //加载ListView2    实际 房间 总总数
            AddGoods();             //商品设置选项卡加载
            Serve();      //服务生设置选项卡加载
            Client();              //加载客户设置
            Sellperson();
            VIPType();              //加载VIP设置
            VIPRoomType();
            AddAdmin();               //加载权限组
            AddUsers();
            AddBM();
     
        }
           public void Readjfset()              //设置 读出的 计费设置数据 
           { 
            JFcoxfullabate.Checked = false;
            comboBox9.Text = cht.desp;  //打折比例
            if (cht.alldz == 'y')
            {
                JFcoxfullabate.Checked = true;
            }
            checkBox14.Checked = false;      //y,n 仅使用每间固定优惠：
            textBox5.Text = cht.preferential.ToString();  //固定优惠
            if (cht.onlypreferential == 'y')
            {
                checkBox14.Checked = true;
            }
            checkBox10.Checked = false;
            if (cht.zero == 'y')
            {
                checkBox10.Checked = true;
                comboBox1.Text = cht.jq;  //精确到个位
            }
            checkBox38.Checked = false;
            if (cht.vip == 'y')
            {
                checkBox38.Checked = true;
                comboBox10.Text = cht._fdiscout;  ////     回头客会员优惠打折：
            }
            checkBox39.Checked = false;         //免单
            if (cht.Freebool == 'y')
            {
                checkBox39.Checked = true;
            }
            checkBox9.Checked = false;      //  开单时允许手动更改房间费打折比率和单价
            if (cht.hand == 'y')
            {
                checkBox9.Checked = true;
            }
            checkBox11.Checked = false;      //  不允许手动输入卡号(对开单和结账有效)   
            if (cht.nohand == 'y')
            {
                checkBox11.Checked = true;
            }
            checkBox12.Checked = false;      //  允许登记时修改房价
            if (cht.oneday == 'y')
            {
                checkBox12.Checked = true;
            }
            dateTimePicker1.Text = cht.newday  ;    //之后按新的一天开始计费
  //          DateTime dateTime1 = Convert.ToDateTime(cht.newday);
            dateTimePicker2.Text = cht.quitroom;      //之后计费天数自动增加半天
            dateTimePicker3.Text = cht.addday;    // 之后计费天数自动增加一天
             numericUpDown2.Value=(decimal) cht.fen;
             checkBox13.Checked = false;      //  住店时间不足一天的按一天计费
             if (cht.oneday == 'y')
             {
                 checkBox13.Checked = true;
             }
             numericUpDown3.Value = (decimal)cht.openf;        //开房后 多长时间开始计费
             numericUpDown4.Value = (decimal)cht.minjf;      //最少按  小时计费，小于这个时间按钟点房计费
             numericUpDown5.Value = (decimal)cht.supertime;      //不足一小时，但超过多少分钟按一小时计费
             numericUpDown6.Value = (decimal)cht.superhalf;       //不足半小时，但超过多少分钟按半小时计费
             numericUpDown7.Value = (decimal)cht.hour;     //超过多少小时 转为 全天房价 计费 
             dateTimePicker4.Text = cht.supertimetoday;   //超过什么时间按 全天计费
             label40.Visible = false;

             textBox17.Text = Convert.ToString(FrmSystemMain.cht.timedep); //钟点房押金
           }
        #region   初始化所有 设置中的tab 表等
        public void AddBM()  /// 加载部门
        {
            string sql = "select * from BM";
            DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
            lvBM.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                ListViewItem item = new ListViewItem();
                lvBM.Items.Add(item);
                item.Tag = row["BMId"];
                item.Text = row["BMNumber"].ToString();
                item.SubItems.Add(row["BMName"].ToString());
            }
        }
        public void AddUsers()   /// 加载用户
        {
            int count = 0;
            string sql = "select * from Users s inner join Admin a on s.AdminId=a.AdminId";
            roomtypedt   = SqlHelp.ExcuteAsAdapter(sql);
     //       DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
            lvUsers.Items.Clear();
            foreach (DataRow row in roomtypedt.Rows)          {
                count++;
                ListViewItem item = new ListViewItem();
                lvUsers.Items.Add(item);
                item.Tag = row["UserId"];
                item.Text = row["AdminName"].ToString();
                item.SubItems.Add(row["UserName"].ToString());
                item.SubItems.Add(row["LoginName"].ToString());
                item.SubItems.Add(row["State"].ToString());
            }
            CZYtxtAnnal.Text = string.Format("共{0}条记录", count);
        }
        public void AddAdmin()   /// 加载权限组
        {
            string sql = "select * from Admin";
            DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
            lvadmin.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                ListViewItem item = new ListViewItem();
                lvadmin.Items.Add(item);
                item.Tag = row["AdminId"];
                item.Text = row["AdminName"].ToString();
            }
        }
        public void VIPRoomType()      /// 加载房间费打折
        {
            string sql = "select * from RoomType";
            DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
            lvVIPDiscount.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                ListViewItem item = new ListViewItem();
                lvVIPDiscount.Items.Add(item);
                item.Tag = row["RoomTypeId"];
                item.Text = "             "+row["RoomTypeName"].ToString();
                item.SubItems.Add(string.Format("{0:F0}", row["PriceOfToday"]));
            }
        }
        public void VIPType()    /// 加载VIP类型
        {
            string sql = "select * from VIPType";
            DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
            lvVIPType.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                ListViewItem item = new ListViewItem();
                lvVIPType.Items.Add(item);
                item.Tag = row["VIPTypeId"];
                item.Text = "              "+row["VIPName"].ToString();
                item.SubItems.Add(string.Format("{0:F0}", row["VIPAbate"]));
            }
        }
        public void Client()   /// 加载客户来源
        {
            int count = 0;
            string sql = "select * from Client";
            DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
            lvClient.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                count++;
                ListViewItem item = new ListViewItem();
                lvClient.Items.Add(item);
                item.Tag = row["clientId"];
                item.Text = "                        "+row["clientName"].ToString();
            }
            KHSourcetxtAnnal.Text = string.Format("共{0}条记录", count);
        }
        public void Sellperson()    /// 加载营销人员
        {
            int count = 0;
            string sql = "select * from Sellperson";
            DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
            lvSellperson.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                count++;
                ListViewItem item = new ListViewItem();
                lvSellperson.Items.Add(item);
                item.Tag = row["SellpersonId"];
                item.Text = "                        " + row["SellpersonName"].ToString();
            }
            KHentangletxtAnnal.Text = string.Format("共{0}条记录", count);
        }
        private void Serve()    /// 加载服务生设置选项卡
        {
            int count = 0;
            //加载服务等级
            AddServeGrade();
            //加载服务生
            count=AddServeMember(count);
            FWStxtWaiterAnnal.Text = string.Format("共{0}条记录", count);
        }
        public int AddServeMember(int count)    /// 加载服务生
        {
            string sql = "select * from ServeMember sm inner join RoomType rt on sm.RoomTypeId=rt.RoomTypeId inner join ServeGrade sg on sm.ServeGradeId=sg.ServeGradeId";
            DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
            lvServeMember.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                count++;
                ListViewItem item = new ListViewItem();
                lvServeMember.Items.Add(item);
                item.Tag = row["ServeMemberId"];
                item.Text = row["ServeNumber"].ToString();
                item.SubItems.Add(row["ServeName"].ToString());
                item.SubItems.Add(row["Spell"].ToString());
                item.SubItems.Add(row["Sex"].ToString());
                item.SubItems.Add(row["RoomTypeName"].ToString());
                item.SubItems.Add(row["PositionQuality"].ToString());
                item.SubItems.Add(row["GradeName"].ToString());
                item.SubItems.Add(row["PhoneNumber"].ToString());
                item.SubItems.Add(row["CardId"].ToString());
            }
            return count;
        }
        public void AddServeGrade()          /// 加载服务等级
        {
            string sql = "select * from ServeGrade";
            DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
            
            lvServeGrade.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                ListViewItem item = new ListViewItem();
                lvServeGrade.Items.Add(item);
                item.Tag = row["ServeGradeId"];
                item.Text = row["GradeNumber"].ToString();
                item.SubItems.Add(row["GradeName"].ToString());
            }
            //加载下拉表
            DataRow r = dt.NewRow();
            r["ServeGradeId"] = -1;
            r["GradeName"] = "所有服务生";
            dt.Rows.InsertAt(r, 0);
            cboServeGrade.DataSource = dt;
            cboServeGrade.DisplayMember = "GradeName";
            cboServeGrade.ValueMember = "ServeGradeId";
        }
        private void AddGoods()     /// 商品设置加载
        {
            //加载商品类别
            AddGoodsType();
            //加载商品信息
            AddGoodsName();
        }
        public void AddGoodsName()    /// 加载商品信息
        {
            string sql2 = "select * from Goods g inner join GoodsClass gc on g.GoodsClassId=gc.GoodsClassId";
            DataTable dt2 = SqlHelp.ExcuteAsAdapter(sql2);
            lvGoodsName.Items.Clear();
            foreach (DataRow r in dt2.Rows)
            {
                ListViewItem item = new ListViewItem();
                lvGoodsName.Items.Add(item);
                item.Tag = r["GoodsId"];
                item.Text = r["GoodsNumber"].ToString();
                item.SubItems.Add(r["GoodsName"].ToString());
                item.SubItems.Add(string.Format("{0:F0}", r["Price"]));
                item.SubItems.Add(r["GoodsClass"].ToString());
                item.SubItems.Add(r["Stock"].ToString());
                item.SubItems.Add(string.Format("{0:F0}", r["encash"]));
                item.SubItems.Add(r["encashBadge"].ToString());
            }
        }
        public void AddGoodsType()          /// 加载商品类别
        {
            //加载商品类别
            string sql = "select * from GoodsClass";
            DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
            lvGoodsClass.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                ListViewItem item = new ListViewItem();
                lvGoodsClass.Items.Add(item);
                item.Tag = row["GoodsClassId"];
                item.Text = row["GoodsClassNumber"].ToString();
                item.SubItems.Add(row["GoodsClass"].ToString());
                item.SubItems.Add(row["Affordserve"].ToString());
            }
            //加载下拉列表
            DataRow row2 = dt.NewRow();
            row2["GoodsClassId"] = -1;
            row2["GoodsClass"] = "所有项目";
            dt.Rows.InsertAt(row2, 0);
            coxGoodsTypeSift.DataSource = dt;
            coxGoodsTypeSift.DisplayMember = "GoodsClass";
            coxGoodsTypeSift.ValueMember = "GoodsClassId";
        }
        public void AddListView1()      /// 加载房间类型ListView1
        {
              string sql = "select * from RoomType";
              roomtypedt = SqlHelp.ExcuteAsAdapter(sql);
   //         DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
 
            listView1.Items.Clear();
    //        foreach (DataRow row in dt.Rows)
            foreach (DataRow row in roomtypedt.Rows)
            {
                ListViewItem item = new ListViewItem();
                listView1.Items.Add(item);
                item.Tag = row["RoomTypeId"];
                Roomtype.Clear();
                Roomtype.Add(item.Tag);   // 添加 文件类型
                item.Text = row["RoomTypeName"].ToString();
                item.SubItems.Add(string.Format("{0:F0}",row["PriceOfToday"]));//预设单价
                item.SubItems.Add(string.Format("{0:F0}",row["PricrOfTodayHalf"]));//预设半价
                item.SubItems.Add(string.Format("{0:F0}",row["PricrOfForegift"]));//预设押金
                item.SubItems.Add(string.Format("{0:F0}",row["PricrOfHours"]));//钟点价
                item.SubItems.Add(string.Format("{0:F0}",row["Bed"]));//床位
                item.SubItems.Add(string.Format("{0:F0}",row["HoursCost"]));//小时计费
            }
        }
        public void AddListView2()        /// 加载ListView2
        {
            //加载下拉列表
           string sql = "select * from RoomType";
           roomtypedt = SqlHelp.ExcuteAsAdapter(sql);
//             DataTable dt = SqlHelp.ExcuteAsAdapter(sql);

            DataRow row = roomtypedt.NewRow();
            row["RoomTypeId"] = -1;
            row["RoomTypeName"] = "所有房间";
            roomtypedt.Rows.InsertAt(row, 0);
            coxRoomStyleSift.DataSource = roomtypedt;
            coxRoomStyleSift.DisplayMember = "RoomTypeName";
            coxRoomStyleSift.ValueMember = "RoomTypeId";

            string sql2 = "select * from Room r inner join RoomType rt on r.RoomTypeId=rt.RoomTypeId inner join RoomState rs on r.RoomStateId=rs.RoomStateId ORDER BY RoomName";   //ORDER BY RoomName  14-8-20
            roomdt  = SqlHelp.ExcuteAsAdapter(sql2);

            listView2.Items.Clear();
            foreach (DataRow row2 in roomdt.Rows)  //??????
            {
                ListViewItem item = new ListViewItem();
                listView2.Items.Add(item);
                item.Tag = row2["RoomTypeId"];
                item.Text = row2["RoomName"].ToString();
                item.SubItems.Add(row2["RoomTypeName"].ToString());
                item.SubItems.Add(row2["RoomStateName"].ToString());
                item.SubItems.Add(row2["Position"].ToString());
                item.SubItems.Add(row2["PhoneNumber"].ToString());
                item.SubItems.Add(row2["DoorLook"].ToString());
            }
        }
 #endregion
        private void FbtnSave_Click(object sender, EventArgs e)
        {
            MessageBox.Show("保存成功!", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        public void coxRoomStyleSift_SelectedIndexChanged(object sender, EventArgs e)    /// 当coxRoomStyleSift下拉框的值改变时发生
        {
            int count = 0;//记录房间数
            //加载ListView2
            if (coxRoomStyleSift.SelectedIndex == 0)
            {
                string sql2 = "select * from Room r inner join RoomType rt on r.RoomTypeId=rt.RoomTypeId inner join RoomState rs on r.RoomStateId=rs.RoomStateId";
                DataTable dt2 = SqlHelp.ExcuteAsAdapter(sql2);
                listView2.Items.Clear();
                foreach (DataRow row2 in dt2.Rows)
                {
                    count++;
                    ListViewItem item = new ListViewItem();
                    listView2.Items.Add(item);
                    item.Tag = row2["RoomTypeId"];
                    item.Text = row2["RoomName"].ToString();
                    item.SubItems.Add(row2["RoomTypeName"].ToString());
                    item.SubItems.Add(row2["RoomStateName"].ToString());
                    item.SubItems.Add(row2["Position"].ToString());
                    item.SubItems.Add(row2["PhoneNumber"].ToString());
                    item.SubItems.Add(row2["DoorLook"].ToString());
                }
                FtxtRoomTypeAnnal.ForeColor = Color.Red;
                FtxtRoomTypeAnnal.Text = string.Format("共{0}条记录", count);
            }
            else
            {
                string sql2 = "select * from Room r inner join RoomType rt on r.RoomTypeId=rt.RoomTypeId inner join RoomState rs on r.RoomStateId=rs.RoomStateId where r.RoomTypeId=" + coxRoomStyleSift.SelectedValue;
                DataTable dt2 = SqlHelp.ExcuteAsAdapter(sql2);
                listView2.Items.Clear();
                foreach (DataRow row2 in dt2.Rows)
                {
                    count++;
                    ListViewItem item = new ListViewItem();
                    listView2.Items.Add(item);
                    item.Tag = row2["RoomTypeId"];
                    item.Text = row2["RoomName"].ToString();
                    item.SubItems.Add(row2["RoomTypeName"].ToString());
                    item.SubItems.Add(row2["RoomStateName"].ToString());
                    item.SubItems.Add(row2["Position"].ToString());
                    item.SubItems.Add(row2["PhoneNumber"].ToString());
                    item.SubItems.Add(row2["DoorLook"].ToString());
                }
                FtxtRoomTypeAnnal.ForeColor = Color.Red;
                FtxtRoomTypeAnnal.Text = string.Format("共{0}条记录", count);
            }
        }
        private void FbtnAddStyle_Click(object sender, EventArgs e)      /// 添加房间类型
        {
            bool b = true;
            SystemAddRoomType RoomType = new SystemAddRoomType(this,b);
            RoomType.ShowDialog();
        }
        private void FbtnUpdateStyle_Click(object sender, EventArgs e)    /// 修改房间类型
        {
            if (listView1.SelectedItems.Count == 0)
            {
            }
            else
            {
                bool b = false;
                SystemAddRoomType RoomType = new SystemAddRoomType(this,b);
                RoomType.ShowDialog();
            }
        }
        private void FbtnDeleteStyle_Click(object sender, EventArgs e)   /// 删除房间类型
        {
            if (listView1.SelectedItems.Count == 0)
            {
            }
            else
            {
                string sql = string.Format("delete RoomType where RoomTypeId={0}", listView1.SelectedItems[0].Tag);
                SqlHelp.ExcuteInsertUpdateDelete(sql);
                AddListView1();
            }
        }
        private void FbtnOddAdd_Click(object sender, EventArgs e)    /// 单个添加房间
        {
            bool b = false;
            FrmSystemAddRoom addroom = new FrmSystemAddRoom(this,b);
            addroom.ShowDialog();
        }
        private void FbtnUpdateRoom_Click(object sender, EventArgs e)    /// 修改房间
        {
            if (listView2.SelectedItems.Count == 0)
            {
            }
            else
            {
                bool b = true;
                FrmSystemAddRoom addroom = new FrmSystemAddRoom(this, b);
                addroom.ShowDialog();
            }
        }
        private void FbtnDeleteRoom_Click(object sender, EventArgs e)       /// 删除房间
        {
            if (listView2.SelectedItems.Count == 0)
            {
            }
            else
            {
                string sql = string.Format("delete Room where RoomName='{0}'", listView2.SelectedItems[0].Text);
                SqlHelp.ExcuteInsertUpdateDelete(sql);
                AddListView2();
            }
        }
        private void FbtnBatchAdd_Click(object sender, EventArgs e)      /// 批量添加房间
        {
            FrmSystemBatchAdd fsba = new FrmSystemBatchAdd(this);
            fsba.ShowDialog();
        }
        private void coxGoodsTypeSift_SelectedIndexChanged(object sender, EventArgs e)      /// 当coxGoodsTypeSift下拉表的值发生改变时
        {
            int count = 0;//记录商品数
            if (coxGoodsTypeSift.SelectedIndex == 0)
            {
                string sql2 = "select * from Goods g inner join GoodsClass gc on g.GoodsClassId=gc.GoodsClassId";
                DataTable dt2 = SqlHelp.ExcuteAsAdapter(sql2);
                lvGoodsName.Items.Clear();
                foreach (DataRow r in dt2.Rows)
                {
                    count++;
                    ListViewItem item = new ListViewItem();
                    lvGoodsName.Items.Add(item);
                    item.Tag = r["GoodsId"];
                    item.Text = r["GoodsNumber"].ToString();
                    item.SubItems.Add(r["GoodsName"].ToString());
                    item.SubItems.Add(string.Format("{0:F0}", r["Price"]));
                    item.SubItems.Add(r["GoodsClass"].ToString());
                    item.SubItems.Add(r["Stock"].ToString());
                    item.SubItems.Add(string.Format("{0:F0}", r["encash"]));
                    item.SubItems.Add(r["encashBadge"].ToString());
                }
                StxtRoomTypeAnnal.ForeColor = Color.Red;
                StxtRoomTypeAnnal.Text = string.Format("共{0}条记录", count);
            }
            else
            {
                string sql2 = "select * from Goods g inner join GoodsClass gc on g.GoodsClassId=gc.GoodsClassId where g.GoodsClassId="+coxGoodsTypeSift.SelectedValue;
                DataTable dt2 = SqlHelp.ExcuteAsAdapter(sql2);
                lvGoodsName.Items.Clear();
                foreach (DataRow r in dt2.Rows)
                {
                    count++;
                    ListViewItem item = new ListViewItem();
                    lvGoodsName.Items.Add(item);
                    item.Tag = r["GoodsId"];
                    item.Text = r["GoodsNumber"].ToString();
                    item.SubItems.Add(r["GoodsName"].ToString());
                    item.SubItems.Add(string.Format("{0:F0}", r["Price"]));
                    item.SubItems.Add(r["GoodsClass"].ToString());
                    item.SubItems.Add(r["Stock"].ToString());
                    item.SubItems.Add(string.Format("{0:F0}", r["encash"]));
                    item.SubItems.Add(r["encashBadge"].ToString());
                }
                StxtRoomTypeAnnal.ForeColor = Color.Red;
                StxtRoomTypeAnnal.Text = string.Format("共{0}条记录", count);
            }
        }
        private void txtBriefSpelling_TextChanged(object sender, EventArgs e)       /// 商品设置选项卡中的简拼
        {
            int count = 0;//记录商品数
            //输入简拼时在消费项目中显示商品
            string sql = string.Format("select * from Goods g inner join GoodsClass gc on g.GoodsClassId=gc.GoodsClassId where g.SpellBrief like '%{0}%'", txtBriefSpelling.Text);
            DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
            lvGoodsName.Items.Clear();
            foreach (DataRow r in dt.Rows)
            {
                count++;
                ListViewItem item = new ListViewItem();
                lvGoodsName.Items.Add(item);
                item.Tag = r["GoodsId"];
                item.Text = r["GoodsNumber"].ToString();
                item.SubItems.Add(r["GoodsName"].ToString());
                item.SubItems.Add(string.Format("{0:F0}", r["Price"]));
                item.SubItems.Add(r["GoodsClass"].ToString());
                item.SubItems.Add(r["Stock"].ToString());
                item.SubItems.Add(string.Format("{0:F0}", r["encash"]));
                item.SubItems.Add(r["encashBadge"].ToString());
            }
            StxtRoomTypeAnnal.ForeColor = Color.Red;
            StxtRoomTypeAnnal.Text = string.Format("共{0}条记录", count);
        }
        private void SbtnAddSort_Click(object sender, EventArgs e)  //增加商品类别
        {
            bool b = false;//判断选中增加还是修改
            FrmSystemAddGoodsType fsagt = new FrmSystemAddGoodsType(this,b);
            fsagt.ShowDialog();
        }
        private void SbtnDeleteSort_Click(object sender, EventArgs e)        /// 删除商品类别
        {
            if (lvGoodsClass.SelectedItems.Count == 0)
            {
            }
            else
            {
                string sql = string.Format("delete GoodsClass where GoodsClassId={0}", lvGoodsClass.SelectedItems[0].Tag);
                SqlHelp.ExcuteInsertUpdateDelete(sql);
                AddGoodsType();
            }
        }
        private void SbtnUpdateSort_Click(object sender, EventArgs e)      /// 修改商品类别
        {
            if (lvGoodsClass.SelectedItems.Count == 0)
            {
            }
            else
            {
                bool b = true;//判断选中增加还是修改
                FrmSystemAddGoodsType fsagt = new FrmSystemAddGoodsType(this, b);
                fsagt.ShowDialog();
            }
        }
        private void SbtnOddAdd_Click(object sender, EventArgs e)    /// 添加商品
        {
            bool b = false;//判断选中增加还是修改
            FrmSystemAddGoods fsagt = new FrmSystemAddGoods(this, b);
            fsagt.ShowDialog();
        }
        private void SbtnBatchAdd_Click(object sender, EventArgs e)     /// 修改商品
        {
            if (lvGoodsName.SelectedItems.Count == 0)
            {
            }
            else
            {
                bool b = true;//判断选中增加还是修改
                FrmSystemAddGoods fsagt = new FrmSystemAddGoods(this, b);
                fsagt.ShowDialog();
            }
        }
        private void SbtnUpdateRoom_Click(object sender, EventArgs e)
        {
            if (lvGoodsName.SelectedItems.Count == 0)
            {
            }
            else
            {
                string sql = string.Format("delete Goods where GoodsId={0}", lvGoodsName.SelectedItems[0].Tag);
                SqlHelp.ExcuteInsertUpdateDelete(sql);
                AddGoodsName();
            }
        }
        private void SDeleteRoom_Click(object sender, EventArgs e)  // 打折设置
        {
            int Abate = 2;
            FrmSystemRoomAbate fsra = new FrmSystemRoomAbate(this,Abate);
            fsra.ShowDialog();
        }
        private void FbtnRoomAbate_Click(object sender, EventArgs e)  // 房间费打折
        {
            if (listView1.SelectedItems.Count == 0)
            {
            }
            else
            {
                int Abate = 1;
                FrmSystemRoomAbate fsra = new FrmSystemRoomAbate(this, Abate);
                fsra.ShowDialog();
            }
        }
        private void cboServeGrade_SelectedIndexChanged(object sender, EventArgs e)      /// 服务生下拉表
        {
            int count = 0;//记录房间数
            if (cboServeGrade.SelectedIndex == 0)
            {
                count=AddServeMember(count);
                FWStxtWaiterAnnal.Text = string.Format("共{0}条记录", count);
            }
            else
            {
                string sql = "select * from ServeMember sm inner join RoomType rt on sm.RoomTypeId=rt.RoomTypeId inner join ServeGrade sg on sm.ServeGradeId=sg.ServeGradeId where sm.ServeGradeId="+cboServeGrade.SelectedValue;
                DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
                lvServeMember.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    count++;
                    ListViewItem item = new ListViewItem();
                    lvServeMember.Items.Add(item);
                    item.Tag = row["ServeMemberId"];
                    item.Text = row["ServeNumber"].ToString();
                    item.SubItems.Add(row["ServeName"].ToString());
                    item.SubItems.Add(row["Spell"].ToString());
                    item.SubItems.Add(row["Sex"].ToString());
                    item.SubItems.Add(row["RoomTypeName"].ToString());
                    item.SubItems.Add(row["PositionQuality"].ToString());
                    item.SubItems.Add(row["GradeName"].ToString());
                    item.SubItems.Add(row["PhoneNumber"].ToString());
                    item.SubItems.Add(row["CardId"].ToString());
                }
                FWStxtWaiterAnnal.Text = string.Format("共{0}条记录", count);
            }
        }
        private void FWSbtnAddGrade_Click(object sender, EventArgs e)  /// 添加服务生等级
        {
            bool b = false;//选择增加
            FrmSystemAddServeGrade fsasg = new FrmSystemAddServeGrade(this, b);
            fsasg.ShowDialog();
        }
        private void FWSbtnUpdateGrade_Click(object sender, EventArgs e)   /// 修改服务生等级
        {
            if (lvServeGrade.SelectedItems.Count == 0)
            {
            }
            else
            {
                bool b = true;//选择修改
                FrmSystemAddServeGrade fsasg = new FrmSystemAddServeGrade(this, b);
                fsasg.ShowDialog();
            }
        }
        private void FWSbtnDeleteGrage_Click(object sender, EventArgs e)       /// 删除服务生等级
        {
            if (lvServeGrade.SelectedItems.Count == 0)
            {
            }
            else
            {
                string sql = "Delete ServeGrade where ServeGradeId=" + lvServeGrade.SelectedItems[0].Tag;
                SqlHelp.ExcuteInsertUpdateDelete(sql);
                AddServeGrade();
            }
        }
        private void FWSbtnAddStaff_Click(object sender, EventArgs e)      /// 增加服务生
        {
            bool b = false;
            FrmSystemAddServeMember fsasm = new FrmSystemAddServeMember(this,b);
            fsasm.ShowDialog();
        }
        private void FWSbtnUpdateStaff_Click(object sender, EventArgs e)      /// 修改服务员
        {
            if (lvServeMember.SelectedItems.Count == 0)
            {
            }
            else
            {
                bool b = true;
                FrmSystemAddServeMember fsasm = new FrmSystemAddServeMember(this, b);
                fsasm.ShowDialog();
            }
        }
        private void FWSbtnDeleteStaff_Click(object sender, EventArgs e)    /// 删除服务员
        {
            if (lvServeMember.SelectedItems.Count == 0)
            {
            }
            else
            {
                int count = 0;
                string sql = "Delete ServeMember where ServeMemberId=" + lvServeMember.SelectedItems[0].Tag;
                SqlHelp.ExcuteInsertUpdateDelete(sql);
                count = AddServeMember(count);
                FWStxtWaiterAnnal.Text = string.Format("共{0}条记录", count);
            }
        }
        private void AddClient_Click(object sender, EventArgs e)    /// 添加客户来源
        {
            bool b1 = false;//增加
            string s = "请输入[客户来源]:";
            bool b = true;//客户来源
            FrmSystemAddClient fac = new FrmSystemAddClient(this,s,b,b1);
            fac.ShowDialog();
        }
        private void UpdateClient_Click(object sender, EventArgs e)
        {
            if (lvClient.SelectedItems.Count == 0)
            {
            }
            else
            {
                bool b1 = true;//修改
                string s = "请输入新的[客户来源]:";
                bool b = true;//客户来源
                FrmSystemAddClient fac = new FrmSystemAddClient(this, s, b, b1);
                fac.ShowDialog();
            }
        }
        private void DeleteClient_Click(object sender, EventArgs e)
        {
            if (lvClient.SelectedItems.Count == 0)
            {
            }
            else
            {
                string sql = "Delete Client where clientId=" + lvClient.SelectedItems[0].Tag;
                SqlHelp.ExcuteInsertUpdateDelete(sql);
                Client();
            }
        }
        private void KHentangleAdd_Click(object sender, EventArgs e)      /// 增加营销人员
        {
            bool b1 = false;//增加
            string s = "请输入[营销来源]:";
            bool b = false;//营销来源
            FrmSystemAddClient fac = new FrmSystemAddClient(this, s, b, b1);
            fac.ShowDialog();
        }
        private void KHentangleUpdate_Click(object sender, EventArgs e)
        {
            if (lvSellperson.SelectedItems.Count == 0)
            {
            }
            else
            {
                bool b1 = true;//修改
                string s = "请输入新的[营销来源]:";
                bool b = false;//营销来源
                FrmSystemAddClient fac = new FrmSystemAddClient(this, s, b, b1);
                fac.ShowDialog();
            }
        }
        private void KHentangleDelete_Click(object sender, EventArgs e)
        {
            if (lvSellperson.SelectedItems.Count == 0)
            {
            }
            else
            {
                string sql = "Delete Sellperson where SellpersonId=" + lvSellperson.SelectedItems[0].Tag;
                SqlHelp.ExcuteInsertUpdateDelete(sql);
                Sellperson();
            }
        }
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                textBox2.Enabled = true;
            }
            else
            {
                textBox2.Enabled = false;
            }
        }
        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked == true)
            {
                textBox4.Enabled = true;
            }
            else
            {
                textBox4.Enabled = false;
            }
        }
        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked == true)
            {
                numericUpDown1.Enabled = true;
            }
            else
            {
                numericUpDown1.Enabled = false;
            }
        }
        private void VIPbtnSave_Click(object sender, EventArgs e)
        {
            MessageBox.Show("保存成功!");
            return;
        }
        private void VIPbtnAddStype_Click(object sender, EventArgs e)   /// 添加VIP类型
        {
            bool b = false;//选择了增加类型
            FrmSystemVIPType fsvt = new FrmSystemVIPType(this,b);
            fsvt.ShowDialog();
        }
        private void VIPbtnUpdateStype_Click(object sender, EventArgs e)
        {
            if (lvVIPType.SelectedItems.Count == 0)
            {
            }
            else
            {
                bool b = true;//选择了增加类型
                FrmSystemVIPType fsvt = new FrmSystemVIPType(this, b);
                fsvt.ShowDialog();
            }
        }
        private void VIPbtnDeleteStype_Click(object sender, EventArgs e)   /// 删除VIP类型
        {
            if (lvVIPType.SelectedItems.Count == 0)
            {
            }
            else
            {
                string sql = "Delete VIPType where VIPTypeId=" + lvVIPType.SelectedItems[0].Tag;
                SqlHelp.ExcuteInsertUpdateDelete(sql);
                VIPType();
            }
        }
        private void VIPbtnRoomAbate_Click(object sender, EventArgs e)   // VIP房间费打折
        {
            if (lvVIPDiscount.SelectedItems.Count == 0)
            {
            }
            else
            {
                int Abate = 3;
                FrmSystemRoomAbate fsra = new FrmSystemRoomAbate(this,Abate);
                fsra.ShowDialog();
            }
        }
        private void FrmSystemMain_FormClosing(object sender, FormClosingEventArgs e)  // 系统设置关闭窗体事件
        {
            _fm.Refresh();
        }
        private void CZYbtnAddpurview_Click(object sender, EventArgs e)   // 添加权限组
        {
            bool b = false;
            FrmSystemAdmin fsa = new FrmSystemAdmin(this, b);
            fsa.ShowDialog();
        }
        private void CZYbtnUpdatepurview_Click(object sender, EventArgs e)   /// 修改
        {
            if (lvadmin.SelectedItems.Count == 0)
            {
            }
            else
            {
                bool b = true;
                FrmSystemAdmin fsa = new FrmSystemAdmin(this, b);
                fsa.ShowDialog();
            }
        }
        private void CZYbtnDeletepurview_Click(object sender, EventArgs e)       /// 删除权限组
        {
            if (lvadmin.SelectedItems.Count == 0)
            {
            }
            else if (lvadmin.SelectedItems[0].Text == "超级管理员")
            {
                MessageBox.Show("该组为系统内定权限组，不能被删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                string sql = "Delete Admin where AdminId=" + lvadmin.SelectedItems[0].Tag;
                SqlHelp.ExcuteInsertUpdateDelete(sql);
                string sql2 = "Delete AdminPhpdom where AdminId=" + lvadmin.SelectedItems[0].Tag;
                SqlHelp.ExcuteInsertUpdateDelete(sql2);
                AddAdmin();
            }
        }
        private void CZYAdddept_Click(object sender, EventArgs e)       /// 添加部门
        {
            bool b = false;
            FrmSystemAddBm fsab = new FrmSystemAddBm(this, b);
            fsab.ShowDialog();
        }
        private void CZYbtnDeletedept_Click(object sender, EventArgs e)      /// 删除部门
        {
            if (lvBM.SelectedItems.Count == 0)
            {
            }
            else
            {
                string sql = "Delete BM where BMId=" + lvBM.SelectedItems[0].Tag;
                SqlHelp.ExcuteInsertUpdateDelete(sql);
                AddBM();
            }
        }
        private void CZYUpdatedept_Click(object sender, EventArgs e)    /// 修改部门
        {
            if (lvBM.SelectedItems.Count == 0)
            {
            }
            else
            {
                bool b = true;
                FrmSystemAddBm fsab = new FrmSystemAddBm(this, b);
                fsab.ShowDialog();
            }
        }
        private void CZYbtnplaneresidedept_Click(object sender, EventArgs e)  /// 本机所属部门
        {
            FrmSystemmount fsm = new FrmSystemmount();
            fsm.ShowDialog();
        }
        private void CZYbtnAddworkmember_Click(object sender, EventArgs e)     /// 添加操作员
        {
            bool b = false;
            FrmSystemAddCZY fsaczy = new FrmSystemAddCZY(this, b);
            fsaczy.ShowDialog();
        }
        private void CZYbtnUpdateworkmember_Click(object sender, EventArgs e)    // 修改操作员
        {
            if (lvUsers.SelectedItems.Count == 0)
            {
            }
            else if (lvUsers.SelectedItems[0].Text == "超级管理员")
            {
                MessageBox.Show("不能对超级管理员做此项操作!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                bool b = true;
                FrmSystemAddCZY fsaczy = new FrmSystemAddCZY(this, b);
                fsaczy.ShowDialog();
            }
        }
        private void CZYbtnDeleteworkmember_Click(object sender, EventArgs e)   /// 删除操作员
        {
            if (lvUsers.SelectedItems.Count == 0)
            {
            }
            else if (lvUsers.SelectedItems[0].Text == "超级管理员")
            {
                MessageBox.Show("不能对超级管理员做此项操作!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                string sql = "Delete Users where UserId=" + lvUsers.SelectedItems[0].Tag;
                SqlHelp.ExcuteInsertUpdateDelete(sql);
                AddUsers();
            }
        }
        private void CZYbtnclearpwd_Click(object sender, EventArgs e)    /// 清空密码
        {
            if (lvUsers.SelectedItems.Count == 0)
            {
            }
            else if (lvUsers.SelectedItems[0].Text == "超级管理员")
            {
                MessageBox.Show("不能对超级管理员做此项操作!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                DialogResult dr= MessageBox.Show("修改后将不能恢复！", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    string sql = "Update Users set PassWord=null where UserId=" + lvUsers.SelectedItems[0].Tag;
                    SqlHelp.ExcuteInsertUpdateDelete(sql);
                    AddUsers();
                    MessageBox.Show("清除密码成功!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    return;
                }
            }
        }
        private void button36_Click(object sender, EventArgs e)  //房间整理  清理 无效房间
        {
           int c = Roomtype.Count;
         c = c + 1;

 
//DataTable dt=new DataTable();
//SqlDataAdapter sda=new SqlDataAdapter (); 
//DataSet ds=new DataSet(); 
//ds.Tabla.Add(dt);
//sda.Fill(ds);        
//sda.Update(ds);  
  
        }
        private void checkBox28_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void button24_Click(object sender, EventArgs e)      //   Close();
        {
            Close();
       
        }
        private void button34_Click(object sender, EventArgs e)  //   Close();
        {
            Close();
        }
 #region    Charging  计费  方法
        private void JFcoxfullabate_CheckedChanged(object sender, EventArgs e)     //y,n 启用全场打折
        {
            cht.alldz = 'n';
            if (JFcoxfullabate.Checked)
            {
                cht.alldz = 'y';   //  JFcoxfullabate.Checked = false;  自动 改变选择，不用这个语句
                cht.desp = comboBox9.Text;
            }
         }
        private void button1_Click(object sender, EventArgs e)  //保存
        {
            cht.newday = dateTimePicker1.Text ;    //之后按新的一天开始计费
            DateTime dateTime1 = Convert.ToDateTime(cht.newday);
            cht.newdayh = dateTime1.Hour;
            if (cht.fen != 0)    // 有 优惠 时间 新一天 递减
                cht.newdayh--;
            cht.quitroom = dateTimePicker2.Text ;    //   //之后计费天数自动增加半天
            dateTime1 = Convert.ToDateTime(cht.quitroom);
            cht.quitroomh = dateTime1.Hour;
            cht.addday = dateTimePicker3.Text ;    // 之后计费天数自动增加一天
            dateTime1 = Convert.ToDateTime(cht.addday);
            cht.adddayh = dateTime1.Hour;
            cht.fen = (int) numericUpDown2.Value;
            cht.openf=(int) numericUpDown3.Value;        //开房后 多长时间开始计费
            cht.minjf=(int) numericUpDown4.Value;      //最少按  小时计费，小于这个时间按钟点房计费
            cht.supertime=(int) numericUpDown5.Value;      //不足一小时，但超过多少分钟按一小时计费
            cht.superhalf=(int) numericUpDown6.Value;       //不足半小时，但超过多少分钟按半小时计费
            cht.hour=(int) numericUpDown7.Value ;     //超过多少小时 转为 全天房价 计费 
            cht.supertimetoday= dateTimePicker4.Text;   //超过什么时间按 全天计费
            Writesys();
            label40.Visible = true;
        }
        private void button8_Click(object sender, EventArgs e)  //close
        {
                Close();
        }
        private void tctMain_Selecting(object sender, TabControlCancelEventArgs e)  // tap  改变
        {
            switch (tctMain.SelectedIndex)
            {
                case 6:
                    label40.Visible = false;
                    break;

            }
        }
        private void listView13_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void comboBox9_Leave(object sender, EventArgs e)         //打折比例
        {
            cht.desp = comboBox9.Text;
        }
        private void checkBox14_CheckedChanged(object sender, EventArgs e)   //y,n 仅使用每间固定优惠： 
        {
            if (checkBox14.Checked == true)
            {
                textBox5.Enabled = true;
                cht.onlypreferential = 'y';
                cht.preferential = Convert.ToInt32(textBox5.Text);  //固定优惠
            }
            else
            {
                textBox5.Enabled = false;
                cht.onlypreferential = 'n';
            }
        }
        private void checkBox10_CheckedChanged(object sender, EventArgs e)   //房间费自动抹零
        {
            cht.zero = 'n';
            if (checkBox10.Checked == true)
            {
                cht.zero = 'y';
                cht.jq = comboBox1.Text;  //固定优惠
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
              cht.jq = comboBox1.Text;  //固定优惠
        }
        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            cht.oneday = 'n'; 
            if (checkBox13.Checked == true)
            {
                cht.oneday = 'y';      //  住店时间不足一天的按一天计费
            }
        }
        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            cht.hand = 'n'; 
            if (checkBox9.Checked == true)
            {
                cht.hand = 'y';      //  开单时允许手动更改房间费打折比率和单价
            }
        }
        private void checkBox12_CheckedChanged(object sender, EventArgs e)   //  修改登记时修改房价
        {
            cht.chang = 'n';   
            if (checkBox12.Checked == true)
            {
                cht.chang = 'y';      //  修改登记时修改房价
            }
        }
        private void checkBox11_CheckedChanged(object sender, EventArgs e)   //  不允许手动输入卡号(对开单和结账有效)   
        {
            cht.nohand = 'n';
            if (checkBox11.Checked == true)
            {
                cht.nohand = 'y';      
            }
        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            cht.preferential = Convert.ToInt32(textBox5.Text);  //固定优惠
        }
        private void checkBox38_CheckedChanged(object sender, EventArgs e)       //     回头客会员优惠打折：
        {
            cht.vip = 'n';
            if (checkBox38.Checked == true)
            {
                cht.vip = 'y';
                cht._fdiscout = comboBox10.Text;
            }                                                       
        }
        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            cht._fdiscout = comboBox10.Text;
        }
        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            cht.desp = comboBox9.Text;
        }
        public static void readsys()
        {   // 读自定义 数据库文件
            AppInfo.StructSize = Marshal.SizeOf(typeof(Charging));
            byte[] bt = new byte[AppInfo.StructSize];  //强制 到现在文件的大小，因为 cht 结构一直在改变
            bt = AppInfo.ReadInfo(AppInfo.Sysfile);            //选择 计费设置 
            AppInfo.StructSize = Marshal.SizeOf(typeof(Charging));
            cht = Byte2Struct(bt);
        }
        public static void Writesys()
        {
            Charging[] arr = new Charging[1];
            byte[] temp = new byte[AppInfo.StructSize];
            arr[0] = cht;
            temp = Struct2Byte(arr[0]);
            AppInfo.WriteInfo(temp);
        }
        private static Charging Byte2Struct(byte[] arr)   //复制 byte 到 结构中，恢复结构数据
        {
            IntPtr ptemp = Marshal.AllocHGlobal(AppInfo.StructSize);
            Marshal.Copy(arr, 0, ptemp, AppInfo.StructSize);
            Charging rs = (Charging)Marshal.PtrToStructure(ptemp, typeof(Charging));
            Marshal.FreeHGlobal(ptemp);
            return rs;
        }
        private static byte[] Struct2Byte(Charging s)     // 结构到 byte 中
        {
            //           AppInfo.structSize = Marshal.SizeOf(typeof(Charging));
            byte[] buffer = new byte[AppInfo.StructSize];
            IntPtr structPtr = Marshal.AllocHGlobal(AppInfo.StructSize);             //分配结构体大小的内存空间 
            Marshal.StructureToPtr(s, structPtr, false);            //将结构体拷到分配好的内存空间 
            Marshal.Copy(structPtr, buffer, 0, AppInfo.StructSize);          //从内存空间拷到byte数组 
            return buffer;
        }
 #endregion
        private void button2_Click(object sender, EventArgs e)
        {

        }
        private void checkBox39_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox39.Checked)
            {
                cht.Freebool = 'y';
            }
            else
            {
                cht.Freebool = 'n';
            }

        }
        private void textBox17_KeyPress(object sender, KeyPressEventArgs e)  //钟点房押金 只能输入数字
        {
            var a = (int)e.KeyChar;
            if (a == 8 || (a >= 48 && a <= 57))
            {
            }
            else
            {
                e.Handled = true;
            }
        }
        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            if (textBox17.Text != "")
            {
                FrmSystemMain.cht.timedep = Convert.ToDouble(textBox17.Text);               
            }
 
        }
        private void checkBox33_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void button32_Click(object sender, EventArgs e)   //数据清理 
        {
            //if (lvUsers.SelectedItems.Count == 0)          //添加权限组
            //{
            //}
            //else if (lvUsers.SelectedItems[0].Text == "超级管理员")
            if (AppInfo.AdminName != "超级管理员")
            {
                MessageBox.Show("非超级管理员做此项操作!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                DialogResult dr = MessageBox.Show("慎重处理初始化数据？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    cht.chaneUnit = 0;
                    cht.deposit = 0;
                    cht.totalincome = 0;
                    cht.Free = 0;
                    cht.FreeC = 0;
                    cht.Freebool = '0';
                    cht.Freemoney = 0;
                    cht.FreemoneyC = 0;
                    cht.timedep = 0;


                    FrmSystemMain.cht.Hourtime = 0;
                    FrmSystemMain.cht.deposit = 0;
                    FrmSystemMain.cht.Upmoney = 0;
                    FrmSystemMain.cht.Downmoney = 0;
                    FrmSystemMain.cht.totalincome = 0; //
                    FrmSystemMain.cht.Openroom = 0;
                    FrmSystemMain.cht.Closeroom = 0;
                    FrmSystemMain.cht.Free = 0;
                    FrmSystemMain.cht.chaneUnit = 0;
                    FrmSystemMain.cht.timedep = 0;
                    FrmSystemMain.cht.FreeC = 0;
                    FrmSystemMain.cht.FreemoneyC = 0;
                    FrmSystemMain.cht.Freemoney = 0;

                    Writesys(); //写数据库
                }

            }

        }
    }

}
