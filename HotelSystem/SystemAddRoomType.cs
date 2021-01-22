using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HotelSystem1115
{
    public partial class SystemAddRoomType : Form
    {
        private FrmSystemMain _fsm;
        private bool _b;//存储用户选中的是增加还是修改
        public SystemAddRoomType(FrmSystemMain fsm,bool b)
        {
            _b = b;
            _fsm = fsm;
            InitializeComponent();
        }
        /// <summary>
        /// 床位 禁止输入除非数字外的键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            int a = (int)e.KeyChar;
            if (a == 8 || (a >= 48 && a <= 58))
            {
            }
            else
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// 预设单价 禁止输入除非数字外的键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            int a = (int)e.KeyChar;
            if (a == 8 || (a >= 47 && a <= 58))
            {
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtPricrOfTodayHalf_KeyPress(object sender, KeyPressEventArgs e)
        {
            int a = (int)e.KeyChar;
            if (a == 8 || (a >= 47 && a <= 58))
            {
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtPricrOfForegift_KeyPress(object sender, KeyPressEventArgs e)
        {
            int a = (int)e.KeyChar;
            if (a == 8 || (a >= 47 && a <= 58))
            {
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtPricrOfHours_KeyPress(object sender, KeyPressEventArgs e)
        {
            int a = (int)e.KeyChar;
            if (a == 8 || (a >= 47 && a <= 58))
            {
            }
            else
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                MessageBox.Show("没有计费类型可指定,请先设置计费类型！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            else
            { 
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (_b)
            {
                AddRoomType();//调用增加房间方法
            }
            else
            {
                UpdateRoomType();//调用修改房间方法
            }
        }
        /// <summary>
        /// 修改房间类型
        /// </summary>
        private void UpdateRoomType()
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("房间类型不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("请输入床位数量！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            else if (textbox3.Text == "")
            {
                MessageBox.Show("请输入预设单价！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            else if (txtPricrOfTodayHalf.Text == "")
            {
                MessageBox.Show("请输入预设半天价！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            else if (txtPricrOfForegift.Text == "")
            {
                MessageBox.Show("请输入预设押金！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            else if (txtPricrOfHours.Text == "")
            {
                MessageBox.Show("请输入钟点价！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            else
            {
                string sql2 = "select * from RoomType";
                DataTable dt = SqlHelp.ExcuteAsAdapter(sql2);
                foreach (DataRow row in dt.Rows)
                {
                    if (textBox1.Text == row["RoomTypeName"].ToString() && textBox1.Text != _fsm.listView1.SelectedItems[0].Text)
                    {
                        MessageBox.Show("此房间类型以存在!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        textBox1.Focus();
                        return;
                    }
                }
                if (cboHoursCost.Checked == true)//判断是否选中钟点计费
                {
                    string sql = string.Format("update RoomType seti RoomTypeName='{0}',PriceOfToday='{1}',PriceOfDiscount='{2}',PriceOfVIP='{3}',PriceOfTreat='{4}',PricrOfTodayHalf='{5}',PricrOfForegift='{6}',PricrOfHours='{7}',Bed={8},HoursCost='Y'where RoomTypeId={9}",
                        textBox1.Text,
                        Convert.ToDouble(textbox3.Text),
                        Convert.ToDouble(textbox3.Text),
                       (Convert.ToDouble(textbox3.Text) - 20),
                       (Convert.ToDouble(textbox3.Text) - 40),
                       Convert.ToDouble(txtPricrOfTodayHalf.Text),
                       Convert.ToDouble(txtPricrOfForegift.Text),
                       Convert.ToDouble(txtPricrOfHours.Text),
                       Convert.ToDouble(textBox2.Text),
                       _fsm.listView1.SelectedItems[0].Tag);
                    SqlHelp.ExcuteInsertUpdateDelete(sql);
                    _fsm.AddListView1();
                    _fsm.AddListView2();
                    Close();
                }
                else
                {
                    string sql = string.Format("update RoomType set RoomTypeName='{0}',PriceOfToday='{1}',PriceOfDiscount='{2}',PriceOfVIP='{3}',PriceOfTreat='{4}',PricrOfTodayHalf='{5}',PricrOfForegift='{6}',PricrOfHours='{7}',Bed={8},HoursCost='N'where RoomTypeId={9}",
                        textBox1.Text,
                        Convert.ToDouble(textbox3.Text),
                        Convert.ToDouble(textbox3.Text),
                       (Convert.ToDouble(textbox3.Text) - 20),
                       (Convert.ToDouble(textbox3.Text) - 40),
                       Convert.ToDouble(txtPricrOfTodayHalf.Text),
                       Convert.ToDouble(txtPricrOfForegift.Text),
                       Convert.ToDouble(txtPricrOfHours.Text),
                       Convert.ToDouble(textBox2.Text),
                       _fsm.listView1.SelectedItems[0].Tag);
                    SqlHelp.ExcuteInsertUpdateDelete(sql);
                    _fsm.AddListView1();
                    _fsm.AddListView2();
                    Close();
                }
            }
        }
        /// <summary>
        /// 增加房间类型
        /// </summary>
        private void AddRoomType()
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("房间类型不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("请输入床位数量！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            else if (textbox3.Text == "")
            {
                MessageBox.Show("请输入预设单价！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            else if (txtPricrOfTodayHalf.Text == "")
            {
                MessageBox.Show("请输入预设半天价！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            else if (txtPricrOfForegift.Text == "")
            {
                MessageBox.Show("请输入预设押金！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            else if (txtPricrOfHours.Text == "")
            {
                MessageBox.Show("请输入钟点价！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            else
            {
                string sql2 = "select * from RoomType";
                DataTable dt = SqlHelp.ExcuteAsAdapter(sql2);
                foreach (DataRow row in dt.Rows)
                {
                    if (textBox1.Text == row["RoomTypeName"].ToString())
                    {
                        MessageBox.Show("此房间类型以存在!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        textBox1.Focus();
                        return;
                    }
                }
                if (cboHoursCost.Checked == true)//判断是否选中钟点计费
                {
                    string sql = string.Format("insert into RoomType values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',{8},'Y')",
                        textBox1.Text,
                        Convert.ToDouble(textbox3.Text),
                        Convert.ToDouble(textbox3.Text),
                       (Convert.ToDouble(textbox3.Text) - 20),
                       (Convert.ToDouble(textbox3.Text) - 40),
                       Convert.ToDouble(txtPricrOfTodayHalf.Text),
                       Convert.ToDouble(txtPricrOfForegift.Text),
                       Convert.ToDouble(txtPricrOfHours.Text),
                       Convert.ToDouble(textBox2.Text));
                    SqlHelp.ExcuteInsertUpdateDelete(sql);
                    _fsm.AddListView1();
                    _fsm.AddListView2();
                    Close();
                }
                else
                {
                    string sql = string.Format("insert into RoomType values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',{8},'N')",
                        textBox1.Text,
                        Convert.ToDouble(textbox3.Text),
                        Convert.ToDouble(textbox3.Text),
                       (Convert.ToDouble(textbox3.Text) - 20),
                       (Convert.ToDouble(textbox3.Text) - 40),
                       Convert.ToDouble(txtPricrOfTodayHalf.Text),
                       Convert.ToDouble(txtPricrOfForegift.Text),
                       Convert.ToDouble(txtPricrOfHours.Text),
                       Convert.ToDouble(textBox2.Text));
                    SqlHelp.ExcuteInsertUpdateDelete(sql);
                    _fsm.AddListView1();
                    _fsm.AddListView2();
                    Close();
                }
                string s = string.Format("增加房间类型{0}",textBox1.Text);
                string sql3 = string.Format("insert into SystemLog values ('{0}','{1}','增加类型','{2}')", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), AppInfo.UserName, s);
                SqlHelp.ExcuteInsertUpdateDelete(sql3);
            }
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SystemAddRoomType_Load(object sender, EventArgs e)
        {
            if (_b)
            {
            }
            else
            {
                Text = "修改房间信息";
                button3.Visible = true;
                textBox1.Text = _fsm.listView1.SelectedItems[0].Text;
                textBox2.Text = _fsm.listView1.SelectedItems[0].SubItems[5].Text;
                textbox3.Text = string.Format("{0:F2}", _fsm.listView1.SelectedItems[0].SubItems[1].Text);
                txtPricrOfTodayHalf.Text = string.Format("{0:F2}", _fsm.listView1.SelectedItems[0].SubItems[2].Text);
                txtPricrOfForegift.Text = string.Format("{0:F2}", _fsm.listView1.SelectedItems[0].SubItems[3].Text);
                txtPricrOfHours.Text = string.Format("{0:F2}", _fsm.listView1.SelectedItems[0].SubItems[4].Text);
                if (_fsm.listView1.SelectedItems[0].SubItems[6].Text == "Y")
                {
                    cboHoursCost.Checked = true;
                }
                else
                {
                    cboHoursCost.Checked = false;
                }
            }
        }
        /// <summary>
        /// 打折设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("该类型的房间正在使用中,请确定该类房间中没有正在消费的顾客后重试！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            return;
        }
    }
}
