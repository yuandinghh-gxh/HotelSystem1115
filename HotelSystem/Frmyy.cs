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
    public partial class Frmyy : Form
    {
        private FrmMain fm;
   //     private string roomlx;
        private string sql,year,mon,day;
        public DateTime NowDateTime;
        public DataTable RentRoom = new DataTable(); //旅馆 总 房间
        public DataTable Expense = new DataTable(); //旅馆 总 房间
        private int Receivable;  //  结账收到的金额
        private int i,count,yearincome,monincome,dayincome;  //年月日收入
        private int cDestine;
        private string selitem;

        public Frmyy(FrmMain _fm)
        {
            fm = _fm;
            InitializeComponent();
        }
//SELECT RentRoomInfoId, RentRoomOrder, OrderState, RoomId, RentTime, RentDuration, RentDurationUnit, RentCost, Deposit, GusetType, GuestName, GuestCardType, 
//      GuestCardId, GusetPhone, VIPGuestId, LastEditDate, Receivable, Paid, PhoneCost, Settlenum, Receipt, Free, QuitD, Settlenote, DepositType, RoomName, Jointcheck FROM RentRoom
        private void Frmyy_Load(object sender, EventArgs e)
        {
            NowDateTime = DateTime.Now;
            Receivable = 0;
            year = NowDateTime.Year.ToString(CultureInfo.InvariantCulture);
            mon = NowDateTime.Month.ToString(CultureInfo.InvariantCulture);
            day = NowDateTime.Day.ToString(CultureInfo.InvariantCulture);
            count = 0;
            foreach (DataRow r in FrmMain.Room.Rows)
            {
                i = Convert.ToInt32(r["RoomStateId"]);
                if (i == 2) // 入住
                {
                    count++;
                }
            }
            sql = count.ToString();
            sql = "今日入住：" + sql + " 间";
            label1.Text = sql;

            sql = string.Format("select * from RentRoom where OrderState = '已结账' and DATEPART(YEAR, RentTime) = '{0}'",year);
            RentRoom = SqlHelp.ExcuteAsAdapter(sql);
            count = 0;
            monincome = 0;
            yearincome = 0;
            dayincome = 0;
            foreach (DataRow r in RentRoom.Rows) //取得年月日 营业额
            {
                NowDateTime = Convert.ToDateTime(r["RentTime"]);
                Receivable = Convert.ToInt32(r["Receivable"]);
                yearincome += Receivable;
                if (mon == NowDateTime.Month.ToString(CultureInfo.InvariantCulture))
                {
                    monincome += Receivable;
                    if (day == NowDateTime.Day.ToString())
                    {
                        dayincome += Receivable;

                    }
                }
                count++;
            }

            sql = dayincome.ToString();
            sql = "今日累计营业额：" + sql + "元";
            label2.Text = sql;
            sql = monincome.ToString();
            sql = "本月累计收入：" + sql + "元";
            label5.Text = sql;

            foreach (DataRow r in RentRoom.Rows) //取得年月日 营业额
            {
                Receivable = Convert.ToInt32(r["Receivable"]);
                cDestine = 0;
                if (mon == NowDateTime.Month.ToString(CultureInfo.InvariantCulture))
                {
                    monincome += Receivable;
                    if (day == NowDateTime.Day.ToString())
                    {
                        dayincome += Receivable;
                    }
                }
            }
            DataTable destineTable = SqlHelp.ExcuteAsAdapter("select * from Destine"); // 预订数量房间
            cDestine = 0;
            foreach (DataRow r in destineTable.Rows) //取 房间预订
            {
                sql = r["DestineState"].ToString();
                if ("预定中" == sql || sql == "强制预定")
                {
                    cDestine++;
                }
            }
            sql = cDestine.ToString();
            sql = "已经预订：" + sql + " 房间";
            label3.Text = sql;

            sql = string.Format("select * from Expense where DATEPART(MONTH, data) = '{0}' and DATEPART(YEAR, data) = '{1}'", mon,year);
            Expense =SqlHelp.ExcuteAsAdapter(sql);
            cDestine = 0;
            foreach (DataRow r in Expense.Rows) //取 支出数据库 
            {
                cDestine = Convert.ToInt32(r["itemcash"]);
                //if ("预定中" == sql || sql == "强制预定")
                //{
                //    cDestine++;
                //}
            }
            sql = cDestine.ToString();
            sql = "本月支出：" + sql + " 元";
            label4.Text = sql;
        }
   
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            var lv = (ListView)sender; //获取当前点击的ListView
            selitem = lv.SelectedItems[0].Text; //把用户选中项的房间名称赋值给变量
            switch (selitem)
            {
                case "来宾信息查询"  :
                    var rs = new FrmDayinquire();
                    rs.ShowDialog();
                    break;
                case "结账明细查询" :
                    var rs1 = new FrmCheckout();      rs1.ShowDialog();      break;
                case "消费退单明细查询":

                    break;
                case "历史进店/离店查询":

                    break;
                case "结账单消费查询":

                    break;
                case "收银明细查询":

                    break;
                case "更换房间信息查询":

                    break;
                case "客人消费查询":

                    break;
            }
        }

        private void listView3_Click(object sender, EventArgs e)  //经营分析
        {
            var lv = (ListView)sender; //获取当前点击的ListView
            selitem = lv.SelectedItems[0].Text; //把用户选中项的房间名称赋值给变量
            switch (selitem)
            {
                case "日营业分析":

                    break;
                case "月营业分析":

                    break;
                case "年营业分析":

                    break;
                case "客源分析":

                    break;
                case "消费品分析":

                    break;
            }

        }

        private void listView2_Click(object sender, EventArgs e) // 财务分析
        {
            var lv = (ListView)sender; //获取当前点击的ListView
            selitem = lv.SelectedItems[0].Text; //把用户选中项的房间名称赋值给变量
            switch (selitem)
            {
                case "日/月营业查询":

                    break;
                case "日/月营业统计":

                    break;
                case "营业统计排行":

                    break;
                case "房类销售分析":

                    break;
                case "入住率统计":

                    break;
                case "综合统计报表":

                    break;
                case "消费分类统计":

                    break;
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
