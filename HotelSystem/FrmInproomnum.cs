using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HotelSystem1115
{
    public partial class FrmInproomnum : Form    //输入房间号 
    {
        public FrmMain Frmmain;   //FrmMain  
        DataSet ds = new DataSet();
        public int RoomStateId;
        public string RoomName,RoomTypeName;
        public int RoomId;
        DataTable dt = new DataTable();
        private string lstr;
        
        public FrmInproomnum(FrmMain frmain)  //本地类 的实例化
        {
            Frmmain = frmain;
            InitializeComponent();
            AppInfo.RoomName = null;  //关闭 窗体 无输入
            string sql = "SELECT RoomId,RoomName, RoomTypeId, RoomStateId FROM Room";   //ORDER BY RoomName  15-10-6
            var conn = new SqlConnection(SqlHelp.connStr);
            var com = new SqlCommand(sql, conn);
            var adapter = new SqlDataAdapter(com);
            conn.Open();
            adapter.Fill(dt);   //取数据表
            adapter.Fill(ds, "lbname");   //取数据源
            conn.Close(); com.Clone();
            comboBoxdata.DisplayMember = "RoomName";
            comboBoxdata.ValueMember = "RoomTypeId";
            comboBoxdata.DataSource = ds.Tables["lbname"];
            comboBoxdata.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();   //           Application.Exit();  这个为 退出系统。
        }

        private void button2_Click(object sender, EventArgs e)  //确认输入
        {
            {
                if (ok.Text == "可入住")
                {
                    Frmmain.lbool = true;
                    AppInfo.RoomName = textBox1.Text;
                }
            }
            Close();
        }

        private void FrmInproomnum_Load(object sender, EventArgs e)
        {
            if (Frmmain.B)  //如果有选中 房间
            {
                textBox1.Text = Frmmain.RoomName;
            }
        }

        private void comboBoxdata_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxdata.Text != "")
            {
                textBox1.Text = comboBoxdata.Text;
                foreach (DataRow r in dt.Rows) //Frmmain.Roomlx.Rows
                {
                    lstr = r["RoomName"].ToString();
                    if (textBox1.Text == lstr)
                    {
                        Readdataz(r);
                    }
                }
                if (Frmmain.B)  //如果有选中 房间
                {
                    textBox1.Text = Frmmain.RoomName;
                }
            }
        }

        private void textBox1_Click(object sender, EventArgs e)  //输入房间号
        {
            ok.Text = ""; textBox1.Text = "";    }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
   
        }

        public void Readdataz(DataRow r)
        {
            ok.Text = "";
            int ii = Convert.ToInt32(r["RoomTypeId"]);
            labellx.Text = FrmMain.Getdatalx(ii);
            Frmmain.RoomName = lstr;
            Frmmain.RoomId = Convert.ToInt32(r["RoomId"]);//把用户选中项的房间ID赋值给变量
            Frmmain.RoomTypeName = labellx.Text;   //r["RoomTypeName"].ToString();//把用户选中项的房间类型赋值给变量
            ii = Convert.ToInt32(r["Roomstateid"]);   //显示房间状态
            labelzt.Text = FrmMain.Getdatazt(ii);
            if (labelzt.Text == "可供" || labelzt.Text == "预定")
            {
                ok.Text = "可入住";
                Frmmain.RoomStateId = labelzt.Text;
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)  //按键抬起 输入完 房间号
        {
            string ss = textBox1.Text;
            if (ss.Length >= 3)
            {
                foreach (DataRow r in dt.Rows) //Frmmain.Roomlx.Rows
                {
                    lstr = r["RoomName"].ToString();
                    if (ss == lstr)
                    {
                        Readdataz(r);
                    }
                }
            }
        }
    }
}

