using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace HotelSystem1115
{
    public partial class FrmDayinquire : Form
    {
        private Frmyy fm;
        public  string Pphone;      //客户电话
        public  string Pguestsou;      //客户来源
        private double idouble;
        public string Pyltime;
        System.IO.MemoryStream userInput = new System.IO.MemoryStream();     // Declare a new memory stream.
        private string lstr, sql;  //, year, mon, day;
        private DateTime NowDateTime,time;
        public static DateTime d1, d2;   //进离店 时间选择
        public static bool bjc;             // 进离店 判断
        private DataTable Clientdt = new DataTable(); //房间预订表
        private DataTable RentRoom = new DataTable(); //旅馆 总 房间
 //      private DataTable RentRoomall = new DataTable(); //旅馆 总 房间
        private int Receivable, allReceivable;  //  结账收到的金额
        private int i, count, yearincome, monincome, dayincome,sel;  //年月日收入
   //     private int cDestine;
        private string selitem;

        public FrmDayinquire()
        {
    //       fm = _fm;
            InitializeComponent();
        }

        private void FrmDayinquire_Load(object sender, EventArgs e)
        {

            NowDateTime = DateTime.Now;
            sel = 0;                                    //选择所有
            Clientdt = SqlHelp.ExcuteAsAdapter("select * from Client");
            DataRow row = Clientdt.NewRow();                     //前面加一个空白 行
            row["clientid"] = 0;
            row["clientName"] = "选择所有";
            Clientdt.Rows.InsertAt(row, 0);
            cmbsource.DataSource = Clientdt;  //加载宾客来源
            cmbsource.DisplayMember = "clientName";
            cmbsource.ValueMember = "clientId";
            RentRoom = SqlHelp.ExcuteAsAdapter("select * from RentRoom where OrderState = '正常'");
            count = 0;
            allReceivable = 0;   //总收入
            Showitem();

        }

        private void Showitem()     //xian
        {
            listView1.Items.Clear();  
            count = 0;
            allReceivable = 0;
            try
            {
                foreach (DataRow r in RentRoom.Rows) //取得年月日 营业额
                {
                    //SELECT RentRoomInfoId, RentRoomOrder, OrderState, RoomId, RentTime, RentDuration, RentDurationUnit, RentCost, Deposit, GusetType, GuestName, GuestCardType, 
                    //   GuestCardId, GusetPhone, VIPGuestId, LastEditDate, Receivable, Paid, PhoneCost, Settlenum, Receipt, Free, QuitD, Settlenote, DepositType, RoomName, Jointcheck FROM RentRoom
                    sql =  r["OrderState"].ToString();    //  = '正常'"
                    if (sql == "正常")
                    {
                        lstr = r["GusetType"].ToString();
                        if (sel == 0 || lstr == selitem)
                        {
                            var item = new ListViewItem();
                            listView1.Items.Add(item);
                            item.Tag = r["RentRoomInfoId"];
                            item.Text = r["RoomName"].ToString().Trim();
                            item.SubItems.Add(r["RoomTypeName"].ToString().Trim()); //RoomTypeName
                            lstr = r["GuestName"].ToString();
                            item.SubItems.Add(lstr);
                            lstr = r["GusetPhone"].ToString();
                            item.SubItems.Add(lstr);
                            item.SubItems.Add(r["GusetType"].ToString());
                            item.SubItems.Add(r["RentTime"].ToString());
                            item.SubItems.Add(r["GuestCardType"].ToString());
                            item.SubItems.Add(r["GuestCardId"].ToString());
                            time = Convert.ToDateTime(r["RentTime"]);
                            TimeSpan daydiff = NowDateTime - time; //入住时间差           
                            int cday = daydiff.Days;
                            if (cday == 0)
                            {
                                cday = 1;
                            }
                            sql = cday.ToString();
                            item.SubItems.Add(sql);
                            lstr = r["RentRoomOrder"].ToString();
                            idouble = double.Parse(lstr);
                            idouble = cday*idouble;
                            sql = idouble.ToString();
                            sql = sql + ".00";
                            item.SubItems.Add(sql);
                            item.SubItems.Add(r["Deposit"].ToString()); //押金
                            //         item.SubItems.Add(r["Remak"].ToString());
                            allReceivable = allReceivable + (int) idouble;
                            count++;
                        }
                        if (sel == 2)
                        {
                            string home = r["RoomName"].ToString().Trim(); //RoomTypeName
                            string name = r["GuestName"].ToString().Trim();
                            string phone = r["GusetPhone"].ToString().Trim();
                            time = Convert.ToDateTime(r["RentTime"]);
                            string stime = time.ToLongDateString();
                            if (selitem == home || selitem == name || selitem == phone || stime == selitem)
                            {
                                var item = new ListViewItem();
                                listView1.Items.Add(item);
                                item.Tag = r["RentRoomInfoId"];
                                item.Text = r["RoomName"].ToString().Trim();
                                item.SubItems.Add(r["RoomTypeName"].ToString().Trim()); //RoomTypeName
                                lstr = r["GuestName"].ToString();
                                item.SubItems.Add(lstr);
                                lstr = r["GusetPhone"].ToString();
                                item.SubItems.Add(lstr);
                                item.SubItems.Add(r["GusetType"].ToString());
                                item.SubItems.Add(r["RentTime"].ToString());
                                item.SubItems.Add(r["GuestCardType"].ToString());
                                item.SubItems.Add(r["GuestCardId"].ToString());
                                TimeSpan daydiff = NowDateTime - time; //入住时间差           
                                int cday = daydiff.Days;
                                if (cday == 0)
                                {
                                    cday = 1;
                                }
                                sql = cday.ToString();
                                item.SubItems.Add(sql);
                                lstr = r["RentRoomOrder"].ToString();
                                idouble = double.Parse(lstr);
                                idouble = cday*idouble;
                                sql = idouble.ToString();
                                sql = sql + ".00";
                                item.SubItems.Add(sql);
                                item.SubItems.Add(r["Deposit"].ToString()); //押金
                                //         item.SubItems.Add(r["Remak"].ToString());
                                allReceivable = allReceivable + (int) idouble;
                                count++;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                sql = e.ToString();
                MessageBox.Show(sql, "提示信息");
                throw;
            }
            sql = count.ToString();
            lstr = allReceivable.ToString();
            sql = "共：" + sql + " 记录";
            richTextBox1.Text = sql + "    合计："+lstr+ ".00元";;
        }

        private void tSbtn1_Click(object sender, EventArgs e)  // 查询
        {
            sel = 2;
            selitem = textBox1.Text.Trim();
            Showitem();
        }

    
        private void toolStripButton3_Click(object sender, EventArgs e) //  刷新
        {
            sel = 0;
            Showitem();
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)  // 导出
        {

        }

        private void toolStripButton7_Click(object sender, EventArgs e)  // 打印
        {

        }

        private void tSbtn9_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 导为文本文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //       RichTextBox rtb = new RichTextBox();     // 动态定义  控件 不能用！！！！
            foreach (ListViewItem item in listView1.Items)
            {
                richTextBox1.Text += item.Text + " 房间类型：";
                richTextBox1.Text += item.SubItems[1].Text + " 房间编号：";
                richTextBox1.Text += item.SubItems[2].Text + " 姓名：";
                richTextBox1.Text += item.SubItems[3].Text + " 电话：";
                richTextBox1.Text += item.SubItems[4].Text + " 预订单号：";
                richTextBox1.Text += item.SubItems[5].Text + " 客人来源：";
                richTextBox1.Text += item.SubItems[6].Text + " 预订时间：";
                richTextBox1.Text += item.SubItems[7].Text + " 保留时间：";
                richTextBox1.Text += item.SubItems[8].Text + " 预订时间：";
                richTextBox1.Text += item.SubItems[9].Text + " 备注：";
                richTextBox1.Text += '\n';
            }
            userInput.Position = 0;
            richTextBox1.SaveFile(userInput, RichTextBoxStreamType.PlainText);
            userInput.WriteByte(13);

            // Display the entire contents of the stream, by setting its position to 0, to RichTextBox2.
            // Call ShowDialog and check for a return value of DialogResult.OK, which indicates that the file was saved. 
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK)    // Open the file, copy the contents of memoryStream to fileStream,
            // and close fileStream. Set the memoryStream.Position value to 0 to  copy the entire stream. 
            {
                System.IO.Stream fileStream = saveFileDialog1.OpenFile();
                userInput.Position = 0;
                userInput.WriteTo(fileStream);
                fileStream.Close();
            }
        }

        private void cmbsource_SelectedIndexChanged(object sender, EventArgs e)
        {
            selitem = cmbsource.Text;
            sel = 1;
            switch (selitem)
            {
                case "选择所有":
                    sel = 0;
                    break;
            }
            Showitem();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e) // 住店宾客
        {
            sel = 0; Showitem();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)  //今日来店
        {
            sel = 2;
            selitem = NowDateTime.ToLongDateString();
            Showitem();
        }

        private void 进店时间ToolStripMenuItem_Click(object sender, EventArgs e)   //进店时间
        {
            bjc = true;
            var rs = new FrmInhotoltime();
            rs.ShowDialog();
            if (bjc)
            {
                alltime(false);
            }
        }

        private void 离店时间ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bjc = false;
            var rs = new FrmInhotoltime();
            rs.ShowDialog();
            if (bjc)
            {
                alltime(false);
            }
        }

        void alltime(bool b)
        {
              listView1.Items.Clear();
              count = 0;
              allReceivable = 0;
              try
              {
                  foreach (DataRow r in RentRoom.Rows) //取得年月日 营业额
                  {
                      //SELECT RentRoomInfoId, RentRoomOrder, OrderState, RoomId, RentTime, RentDuration, RentDurationUnit, RentCost, Deposit, GusetType, GuestName, GuestCardType, 
                      //   GuestCardId, GusetPhone, VIPGuestId, LastEditDate, Receivable, Paid, PhoneCost, Settlenum, Receipt, Free, QuitD, Settlenote, DepositType, RoomName, Jointcheck FROM RentRoom
                      
              //        listView1.Columns["columnHeader9"]= "wreqwer"

                      sql = r["OrderState"].ToString();    //  = '正常'"
                      if (sql == "正常")
                      {
                          lstr = r["GusetType"].ToString();
                          if (sel == 0 || lstr == selitem)
                          {
                              var item = new ListViewItem();
                              listView1.Items.Add(item);
                              item.Tag = r["RentRoomInfoId"];
                              item.Text = r["RoomName"].ToString().Trim();
                              item.SubItems.Add(r["RoomTypeName"].ToString().Trim()); //RoomTypeName
                              lstr = r["GuestName"].ToString();
                              item.SubItems.Add(lstr);
                              lstr = r["GusetPhone"].ToString();
                              item.SubItems.Add(lstr);
                              item.SubItems.Add(r["GusetType"].ToString());
                              item.SubItems.Add(r["RentTime"].ToString());
                              item.SubItems.Add(r["GuestCardType"].ToString());
                              item.SubItems.Add(r["GuestCardId"].ToString());
                              time = Convert.ToDateTime(r["RentTime"]);
                              TimeSpan daydiff = NowDateTime - time; //入住时间差           
                              int cday = daydiff.Days;
                              if (cday == 0)
                              {
                                  cday = 1;
                              }
                              sql = cday.ToString();
                              item.SubItems.Add(sql);
                              lstr = r["RentRoomOrder"].ToString();
                              idouble = double.Parse(lstr);
                              idouble = cday * idouble;
                              sql = idouble.ToString();
                              sql = sql + ".00";
                              item.SubItems.Add(sql);
                              item.SubItems.Add(r["Deposit"].ToString()); //押金
                              //         item.SubItems.Add(r["Remak"].ToString());
                              allReceivable = allReceivable + (int)idouble;
                              count++;
                          }
                          if (sel == 2)
                          {
                              string home = r["RoomName"].ToString().Trim(); //RoomTypeName
                              string name = r["GuestName"].ToString().Trim();
                              string phone = r["GusetPhone"].ToString().Trim();
                              time = Convert.ToDateTime(r["RentTime"]);
                              string stime = time.ToLongDateString();
                              if (selitem == home || selitem == name || selitem == phone || stime == selitem)
                              {
                                  var item = new ListViewItem();
                                  listView1.Items.Add(item);
                                  item.Tag = r["RentRoomInfoId"];
                                  item.Text = r["RoomName"].ToString().Trim();
                                  item.SubItems.Add(r["RoomTypeName"].ToString().Trim()); //RoomTypeName
                                  lstr = r["GuestName"].ToString();
                                  item.SubItems.Add(lstr);
                                  lstr = r["GusetPhone"].ToString();
                                  item.SubItems.Add(lstr);
                                  item.SubItems.Add(r["GusetType"].ToString());
                                  item.SubItems.Add(r["RentTime"].ToString());
                                  item.SubItems.Add(r["GuestCardType"].ToString());
                                  item.SubItems.Add(r["GuestCardId"].ToString());
                                  TimeSpan daydiff = NowDateTime - time; //入住时间差           
                                  int cday = daydiff.Days;
                                  if (cday == 0)
                                  {
                                      cday = 1;
                                  }
                                  sql = cday.ToString();
                                  item.SubItems.Add(sql);
                                  lstr = r["RentRoomOrder"].ToString();
                                  idouble = double.Parse(lstr);
                                  idouble = cday * idouble;
                                  sql = idouble.ToString();
                                  sql = sql + ".00";
                                  item.SubItems.Add(sql);
                                  item.SubItems.Add(r["Deposit"].ToString()); //押金
                                  //         item.SubItems.Add(r["Remak"].ToString());
                                  allReceivable = allReceivable + (int)idouble;
                                  count++;
                              }
                          }
                      }
                  }
              }
              catch (Exception e)
              {
                  sql = e.ToString();
                  MessageBox.Show(sql, "提示信息");
                  throw;
              }
              sql = count.ToString();
              lstr = allReceivable.ToString();
              sql = "共：" + sql + " 记录";
              richTextBox1.Text = sql + "    合计：" + lstr + ".00元"; ;

            }

    }
}
