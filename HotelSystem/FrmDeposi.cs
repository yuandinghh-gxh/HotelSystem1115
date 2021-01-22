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
    public partial class FrmDeposi : Form
    {
        private double _sumdeposit;        //押金总会， 当多房间联合结账时用
        private double _deposit;        //押金总会， 当多房间联合结账时用
        private string _sql;
        public FrmMain Frmmain;   //FrmMain  
        public static bool depbool;
        public static string RoomName;
        public FrmDeposi(FrmMain frmain)  //本地类 的实例化
        {
            Frmmain = frmain;
            InitializeComponent();

        }
        private void FrmDeposi_Load(object sender, EventArgs e)
        {
            _sumdeposit = 0;
            depbool = false;
            label1.Text += Frmmain.RoomName;
            RoomName = Frmmain.RoomName;
            label3.Text += Frmmain.GuestName;
            _sql = string.Format("select RentRoomInfoId from RentRoom where RoomName={0} and RentTime=(select max(RentTime) from RentRoom where RoomName={0})", Frmmain.RoomName);
            int rentRoomInfoId = Convert.ToInt32(SqlHelp.ExcuteScalar(_sql));
            _sql = string.Format("select * from Deposit where RentRoomInfoId ={0}", rentRoomInfoId);
            DataTable dt1 = SqlHelp.ExcuteAsAdapter(_sql);
            listView1.Items.Clear(); //房间费 显示条
            foreach (DataRow row in dt1.Rows)
            {
                var item = new ListViewItem();
                listView1.Items.Add(item);
 //  item.Text = "房间费";   // 第一列是 Text    
                string data = row["Time"].ToString();
                item.SubItems[0].Name = data;
                item.SubItems.Add(data);
                _deposit = Convert.ToDouble(row["Cash"]);
                string ss = string.Format("{0}", _deposit);
                item.SubItems.Add(ss);
                _sumdeposit += _deposit;
                item.SubItems.Add(row["Explain"].ToString());
            }
            var item3 = new ListViewItem();
            listView1.Items.Add(item3);     //空一行
            var item1 = new ListViewItem();
            listView1.Items.Add(item1);
            item1.SubItems.Add("已经缴纳押金合计：");
            item1.UseItemStyleForSubItems = false; //this line makes things work 
            item1.SubItems.Add(string.Format("{0:F2}", _sumdeposit), Color.Red, Color.MintCream, Font);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            depbool = true;
            Close();
        }
    }
}
