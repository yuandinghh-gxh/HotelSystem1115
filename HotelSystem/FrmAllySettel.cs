using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.IO;

namespace HotelSystem1115
{
    public partial class Frmallysettel : Form
    {
        private FrmRentSetle _rs;
        public Frmallysettel(FrmRentSetle rs)
        {
            _rs = rs;
            InitializeComponent();
        }

        private void Frmallysettel_Load(object sender, EventArgs e)
        {
            skinEngine1.SkinFile = "s (1).ssk";
            ListViewItem item = new ListViewItem();
            listView1.Items.Add(item);
            item.Text = "现金";
            item.SubItems.Add(_rs.lbDeposit.Text);
            string sql = string.Format("select RentTime from RentRoom  where RoomId={0} and RentTime=(select max(RentTime) from RentRoom where RoomId={0})", _rs.TreeviewId);   //_rs.SetleRoomId);   
            DateTime d = Convert.ToDateTime(SqlHelp.ExcuteScalar(sql));
            ListViewItem item2 = new ListViewItem();
            listView2.Items.Add(item2);
            item2.Text = d.ToString("yyyy-MM-dd HH:ss:mm");
            item2.SubItems.Add(_rs.lbDeposit.Text);
            item2.SubItems.Add("admin");
            //应收金额
            lbReceivable.Text = _rs.lbReceivable.Text;
            //押金
            lbDeposit.Text = _rs.lbDeposit.Text;
            //签单
            lbpricemoney.Text = _rs.lbpricemoney.Text;
            label18.Text = lbReceivable.Text;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked==true)
            {
                textBox1.Text = label18.Text;
                label18.Text = "0.00";
                label20.Text = lbDeposit.Text;
            }
            else
            {
                textBox1.Text = "0.00";
                label18.Text = lbReceivable.Text;
                label20.Text = "0.00";
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                textBox2.Text = label18.Text;
                label18.Text = "0.00";
                label20.Text = lbDeposit.Text;
            }
            else
            {
                textBox2.Text = "0.00";
                label18.Text = lbReceivable.Text;
                label20.Text = "0.00";
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                textBox3.Text = label18.Text;
                label18.Text = "0.00";
                label20.Text = lbDeposit.Text;
            }
            else
            {
                textBox3.Text = "0.00";
                label18.Text = lbReceivable.Text;
                label20.Text = "0.00";
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                textBox4.Text = label18.Text;
                label18.Text = "0.00";
                label20.Text = lbDeposit.Text;
            }
            else
            {
                textBox4.Text = "0.00";
                label18.Text = lbReceivable.Text;
                label20.Text = "0.00";
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                textBox5.Text = label18.Text;
                label18.Text = "0.00";
                label20.Text = lbDeposit.Text;
            }
            else
            {
                textBox5.Text = "0.00";
                label18.Text = lbReceivable.Text;
                label20.Text = "0.00";
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked == true)
            {
                textBox6.Text = label18.Text;
                label18.Text = "0.00";
                label20.Text = lbDeposit.Text;
            }
            else
            {
                textBox6.Text = "0.00";
                label18.Text = lbReceivable.Text;
                label20.Text = "0.00";
            }
        }
        /// <summary>
        /// 确认结账
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("你确定要结账吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                if (Convert.ToDouble(label20.Text) >= Convert.ToDouble(label18.Text))
                {
                    foreach (TreeNode tn in _rs.Tn.Nodes)//循环遍历treeview1下的所有子节点
                    {
                        //根据子节点下房间的tag查询此房间最近一次开房记录
                        string sql = string.Format("select RentRoomInfoid from RentRoom where RoomId={0} and RentTime=(select max(RentTime) from RentRoom where RoomId={0})", tn.Tag);
                        int a = Convert.ToInt32(SqlHelp.ExcuteScalar(sql));
                        //修改最近一次开房记录的状态为以结账
                        string sql2 = string.Format("update RentRoom set OrderState='已结账',Remark='{0}' where RentRoomInfoId={1}", textBox4.Text, a);
                        SqlHelp.ExcuteInsertUpdateDelete(sql2);
                        //修改房间状态为可供
                        string sql3 = string.Format("update Room set RoomStateId=1 where RoomId={0}", tn.Tag);
                        SqlHelp.ExcuteInsertUpdateDelete(sql3);
                        //系统日志
                        string s = string.Format("[账单号]{0}[房间号]{1}结账", _rs.lbsettelodd.Text, tn.Text);
                        string sql4 = string.Format("insert into SystemLog values ('{0}','{1}','收银结账','{2}')", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), AppInfo.UserName, s);
                        SqlHelp.ExcuteInsertUpdateDelete(sql4);
                    }

                    SoundPlayer sp = new SoundPlayer("sound/jzcg.wav");
                    try
                    {
                        sp.Play();
                    }
                    catch (FileNotFoundException ex)
                    {
                        throw ex;
                    }

                    _rs.Frmmain.RoomStateId = "可供";
                    _rs.Frmmain.GetTab2Tob(); 
                    _rs.Frmmain.AddRoomType();//调用刷新
                    _rs.Frmmain.GetTab2();
                    _rs.Frmmain.listView2.Items.Clear();
                    _rs.Frmmain.lbRoomType.Text = "标准单人间";
                    _rs.Close();
                    Close();
                }
                else
                {
                    MessageBox.Show("付款金额不足，不能结账!", "提示");
                    SoundPlayer sp = new SoundPlayer("sound/jebz.wav");
                    sp.Play();
                }

            }
        }

        private void Frmallysettel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                button1_Click(null, null);
            }
            else if (e.KeyCode == Keys.F4)
            {
                Close();
            }
        }
    }
}
