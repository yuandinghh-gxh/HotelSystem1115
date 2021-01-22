using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HotelSystem1115
{
    public partial class Frmupmeney : Form
    {
       public FrmMain Frmmain;   //FrmMain  
        private double _sum; //客户找零 。。
        private double imprest; //备用金
        private string _sql;
        public static int Shiftid; //存主结账 ID
        private double _sellmoney; //计算优惠金额 
        private DataTable _dt;
  //      private int newinput;           //xin
        private double _upinput;      //上交营业款
        private double _input;

        public Frmupmeney(FrmMain frmain)
        {
            Frmmain = frmain;
            InitializeComponent();
        }

        private void Frmupmeney_Load(object sender, EventArgs e)
        {
            _upinput = 0;
            imprest = 0;
            _sql = "select top 1 * from Shift order by Shiftid desc"; //去最后两行
            _dt = SqlHelp.ExcuteAsAdapter(_sql);
            foreach (DataRow row in _dt.Rows)
            {
                Shiftid = Convert.ToInt32(row["Shiftid"]);
                FrmMain.Shiftidbool = false;
                label2.Text = row["Turnover"].ToString();
                label4.Text = row["Deposit"].ToString();
                label10.Text = row["Operator"].ToString();
                label8.Text = row["Time"].ToString();
                label42.Text = row["Hourtime"].ToString();
                _sum = Convert.ToDouble(label2.Text) + Convert.ToDouble(label4.Text);
                label6.Text = _sum.ToString();
                Shiftid = Convert .ToInt32(row["Shiftid"]);
            }

            label20.Text = FrmSystemMain.cht.totalincome.ToString(); //总收入
            label17.Text = FrmSystemMain.cht.deposit.ToString(); //总押金
            label23.Text = FrmSystemMain.cht.Hourtime.ToString(); //钟点房收入
            _sum = Convert.ToDouble(label20.Text) + Convert.ToDouble(label17.Text);
            label15.Text = _sum.ToString();
            label11.Text = AppInfo.UserName;
            label26.Text = label20.Text;
            label25.Text = label20.Text;

            _sql = string.Format("select * from RentRoom where RentTime >={0}", FrmMain.NowDateTime);
            _dt = SqlHelp.ExcuteAsAdapter(_sql);
            if (_dt.Rows.Count == 0) // 没找到 客户信息
            {
                return;
            }

            listView1.Items.Clear(); //房间费 显示条
            foreach (DataRow row in _dt.Rows)
            {

            }

        }

        #region    已经完成程序  上交营业款，收到备用金

        private void textBox1_TextChanged(object sender, EventArgs e) //上交款
        {
            if (textBox1.Text != "")
            {
                double upinputt = Convert.ToDouble(textBox1.Text);
                double newinput = Convert.ToDouble(label20.Text) - upinputt;
                label25.Text = newinput.ToString();
            }
        }

        private void timer1_Tick(object sender, EventArgs e) //时间
        {
            nowtime.Text = FrmMain.NowDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            // string.Format("{0:U}", dt);//2005年11月5日 14:23
        }

        private void button1_Click(object sender, EventArgs e) //放弃
        {
            Close();
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e) //上交款只能输入数字
        {
            int a = (int) e.KeyChar;
            if (a == 8 || (a >= 48 && a <= 57))
            {
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e) //下放备用金  只能输入数字
        {
            int a = (int) e.KeyChar;
            if (a == 8 || (a >= 48 && a <= 57))
            {
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)  //下放备用金
        {
            if (textBox2.Text != "")
            {
                imprest = Convert.ToInt32(textBox2.Text);
                if (textBox1.Text != "")
                {
                    _input = Convert.ToInt32(label20.Text) + imprest - Convert.ToInt32(textBox1.Text);
                }
                else
                {
                    _input = Convert.ToInt32(label20.Text) + imprest;
                }
                label26.Text = _input.ToString();
   //             input = Convert.ToDouble(label20.Text) + input;
  //              label20.Text = input.ToString();
            }
            else
            {
                imprest = 0;
                label20.Text = FrmSystemMain.cht.totalincome.ToString(); //总收入
                label26.Text = label20.Text;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)  //上交营业款
        {
            if (FrmSystemMain.cht.Upmoney != 0)
            {
                MessageBox.Show("本班已上交营业款，无需再次上交");
                return;
            }
            DialogResult dr = MessageBox.Show("你确定上交营业款？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                if (textBox1.Text != "")
                {
                    _upinput = Convert.ToDouble(textBox1.Text);
                    if (_upinput <= 0)
                    {
                        MessageBox.Show("请输入要上交营业款");
                        return;
                    }
                    FrmSystemMain.cht.totalincome -= _upinput;
                    FrmSystemMain.cht.Upmoney = _upinput;
                    FrmSystemMain.Writesys();
                    Close();
                }
                else
                {
                    MessageBox.Show("请输入要上交营业款");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)  //收到备用金
        {
            if (FrmSystemMain.cht.Downmoney != 0)
            {
                MessageBox.Show("本班已收到备用金 !");
                return;
            }
            DialogResult dr = MessageBox.Show("你确定收到备用金？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                if (textBox2.Text != "")
                {
                    _upinput = Convert.ToDouble(textBox2.Text);
                    if (_upinput <= 0)
                    {
                        MessageBox.Show("请输入下放的备用金");
                        return;
                    }
                    FrmSystemMain.cht.totalincome += imprest;
                    FrmSystemMain.cht.Downmoney = imprest;
                    FrmSystemMain.Writesys();
                    Close();
                }
                else
                {
                    MessageBox.Show("请输入下放的备用金");
                }
            }
        }
 #endregion

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


            foreach (DataRow row in _dt.Rows)
            {
                var item = new ListViewItem();
                listView1.Items.Add(item);
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
                    TimeSpan span1 = FrmMain.NowDateTime.Subtract(dateTime); //计算 相差 时间天数
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
                _sellmoney += Convert.ToDouble(row["RentRoomOrder"])*Convert.ToDouble(row["RentDuration"]); //消费总金额
            }
        }

        private void button4_Click(object sender, EventArgs e) //确认交班
        {
            DialogResult dr = MessageBox.Show("你确定交班吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                _sql = string.Format("insert into Shift values ({0},{1},'{2}','{3}',{4},{5},{6},'')", //再次进入登录收入接班人员
                    FrmSystemMain.cht.totalincome,
                    FrmSystemMain.cht.deposit,
                    AppInfo.UserName,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), //4  [RentTime]
                    FrmSystemMain.cht.Upmoney,
                    FrmSystemMain.cht.Downmoney,
                    FrmSystemMain.cht.Hourtime);
                //    SELECT Shiftid, Turnover, Deposit, Operator, Time, Upmoney, Downmoney, Hourtime, Relief   FROM Shift
                SqlHelp.ExcuteInsertUpdateDelete(_sql);

                string syslogstr = string.Format("营业额：{0}，上交款：{1}，下放备用金：{2}，本班押金：{3}",
                     FrmSystemMain.cht.totalincome,
                     FrmSystemMain.cht.Upmoney,
                     FrmSystemMain.cht.Downmoney,
                     FrmSystemMain.cht.deposit);
                _sql = string.Format("insert into SystemLog values ('{0}','{1}','结账交班','{2}','','')",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), AppInfo.UserName, syslogstr); //  记录  开房事件
                SqlHelp.ExcuteInsertUpdateDelete(_sql);

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
                FrmSystemMain.Writesys();

                FrmMain.Shiftidbool = true;

            }

            MessageBox.Show(" 交班成功! ");
            FrmMain.Shiftidbool = true;
            Close();

        }

  
    }
}

 
    

