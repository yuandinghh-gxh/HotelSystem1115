using System;
using System.Data;
using System.Windows.Forms;

namespace HotelSystem1115
{
    public partial class Frmdestine : Form
    {
        public static string  Pphone;      //客户电话
        public static string Pguestsou;      //客户来源
        private FrmMain _fm;
        private string lstr;
        private DataTable DestineTable = new DataTable(); //房间预订表
        public string Pyltime;
        System.IO.MemoryStream userInput = new System.IO.MemoryStream();     // Declare a new memory stream.

        public Frmdestine(FrmMain fm)
        {
            _fm = fm;
            InitializeComponent();
        }

        private void Frmdestine_Load(object sender, EventArgs e)
        {
            Showdestine();       //加载预定信息
        
        }
//        SELECT DestineId, DestineState, RoomType, GuestName, RoomId, company, Phone, DestineNumber, ClientId, ydtime, yltime, bltime, ydingtime, Remak
        public  void Showdestine()
        {
            DestineTable = SqlHelp.ExcuteAsAdapter("select * from Destine");
            listView1.Items.Clear();
            foreach (DataRow r in DestineTable.Rows) //取 房间预订
            {
                var item = new ListViewItem();
                listView1.Items.Add(item);
                item.Tag = r["DestineId"];
                item.Text = r["DestineState"].ToString();
                item.SubItems.Add(r["RoomType"].ToString());
                item.SubItems.Add(r["RoomId"].ToString());
                item.SubItems.Add(r["GuestName"].ToString());
                item.SubItems.Add(r["Phone"].ToString());
                item.SubItems.Add(r["DestineNumber"].ToString());
                item.SubItems.Add(r["ClientId"].ToString());
                lstr = r["ydtime"].ToString();
                lstr = lstr.Substring(0, 16);
                item.SubItems.Add(lstr);
                Pyltime = r["yltime"].ToString();   // 离店时间
          //      item.SubItems.Add(r["yltime"].ToString());
                lstr = r["bltime"].ToString();
                lstr = lstr.Substring(0, 16);
                item.SubItems.Add(lstr);
                lstr = r["ydingtime"].ToString();
                lstr = lstr.Substring(0, 16);
                item.SubItems.Add(lstr);
                item.SubItems.Add(r["Remak"].ToString());
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)   //新增 预订
        {
            bool b = false;
            FrmDestineregister fdr = new FrmDestineregister(this,b);
            fdr.ShowDialog();
        }

        private void Frmdestine_FormClosing(object sender, FormClosingEventArgs e)  //刷新
        {
            _fm.Refresh();
        }
 
        private void toolStripButton2_Click(object sender, EventArgs e)   // 修改预定
        {
            if (listView1.SelectedItems.Count != 0)
            {
                var fdr = new FrmDestineregister(this, true);
                fdr.ShowDialog();
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)  //取消预订
        {
            if (listView1.SelectedItems.Count == 0)
            {
            }
            else
            {
                if (listView1.SelectedItems[0].Text == "预定中")
                {
                    string sql = string.Format("Update Destine set DestineState='以取消预定' where DestineId={0}", listView1.SelectedItems[0].Tag);
                    SqlHelp.ExcuteInsertUpdateDelete(sql);
                    string sql2 = string.Format("Update Room set RoomStateId=1 where RoomName='{0}'", listView1.SelectedItems[0].SubItems[2].Text);
                    SqlHelp.ExcuteInsertUpdateDelete(sql2);
                    //系统日志
                    string s = string.Format("房间{0}以取消预定", listView1.SelectedItems[0].SubItems[2].Text);
                    string sql6 = string.Format("insert into SystemLog values ('{0}','{1}','预定管理','{2}','','')", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), AppInfo.UserName, s);
                    SqlHelp.ExcuteInsertUpdateDelete(sql6);
                    Showdestine();
                }
                else if (listView1.SelectedItems[0].Text == "以取消预定")
                {
                    string sql = string.Format("Update Destine set DestineState='预定中' where DestineId={0}", listView1.SelectedItems[0].Tag);
                    SqlHelp.ExcuteInsertUpdateDelete(sql);
                    string sql2 = string.Format("Update Room set RoomStateId=4 where RoomName='{0}'", listView1.SelectedItems[0].SubItems[2].Text);
                    SqlHelp.ExcuteInsertUpdateDelete(sql2);
                    //系统日志
                    string s = string.Format("房间{0}以还原预定", listView1.SelectedItems[0].SubItems[2].Text);
                    string sql6 = string.Format("insert into SystemLog values ('{0}','{1}','预定管理','{2}','','')", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), AppInfo.UserName, s);
                    SqlHelp.ExcuteInsertUpdateDelete(sql6);
                    Showdestine();
                }
                else
                {
                    MessageBox.Show("此房间以开房，不能还原！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }
        }
  
        private void toolStripButton4_Click(object sender, EventArgs e)   // 删除预定
        {
            if (listView1.SelectedItems.Count != 0)
            {
                string sql2 = string.Format("Update Room set RoomStateId=1 where RoomName='{0}'", listView1.SelectedItems[0].SubItems[2].Text);
                SqlHelp.ExcuteInsertUpdateDelete(sql2);
                string sql = "Delete Destine where DestineId=" + listView1.SelectedItems[0].Tag;
                SqlHelp.ExcuteInsertUpdateDelete(sql);
                string s = string.Format("房间{0}预定删除", listView1.SelectedItems[0].SubItems[2].Text);       //系统日志
                string sql6 = string.Format("insert into SystemLog values ('{0}','{1}','预定管理','{2}','','')", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), AppInfo.UserName, s);
                SqlHelp.ExcuteInsertUpdateDelete(sql6);
                FrmMain.Room = SqlHelp.ExcuteAsAdapter("select * from Room order by RoomName ");  //更新room数据
                Showdestine();
            }
        }

        private void toolStripButton8_Click(object sender, EventArgs e)  // 退出 窗口
        {
            Close();
        }
    
        private void button1_Click(object sender, EventArgs e)  // 查询
        {
            MessageBox.Show("预订人多吗，要查询 ？", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);

  
        }

        private void toolStripButton5_Click(object sender, EventArgs e)  // 开单
        {
            FrmMain.bdestine = true;  //非 预订进入开房
            if (listView1.SelectedItems.Count != 0)
            {
               _fm.RoomTypeName = listView1.SelectedItems[0].SubItems[1].Text;                     //存放用户选中项的房间类型
               _fm.RoomName = listView1.SelectedItems[0].SubItems[2].Text;      //存放用户选中项的房间名称
               _fm.GuestName = listView1.SelectedItems[0].SubItems[3].Text;
               foreach (DataRow r in FrmMain.Room.Rows)                 //房间名字   返回 房间ID  roomid 
               {
                   if (_fm.RoomName == r["RoomName"].ToString())
                   {
                       _fm.RoomId = Convert.ToInt32(r["RoomId"]);
                       break;
                   }
               }
               Pphone = listView1.SelectedItems[0].SubItems[4].Text;      //存放用户选中项的房间姓名
               Pguestsou = listView1.SelectedItems[0].SubItems[6].Text;
                //lstr = string.Format("Update Room set RoomStateId=2 where RoomName='{0}'", listView1.SelectedItems[0].SubItems[2].Text); //设置房间已经入住
                //SqlHelp.ExcuteInsertUpdateDelete(lstr);
                //string s = string.Format("预定房{0}以开单", listView1.SelectedItems[0].SubItems[2].Text);      //系统日志
                //lstr  = string.Format("insert into SystemLog values ('{0}','{1}','预定管理','{2}')", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), AppInfo.UserName, s);
                //SqlHelp.ExcuteInsertUpdateDelete(lstr);
                //Showdestine();
                Close();
            }
            else
            {
                MessageBox.Show("你没选中要预订的客人！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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

                // Display the entire contents of the stream,
                // by setting its position to 0, to RichTextBox2.
                // Call ShowDialog and check for a return value of DialogResult.OK,
                // which indicates that the file was saved. 
                DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK)    // Open the file, copy the contents of memoryStream to fileStream,
                // and close fileStream. Set the memoryStream.Position value to 0 to 
                // copy the entire stream. 
                {
                    System.IO.Stream fileStream = saveFileDialog1.OpenFile();
                    userInput.Position = 0;
                    userInput.WriteTo(fileStream);
                    fileStream.Close();
                }
        }
        private void 导出ToolStripMenuItem_Click(object sender, EventArgs e)  //导出为EXCEL文件
        {
            MessageBox.Show("未完成Excel 文件输出!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("未完成打印功能 ！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {

        }
    }
}
