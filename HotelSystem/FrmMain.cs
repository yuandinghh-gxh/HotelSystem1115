 using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
//using Microsoft.Synchronization.Data;
#region    开始变量区
namespace HotelSystem1115
{
    public partial class FrmMain : Form
    {
        public static DateTime NowDateTime; // 当前时间 由 time 中断设置
        public static bool Shiftidbool;      //成功交班
        public static bool bdestine;            //由预订窗口进入 客人开单
        private int lalamin = 10;           // 10 分钟提醒一次
        private int lalamincount = 0;        //  分钟提醒 计数
        public bool Alaminbool = false;      //到 13点，入住增加半天
        private DataTable ldtc;
        private string lsqlc;
        private DateTime ldateTimec;
        private TimeSpan lspan;
        private int lh;
        private int lm;
        private decimal ldayc;
        public bool Timecount13 = false;            //到 13点，入住增加半天
        public bool Timecount18 = false;            //到 18点，晚上六点起 入住增加一天 
        // 上面变量 为 在timer 中断中使用的
        public int RoomCount = 0;                       //房间总数
        public double Receivable = 0;                 //存储应收金额
        public DateTime GuestDateTime;                // 客人入住时间 
        public  int RoomId;                            //存放用户选中项的房间Id
        public  string RoomName;                       //存放用户选中项的房间名称
        public string GuestName;                       //存放用户选中项的房间姓名
        public string RoomStateId;                     //存放用户选中项的房间状态
        public string RoomTypeName;                 //存放用户选中项的房间类型
        private string ltabPageText = string.Empty; //存储选中的选项卡上的文本
         private string ltsmRoomState = "全部";        //保存下拉按钮选中房间状态
        public bool B = false;                          //判断用户是否选中一项，默认没选中
        private bool lbVision = true;               //判断用户是否选择列表形式
        public bool lbool;                               //布尔值  还是布尔值 
        private string limage = "大图标";           //保存用户查看方式
        public double Sellmoney = 0;                //存储消费总金额
        private string lvision = "分页";              //用户选择显示的方式
        private DataTable lroomdt = new DataTable(); //实际房间 总数
        public static DataTable Roomlx = new DataTable(); //房间类型
        public static DataTable Roomzt = new DataTable(); //房间状态
        public static DataTable Room = new DataTable(); //旅馆 总 房间
        public static DataTable Clientdt = new DataTable(); //宾客来源 单位= 旅行社，团购，会员等
        private string lsql;
        public int RentRoomInfoId;
        private string loldRoomName;
        #endregion
        public FrmMain()        {
            InitializeComponent();
            FrmSystemMain.readsys(); //读取 自定义数据库
            //  char c=   FrmSystemMain.cht.alldz;
        }
        private void FrmMain_Load(object sender, EventArgs e)        {
            if (AppInfo.Database)            {
                //   process1.StartInfo.FileName = "UpDate.exe";   //更新什么数据库？ 不知道什么意思  再次改变
                //   process1.Start();
                AppInfo.Database = false;
            }
            //SoundPlayer sp = new SoundPlayer("sound/tc.wav"); //??????????
            // sp.Play();
       //     skinEngine1.SkinFile = "s (1).ssk"; //C# Winform中窗体的美化—————— 用IrisSkin4.dll美化你的WinForm
            Roomlx = SqlHelp.ExcuteAsAdapter("select * from RoomType");            //           panel14.Width = 29;
            Roomzt = SqlHelp.ExcuteAsAdapter("select * from roomstate");
            Room = SqlHelp.ExcuteAsAdapter("select * from Room order by RoomName ");
            Clientdt = SqlHelp.ExcuteAsAdapter("select * from Client");

            //string sql2 = "select * from Room r inner join RoomType rt on r.RoomTypeId=rt.RoomTypeId inner join RoomState rs on r.RoomStateId=rs.RoomStateId ORDER BY RoomName";   //ORDER BY RoomName  14-8-20
            // private  DataTable roomdt = SqlHelp.ExcuteAsAdapter(sql2);
            //RoomCount = roomdt.Rows.Count;   //有效房间总数

            string adname = AppInfo.AdminName;
            if (adname == "超级管理员")            {
                usename.Text = "使用完系统，请及时退出 ！";
                usename.Visible = true;
            }
            else            {
                usename.Visible = true;
            }
            AddRoomType();
            GetTab2(); //调用显示主界面左边信息方法
            Alarm(); //         Tt();
        }

        private void lv_MouseDown(object sender, EventArgs e) // listview 键盘点击
        {
            var tpp = (ListView) sender;
            if (tpp == null)
            {
                B = false;
            }
        }

        public void AddRoomType() //设置里添加房间　　　
        {
            panel13.Visible = false;
            tabControl1.Visible = true;
            string sql = "select * from RoomType"; // 添加房间类型   大床房。。。标间。。。
            DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
            tabControl1.TabPages.Clear();
            foreach (DataRow row in dt.Rows)
            {
                var tp = new TabPage {Tag = row["RoomTypeId"], Text = row["RoomTypeName"].ToString()}; //动态生成选项卡
                tabControl1.TabPages.Add(tp);
                tabControl1.Click += Frm_TabClick; //单击选项卡时调用Frm_Click
                //  tabControl1.TabIndexChanged += new EventHandler(tptab);
                //  tp.Click  +=  tptab;
                var lv = new ListView(); //动态生成ListView
                lv.Dock = DockStyle.Fill;
                lv.BorderStyle = BorderStyle.None;
                lv.HideSelection = false;
                if (limage == "中图标")
                {
                    lv.LargeImageList = imageListhit;
                }
                else if (limage == "小图标")
                {
                    lv.LargeImageList = imageListlittle;
                }
                else
                {
                    lv.LargeImageList = imageList1;
                }
                lv.ContextMenuStrip = contextMenuStrip1; //在listview 设置 右键菜单  ！！！！！！！
                //     lv.MouseClick +=new MouseEventHandler(lv_MouseClick);
                lv.MouseDown += lv_MouseDown;
                lv.Click += Frm_Click; //单击时调用Frm_Click
                lv.DoubleClick += lv_DoubleClick; //// 双击 房间号
                lsql = "select r.*,rt.RoomTypeName,rs.RoomStateName from RoomType rt inner join Room r on rt.RoomTypeId=r.RoomTypeId inner join RoomState rs on r.RoomStateId=rs.RoomStateId  ORDER BY RoomName";
                lroomdt = SqlHelp.ExcuteAsAdapter(lsql);
                lv.Items.Clear();
                foreach (DataRow r in lroomdt.Rows) //取 一行 表数据
                {
                    if (Convert.ToInt32(tp.Tag) == Convert.ToInt32(r["RoomTypeId"]))
                    {
                        if (ltsmRoomState == r["RoomStateName"].ToString())
                        {
                            var item = new ListViewItem();
                            lv.Items.Add(item);
                            item.Tag = r["RoomId"];
                            item.Text = r["RoomName"].ToString();
                            item.SubItems.Add(r["RoomTypeName"].ToString());
                            item.SubItems.Add(r["RoomStateName"].ToString());
                            item.SubItems.Add(r["Position"].ToString());
                            item.SubItems.Add(string.Format("{0:f2}", Convert.ToDouble(r["Area"])));
                            item.SubItems.Add(r["Remark"].ToString());
                            item.SubItems.Add(r["PhoneNumber"].ToString());
                            item.ImageIndex = imageList1.Images.IndexOfKey(r["RoomStateName"] + ".JPG");
                        }
                        else if (ltsmRoomState == "全部")
                        {
                            var item = new ListViewItem();
                            lv.Items.Add(item);
                            item.Tag = r["RoomId"];
                            item.Text = r["RoomName"].ToString();
                            item.SubItems.Add(r["RoomTypeName"].ToString());
                            item.SubItems.Add(r["RoomStateName"].ToString());
                            item.SubItems.Add(r["Position"].ToString());
                            item.SubItems.Add(string.Format("{0:f2}", Convert.ToDouble(r["Area"])));
                            item.SubItems.Add(r["Remark"].ToString());
                            item.SubItems.Add(r["PhoneNumber"].ToString());
                            item.ImageIndex = imageList1.Images.IndexOfKey(r["RoomStateName"] + ".JPG");
                        }
                    }
                }
                tp.Controls.Add(lv); //把动态生成的listView加到TabPage控件里面去
            }

            var tp1 = new TabPage {Tag = 99, Text = "==所有房间=="}; //动态生成选项卡
            //        tp1.BackColor.
            tabControl1.TabPages.Add(tp1);
            tabControl1.Click += Frm_TabClick; //单击选项卡时调用Frm_Click
            //?????   14-8-22  显示全部房间  
            var lv1 = new ListView {Dock = DockStyle.Fill, BorderStyle = BorderStyle.None, HideSelection = false};
                //动态生成ListView
            if (limage == "中图标")
            {
                lv1.LargeImageList = imageListhit;
            }
            else if (limage == "小图标")
            {
                lv1.LargeImageList = imageListlittle;
            }
            else
            {
                lv1.LargeImageList = imageList1;
            }
            lv1.ContextMenuStrip = contextMenuStrip1;
            lv1.Click += Frm_Click; //单击时调用Frm_Click
            lv1.DoubleClick += new EventHandler(lv_DoubleClick); //// 双击 房间号
            lv1.Items.Clear();
            foreach (DataRow r in lroomdt.Rows) //?????????
            {
                if (ltsmRoomState == r["RoomStateName"].ToString())
                {
                    var item = new ListViewItem();
                    lv1.Items.Add(item);
                    item.Tag = r["RoomId"];
                    item.Text = r["RoomName"].ToString();
                    item.SubItems.Add(r["RoomTypeName"].ToString());
                    item.SubItems.Add(r["RoomStateName"].ToString());
                    item.SubItems.Add(r["Position"].ToString());
                    item.SubItems.Add(string.Format("{0:f2}", Convert.ToDouble(r["Area"])));
                    item.SubItems.Add(r["Remark"].ToString());
                    item.SubItems.Add(r["PhoneNumber"].ToString());

                    item.ImageIndex = imageList1.Images.IndexOfKey(r["RoomStateName"] + ".JPG");
                }
                else if (ltsmRoomState == "全部")
                {
                    var item = new ListViewItem();
                    lv1.Items.Add(item);
                    item.Tag = r["RoomId"];
                    item.Text = r["RoomName"].ToString();
                    item.SubItems.Add(r["RoomTypeName"].ToString());
                    item.SubItems.Add(r["RoomStateName"].ToString());
                    item.SubItems.Add(r["Position"].ToString());
                    item.SubItems.Add(string.Format("{0:f2}", Convert.ToDouble(r["Area"])));
                    item.SubItems.Add(r["Remark"].ToString());
                    item.SubItems.Add(r["PhoneNumber"].ToString());

                    item.ImageIndex = imageList1.Images.IndexOfKey(r["RoomStateName"] + ".JPG");
                }
            }
            tp1.Controls.Add(lv1); //把动态生成的listView加到TabPage控件里面去
        }

        private void lv_DoubleClick(object sender, EventArgs e) // ???双击 房间号
        {

            textBox1.Text = "双击 房间号";
            //           throw new NotImplementedException();
        }

        public void VisionRoomType() // 用户切换分页显示
        {
            if (lvision == "分页")
            {
                Visible = false;
                tabControl1.Visible = true;
                string sql = "select * from RoomType";
                DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
                tabControl1.TabPages.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    //动态生成选项卡
                    var tp = new TabPage();
                    tp.Tag = row["RoomTypeId"];
                    tp.Text = row["RoomTypeName"].ToString();
                    tabControl1.TabPages.Add(tp);

                    //动态生成ListView
                    var lv = new ListView {Dock = DockStyle.Fill, BorderStyle = BorderStyle.None, View = View.Details};
                    lv.Columns.Add("房间名称", 100);
                    lv.Columns.Add("房间类型名称", 100);
                    lv.Columns.Add("房间状态名称", 100);
                    lv.Columns.Add("房间位置", 100);
                    lv.Columns.Add("房间面积", 100);
                    lv.Columns.Add("备注", 100);
                    lv.Columns.Add("电话", 100);
                    lv.FullRowSelect = true;
                    lv.GridLines = true;
                    lv.HideSelection = true;
                    lv.MultiSelect = false; //只允许选中一项
                    if (limage == "中图标")
                    {
                        lv.LargeImageList = imageListhit;
                    }
                    else if (limage == "小图标")
                    {
                        lv.LargeImageList = imageListlittle;
                    }
                    else
                    {
                        lv.LargeImageList = imageList1;
                    }
                    lv.ContextMenuStrip = contextMenuStrip1;
                    lv.Click += Frm_Click; //单击时调用Frm_Click
                    string mysql =
                        "select r.*,rt.RoomTypeName,rs.RoomStateName from RoomType rt inner join Room r on rt.RoomTypeId=r.RoomTypeId inner join RoomState rs on r.RoomStateId=rs.RoomStateId";
                    DataTable mydt = SqlHelp.ExcuteAsAdapter(mysql);
                    lv.Items.Clear();
                    foreach (DataRow r in mydt.Rows)
                    {
                        if (Convert.ToInt32(tp.Tag) == Convert.ToInt32(r["RoomTypeId"]))
                        {
                            if (ltsmRoomState == r["RoomStateName"].ToString())
                            {
                                var item = new ListViewItem();
                                lv.Items.Add(item);
                                item.Tag = r["RoomId"];
                                item.Text = r["RoomName"].ToString();
                                item.SubItems.Add(r["RoomTypeName"].ToString());
                                item.SubItems.Add(r["RoomStateName"].ToString());
                                item.SubItems.Add(r["Position"].ToString());
                                item.SubItems.Add(string.Format("{0:f2}", Convert.ToDouble(r["Area"])));
                                item.SubItems.Add(r["Remark"].ToString());
                                item.SubItems.Add(r["PhoneNumber"].ToString());

                                item.ImageIndex = imageList1.Images.IndexOfKey(r["RoomStateName"] + ".JPG");
                            }
                            else if (ltsmRoomState == "全部")
                            {
                                var item = new ListViewItem();
                                lv.Items.Add(item);
                                item.Tag = r["RoomId"];
                                item.Text = r["RoomName"].ToString();
                                item.SubItems.Add(r["RoomTypeName"].ToString());
                                item.SubItems.Add(r["RoomStateName"].ToString());
                                item.SubItems.Add(r["Position"].ToString());
                                item.SubItems.Add(string.Format("{0:f2}", Convert.ToDouble(r["Area"])));
                                item.SubItems.Add(r["Remark"].ToString());
                                item.SubItems.Add(r["PhoneNumber"].ToString());

                                item.ImageIndex = imageList1.Images.IndexOfKey(r["RoomStateName"] + ".JPG");
                            }
                        }
                    }
                    //把动态生成的listView加到TabPage控件里面去
                    tp.Controls.Add(lv);
                }
            }
            else
            {
                //单页显示   
                panel13.Visible = true;

                var lv = new ListView(); //动态生成ListView
                lv.Dock = DockStyle.Fill;
                lv.BorderStyle = BorderStyle.None;
                lv.View = View.Details; //列表显示
                lv.Columns.Add("房间名称", 100);
                lv.Columns.Add("房间类型名称", 100);
                lv.Columns.Add("房间状态名称", 100);
                lv.Columns.Add("房间位置", 100);
                lv.Columns.Add("房间面积", 100);
                lv.Columns.Add("备注", 100);
                lv.Columns.Add("电话", 100);
                lv.FullRowSelect = true;
                lv.GridLines = true;
                lv.HideSelection = true;
                lv.MultiSelect = false; //只允许选中一项
                if (limage == "中图标")
                {
                    lv.LargeImageList = imageListhit;
                }
                else if (limage == "小图标")
                {
                    lv.LargeImageList = imageListlittle;
                }
                else
                {
                    lv.LargeImageList = imageList1;
                }
                lv.ContextMenuStrip = contextMenuStrip1;
                lv.Click += Frm_Click; //单击时调用Frm_Click
                string mysql;
                if (ltsmRoomState == "全部")
                {
                    mysql =
                        string.Format(
                            "select r.*,rt.RoomTypeName,rs.RoomStateName from RoomType rt inner join Room r on rt.RoomTypeId=r.RoomTypeId inner join RoomState rs on r.RoomStateId=rs.RoomStateId");
                }
                else
                {
                    mysql =
                        string.Format(
                            "select r.*,rt.RoomTypeName,rs.RoomStateName from RoomType rt inner join Room r on rt.RoomTypeId=r.RoomTypeId inner join RoomState rs on r.RoomStateId=rs.RoomStateId where rs.RoomStateName='{0}'",
                            ltsmRoomState);
                }
                DataTable mydt = SqlHelp.ExcuteAsAdapter(mysql);
                lv.Items.Clear();
                foreach (DataRow r in mydt.Rows)
                {
                    if (ltsmRoomState == r["RoomStateName"].ToString())
                    {
                        var item = new ListViewItem();
                        lv.Items.Add(item);
                        item.Tag = r["RoomId"];
                        item.Text = r["RoomName"].ToString();
                        item.SubItems.Add(r["RoomTypeName"].ToString());
                        item.SubItems.Add(r["RoomStateName"].ToString());
                        item.SubItems.Add(r["Position"].ToString());
                        item.SubItems.Add(string.Format("{0:f2}", Convert.ToDouble(r["Area"])));
                        item.SubItems.Add(r["Remark"].ToString());
                        item.SubItems.Add(r["PhoneNumber"].ToString());

                        item.ImageIndex = imageList1.Images.IndexOfKey(r["RoomStateName"] + ".JPG");
                    }
                    else if (ltsmRoomState == "全部")
                    {
                        var item = new ListViewItem();
                        lv.Items.Add(item);
                        item.Tag = r["RoomId"];
                        item.Text = r["RoomName"].ToString();
                        item.SubItems.Add(r["RoomTypeName"].ToString());
                        item.SubItems.Add(r["RoomStateName"].ToString());
                        item.SubItems.Add(r["Position"].ToString());
                        item.SubItems.Add(string.Format("{0:f2}", Convert.ToDouble(r["Area"])));
                        item.SubItems.Add(r["Remark"].ToString());
                        item.SubItems.Add(r["PhoneNumber"].ToString());

                        item.ImageIndex = imageList1.Images.IndexOfKey(r["RoomStateName"] + ".JPG");
                    }
                }
                //把动态生成的listView加到panel4控件里面去
                panel13.Controls.Clear();
                panel13.Controls.Add(lv);
                //隐藏原有的TabControl1控件
                tabControl1.Visible = false;
            }
        }

        private void Frm_TabClick(object sender, EventArgs e) //tab  选择 房间类型菜单
        {
            var tp = (TabControl) sender; //获取当前点击的TabPage
            ltabPageText = tp.SelectedTab.Text; //获取当前选中选项卡上的文本
            GetTab2Tob(); //调用显示主界面左上方信息
            B = false;
        }

        private void Frm_Click(object sender, EventArgs e) // 事件-选择 房间 列表
        {
            var lv = (ListView) sender; //获取当前点击的ListView
            B = true; //房间号被选中  
            lbool = true;
            RoomName = lv.SelectedItems[0].Text; //把用户选中项的房间名称赋值给变量
            RoomStateId = lv.SelectedItems[0].SubItems[2].Text.Trim(); //把用户选中项的房间状态赋值给变量
            RoomId = Convert.ToInt32(lv.SelectedItems[0].Tag); //把用户选中项的房间ID赋值给变量
            RoomTypeName = lv.SelectedItems[0].SubItems[1].Text.Trim(); //把用户选中项的房间类型赋值给变量
            //当房间状态为入住或长包时调用GetGoodSellInfo()方法显示底部消费信息
            if (lv.SelectedItems.Count == 0)
            {
                return;
            }
            if (RoomStateId == "入住" || RoomStateId == "长包") //只有入住和长包状态的房间需要查询消费信息
            {
                GetGoodSellInfo();
            }
            else
            {
                listView2.Items.Clear();
            }
            GetTab2Tob(); //调用显示主界面左上方信息
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e) // 右键菜单
        {
            if (B)
            {
                B = false;
                for (int i = 0; i <= 18; i++)
                {
                    contextMenuStrip1.Items[i].Visible = true;
                    contextMenuStrip1.Items[i].Enabled = true;
                    //根据房间状态禁用右键菜单的某些功能
                    switch (RoomStateId)
                    {
                        case "可供":
                            contextMenuStrip1.Items[0].Enabled = false;
                            contextMenuStrip1.Items[1].Enabled = false;
                            contextMenuStrip1.Items[2].Enabled = false;
                            contextMenuStrip1.Items[5].Enabled = false;
                            contextMenuStrip1.Items[6].Enabled = false;
                            contextMenuStrip1.Items[7].Enabled = false;
                            contextMenuStrip1.Items[10].Enabled = false;
                            contextMenuStrip1.Items[11].Enabled = false;
                            contextMenuStrip1.Items[14].Enabled = false;
                            contextMenuStrip1.Items[15].Enabled = false;
                            break;
                        case "入住":
                            contextMenuStrip1.Items[3].Enabled = false;
                            contextMenuStrip1.Items[18].Enabled = false;
                            break;
                        case "清扫":
                            contextMenuStrip1.Items[0].Enabled = false;
                            contextMenuStrip1.Items[1].Enabled = false;
                            contextMenuStrip1.Items[2].Enabled = false;
                            contextMenuStrip1.Items[3].Enabled = false;
                            contextMenuStrip1.Items[5].Enabled = false;
                            contextMenuStrip1.Items[6].Enabled = false;
                            contextMenuStrip1.Items[7].Enabled = false;
                            contextMenuStrip1.Items[10].Enabled = false;
                            contextMenuStrip1.Items[11].Enabled = false;
                            contextMenuStrip1.Items[14].Enabled = false;
                            contextMenuStrip1.Items[15].Enabled = false;
                            contextMenuStrip1.Items[18].Enabled = false;
                            break;
                        case "预定":
                            contextMenuStrip1.Items[0].Enabled = false;
                            contextMenuStrip1.Items[1].Enabled = false;
                            contextMenuStrip1.Items[2].Enabled = false;
                            contextMenuStrip1.Items[5].Enabled = false;
                            contextMenuStrip1.Items[6].Enabled = false;
                            contextMenuStrip1.Items[7].Enabled = false;
                            contextMenuStrip1.Items[10].Enabled = false;
                            contextMenuStrip1.Items[11].Enabled = false;
                            contextMenuStrip1.Items[14].Enabled = false;
                            contextMenuStrip1.Items[15].Enabled = false;
                            break;
                        case "停用":
                            contextMenuStrip1.Items[0].Enabled = false;
                            contextMenuStrip1.Items[1].Enabled = false;
                            contextMenuStrip1.Items[2].Enabled = false;
                            contextMenuStrip1.Items[3].Enabled = false;
                            contextMenuStrip1.Items[5].Enabled = false;
                            contextMenuStrip1.Items[6].Enabled = false;
                            contextMenuStrip1.Items[7].Enabled = false;
                            contextMenuStrip1.Items[10].Enabled = false;
                            contextMenuStrip1.Items[11].Enabled = false;
                            contextMenuStrip1.Items[14].Enabled = false;
                            contextMenuStrip1.Items[15].Enabled = false;
                            contextMenuStrip1.Items[18].Enabled = false;
                            break;
                    }
                }
                for (int i = 19; i <= 25; i++)
                {
                    contextMenuStrip1.Items[i].Visible = false;
                }
            }
            else
            {
                for (int i = 0; i <= 18; i++)
                {
                    contextMenuStrip1.Items[i].Visible = false;
                }
                for (int i = 19; i <= 25; i++)
                {
                    contextMenuStrip1.Items[i].Visible = true;
                }
            }
        }

        private void 散客开单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bdestine = false;  //非 预订进入开房
            string IsHave = AdminPhpdom.Phpdom(1);
            if (IsHave.Trim() == "N")
            {
                MessageBox.Show("您没有操作此项的权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                var frmrentroom = new FrmRentRoom(this);
                frmrentroom.ShowDialog();
            }
   
        }

        private void tSbtn1_Click(object sender, EventArgs e) // 散客开单
        {
            //B = false;
            string IsHave = AdminPhpdom.Phpdom(1);
            if (IsHave.Trim() == "N")
            {
                MessageBox.Show("您没有操作此项的权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                var frm = new FrmInproomnum(this);
                frm.ShowDialog();
                if (lbool)   //可以登记入住
                {
                       var frmrentroom = new FrmRentRoom(this);  //客人登记入住
                       frmrentroom.ShowDialog();
                }
            }
    //        Checkin();
        }

        private void 散客开单GToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bdestine = false;  //非 预订进入开房
            Checkin();
        }

        private void Checkin() //?????   开房
        {
            if (B) //判断用户是否选中一项，默认没选中
            {
                B = false;
                if (RoomStateId == "可供" || RoomStateId == "预定")
                {
                    string IsHave = AdminPhpdom.Phpdom(1);
                    if (IsHave.Trim() == "N")
                    {
                        MessageBox.Show("您没有操作此项的权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    var frmrentroom = new FrmRentRoom(this);
                    frmrentroom.ShowDialog();
                }
                else
                {
                    MessageBox.Show("只有可供或预定房可开单", "提示");
                }
            }
            else
            {
                lbool = false;
                var frm = new FrmInproomnum(this);
                frm.ShowDialog();
                frm.Close();
                if (lbool)   //可以登记入住
                {
                    if (AppInfo.RoomName != null)
                    {
                        string IsHave = AdminPhpdom.Phpdom(1);
                        if (IsHave.Trim() == "N")
                        {
                            MessageBox.Show("您没有操作此项的权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }
                        var frmrentroom = new FrmRentRoom(this);  //客人登记入住
                        frmrentroom.ShowDialog();
                    }
                }
            }
            //          throw new System.NotImplementedException();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void 增加消费ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string IsHave = AdminPhpdom.Phpdom(3);
            if (IsHave.Trim() == "N")
            {
                MessageBox.Show("您没有操作此项的权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            var f = new FrmAddConsums(this);
            f.ShowDialog();
        }

        private void tSbtn3_Click(object sender, EventArgs e) // 增加消费
        {
            if (B)
            {
                B = false;
                if (RoomStateId == "入住" || RoomStateId == "长包")
                {
                    string IsHave = AdminPhpdom.Phpdom(3);
                    if (IsHave.Trim() == "N")
                    {
                        MessageBox.Show("您没有操作此项的权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    var f = new FrmAddConsums(this);
                    f.ShowDialog();
                }
                else
                {
                    MessageBox.Show("只有入住或长包房可增加消费", "提示");
                }
            }
            else
            {
                MessageBox.Show("请选择一个房间", "提示");
            }
        }

        public void GetGoodSellInfo() // 主 页面下部显示 查询房间消费信息
        {
            Sellmoney = 0; //根据房间ID得取最近一次开房记录
            lsql = string.Format("select * from RentRoom where RoomId={0} and RentTime=(select max(RentTime) from RentRoom where RoomId={0})",RoomId);
            DataTable dt1 = SqlHelp.ExcuteAsAdapter(lsql);
            if (dt1.Rows.Count == 0) // 没找到 客户信息
            {
                return;
            }
            string orderState = dt1.Rows[0]["OrderState"].ToString(); //按列 取 行 项目内容
            if (orderState == "已结账")
            {
                return;
            }
            RentRoomInfoId = Convert.ToInt32(dt1.Rows[0]["RentRoomInfoId"]);
            listView2.Items.Clear(); //房间费 显示条
            foreach (DataRow row in dt1.Rows)
            {
                var item = new ListViewItem();
                listView2.Items.Add(item);
                item.Text = "房间费";
                item.SubItems.Add(string.Format("{0:F2}", row["RentCost"]));
                item.SubItems.Add(row["RentRoomOrder"].ToString());
                double d = Convert.ToDouble(row["RentDuration"]); //实际入住天数
                DateTime dateTime = Convert.ToDateTime(row["RentTime"].ToString());
                string s = row["RentDurationUnit"].ToString();
                int rentCost = Convert.ToInt32(row["RentCost"]);
                if (s == "天")
                {
                    item.SubItems.Add(string.Format("{0:F2} 天", d.ToString(CultureInfo.InvariantCulture))); //入住天数  ??
                    item.SubItems.Add(
                        (string.Format("{0:F2}",
                            Convert.ToDouble(row["RentRoomOrder"])*Convert.ToDouble(row["RentDuration"]))));
                }
                else // 钟点房 
                {
             //       TimeSpan span1 = NowDateTime.Subtract(dateTime); //计算 相差 时间天数
                    TimeSpan span1 = DateTime.Now - dateTime;
                    string outputStr = string.Format("{0}小时{1}分钟", span1.Hours, span1.Minutes);
                    item.SubItems.Add(outputStr); //入住小时数
                    decimal rcast = (decimal) FrmSystemMain.cht.minjf;
                    outputStr = rcast.ToString();
                    if (span1.Minutes <= FrmSystemMain.cht.openf && span1.Hours == 0)
                    {
                        item.SubItems.Add("0");
                    }
                    else
                    {
                        if (span1.Hours > FrmSystemMain.cht.minjf)
                            rcast = span1.Hours; // 大于 要求的最小时间开始 钟点房 ，一般 2小时；
                        if (span1.Minutes > FrmSystemMain.cht.superhalf) // 分钟没超过30分钟
                        {
                            if (span1.Minutes < FrmSystemMain.cht.supertime)
                            {
                                rcast += 0.5m;
                            }
                            else
                            {
                                rcast++; //设置最小 数
                            }
                        }
                        item.SubItems.Add(string.Format("{0:F2}", rentCost*rcast));
                    }
                }
                item.SubItems.Add(row["RentTime"].ToString());
                item.SubItems.Add(string.Format("{0:F2}", Convert.ToDouble(row["Deposit"])));
                item.SubItems.Add(AppInfo.AdminName);
                item.SubItems.Add("服务生?");
                item.SubItems.Add(row["RoomName"].ToString()); //房间号
                item.SubItems.Add(row["GuestName"].ToString().Trim()); //宾客姓名
            //       item.SubItems.Add(row["RoomTypeName"].ToString().Trim()); //房间类型      ???????????  lbRoomType.Text
                item.SubItems.Add(lbRoomType.Text);
                Sellmoney += Convert.ToDouble(row["RentRoomOrder"])*Convert.ToDouble(row["RentDuration"]); //消费总金额
            }
            //取得该房间最后一次开房id对应的商品信息
            lsql = "select * from GoodsSell gs inner join Goods g on gs.GoodsId=g.GoodsId where gs.RentRoomInfoId=" +
                   RentRoomInfoId;
            DataTable dt = SqlHelp.ExcuteAsAdapter(lsql);
            {
                foreach (DataRow row in dt.Rows)
                {
                    var item = new ListViewItem();
                    listView2.Items.Add(item);
                    item.Text = row["GoodsName"].ToString();
                    lsql = (string.Format("{0:F2}", row["Price"]));
                    item.SubItems.Add(lsql);
                    item.SubItems.Add(lsql);
                    item.SubItems.Add(row["SellNum"].ToString());
                    item.SubItems.Add(
                        (string.Format("{0:F2}", Convert.ToDouble(row["SellNum"])*Convert.ToDouble(row["Price"]))));
                    item.SubItems.Add(row["SellTime"].ToString());
                    item.SubItems.Add(" "); // 不显示押金
                    item.SubItems.Add(AppInfo.AdminName);
                    Sellmoney += Convert.ToDouble(row["SellNum"])*Convert.ToDouble(row["Price"]); //消费总金额
                }
            }
            var item2 = new ListViewItem();
            listView2.Items.Add(item2); //空一行
            var item1 = new ListViewItem();
            listView2.Items.Add(item1);
            item1.Text = "消费合计：";
            item1.UseItemStyleForSubItems = false; //this line makes things work 
            item1.SubItems.Add(string.Format("{0:F2}", Sellmoney), Color.Red, Color.MintCream, Font);
        }

        private void 宾客结账ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            B = true;
            Setle();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)  //右边 便签 赋值 当前时间
        {
            richTextBox1.Text = string.Format("-{0}-", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        private void toolStripButton3_Click(object sender, EventArgs e)   //打开本地计算器
        {
            process1.StartInfo.FileName = "C:/WINDOWS/system32/calc.exe";
            process1.Start();
        }

        private void toolStripButton5_Click(object sender, EventArgs e) //清空便签
        {
            DialogResult dr = MessageBox.Show("你确定要清空便签吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                richTextBox1.Clear();
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e) // 保存便签
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK && saveFileDialog1.FileName.Length > 0)
            {
                richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e) // 主界面下拉按钮
        {
            //可供
            ltsmRoomState = "可供";
            if (lbVision)
            {
                AddRoomType();
            }
            else
            {
                VisionRoomType();
            }
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e) //入住
        {
            ltsmRoomState = "入住";
            if (lbVision)
            {
                AddRoomType();
            }
            else
            {
                VisionRoomType();
            }
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e) //停用
        {
            ltsmRoomState = "停用";
            if (lbVision)
            {
                AddRoomType();
            }
            else
            {
                VisionRoomType();
            }
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e) //预定
        {
            ltsmRoomState = "预定";
            if (lbVision)
            {
                AddRoomType();
            }
            else
            {
                VisionRoomType();
            }
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e) //清理
        {
            ltsmRoomState = "清扫";
            if (lbVision)
            {
                AddRoomType();
            }
            else
            {
                VisionRoomType();
            }
        }

        private void toolStripMenuItem16_Click(object sender, EventArgs e) //全部
        {
            ltsmRoomState = "全部";
            if (lbVision)
            {
                AddRoomType();
            }
            else
            {
                VisionRoomType();
            }
        }

        private void 大图标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lbVision = true;
            limage = "大图标";
            AddRoomType();
        }

        private void 中图标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lbVision = true;
            limage = "中图标";
            AddRoomType();
        }

        private void 小图标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lbVision = true;
            limage = "小图标";
            AddRoomType();
        }

        private void 列表显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lbVision = false;
            VisionRoomType();
        }

        private void 单页显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lbVision = false;
            lvision = "单页";
            VisionRoomType();
        }

        private void 分页显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //_bVision = false;   //???????????
            //_vision = "分页";
            //VisionRoomType();
        }

        private void toolStripDropDownButton5_Click(object sender, EventArgs e) //刷新显示 
        {
            Refresh();
        }

        public override void Refresh()  // 刷新显示
        {
            //刷新显示
            AddRoomType();
            //调用显示主界面左边信息方法
            GetTab2();
            //调用报警库存
            Alarm();
        }

        private void tSbtn4_Click(object sender, EventArgs e) // 宾客结账
        {
            Setle();
        }

        private void Setle() // 宾客结账
        {
            if (B)
            {
                B = false;
                if (RoomStateId == "入住")
                {
                    string sql1 = string.Format("select * from RentRoom where RoomId={0} and  OrderState='正常'", RoomId);
                    lroomdt = SqlHelp.ExcuteAsAdapter(sql1);
                    int rentRoomInfoid = Convert.ToInt32(lroomdt.Rows[0]["RentRoomInfoid"]);
                    double dep = Convert.ToDouble(lroomdt.Rows[0]["Deposit"]); //押金
                    GuestName = Convert.ToString(lroomdt.Rows[0]["GuestName"]);
                    DateTime dateTime = Convert.ToDateTime(lroomdt.Rows[0]["RentTime"].ToString());
                    TimeSpan span = NowDateTime - dateTime; //得取时间间隔差
                    if ((span.Days == 0) && (span.Hours == 0) && (span.Minutes < FrmSystemMain.cht.openf))
                    {
                        DialogResult dr = MessageBox.Show("此宾客还未消费，确定退单吗？", "提示信息", MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Question);
                        if (dr == DialogResult.OK)
                        {
                            lsql = "update Room set RoomStateId=1 where RoomId=" + RoomId;
                            SqlHelp.ExcuteInsertUpdateDelete(lsql);
                            lsql = string.Format("update RentRoom set OrderState='已结账',Receivable=0,Deposit=0,Settlenote='{0}',LastEditDate='{1}' where RentRoomInfoId={2}",
                                    "宾客还未消费,退单！", // 结账注释
                                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                    rentRoomInfoid
                                    ); //存主 结账ID                                           
                            FrmSystemMain.cht.deposit -= dep; //押金总是要减去
                            SqlHelp.ExcuteInsertUpdateDelete(lsql);
                            string temp = string.Format("[主客房号]{0}[宾客姓名]{1}", RoomName, GuestName);
                            lsql = string.Format("insert into SystemLog values ('{0}','{1}','宾客还未消费,退单','{2}','','')",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), AppInfo.UserName, temp);
                            SqlHelp.ExcuteInsertUpdateDelete(lsql);
                            Refresh();
                        }
                    }
                    else //已经消费 
                    {
                        //if (_roomdt.Rows[0]["OrderState"].ToString() == "以结账")
                        //{
                        //    DialogResult dr = MessageBox.Show("次宾客还未消费，确定退单吗？", "提示信息", MessageBoxButtons.OKCancel,
                        //        MessageBoxIcon.Question);
                        //    if (dr == DialogResult.OK)
                        //    {
                        //        string sql = "update Room set RoomStateId=1 where RoomId=" + RoomId;
                        //        SqlHelp.ExcuteInsertUpdateDelete(sql);
                        //        Refresh();
                        //    }
                        //}
                        //else
                        //    {
                        string IsHave = AdminPhpdom.Phpdom(4);
                        if (IsHave.Trim() == "N")
                        {
                            MessageBox.Show("您没有操作此项的权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        var rs = new FrmRentSetle(this); //??????????
                        rs.ShowDialog();
                        //  }
                    }
                }
                else
                {
                    MessageBox.Show("只有入住可增加消费", "提示");
                }
            }
            else
            {
                MessageBox.Show("请选择一个房间", "提示");
            }
        }

        private void tSbtn12_Click(object sender, EventArgs e) // 系统设置
        {
            string IsHave = AdminPhpdom.Phpdom(12);
            if (IsHave.Trim() == "N")
            {
                MessageBox.Show("您没有操作此项的权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            var System = new FrmSystemMain(this);
            System.ShowDialog();
        }

        public void GetTab2() // 显示主界面左下边的信息
        {
            int RoomCount = 0; //房间总数
            int RoomConfess = 0; //可供
            int RoomHouse = 0; //入住长包
            int RoomDestine = 0; //预定
            int RoomCease = 0; //停用
            int RoomClear = 0; //清理
            //     string sql = "select * from Room  where RoomTypeId < 7 ORDER BY RoomName,RoomTypeId";  //??????   有问题  14-8-20
            string sql =     "select * from Room r inner join RoomType rt on r.RoomTypeId=rt.RoomTypeId inner join RoomState rs on r.RoomStateId=rs.RoomStateId ORDER BY RoomName";
            DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
            foreach (DataRow row in dt.Rows)
            {
                RoomCount++; //总数自增
                if (Convert.ToDouble(row["RoomStateId"]) == 1) //如果房间状态为可供，可供及为1,可供自增1
                {
                    RoomConfess++;
                }
                if (Convert.ToDouble(row["RoomStateId"]) == 2 || Convert.ToDouble(row["RoomStateId"]) == 5)
                    //如果房间状态为入住长包，入住及为1,入住自增1
                {
                    RoomHouse++;
                }
                if (Convert.ToDouble(row["RoomStateId"]) == 3) //如果房间状态为清理，清理及为1,清理自增1
                {
                    RoomClear++;
                }
                if (Convert.ToDouble(row["RoomStateId"]) == 4) //如果房间状态为预定，预定及为1,预定自增1
                {
                    RoomDestine++;
                }
                if (Convert.ToDouble(row["RoomStateId"]) == 6) //如果房间状态为停用，停用及为1,停用自增1
                {
                    RoomCease++;
                }
            }
            lbRoomCount.Text = RoomCount.ToString();
            lbRoomConfess.Text = RoomConfess.ToString();
            lbRoomHouse.Text = RoomHouse.ToString();
            lbRoomDestine.Text = RoomDestine.ToString();
            lbRoomCease.Text = RoomCease.ToString();
            lbRoomClear.Text = RoomClear.ToString();
            lbSum.Text = Convert.ToInt32(((RoomHouse/(double) RoomCount)*100)) + "%"; //总入住率
        }

        public void GetTab2Tob() // 显示主界面左上边信息
        {
            lbRoomType.Text = ltabPageText;
            if (lbool) //判断用户是否选中项
            {
                lbool = false;
                if (RoomStateId != "入住")
                {
                    string sql = string.Format("select * from Room r inner join RoomType rt on r.RoomTypeId=rt.RoomTypeId where r.RoomId={0}",RoomId);
                    DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
                    foreach (DataRow row in dt.Rows)
                    {
                        lbRoomType.Text = row["RoomTypeName"].ToString();
                        lbRentName.Text = RoomStateId;              //显示房间状态    ""; //宾客姓名
                        lbPriceOfToday.Text = "￥" + string.Format("{0:F2}", row["PriceOfToday"]); //预设单价
                        lbPriceOfDiscount.Text = "￥0.00"; //折后单价
                        label22.Text = row["PhoneNumber"].ToString(); //房间电话
                        label23.Text = row["Position"].ToString(); //所在区域
                        label24.Text = ""; //进店时间
                        label25.Text = ""; //以用时间
                        label26.Text = ""; //已交押金
                        label27.Text = ""; //应收金额
                    }
                }
                else
                {
                    double sellmoney = 0.00; //应收金额
                    string sql =string.Format("select * from Room r inner join RoomType rt on r.RoomTypeId=rt.RoomTypeId inner join RentRoom rr on r.RoomId=rr.RoomId where r.RoomId={0} and RentTime=(select max(RentTime) from RentRoom where RoomId={0})",RoomId);
                    DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
                    foreach (DataRow row in dt.Rows)
                    {
                        lbRoomType.Text = row["RoomTypeName"].ToString();
                        lbPriceOfToday.Text = "￥" + string.Format("{0:F2}", row["PriceOfToday"]); //预设单价
                        lbPriceOfDiscount.Text = "￥0.00"; //折后单价
                        label22.Text = row["PhoneNumber"].ToString(); //房间电话
                        label23.Text = row["Position"].ToString(); //所在区域
                        lbRentName.Text = row["GuestName"].ToString(); //宾客姓名
                        label24.Text = string.Format("{0:HH:mm:ss}", Convert.ToDateTime(row["RentTime"].ToString()));
                        //进店时间
                        DateTime datet = Convert.ToDateTime(row["RentTime"]); //获取进店时间
                        TimeSpan span = DateTime.Now - datet; //得取时间间隔差
                        label25.Text = span.Days + "天" + span.Hours + "时"; //以用时间
                        sellmoney = Convert.ToDouble(row["RentCost"]); //获取房间费
                        int rentRoomInfoId = Convert.ToInt32(row["RentRoomInfoId"]); //取最近一次开房记录
                        label26.Text = row["Deposit"].ToString(); //已交押金  
                        //应收金额
                        //取得该房间最后一次开房id对应的商品信息
                        string sql2 ="select * from GoodsSell gs  inner join Goods g on gs.GoodsId=g.GoodsId where gs.RentRoomInfoId=" +rentRoomInfoId;
                        DataTable dt1 = SqlHelp.ExcuteAsAdapter(sql2);
                        foreach (DataRow r in dt1.Rows)
                        {
                            sellmoney += Convert.ToDouble(r["SellNum"])*Convert.ToDouble(r["Price"]);
                        }
                        label27.Text = string.Format("{0:F2}", sellmoney); //应收金额
                    }
                }
            }
            else
            {
                lbRentName.Text = ""; //宾客姓名
                lbPriceOfToday.Text = ""; //预设单价
                lbPriceOfDiscount.Text = ""; //折后单价
                label22.Text = ""; //房间电话
                label23.Text = ""; //所在区域
                label24.Text = ""; //进店时间
                label25.Text = ""; //以用时间
                label26.Text = ""; //已交押金
                label27.Text = ""; //应收金额
            }
        }

        private void 随客信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RoomStateId == "入住")
            {
                var fgi = new FrmGuestInfo(this);
                fgi.ShowDialog();
            }
            else
            {
                MessageBox.Show("不能对处于非入住状态的房间做此项操作！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

   #region    未使用程序  简介
        //private void panel14_MouseEnter(object sender, EventArgs e)
        //{
        //    panel14.Width = 148;
        //}

        //private void panel14_MouseLeave(object sender, EventArgs e)
        //{
        //    panel14.Width = 29;
        //}

        //private void button3_Click(object sender, EventArgs e)
        //{
        //    panel14.Width = 148;
        //}

        //private void tSbtn5_Click(object sender, EventArgs e) // 酒店外卖
        //{
        //    string IsHave = AdminPhpdom.Phpdom(5);
        //    if (IsHave.Trim() == "N")
        //    {
        //        MessageBox.Show("您没有操作此项的权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //    }
        //    var tg = new FrmToGo(this);
        //    tg.ShowDialog();
        //}
        /*
                   string sql1 = string.Format( "select RentRoomInfoid from RentRoom where RoomId={0} and RentTime=(select max(RentTime) from RentRoom where RoomId={0})",RoomId);
                       int a = Convert.ToInt32(SqlHelp.ExcuteScalar(sql1)); //取最近一次开房记录
                       if (a == 0)
                       {
                           DialogResult dr = MessageBox.Show("此宾客还未消费，确定退单吗？", "提示信息", MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
                           if (dr == DialogResult.OK)
                           {
                               string sql = "update Room set RoomStateId=1 where RoomId=" + RoomId;
                               SqlHelp.ExcuteInsertUpdateDelete(sql);
                               Refresh();
                           }
                       }
                       else
                       {
                           string sql2 = string.Format("select OrderState from RentRoom where RentRoomInfoid={0}", a);
                           string State1 = SqlHelp.ExcuteScalar(sql2).ToString();
                           if (State1 == "以结账")
                           {
                               DialogResult dr = MessageBox.Show("此宾客还未消费，确定退单吗？", "提示信息", MessageBoxButtons.OKCancel,
                                   MessageBoxIcon.Question);
                               if (dr == DialogResult.OK)
                               {
                                   string sql = "update Room set RoomStateId=1 where RoomId=" + RoomId;
                                   SqlHelp.ExcuteInsertUpdateDelete(sql);
                                   Refresh();
                               }
                           }

                           else
                           {
                               string IsHave = AdminPhpdom.Phpdom(4);
                               if (IsHave.Trim() == "N")
                               {
                                   MessageBox.Show("您没有操作此项的权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                               }
                               var rs = new FrmRentSetle(this);
                               rs.ShowDialog();
                           }
                       }
                   } */
        //private void tSbtn2_Click(object sender, EventArgs e)      /// 团体开单
        //{
        //    string IsHave = AdminPhpdom.Phpdom(2);
        //    if (IsHave.Trim() == "N")
        //    {
        //        MessageBox.Show("您没有操作此项的权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //        return;
        //    }
        //    else
        //    {
        //        MessageBox.Show("此功能尚未完善，请待续！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //        return;
        //    }
        //}

   #endregion

        private void 显示可供ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //可供
            ltsmRoomState = "可供";
            if (lbVision)
            {
                AddRoomType();
            }
            else
            {
                VisionRoomType();
            }
        }

        private void 显示占用ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //入住
            ltsmRoomState = "入住";
            if (lbVision)
            {
                AddRoomType();
            }
            else
            {
                VisionRoomType();
            }
        }

        private void 显示停用ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //停用
            ltsmRoomState = "停用";
            if (lbVision)
            {
                AddRoomType();
            }
            else
            {
                VisionRoomType();
            }
        }

        private void 显示预定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //预定
            ltsmRoomState = "预定";
            if (lbVision)
            {
                AddRoomType();
            }
            else
            {
                VisionRoomType();
            }
        }

        private void 显示清理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //清理
            ltsmRoomState = "清扫";
            if (lbVision)
            {
                AddRoomType();
            }
            else
            {
                VisionRoomType();
            }
        }

        private void 显示全部ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //全部
            ltsmRoomState = "全部";
            if (lbVision)
            {
                AddRoomType();
            }
            else
            {
                VisionRoomType();
            }
        }
  
        private void 更改房态ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fart = new FrmAlterRoomType(this);
            fart.ShowDialog();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e) /// 快速通道
        {
            int a = e.KeyChar;
            if (a == 13)
            {
                string sql = "select * from Room";
                DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
                foreach (DataRow row in dt.Rows)
                {
                    if (textBox1.Text == "")
                    {
                    }
                    else
                    {
                        if (textBox1.Text.Trim() == row["RoomName"].ToString())
                        {
                            //有此房间
                            string sql2 =
                                string.Format(
                                    "select * from Room r inner join RoomType rt on r.RoomTypeId=rt.RoomTypeId inner join RoomState rs on r.RoomStateId=rs.RoomStateId where r.RoomName='{0}'",
                                    textBox1.Text);
                            DataTable dt1 = SqlHelp.ExcuteAsAdapter(sql2);
                            foreach (DataRow r in dt1.Rows)
                            {
                                B = true;
                                lbool = true;
                                RoomName = textBox1.Text; //把用户选中项的房间名称赋值给变量
                                RoomStateId = r["RoomStateName"].ToString(); //把用户选中项的房间状态赋值给变量
                                RoomId = Convert.ToInt32(r["RoomId"]); //把用户选中项的房间ID赋值给变量
                                RoomTypeName = r["RoomTypeName"].ToString(); //把用户选中项的房间类型赋值给变量

                                //只有入住和长包状态的房间需要查询消费信息
                                if (RoomStateId == "入住" || RoomStateId == "长包")
                                {
                                    GetGoodSellInfo();
                                }
                                else
                                {
                                    listView2.Items.Clear();
                                }

                                //调用显示主界面左上方信息
                                GetTab2Tob();
                                textBox1.Text = "";
                            }
                            return;
                        }
                    }
                }
                MessageBox.Show("没有发现此房间，请确认输入是否正确！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                textBox1.Text = "";
                textBox1.Focus();
            }
        }

        private void 更改状态ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (B)
            {
                B = false;
                var fart = new FrmAlterRoomType(this);
                fart.ShowDialog();
            }
            else
            {
                MessageBox.Show("请选择一个房间", "提示");
            }
        }

        private void 修改登记ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void 更改房间ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fir = new FrminsteadRoom(this);
            fir.ShowDialog();
        }

        private void 更改房间AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (B)
            {
                if (RoomStateId != "入住")
                {
                    MessageBox.Show("不能对处于非占用状态的房间进行此项操作！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                var fir = new FrminsteadRoom(this);
                fir.ShowDialog();
            }
            else
            {
                MessageBox.Show("请选择一个房间", "提示");
            }
        }

        private void 宾客结账JToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Setle();
        }

        private void 系统设置XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string IsHave = AdminPhpdom.Phpdom(12);
            if (IsHave.Trim() == "N")
            {
                MessageBox.Show("您没有操作此项的权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            var System = new FrmSystemMain(this);
            System.ShowDialog();
        }

        private void 软件帮助HToolStripMenuItem_Click(object sender, EventArgs e) //ydyd
        {
            //process1.StartInfo.FileName = "readme.htm";
            //process1.Start();
        }

        private void 修改当前操作员密码GToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("使用版本不提供此功能，请购买正版软件！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private void 锁定屏幕OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fls = new FrmLockscreen();
            fls.ShowDialog();
        }

        private void 系统日志JToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fsl = new FrmSystemlog();
            fsl.ShowDialog();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e) //系统退出
        {
            /*        DialogResult dr = MessageBox.Show("确定退出本系统吗？", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                 if (dr == DialogResult.No)
                 {
                     e.Cancel = true;
                 }
                 else //退出系统
                 {
               //      e.CloseReason = 0;
                 }
              string s = string.Format("{0}在{1}退出本系统", AppInfo.UserName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                     //         string sql = string.Format("insert into SystemLog values ('{0}','{1}','退出系统','{2}')", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), AppInfo._UserName, s);
                     string sql = string.Format("insert into SystemLog values ('{0}','{1}','退出系统','{2}'",
                         DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), AppInfo.UserName, s);
                     sql += ",'2014-08-09 15:51:45'" + ",'2014-08-09 15:51:45')"; //add yd 使 insert  命令 保持和 表SystemLog 列的 一致
                     SqlHelp.ExcuteInsertUpdateDelete(sql);
          //           停止退出发声     SoundPlayer sp = new SoundPlayer("sound/xttc.wav");
                     try
                     {
                         sp.Play();
                     }
                     catch (FileNotFoundException ex)
                     {
                         throw ex;
                     } 
                     Thread.Sleep(800);
                 }   
        */
        }

        private void tSbtn6_Click(object sender, EventArgs e) // 预定管理
        {
            string IsHave = AdminPhpdom.Phpdom(6);
            if (IsHave.Trim() == "N")
            {
                MessageBox.Show("您没有操作此项的权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            var fd = new Frmdestine(this);
            fd.ShowDialog();
            if (bdestine)   //预定
            {
                var frmrentroom = new FrmRentRoom(this);
                frmrentroom.ShowDialog();
                if (bdestine)
                {               //预订 开房成功
                         lsql = "Delete Destine where RoomId=" +  RoomName;   //删除 预订表项
                        SqlHelp.ExcuteInsertUpdateDelete(lsql);
                }
            }
            bdestine = false;
        }

        private void 修改登记JToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RoomStateId == "入住")
            {
                var fgi = new FrmGuestInfo(this);
                fgi.ShowDialog();
            }
            else
            {
                MessageBox.Show("不能对处于非入住状态的房间做此项操作！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void 制作门卡ToolStripMenuItem_Click(object sender, EventArgs e) //??????
        {
            process1.StartInfo.FileName = "lockhua/zzmk.exe";
            process1.Start();
        }

        private void 预定管理TToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string IsHave = AdminPhpdom.Phpdom(6);
            if (IsHave.Trim() == "N")
            {
                MessageBox.Show("您没有操作此项的权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            var fd = new Frmdestine(this);
            fd.ShowDialog();
        }

        private void 宾客预定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string IsHave = AdminPhpdom.Phpdom(6);
            if (IsHave.Trim() == "N")
            {
                MessageBox.Show("您没有操作此项的权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            var fd = new Frmdestine(this);
            fd.ShowDialog();
        }

        public void Alarm() // 库存报警
        {
            string sql = string.Format("select * from Goods");
            DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
            foreach (DataRow row in dt.Rows)
            {
                if (Convert.ToInt32(row["StockNum"]) < Convert.ToInt32(row["StockAlarm"]))
                {
                    string GoodsName = row["GoodsName"].ToString();
                    int Stock = Convert.ToInt32(row["StockAlarm"]);
                    //如果实际数量小于报警库存数量则弹出报警框
                    var fsa = new FrmStockAlarm(GoodsName, Stock);
                    fsa.Show();
                }
            }
        }

        private void 续退押金ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var xtd = new frmXTDeposit(this);
            xtd.ShowDialog();
        }

        private void 续退押金ZToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (B)
            {
                B = false;
                if (RoomStateId == "入住")
                {
                    var xtd = new frmXTDeposit(this);
                    xtd.ShowDialog();
                }
                else
                {
                    MessageBox.Show("只有以入住的房间可以操作此项功能！", "                    提示", MessageBoxButtons.OK,
                        MessageBoxIcon.Stop);
                }
            }
            else
            {
                MessageBox.Show("请选择一个房间", "提示");
            }
        }

        private void 团体开单ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string IsHave = AdminPhpdom.Phpdom(2);
            if (IsHave.Trim() == "N")
            {
                MessageBox.Show("您没有操作此项的权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            MessageBox.Show("此功能尚未完善，请待续！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private void 团体开单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string IsHave = AdminPhpdom.Phpdom(2);
            if (IsHave.Trim() == "N")
            {
                MessageBox.Show("您没有操作此项的权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            MessageBox.Show("此功能尚未完善，请待续！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private void tSbtn7_Click(object sender, EventArgs e)    //营业查询
        {
            string isHave = AdminPhpdom.Phpdom(7);
            if (isHave.Trim() == "N")
            {
                MessageBox.Show("您没有操作此项的权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            var frmrentroom = new Frmyy(this);
            frmrentroom.ShowDialog();
        }

        private void tSbtn8_Click(object sender, EventArgs e)
        {
            string IsHave = AdminPhpdom.Phpdom(8);
            if (IsHave.Trim() == "N")
            {
                MessageBox.Show("您没有操作此项的权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            var frmrentroom = new Frmyx();
            frmrentroom.ShowDialog();
        }

        private void tSbtn9_Click(object sender, EventArgs e)
        {
            string IsHave = AdminPhpdom.Phpdom(9);
            if (IsHave.Trim() == "N")
            {
                MessageBox.Show("您没有操作此项的权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            var frmrentroom = new Frmsp();
            frmrentroom.ShowDialog();
        }

        private void tSbtn10_Click(object sender, EventArgs e)  //财务管理
        {
            string IsHave = AdminPhpdom.Phpdom(10);
            if (IsHave.Trim() == "N")
            {
                MessageBox.Show("您没有操作此项的权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            var frmrentroom = new Frmcw();
            frmrentroom.ShowDialog();
        }

        private void tSbtn11_Click(object sender, EventArgs e) //交班  管理
        {
            string IsHave = AdminPhpdom.Phpdom(11);
            if (IsHave.Trim() == "N")
            {
                MessageBox.Show("您没有操作此项的权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            Shiftidbool = false;
            var frmupmeney = new Frmupmeney(this);
            frmupmeney.ShowDialog();
            if (Shiftidbool == true) //成功交班
            {
                更换登陆用户ToolStripMenuItem_Click(null, null);
                //string sql = "select top 1 * from Shift order by RentRoomInfoId desc";   //去最后两行
                //DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
                //int Shiftid = Convert.ToInt32(dt.Rows[0]["Shiftid"]);

                lsql = string.Format("update Shift Set Relief='{0}' where Shiftid={1}", AppInfo.UserName,
                    Frmupmeney.Shiftid + 1); //改变房间状态
                SqlHelp.ExcuteInsertUpdateDelete(lsql);

            }

            //var frmrentroom = new Frmjb();
            //frmrentroom.ShowDialog();
        }

        private void 退出系统XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 更换登陆用户ToolStripMenuItem_Click(object sender, EventArgs e) //更换登陆用户
        {
            AppInfo.AdminName = "";
            AppInfo.AdminPwd = "";
            AppInfo.Login = false;
            AppInfo.AdminName = "";
            AppInfo.AdminPwd = "";
            var frml = new FrmLogin();
            frml.ShowDialog();
            string adname = AppInfo.AdminName;
            if (adname == "超级管理员")
            {
                usename.Text = "使用完系统，请及时退出 ！";
                usename.Visible = true;
            }
            else
            {
                usename.Visible = false;
            }
            //          FrmMain_Load();
        }

        private void toolStripButton6_Click(object sender, EventArgs e) //增加 押金
        {
            var frm11 = new FrmaddDeposit(this);
            frm11.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e) //时间定时器  time
        {
            NowDateTime = DateTime.Now;
            lbdatetime.Text = NowDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            if (NowDateTime.Second == 0)
            {
                // 一分钟 判断一次
                lh = NowDateTime.Hour;
                lm = NowDateTime.Minute;
                if (lh == 0)
                {
                    Timecount13 = false; //午夜12点复位 过天设置
                    Timecount18 = false;
                    FrmSystemMain.cht.chaneUnit = 0; // 今天出现 营业员 改变价格
                }
                if (Alaminbool) // 钟点房 是否要显示
                {
                    lalamincount++;
                    if (lalamin == lalamincount) //10分钟提醒一次钟点房
                    {
                        lalamincount = 0;
                        Alarmjd();
                    }
                }
                if (lh >= FrmSystemMain.cht.adddayh) //增加 一天   ????
                {
                    if (!Timecount18) //已经处理过
                    {
                        if (lm >= FrmSystemMain.cht.fen) // 过了 指定分钟时间
                        {
                            Timecount18 = true;
                            lsqlc =
                                string.Format(
                                    "select RentRoomInfoId,RentTime from RentRoom where OrderState = '正常' and RentDurationUnit = '天'");
                            //           string sqlc = string.Format("select RentRoomInfoId,RentTime from RentRoom where OrderState == '正常' and RentDurationUnit = '天'");
                            ldtc = SqlHelp.ExcuteAsAdapter(lsqlc);
                            foreach (DataRow row in ldtc.Rows)
                            {
                                //item.SubItems.Add(string.Format("{0:F2}", row["RentCost"]));    
                                ldateTimec = Convert.ToDateTime(row["RentTime"].ToString());
                                lspan = NowDateTime.Subtract(ldateTimec); //计算 相差 时间天数
                                ldayc = lspan.Days + 1; // 增加一天
                                if (ldateTimec.Hour <= FrmSystemMain.cht.newdayh)
                                {
                                    int fenc = 60 - FrmSystemMain.cht.fen; // 优惠分数
                                    if (ldateTimec.Minute < FrmSystemMain.cht.fen) // 在早上 六点之前
                                    {
                                        ldayc++;
                                    }
                                }
                                lsqlc = string.Format("Update RentRoom set RentDuration={0} where RentRoomInfoId={1}",
                                    ldayc, Convert.ToInt32(row["RentRoomInfoId"]));
                                SqlHelp.ExcuteInsertUpdateDelete(lsqlc);
                            }
                        }
                    }
                }
                if (lh >= FrmSystemMain.cht.quitroomh) //增加 半天  13点过后  ??
                {
                    if (!Timecount13) //已经处理过
                    {
                        if (lm >= FrmSystemMain.cht.fen) // 过了 指定分钟时间
                        {
                            Timecount13 = true;
                            lsqlc =
                                string.Format(
                                    "select RentRoomInfoId,RentTime from RentRoom where OrderState = '正常' and RentDurationUnit = '天'");
                            ldtc = SqlHelp.ExcuteAsAdapter(lsqlc);
                            foreach (DataRow row in ldtc.Rows)
                            {
                                ldateTimec = Convert.ToDateTime(row["RentTime"].ToString());
                                lspan = NowDateTime.Subtract(ldateTimec); //计算 相差 时间天数
                                ldayc = lspan.Days + 0.5m; // 增加一天
                                if (ldateTimec.Hour <= FrmSystemMain.cht.newdayh) //早上 6点开始
                                {
                                    int fenc = 60 - FrmSystemMain.cht.fen; // 优惠分分钟
                                    if (ldateTimec.Minute < FrmSystemMain.cht.fen) // 在早上 六点之前
                                    {
                                        ldayc++;
                                    }
                                }
                                lsqlc = string.Format("Update RentRoom set RentDuration={0} where RentRoomInfoId={1}",
                                    ldayc, Convert.ToInt32(row["RentRoomInfoId"]));
                                SqlHelp.ExcuteInsertUpdateDelete(lsqlc);
                            }
                        }
                    }
                }
            }
        }

        private void toolStrip4_ItemClicked(object sender, ToolStripItemClickedEventArgs e) //点击 toolStrip4
        {
            string ss = toolStrip4.Text;
            int i = toolStrip4.Items.Count;
        }

        private void toolStripDropDownButton4_Click(object sender, EventArgs e) // 押金状态
        {
            string sql3 = string.Format("select * from RentRoom where OrderState='正常'");
            DataTable dt1 = SqlHelp.ExcuteAsAdapter(sql3);
            listView2.Items.Clear(); //房间费 显示条
            foreach (DataRow row in dt1.Rows)
            {
                var item = new ListViewItem();
                listView2.Items.Add(item);
                item.Text = "房间费";
                int rentCost = Convert.ToInt32(row["RentCost"]);
                double d = Convert.ToDouble(row["RentDuration"]); //实际入住天数
                double deposit = Convert.ToDouble(row["Deposit"]);
                DateTime dateTime = Convert.ToDateTime(row["RentTime"].ToString());
                string s = row["RentDurationUnit"].ToString();
                double max = Convert.ToDouble(row["RentRoomOrder"])*Convert.ToDouble(row["RentDuration"]);
                double maxadd = max;
                if (NowDateTime.Hour < FrmSystemMain.cht.quitroomh) // 13点之前 提供预警
                {
                    maxadd = max + rentCost;
                }
                if (maxadd > deposit) //  押金不够 消费金额
                {
                    item.UseItemStyleForSubItems = false; //this line makes things work 
                    item.SubItems.Add(string.Format("{0:F0}", row["RentCost"]), Color.Red, Color.AliceBlue, Font);
                    item.SubItems.Add(row["RentRoomOrder"].ToString(), Color.Red, Color.AliceBlue, Font);
                    item.SubItems.Add(string.Format("{0:F0} 天", d.ToString(CultureInfo.InvariantCulture)), Color.Red,
                        Color.AliceBlue, Font); //入住天数  ??
                    item.SubItems.Add(string.Format("{0:F2}", max), Color.Red, Color.AliceBlue, Font);
                    item.SubItems.Add(row["RentTime"].ToString(), Color.Red, Color.AliceBlue, Font);
                    item.SubItems.Add(string.Format("{0:F2}", deposit), Color.Red, Color.AliceBlue, Font);
                    item.SubItems.Add(AppInfo.AdminName, Color.Red, Color.AliceBlue, Font);
                }
                else
                {
                    item.SubItems.Add(string.Format("{0:F0}", row["RentCost"]));
                    item.SubItems.Add(row["RentRoomOrder"].ToString());
                    item.SubItems.Add(string.Format("{0:F0} 天", d.ToString(CultureInfo.InvariantCulture))); //入住天数  ??
                    item.SubItems.Add(string.Format("{0:F2}", max));
                    item.SubItems.Add(row["RentTime"].ToString());
                    item.SubItems.Add(string.Format("{0:F2}", deposit));
                    item.SubItems.Add(AppInfo.AdminName);
                }
                item.SubItems.Add("服务生?");
                item.SubItems.Add(row["RoomName"].ToString().Trim()); //房间号
                item.SubItems.Add(row["GuestName"].ToString().Trim()); //宾客姓名
            }
        }

        /*       public void Tt()  //test
                {
            NowDateTime = DateTime.Now;
            _h = NowDateTime.Hour;
            _m = NowDateTime.Minute;
            if (_h == 0)
            {
                Timecount13 = false;            //午夜12点复位 过天设置
                Timecount18 = false;
            }
goto LinkLabel;
            if (_h != FrmSystemMain.cht.adddayh) //增加 一天
            {
                if (Timecount18) //已经处理过
                {
                    if (_m >= FrmSystemMain.cht.fen) // 过了 指定分钟时间
                    {
                        Timecount18 = true;
                        _sqlc = string.Format("select RentRoomInfoId,RentTime from RentRoom where OrderState = '正常' and RentDurationUnit = '天'");
                        _dtc = SqlHelp.ExcuteAsAdapter(_sqlc);
                        foreach (DataRow row in _dtc.Rows)
                        {
                            //item.SubItems.Add(string.Format("{0:F2}", row["RentCost"]));
                            //item.SubItems.Add(row["RentRoomOrder"].ToString());
                            //          double d = Convert.ToDouble(row["RentDuration"]);

                            _dateTimec = Convert.ToDateTime(row["RentTime"].ToString());
                            _span = NowDateTime.Subtract(_dateTimec); //计算 相差 时间天数
                            _dayc = _span.Days + 1;   // 增加一天
                            if (_dateTimec.Hour <= FrmSystemMain.cht.adddayh)
                            {
                                int fenc = 60 - FrmSystemMain.cht.fen;   // 优惠分数
                                if (_dateTimec.Minute < FrmSystemMain.cht.fen) // 在早上 六点之前
                                {
                                    _dayc++;
                                }
                            }
                            _sqlc = string.Format("Update RentRoom set RentDuration={0} where RentRoomInfoId={1}", _dayc, Convert.ToInt32(row["RentRoomInfoId"]));
                            SqlHelp.ExcuteInsertUpdateDelete(_sqlc);
                        }
                    }
                }

            }

            if (_h == FrmSystemMain.cht.quitroomh) //增加 半天  13点过后
            {
                if (!Timecount13) //已经处理过
                {
                    if (_m >= FrmSystemMain.cht.fen) // 过了 指定分钟时间
                    {
                        Timecount13 = true;
                        _sqlc = string.Format("select RentRoomInfoId,RentTime from RentRoom where OrderState = '正常' and RentDurationUnit = '天'");
                        _dtc = SqlHelp.ExcuteAsAdapter(_sqlc);
                        foreach (DataRow row in _dtc.Rows)
                        {
                            _dateTimec = Convert.ToDateTime(row["RentTime"].ToString());
                            _span = NowDateTime.Subtract(_dateTimec); //计算 相差 时间天数
                            _dayc = _span.Days + 0.5m;   // 增加一天
                            if (_dateTimec.Hour <= FrmSystemMain.cht.newdayh)    //早上 6点开始
                            {
                                int fenc = 60 - FrmSystemMain.cht.fen;   // 优惠分分钟
                                if (_dateTimec.Minute < FrmSystemMain.cht.fen) // 在早上 六点之前
                                {
                                    _dayc++;
                                }
                            }
                            _sqlc = string.Format("Update RentRoom set RentDuration={0} where RentRoomInfoId={1}", _dayc, Convert.ToInt32(row["RentRoomInfoId"]));
                            SqlHelp.ExcuteInsertUpdateDelete(_sqlc);
                        }
                    }
                }
            }
LinkLabel:;
               }*/

        private void toolStripDropDownButton5_Click1(object sender, EventArgs e) //钟点房提醒 ????????????
        {
            if (Alaminbool) // 显示 钟点房
            {
                Alaminbool = false;
                toolStripDropDownButton5.Image = global::HotelSystem1115.Properties.Resources.ala;
                toolStripDropDownButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            }
            else
            {
                Alatrue();
            }
        }

        private void Alarmjd() //显示钟点房 计费信息
        {
            string sql3 = string.Format("select * from RentRoom where OrderState='正常' and RentDurationUnit='小时'");
            DataTable dt1 = SqlHelp.ExcuteAsAdapter(sql3);
            listView2.Items.Clear(); //房间费 显示条
            foreach (DataRow row in dt1.Rows)
            {
                var item = new ListViewItem();
                listView2.Items.Add(item);
                item.Text = "房间费";
                item.SubItems.Add(string.Format("{0:F2}", row["RentCost"]));
                item.SubItems.Add(row["RentRoomOrder"].ToString());
                double d = Convert.ToDouble(row["RentDuration"]); //实际入住天数
                DateTime dateTime = Convert.ToDateTime(row["RentTime"].ToString());
                int rentCost = Convert.ToInt32(row["RentCost"]);

                TimeSpan span1 = NowDateTime.Subtract(dateTime); //计算 相差 时间天数
                string outputStr = string.Format("{0}小时{1}分钟", span1.Hours, span1.Minutes);
                item.SubItems.Add(outputStr); //入住小时数
                decimal rcast = (decimal) FrmSystemMain.cht.minjf; //最少按  小时计费，小于这个时间按钟点房计费
                if (span1.Minutes <= FrmSystemMain.cht.openf && span1.Hours == 0)
                {
                    item.SubItems.Add("0");
                }
                else
                {
                    if (span1.Hours > FrmSystemMain.cht.minjf)
                    {
                        rcast = span1.Hours; // 大于 要求的最小时间开始 钟点房 ，一般 2小时；
                        if (span1.Minutes > FrmSystemMain.cht.superhalf) // 分钟没超过30分钟
                        {
                            if (span1.Minutes < FrmSystemMain.cht.supertime)
                            {
                                rcast += 0.5m;
                            }
                            else
                            {
                                rcast++; //设置最小 数
                            }
                        }
                        dateTime = Convert.ToDateTime(FrmSystemMain.cht.supertimetoday);
                        if (span1.Hours > FrmSystemMain.cht.hour || span1.Hours >= dateTime.Hour) //超过多少小时 转为 全天房价 计费 
                        {
                            // 转为 全天房价
                            int roomid = Convert.ToInt32(row["RoomId"]);
                            outputStr =
                                string.Format(
                                    "select * FROM RoomType WHERE (RoomTypeId = (SELECT RoomTypeId FROM Room WHERE (RoomId = {0})))",
                                    roomid);
                            DataTable dataTable = SqlHelp.ExcuteAsAdapter(outputStr);
                            string rentcost = string.Format("{0:F2}",
                                Convert.ToDouble(dataTable.Rows[0]["PriceOfToday"])); // 挂牌单价
                            double turecost = Convert.ToDouble(dataTable.Rows[0]["PriceOfVIP"]); //实际成交价      
                            int intRentRoomInfoId = Convert.ToInt32(row["RentRoomInfoId"]);
                            string times = NowDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                            string sql =
                                string.Format(
                                    "update RentRoom set RentRoomOrder='{0}', RentCost={1},RentDurationUnit='天',RentDuration=1, LastEditDate='{2}' where RentRoomInfoId={3}",
                                    turecost, rentcost, times, intRentRoomInfoId);
                            SqlHelp.ExcuteInsertUpdateDelete(sql);
                            string s = string.Format("主房号:{0}宾客姓名:{1}", row["RoomName"].ToString(),
                                row["GuestName"].ToString());
                            sql = string.Format("insert into SystemLog values ('{0}','{1}','钟点房改全天房','{2}','{3}','')",
                                times, AppInfo.UserName, s, times); //  记录 钟点房 事件
                            SqlHelp.ExcuteInsertUpdateDelete(sql);
                        }
                    }
                    item.SubItems.Add(string.Format("{0:F2}", rentCost*rcast));
                }
                item.SubItems.Add(row["RentTime"].ToString());
                item.SubItems.Add(string.Format("{0:F2}", Convert.ToDouble(row["Deposit"])));
                item.SubItems.Add(AppInfo.AdminName);
                item.SubItems.Add("服务生?"); //?????????
                RoomName = row["RoomName"].ToString().Trim();
                item.SubItems.Add(RoomName); //房间号
                item.SubItems.Add(row["GuestName"].ToString().Trim()); //宾客姓名
            }
        }

        private void toolStripMenuItem22_Click(object sender, EventArgs e) //钟点房提醒   5分钟
        {
            lalamin = 5;
            Alatrue();
        }

        private void toolStripMenuItem23_Click(object sender, EventArgs e) //钟点房提醒   10分钟
        {
            lalamin = 10;
            Alatrue();
        }

        private void toolStripMenuItem24_Click(object sender, EventArgs e) //钟点房提醒   20分钟
        {
            lalamin = 15;
            Alatrue();
        }

        private void Alatrue() // 显示与停止显示 钟点房
        {
            Alaminbool = true;
            toolStripDropDownButton5.Image = global::HotelSystem1115.Properties.Resources.ala1;
            toolStripDropDownButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            Alarmjd();
        }

        private void listView2_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)  // 选择listview2 的每行事件
        {   
            ListViewItem aa = e.Item;
            if (aa.SubItems[0].Text.Trim() != "房间费")
            {
                return;
            }
            try
            {
                RoomName = aa.SubItems[9].Text.Trim(); // 获取房间号
                GuestName = aa.SubItems[10].Text; //  ??? 取得姓名
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
                //            throw  new ArgumentOutOfRangeException();
            }
 
            //RoomName = aa.SubItems[9].Text.Trim(); // 获取房间号
            //GuestName = aa.SubItems[10].Text;   //  ??? 取得姓名
            GuestDateTime = Convert.ToDateTime(aa.SubItems[5].Text); // 取得客户入住时间
            if (RoomName != "")
            {
                if (loldRoomName != RoomName)
                {
                    var rs = new FrmDeposi(this);
                    rs.ShowDialog();
                    if (FrmDeposi.depbool)
                    {
                        B = true;
                        RoomStateId = "入住";
                        RoomName = FrmDeposi.RoomName;
                        var frm11 = new FrmaddDeposit(this);
                        frm11.ShowDialog();
                    }
                    loldRoomName = RoomName;
                }
            }

        }

        private void listView2_MouseClick(object sender, MouseEventArgs e)   //?????????
        {
            if (listView2.Items.Count != 0)
            {
                //  i = listView2.Selectedindex;
                int i = listView2.SelectedItems.Count;
     //           string ss = listView2.Items[2].SubItems[3].Text;
            }
        }

  #region  数据库使用表 公共类的 方法 。。。
        public static string Getdatalx(int ii) //返回 房间类型 的文字串 
        {
            string localss = "";
            foreach (DataRow r in Roomlx.Rows) //取 一行 表数据
            {
                if (ii == Convert.ToInt32(r["RoomTypeId"]))
                {
                    localss = r["RoomTypeName"].ToString();
                    break;
                }
            }
            return localss;
        }
        public static string Getdatazt(int ii) //返回 房间状态 的文字串 
        {
            string localss = "";
            foreach (DataRow r in Roomzt.Rows) //取 一行 表数据
            {
                if (ii == Convert.ToInt32(r["RoomStateId"]))
                {
                    localss = r["RoomStateName"].ToString();
                    break;
                }
            }
            return localss;
        }
        public static int Getdataztid(string lstr) //返回 房间状态 ID 返回 ID 
        {
            int localss = -1;
            foreach (DataRow r in Roomzt.Rows) //取 一行 表数据
            {
                if (lstr == r["RoomStateName"].ToString())
                {
                    localss = Convert.ToInt32(r["RoomStateName"]);
                    break;
                }
            }
            return localss;
        }
        public static int Getdatalxid(string lstr) //返回 房间类型  返回 ID 
        {
            int localss = -1;
            foreach (DataRow r in Roomlx.Rows) //取 一行 表数据
            {
                if (lstr == r["RoomTypeName"].ToString())
                {
                    localss = Convert.ToInt32(r["RoomTypeId"]);
                    break;
                }
            }
            return localss;
        }
  #endregion

        private void button1_Click(object sender, EventArgs e)  //测试
        {
   //        var rs = new FrmDayinquire();
            var rs = new FrmCheckout();
            rs.ShowDialog();
        }

    #region  无效 点击事件
        private void lbdatetime_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        //public static void  Getroomid() //房间名字   返回 房间ID  roomid 
        //{
        //    foreach (DataRow r in Room.Rows) //取 一行 表数据
        //    {
        //        if (RoomName == r["RoomName"].ToString())
        //        {
        //            RoomId = Convert.ToInt32(r["RoomId"]);
        //            break;
        //        }
        //    }
        //}
  #endregion
    }
}


