using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using System.Media;
using System.IO;
namespace HotelSystem1115
{
    #region    累 初始化  公共变量
    public partial class FrmRentSetle : Form
    {

        public FrmMain Frmmain;
        private double _seek    ;           //客户找零 。。
       private double _roomsellmoney;      //开房费
        public int TreeviewId;       //保存选中树节点ID
        private string _treeViewName;       //保存选中树节点名称
   //     public int      SetleRoomId;        //取得结账房间Id
        private double _sellMoney;          //存储当前总金额 ， 第一个选择的房间的总价
        private bool     _b = true;         //用于控制窗体加载后单击窗体播放声音
  //      private string   _s;                //  播放声音 用 ????
        public TreeNode Tn;             //view1  结账区    
        public string   Free = "";          // 挂单 和 免单
        private string _sql;
        private double _lbReceivable;    //  实际应该收款  总金额-押金
        private TreeNode _lastNode;      //上次被选中的节点
        private double _sumdeposit;        //押金总会， 当多房间联合结账时用
        private double _deposit;        //押金总会， 当多房间联合结账时用
        private char _receipt;              //  开发票
        private char _quitD;            //退押金
        private int _rentRoomInfoId ; //存主结账 ID
        private int _roomInfoId;            //temp 存放 = Convert.ToInt32(dt1.Rows[0]["RentRoomInfoId"]);
        private double _lbpricemoney;          //计算优惠金额 
  //      public FrmRentSetle(FrmMain frmmain, string s, int roollen)

        public FrmRentSetle(FrmMain frmmain)
        {
            Frmmain = frmmain;  //主 程序指针给 。。
            //           _s = s;    //?????????
            InitializeComponent();
            _sumdeposit = 0;
            _lbReceivable = 0;
            _lbpricemoney = 0;
            _receipt = 'n';
            _quitD = 'n';
            checkBox3.Enabled = false;          //不允许免单
            if (FrmSystemMain.cht.Freebool == 'y')
            {
                checkBox3.Enabled = true;          //允许免单
            }
            First();
        }
    #endregion
        private void First() 
        {
       //     skinEngine1.SkinFile = "s (1).ssk";
            _treeViewName = Frmmain.RoomName;
            TreeviewId = Frmmain.RoomId;             //根据房间ID得取最近一次开房记录的开放费用
            _sql = string.Format("select * from RentRoom where RoomId={0} and RentTime=(select max(RentTime) from RentRoom where RoomId={0})", TreeviewId);
            DataTable dt = SqlHelp.ExcuteAsAdapter(_sql);
            foreach (DataRow row in dt.Rows)
            {
                _roomsellmoney = Convert.ToDouble(row["RentRoomOrder"]);         //成交价
                lbRentName.Text = row["GuestName"].ToString();                   //宾客姓名
                //lbRoomodd.Text = row["RentRoomOrder"].ToString();            //开房单号 现在使用 RentRoomInfoId
                _sql = row["RentRoomInfoId"].ToString();                           //开房单号 现在使用 RentRoomInfoId
                lbGusetCompany.Text = row["GuestCardId"].ToString();              //改为 宾客ID
                lbDeposit.Text = string.Format("{0:F2}", row["Deposit"]);      //押金
                label7.Text = string.Format("{0:F2}",row["RentCost"]);           //酒店 挂牌价
                label13.Text = string.Format("{0:F2}", _roomsellmoney);      //酒店成交价
                label15.Text = row["GusetType"].ToString();                     //会员类型
      //          lbsettelRoom.Text = row["RoomName"].ToString(); ;              //结账房间号
  //   string strRoomName       =     Frmmain.RoomName;
                lbsettelRoom.Text = Frmmain.RoomName;       //strRoomName;
                _rentRoomInfoId = Convert.ToInt32(row["RentRoomInfoId"]);           //取得主结账房间ID
            }
   //         _sellMoney = Convert.ToDouble(string.Format("{0:F2}", Frmmain.Sellmoney + _roomsellmoney));//把当前总金额存储在变量中
            _sellMoney = Convert.ToDouble(string.Format("{0:F2}", Frmmain.Sellmoney)); //把当前总金额存储在变量中
            lbsettelodd.Text = string.Format("{0}{1}", DateTime.Now.ToString("yyMMddhhmm"), _sql);            //结账单号
            lbsettel.Text = lbsettelRoom.Text;    //结账房间号
            lbmoney.Text = lbsellmoney.Text;
            _sumdeposit = Convert.ToDouble(lbDeposit.Text);  // 累计 押金
            Lbshow();       
 //-----------------------------  treeView2  下面创建 待选 区 已开房 号   加载可结账房间树状表------------------
            _sql = "select * from Room where RoomStateId=2 or RoomStateId=5 ORDER BY RoomName";     //查询入住或长包房
            DataTable dt2 = SqlHelp.ExcuteAsAdapter(_sql);     
            foreach (DataRow r in dt2.Rows)
            {
                if (r["RoomName"].ToString() != Frmmain.RoomName)
                {
                    TreeNode tn2 = new TreeNode();
                    treeView2.Nodes.Add(tn2);
                    tn2.Tag = r["RoomId"];
                    tn2.Text = r["RoomName"].ToString();
                }
            }
//-------------------------------------------------------------------------------------------------------------------------
            TreeNode();   //创建 结账区tree
            _roomsellmoney = 0;
            listView2.Items.Clear(); //房间费 显示条
            GetGoodSellInfo();    // 取得房间的消费金额
            _sumdeposit=_deposit;   // 取得当前 押金
            timer1.Start();
        }

        private void TreeNode()   //创建 结账区tree
        {
            TreeNode tn = new TreeNode();
            treeView1.Nodes.Add(tn);
            Tn   = tn;                  //创建view1的节点累
            tn.Tag = 0;
            tn.Text = string.Format("结账单号:{0}", lbsettelodd.Text);
            tn.ImageIndex = 0;
            TreeNode tn1 = new TreeNode();
            tn.Nodes.Add(tn1);
            tn1.Tag = Frmmain.RoomId;
            tn1.Text = Frmmain.RoomName;
            tn.ExpandAll();
        }

        private void GetGoodSellInfo() // 取得房间的消费金额
        {
            _sql = string.Format("select * from RentRoom where RoomId={0} and RentTime=(select max(RentTime) from RentRoom where RoomId={0})",
                   TreeviewId);   // _treeViewName);
            string unit;
            DataTable dt1 = SqlHelp.ExcuteAsAdapter(_sql);
            if (dt1.Rows.Count == 0) // 没找到客户信息
            {
                MessageBox.Show("错误代码001-没找到客户信息");
                return;
            }
            string orderState = dt1.Rows[0]["OrderState"].ToString(); //按列 取 行 项目内容
            if (orderState == "已结账")
            {
                MessageBox.Show("错误代码002-ROOM数据库和入住数据库出错");
                return;
            }
            _roomInfoId = Convert.ToInt32(dt1.Rows[0]["RentRoomInfoId"]);
            Frmmain.Sellmoney = 0;    // 本次 房间价格
            foreach (DataRow row in dt1.Rows)
            {
                var item = new ListViewItem();
                listView2.Items.Add(item);
                item.Text = row["RoomName"].ToString().Trim();    //房间号
                item.SubItems.Add(string.Format("{0:F2}", row["RentCost"]));
                item.SubItems.Add(row["RentRoomOrder"].ToString());
                double d = Convert.ToDouble(row["RentDuration"]); //实际入住天数
                DateTime dateTime = Convert.ToDateTime(row["RentTime"].ToString());
                unit = row["RentDurationUnit"].ToString();
                int rentCost = Convert.ToInt32(row["RentCost"]);
                if (unit == "天")
                {
                    item.SubItems.Add(string.Format("{0:F2} 天", d.ToString(CultureInfo.InvariantCulture))); //入住天数  ??
                    item.SubItems.Add(string.Format("{0:F2}",Convert.ToDouble(row["RentRoomOrder"])*d));
                }
                else // 钟点房 
                {
                    TimeSpan span1 = FrmMain.NowDateTime.Subtract(dateTime); //计算 相差 时间天数
                    string outputStr = string.Format("{0}小时{1}分钟", span1.Hours, span1.Minutes);
                    item.SubItems.Add(outputStr); //入住小时数
                    decimal rcast = (decimal) FrmSystemMain.cht.minjf;    // 最少按  小时计费，小于这个时间按钟点房计费
               //outputStr = rcast.ToString();
                  if (span1.Minutes <= FrmSystemMain.cht.openf && span1.Hours == 0)      //开房后 多长时间开始计费
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
                _deposit = Convert.ToDouble(row["Deposit"]);  // 累计 押金
     //           item.SubItems.Add(string.Format("{0:F2}", Convert.ToDouble(row["Deposit"])));
                item.SubItems.Add(AppInfo.AdminName);
                item.SubItems.Add("服务生?");   
                //item.SubItems.Add(row["RoomName"].ToString()); //房间号//item.SubItems.Add(row["GuestName"].ToString().Trim()); //宾客姓名
                if (unit == "天")
                {
                    Frmmain.Sellmoney += Convert.ToDouble(row["RentRoomOrder"])*d; //消费总金额  累计
                    double temp = rentCost * d;
                    _lbpricemoney += Frmmain.Sellmoney - temp;
                }
            }
          
            _sql = "select * from GoodsSell gs inner join Goods g on gs.GoodsId=g.GoodsId where gs.RentRoomInfoId=" + _roomInfoId;
            DataTable dt = SqlHelp.ExcuteAsAdapter(_sql);     //取得该房间最后一次开房id对应的商品信息
            {
                foreach (DataRow row in dt.Rows)
                {
                    var item = new ListViewItem();
                    listView2.Items.Add(item);
                    item.Text = row["GoodsName"].ToString();
                    _sql = (string.Format("{0:F2}", row["Price"]));
                    item.SubItems.Add(_sql);
                    item.SubItems.Add(_sql);
                    item.SubItems.Add(row["SellNum"].ToString());
                    item.SubItems.Add(string.Format("{0:F2}", Convert.ToDouble(row["SellNum"])*Convert.ToDouble(row["Price"])));
                    item.SubItems.Add(row["SellTime"].ToString());
                    item.SubItems.Add(" "); // 不显示押金
                    item.SubItems.Add(AppInfo.AdminName);
                    Frmmain.Sellmoney += Convert.ToDouble(row["SellNum"])*Convert.ToDouble(row["Price"]); //消费总金额
                }
            }
            var item2 = new ListViewItem();
            listView2.Items.Add(item2); //空一行
            //var item1 = new ListViewItem();
            //listView2.Items.Add(item1);
            //item1.Text = "消费合计：";
            //item1.UseItemStyleForSubItems = false;              //this line makes things work 
            //item1.SubItems.Add(string.Format("{0:F2}", Frmmain.Sellmoney), Color.Red, Color.MintCream, Font);
            lbmoney.Text = string.Format("{0:F2}", Frmmain.Sellmoney);    //  消费合计
            lbsellmoney.Text = lbmoney.Text;
            lbpricemoney.Text =_lbpricemoney.ToString();
        }

        private void Lbshow()    //确认客户支付 
        {
            _lbReceivable = _sellMoney -_sumdeposit;     // 实收 如果押金大于应收金额 则押金减应收金额
            lbReceivable.Text = string.Format("{0:F2}", _lbReceivable);    //应收金额
            if (_lbReceivable >= 0)   //  说明还要收款金额
            {
                txtPaid.Text = lbReceivable.Text;       //string.Format("{0:F2}", _sellMoney);// 房间价
                lbseek.Text = "0";
                _seek = 0;

                // if (Convert.ToDouble(lbReceivable.Text) < Convert.ToDouble(lbDeposit.Text))      //实收 如果押金大于应收金额 则押金减应收金额
                //{
                //    lbseek.Text = string.Format("{0:F2}", Convert.ToDouble(lbDeposit.Text) - Convert.ToDouble(lbReceivable.Text));
                //    _seek = Convert.ToDouble(lbseek.Text);
                //    txtPaid.Text = "-" + lbseek.Text;
                //}
                //else if (Convert.ToDouble(lbReceivable.Text) > Convert.ToDouble(lbDeposit.Text))
                //{
                //    lbseek.Text = "-" + lbReceivable.Text;
                //    _seek = Convert.ToDouble(lbseek.Text);
                //    txtPaid.Text = lbReceivable.Text;
                //}
                //else
                //{
                //    lbseek.Text = "0";
                //    _seek = Convert.ToDouble(lbseek.Text);
                //    txtPaid.Text = lbReceivable.Text;
                //}
            }
            else
            {
                textBox4.Text = "宾客押金超过住宿消费，请找客户钱！";
                txtPaid.Text = string.Format("{0:F2}",_sellMoney);// 房间价
                txtPaid.Enabled = false;

                lbseek.Text = lbReceivable.Text;
            }
        }
  
        private void txtGusetShell_TextChanged(object sender, EventArgs e)    // 宾客支付
        {
            double temp;
            if (txtGusetShell.Text == "")
            {
                lbseek.Text = _seek.ToString();
            }
            else
            {
                temp = Convert.ToDouble(txtGusetShell.Text);
                _seek = _lbReceivable - temp;                  //找零 txtGusetShell.Text 收入的金额如果多了肯定是负值
                lbseek.Text = string.Format("{0:F2}", _seek );
            }
        }

        private void 直接抹零ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtGusetShell.Text = string.Format("{0:F2}", Convert.ToInt32(Convert.ToDouble(txtGusetShell.Text)));
        }
        private void 四舍五入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtGusetShell.Text = string.Format("{0:F2}", Convert.ToInt32(Convert.ToDouble(txtGusetShell.Text)));
        }
 
        private void cboowemoney_CheckedChanged(object sender, EventArgs e)      // 挂账
        {    //当选择挂账时
            if (cboowemoney.Checked == true)
            {
                checkBox3.Checked = false;      //退押金
                if (_sumdeposit >= _sellMoney || Convert.ToDouble(lbseek.Text) > 0 ) //判断用户支付金额是否大于实际消费金额      消费总额
                {
                    cbxfadeDeposit.Enabled = false;
                    cbxfadeDeposit.Checked = false;
                    //实收金额、宾客支付禁用//txtPaid.Text = "0.00";//txtPaid.Enabled = false;//txtGusetShell.Text = "0.00";
                    //txtGusetShell.Enabled = false; //lbseek.Text = lbDeposit.Text;   //找零  如果 挂账 应该讲找零 改为 下次使用押金
                    Free = "挂单";
                    textBox4.Text = "宾客挂单，剩余金额下次使用 ！";
                }
                else
                {
                        cboowemoney.Checked = false;
                        DialogResult dr = MessageBox.Show("押金不能抵消消费款，不能挂单！", "提示", MessageBoxButtons.OKCancel);
                }
            }
            else
            {
                //退押金
                //cbxfadeDeposit.Enabled = false;
                //cbxfadeDeposit.Checked = false;
                ////实收金额、宾客支付启用
                //txtPaid.Enabled = true;
                //txtGusetShell.Enabled = true;
                Free = "";
                textBox4.Text = "";
                //Lbshow();
            }
        }
 
        private void checkBox3_CheckedChanged(object sender, EventArgs e)      // 免单
        {
            //当选择免单时
            if (checkBox3.Checked == true)
            {
                cboowemoney.Checked = false;    //挂单？
//                txtGuestType.Enabled = false;
               cbxfadeDeposit.Enabled = true;           //退押金
       //        cbxfadeDeposit.Checked = true;     //允许退押金 不一定真退
   //               txtPaid.Text = "0.00";             //实收金额、宾客支付禁用
                 txtPaid.Enabled = false;
               txtGusetShell.Text = "0.00";          //宾客支付
                  txtGusetShell.Enabled = false;
    //            lbseek.Text = lbDeposit.Text;
                Free = "免单";
                textBox4.Text = "宾客免单！";
            }
            else
            {
//??                txtGuestType.Enabled = true;
                cbxfadeDeposit.Enabled = false;     //退押金
                cbxfadeDeposit.Checked = false;
    //            txtPaid.Enabled = true;           //实收金额、宾客支付启用
                txtGusetShell.Enabled = true;       //宾客支付  
                txtGusetShell.Text = "";
                Lbshow();
                Free = "";
                textBox4.Text = "";
            }
        }
        private void cbxfadeDeposit_CheckedChanged(object sender, EventArgs e)      // 退押金
        {
            _quitD = 'n';
            if (cbxfadeDeposit.Checked)
            {
                lbseek.Text = lbDeposit.Text;
                _quitD = 'y';
                lbReceivable.Text = lbsellmoney.Text;
   
            }
            else
            {
                lbseek.Text = "0.00";
                double dep = _sellMoney - _sumdeposit;
                lbReceivable.Text =  string.Format("{0:F2}",dep);       
            }
        }

         /*    /// 退单
                private void checkBox4_CheckedChanged(object sender, EventArgs e)      /// 退单
                {
                    //当选择退单时
                    if (checkBox4.Checked == true)
                    {
                        checkBox3.Checked = false;
                        cboowemoney.Checked = false;
        //??                txtGuestType.Enabled = false;

                        cbxfadeDeposit.Enabled = true;          //退押金
                        cbxfadeDeposit.Checked = true;
                        txtPaid.Text = "0.00";                    //实收金额、宾客支付禁用
                        txtPaid.Enabled = false;
                        txtGusetShell.Text = "0.00";
                        txtGusetShell.Enabled = false;
                        lbseek.Text = lbDeposit.Text;
                
                    }
                    else
                    {
         //??               txtGuestType.Enabled = true;
                        //退押金
                        cbxfadeDeposit.Enabled = false;
                        cbxfadeDeposit.Checked = false;
                        //实收金额、宾客支付启用
                        txtPaid.Enabled = true;
                        txtGusetShell.Enabled = true;
                        lbshow();
                    }
                }
                */

         private void treeView2_AfterSelect(object sender, TreeViewEventArgs e)  // 待选区 选择房间
         {
             _treeViewName = treeView2.SelectedNode.Text;
             if (_lastNode != null)
             {
                 _lastNode.ForeColor = Color.Black;
             }
              TreeviewId = Convert.ToInt32(treeView2.SelectedNode.Tag);
              treeView2.SelectedNode.ForeColor = Color.Red;
             _lastNode = treeView2.SelectedNode;
             //          GetGoodSellInfo();
         }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e) // 结账区内的房间
        {
            
            if (treeView1.SelectedNode.Parent == null) //当选择的是父节点时，输出所有房间的消费信息
            {
                listView2.Items.Clear();
                lbsettelRoom.Text = "结账区内的房间"; //选中父节点时改变结账房间名称
                lbsettel.Text = lbsettelRoom.Text;
                Frmmain.Sellmoney = 0;
                _roomsellmoney = 0;           //房屋成交价
                foreach (TreeNode tn in Tn.Nodes)
                {
                    TreeviewId = Convert.ToInt32(tn.Tag);
                    _treeViewName = tn.Text; //根据房间ID得取最近一次开房记录
                    GetGoodSellInfo(); // 取得房间的消费金额 
             //       tn.Tag = _roomInfoId;

                }
            }
        }

        private void button9_Click(object sender, EventArgs e)   // 将 房间号从待定tree区 到 合计 结账区
        {
            if (Free == "免单" || Free == "挂单")
            {
                MessageBox.Show("挂单和免单 不得联合结账 ！", "提示", MessageBoxButtons.OKCancel);
                return;
            }
            if (treeView2.SelectedNode == null)
            {
                return;
            }
            else
            {
                TreeNode tn = new TreeNode();
                Tn.Nodes.Add(tn);                           //添加节点
                tn.Tag = treeView2.SelectedNode.Tag;
                tn.Text = treeView2.SelectedNode.Text;
                TreeviewId = Convert.ToInt32(tn.Tag);        //计算总金额
                GetGoodSellInfo();
     //           tn.Tag = _roomInfoId;           //rentroomID
                _sellMoney = _sellMoney + Frmmain.Sellmoney;   // 消费总价
                _sumdeposit += _deposit;        // 押金累计
                lbDeposit.Text = string.Format("{0:F2}", _sumdeposit);// 显示押金
                lbsellmoney.Text = string.Format("{0:F2}", _sellMoney);//消费总额
                lbReceivable.Text = lbsellmoney.Text;//应收金额
                lbmoney.Text = lbsellmoney.Text;//合计
                txtGusetShell.Text = null;   //  宾客支付清零
                Lbshow();
                treeView2.SelectedNode.Remove();  // 删除节点
            }
        }

        private void button10_Click(object sender, EventArgs e)   // 从结账 tree 中 驱除，到 待定显示 中
        {
            if (treeView1.SelectedNode != null)
            {
                if (Convert.ToInt32(treeView1.SelectedNode.Tag) != 0)
                {
                    if (treeView1.SelectedNode.Text != Frmmain.RoomName)
                    {
                        TreeNode tn = new TreeNode();
                        treeView2.Nodes.Add(tn);                        //添加到 待定tree 区
                        tn.Tag = treeView1.SelectedNode.Tag;
                        tn.Text = treeView1.SelectedNode.Text;
                        treeView1.SelectedNode.Remove();                // 结账区的tree 移出
                        listView2.Items.Clear(); //房间费
                         _roomsellmoney = 0;           //房屋成交价
                        _sumdeposit = 0;
                        _lbReceivable = 0;
                        _sellMoney = 0;
            
                            foreach (TreeNode tnn in Tn.Nodes)
                            {

                                TreeviewId = Convert.ToInt32(tnn.Tag);        //计算总金额
                                GetGoodSellInfo();
                              //  tn.Tag = _roomInfoId;           //rentroomID
                                _sellMoney = _sellMoney + Frmmain.Sellmoney;   // 消费总价
                                _sumdeposit += _deposit;        // 押金累计
                                lbDeposit.Text = string.Format("{0:F2}", _sumdeposit);// 显示押金
                                lbsellmoney.Text = string.Format("{0:F2}", _sellMoney);//消费总额
                                lbReceivable.Text = lbsellmoney.Text;//应收金额
                                lbmoney.Text = lbsellmoney.Text;//合计
                                txtGusetShell.Text = null;   //  宾客支付清零
                            }
                            Lbshow();
                    }
                    else
                    {
                        MessageBox.Show("主单房间不可删除!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("不能删除父节点", "提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    return;
                }
            }
            else
            {
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)   //      确认结账
        {
            DialogResult dr;
            switch (Free)
            {
                case "挂单":
                         dr = MessageBox.Show("你确定挂单吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dr == DialogResult.OK)
                        {
                            goto jmpp;
                        }
                        else
                        {
                            return;
                        }
                case "免单":
                        dr = MessageBox.Show("你确定给客户免单吗？？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dr == DialogResult.OK)
                        {
                            goto jmpp;
                        }
                        else
                        {
                            return;
                        }       
                default:
                        dr = MessageBox.Show("你确定要结账吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dr == DialogResult.OK)
                        {
                            goto jmpp;
                        }
                        else
                        {
                            return;
                        }       
            }

jmpp:        if (Convert.ToDouble(lbseek.Text) < 0  || Free == "免单"  )//判断用户支付金额是否大于实际消费金额
                {
                foreach (TreeNode tn in Tn.Nodes)//循环遍历treeview1下的所有子节点
                    {    //根据子节点下房间的tag查询此房间最近一次开房记录
                         TreeviewId = (int)tn.Tag;        // = treeView2.SelectedNode.Tag;
                        _seek = Convert.ToDouble(lbseek.Text);      // 是否找零??????????
                        if (tn.Text == Frmmain.RoomName)    // 主结账 项目
                        {
                            if (Free == "免单")
                            {
                                
                                if (cbxfadeDeposit.Checked)
                                {   //  退押金
                                    _seek = _sellMoney;     // 免单金额是 全额款
                                    goto jmpp2;
                                }
                                else
                                {
                                    _seek = _sellMoney - _sumdeposit;   // 免单金额是 全额款-押金
                                    FrmSystemMain.cht.totalincome += _sumdeposit;  //押金计入收入
                                    goto jmpp2;
                                }
                            }
         //                   FrmSystemMain.cht.deposit -= _sumdeposit;    //_lbReceivable
                            FrmSystemMain.cht.totalincome = _sumdeposit + _lbReceivable + FrmSystemMain.cht.totalincome;  //应收入
   //                         FrmSystemMain.cht.totalincome = _sumdeposit + _sellMoney + FrmSystemMain.cht.totalincome;  //应收入
                            if (Free == "挂单")
                            {
                                FrmSystemMain.cht.totalincome += _seek;     //把挂单的也作为收入
                                FrmSystemMain.cht.Freemoney += _seek;        //挂单金额
                            }
 jmpp2:                     FrmSystemMain.cht.deposit -= _sumdeposit;       //押金总是要减去
                            FrmSystemMain.Writesys();  // 写 文件数据
                            _sql = string.Format("update RentRoom set OrderState='已结账',Receivable={0},Paid={1},PhoneCost={2},Settlenum='{3}', Receipt='{4}',Free='{5}',QuitD='{6}',Settlenote='{7}',LastEditDate='{8}',Jointcheck={9} where RentRoomInfoId={10}",
                                    _lbReceivable,            //应收款
                                    _seek,                    //找钱？ 
                                    0,                      //电话费
                                    lbsettelodd.Text,       //结账单号
                                    _receipt,               //发票
                                    Free,                   //挂单或免单
                                    _quitD,                 //退押金
                                    textBox4.Text,              // 结账注释
                                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                    0,                  //Jointcheck  联合结账 =0 为主结账， 非零为从属结账，指向 rentRoomInfoId
                                    _rentRoomInfoId);                           
                        }
                        else
                        {   // 联合结账 
                            _sql = string.Format("select RentRoomInfoId from RentRoom where RoomId={0} and RentTime=(select max(RentTime) from RentRoom where RoomId={0})", tn.Tag);
                            TreeviewId = Convert.ToInt32(SqlHelp.ExcuteScalar(_sql));    //修改最近一次开房记录的状态为以结账
                            _sql = string.Format("update RentRoom set OrderState='已结账',Receivable={0},Paid={1},PhoneCost={2},Settlenum='{3}', Receipt='{4}',Free='{5}',QuitD='{6}',Settlenote='{7}',LastEditDate='{8}',Jointcheck={9} where RentRoomInfoId={10}",
                              0,                    //应收款
                              _seek,                  //找钱？
                              0,                      //电话费
                              lbsettelodd.Text,       //结账单号
                              _receipt,               //发票
                              Free,                   //挂单或免单
                              _quitD,                 //退押金
                              textBox4.Text,              // 结账注释
                              DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                              _rentRoomInfoId,    //Jointcheck  联合结账 =0 为主结账， 非零为从属结账，指向 rentRoomInfoId
                              TreeviewId 
                              );      //存主 结账ID                                           
                        }
                        SqlHelp.ExcuteInsertUpdateDelete(_sql);
                        _sql = string.Format("update Room set RoomStateId=1 where  RoomId={0}", tn.Tag);        //修改房间状态为可供
                        SqlHelp.ExcuteInsertUpdateDelete(_sql);        //系统日志
                        string s = string.Format("[账单号]{0}[房间号]{1}结账", lbsettelodd.Text, tn.Text);
                        if (Free == "挂单")
                        {
                            _sql = string.Format("insert into SystemLog values ('{0}','{1}','挂单结账','{2}','','')",
                                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), AppInfo.UserName, s);
                        }
                        else if (Free == "免单")
                        {
                            s = s + "免单金额：" + string.Format("{0:F2}", _seek);    // string.Format("{0:F2}", _sumdeposit);// 显示押金
                            _sql = string.Format("insert into SystemLog values ('{0}','{1}','免单结账','{2}','','')",
      DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), AppInfo.UserName, s);
                        }
                        else
                        {
                            _sql = string.Format("insert into SystemLog values ('{0}','{1}','财务结账','{2}','','')",
DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), AppInfo.UserName, s); 
                        }
                        SqlHelp.ExcuteInsertUpdateDelete(_sql);
                    }
   //                 SoundPlayer sp = new SoundPlayer("sound/jzcg.wav");  //???????????
                    //try
                    //{
                    //    sp.Play();
                    //}
                    //catch (FileNotFoundException ex)
                    //{
                    //    throw ex;
                    //}

                    Frmmain.AddRoomType();              //调用刷新
                    Frmmain.GetTab2();
                    Frmmain.RoomStateId = "可供";
                    Frmmain.GetTab2Tob();
                    Frmmain.listView2.Items.Clear();
                    Frmmain.lbRoomType.Text = "标准单人间";
                    Close();
                }
                else
                {
                    MessageBox.Show("付款金额不足，不能结账!", "提示");
                    SoundPlayer sp = new SoundPlayer("sound/jebz.wav");
     //               sp.Play();   //???????????
                }
            }

        private void txtGusetShell_KeyPress(object sender, KeyPressEventArgs e)  //   /// 只允许用户输入数字
        {
            int a = (int)e.KeyChar;//得到用户按下的键并转换为整型
            if (a == 8 || (a >= 48 && a <= 57))//当用户按的是数字键或退格键或点时不做处理
            {
            }
            else
            {
                e.Handled = true;
            }
        }
 
        private void button4_Click(object sender, EventArgs e)          // 联合结账
        {
            if (cboowemoney.Checked == true)
            {
                MessageBox.Show("你已经选择了”挂账“,不能执行联合结账", "提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            else if (checkBox3.Checked == true)
            {
                MessageBox.Show("你已经选择了”免单“,不能执行联合结账", "提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            else
            {
                Frmallysettel allysettel = new Frmallysettel(this);
                allysettel.ShowDialog();
            }
        }
  
        private void button8_Click(object sender, EventArgs e)     // 电话费
        {
            //process1.StartInfo.FileName = "./tel.exe";
            //process1.Start();
        }
 
        private void checkBox1_CheckedChanged(object sender, EventArgs e)     // 签单
        {
            if (checkBox1.Checked == true)
            {
                lbpricemoney.Text = "0.00";
                txtpricemoney.Text = "0";
                panel9.Visible = true;
            }
            else
            {
                panel9.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel9.Visible = false;
            checkBox1.Checked = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (txtpricemoney.Text != "")
            {
                if (Convert.ToDouble(txtpricemoney.Text) <1)
                {
                    MessageBox.Show("请填写签单金额!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    return;
                }
                else
                {
                    lbpricemoney.Text = string.Format("{0:F2}", Convert.ToDouble(txtpricemoney.Text));
                    Lbshow();
                    textBox4.Text = string.Format("admin(admin)签单金额：{0}", Convert.ToDouble(txtpricemoney.Text));
                    panel9.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("请填写签单金额!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                txtpricemoney.Text = "0";
                return;
            }
        }

        private void txtpricemoney_KeyPress(object sender, KeyPressEventArgs e)   // 签单 只能输入 退格键和数字
        {
            int a = (int)e.KeyChar;
            if (a == 8 || (a >= 48 && a <= 57))
            {
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtPaid_KeyPress(object sender, KeyPressEventArgs e)  //实收金额  只输入数字和 退格键
        {
            int a = (int)e.KeyChar;
            if (a == 8 || (a >= 48 && a <= 57))
            {
            }
            else
            {
                e.Handled = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)  //打印 消费清单
        {
            MessageBox.Show("使用版本不提供此功能！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            return;
        }
  
        private void timer1_Tick_1(object sender, EventArgs e)    //播放声音
        {
            if (_b)
            {
                _b = false;
                Sound();         //播放声音
            }
            timer1.Stop();
        }

        private void FrmRentSetle_KeyDown(object sender, KeyEventArgs e)   //  主窗口 F3---F8 快捷键 执行
        {
            if (e.KeyCode == Keys.F5)
            {
                button3_Click(null, null);
            }
            else if (e.KeyCode == Keys.F9)
            {
                button4_Click(null, null);
            }
            else if (e.KeyCode == Keys.F3)
            {
                button5_Click(null, null);
            }
            else if (e.KeyCode == Keys.F8)
            {
                button6_Click(null,null);
            }
        }
        private void Sound()  //????????????   /// 播放声音
        {
        /*    string money = lbsellmoney.Text;
            int lang = money.IndexOf(".");
            SoundPlayer sp = new SoundPlayer("sound/nyxf.wav");
            try
            {
                sp.Play();
            }
            catch (FileNotFoundException ex)
            {
                throw ex;
            }

            Thread.Sleep(1000);
            for (int i = 0; i < money.Length; i++)
            {
                _s = money.Substring(i, 1);
                Soundpaly.Sound(_s);
            }
            SoundPlayer sp1 = new SoundPlayer("sound/yuan.wav");
            try
            {
                sp1.Play();
            }
            catch (FileNotFoundException ex)
            {
                throw ex;
            } 
            */
        }
        private void button6_Click(object sender, EventArgs e)   //print 打印 
        {
            MessageBox.Show("发票只能在税务局的网站上打印", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            return;
        }
        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)  // 指定房间 人工输入房间号
        {
            TreeNodeCollection nodes = treeView2.Nodes;  // 遍历TreeView并查找和选定节点  
            if (e.KeyChar == 13)  //  retrun 回车键
            {
                foreach (TreeNode tn2 in nodes)
                {
                    if (tn2.Text == textBox5.Text)
                    {
                        //tn2.
                        tn2.Checked = true;
                        tn2.ForeColor = Color.Red;
                        if (_lastNode != null)
                        {
                            _lastNode.ForeColor = Color.Black;
                        }
                        _lastNode = tn2;
                        treeView2.SelectedNode = tn2;
                        return;
                    }
                }
            }
        }

        private bool TextBox5Comp()   //没意义
        {
            if (textBox5.Text == _treeViewName)
            {
                if (_lastNode != null)
                {
                    _lastNode.ForeColor = Color.Black;
                }
                _lastNode = treeView2.SelectedNode;
                treeView1.SelectedNode.ForeColor = Color.Red;
                return true;
            }           
            return false;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)   // 开发票
        {
            _receipt = 'n';
            if (checkBox6.Checked)
            {
                _receipt = 'y';
            }
        }
    }
}
