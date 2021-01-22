using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HotelSystem1115
{
    public partial class FrmaddDeposit : Form
    {
        public FrmMain Frmmain;   //FrmMain  
        string  _rentDurationUnit, _sql, _rentDuration;
        DataTable _dt;
        private double _deposit, _sumday, _depositlev,_truedep;             //挂单 如果没挂单==0 否则为上次押金剩余金额
        private double _rentRoomOrder;       //入住价
        private int _rentRoomInfoId;
        
        public FrmaddDeposit(FrmMain frmain)
        {
            Frmmain = frmain;
            InitializeComponent();
        }

        private void FrmaddDeposit_Load(object sender, EventArgs e)
        {
    //      _timestr=  FrmMain.NowDateTime.ToString("yyyy-MM-dd HH:mm:ss");
       //     _now = DateTime.Now;
            textBox6.Select();
            ActiveControl = textBox6;  //激活输入框
            comboBox1.Text = "现金";
            if (Frmmain.B)
            {
                if (Frmmain.RoomStateId != "入住")
                {
                    MessageBox.Show("该房间不为入住客人，无需交押金 ！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    Close();
                }
                else
                {
                    Getroot();
                }
             }
            else
            {
                MessageBox.Show("请选择房间 ！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Close();
            }
 
        }
        private void Getroot()
        {
         _rentDuration = Frmmain.RoomName.Trim();  //temp use
   //         _sql = string.Format("select * from RentRoom  where RoomId ='{0}' and OrderState = '正常' ", Frmmain.RoomId);  //确认是否已经使用 本身份证
         _sql = string.Format("select * from RentRoom where RoomName={0} and RentTime=(select max(RentTime) from RentRoom where RoomName={0})", _rentDuration);
    //        int rentRoomInfoId = Convert.ToInt32(SqlHelp.ExcuteScalar(_sql));
            
            _dt = SqlHelp.ExcuteAsAdapter(_sql);
            foreach (DataRow row in _dt.Rows)
            {
                label2.Text = "您入住时间：" + Convert.ToDateTime(row["RentTime"].ToString());
                textBox7.Text = row["GuestName"].ToString();                    //宾客姓名
                _deposit = Convert.ToDouble(row["Deposit"]);                // 累计 押金
                textBox8.Text = Convert.ToString(_deposit);             //缴纳押金
                _rentRoomOrder = Convert.ToDouble(row["RentRoomOrder"]);  // 房价
                textBox1.Text = row["RoomName"].ToString();     //房间号                         // 房间号
                _rentDurationUnit = row["RentDurationUnit"].ToString();     //住店客人 ‘天’ 还是 钟点房？
                _rentDuration = row["RentDuration"].ToString();
                textBox9.Text = _rentDuration;          //入住天数
                _sumday = _rentRoomOrder*Convert.ToDouble(_rentDuration);
                _depositlev = _deposit - _sumday;
                textBox3.Text = Convert.ToString(_depositlev);
                _rentRoomInfoId = Convert.ToInt32(row["RentRoomInfoId"]);           //取得主结账房间ID
                if (_depositlev < 0)            //欠 押金了
                {
           //        textBox3.Font.Bold = true;
                    textBox3.ForeColor = Color.Red;
                    double sun = Convert.ToDouble(textBox4.Text);
                    sun = _rentRoomOrder * sun - _depositlev+100; //押金一次不少于
                    textBox5.Text = sun.ToString();
                }
                else
                {
                    textBox5.Text = "不用交押金";
                }
                textBox2.Text = Convert.ToString(_sumday);                  //消费金额
  //              DateTime datet = Convert.ToDateTime(row["RentTime"]);       //获取进店时间
                if (_rentDurationUnit == "天")
                {
                    label9.Visible = false; radioButton1.Visible = false;


                }
                else
                {
                    label9.Visible = true; radioButton1.Visible = true;
                } 

            }
        }
        private void button1_Click(object sender, EventArgs e)   //           Application.Exit();  这个为 退出系统。
        {
            Close();  
        }
        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)  //输入押金确认为数字和 back
        {
            int a = (int)e.KeyChar;
            if (a == 8 || (a >= 48 && a <= 57) || a == 45 ) // 45 -号
            {
            }
            else
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)  //确认支付押金
        {
            _truedep=Convert.ToDouble(textBox6.Text);
            if ( _truedep < 100)
            {
                if (_truedep >= 0)
                {
                    MessageBox.Show("押金一次支付不得少于100元", "提示");
                    return;
                }
                else
                {
                    DialogResult dr = MessageBox.Show("确认客户退押金？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dr == DialogResult.OK)
                    {
                        _depositlev += _truedep;
                        if (_depositlev <= 0)
                        {
                            MessageBox.Show("剩余押金不够支付房费不能退款", "提示");
                            return;
                        }
                        FrmSystemMain.cht.deposit += _truedep;       //减押金到当前 
                        FrmSystemMain.Writesys();  // 写 文件数据
                        _deposit += _truedep;
                        _sql = string.Format("update RentRoom set  Deposit={0},LastEditDate='{1}' where RentRoomInfoId={2}",
                                _deposit,               //押金
                                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                _rentRoomInfoId);
                        SqlHelp.ExcuteInsertUpdateDelete(_sql);
                        _sql = string.Format("insert into  Deposit values ('{0}','{1}','{2}',{3},{4},'退押金')", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), textBox1.Text, textBox7.Text, _truedep, _rentRoomInfoId); // 记录 第一次 押金
                        SqlHelp.ExcuteInsertUpdateDelete(_sql);
                        MessageBox.Show("客户押金已经减少");
                        Close();    
                    }
                }
            }
            else
            {
                DialogResult dr = MessageBox.Show("确认客户支付押金？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    _deposit += _sumday;       //增加押金
                    FrmSystemMain.cht.deposit += _sumday;       //增加押金到当前 
                    FrmSystemMain.Writesys();  // 写 文件数据
                    _sql = string.Format("update RentRoom set  Deposit={0},LastEditDate='{1}' where RentRoomInfoId={2}",
                            _deposit,               //押金
                            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            _rentRoomInfoId);
                    SqlHelp.ExcuteInsertUpdateDelete(_sql);
                    _sql = string.Format("insert into  Deposit values ('{0}','{1}','{2}',{3},{4},'增加押金')", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), textBox1.Text, textBox7.Text, _sumday, _rentRoomInfoId); // 记录 第一次 押金
                    SqlHelp.ExcuteInsertUpdateDelete(_sql); 
                    MessageBox.Show("客户押金已经增加");
                     Close();    
                 }
            }
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                double sun = Convert.ToDouble(textBox4.Text);
                sun = _rentRoomOrder*sun - _depositlev;
                textBox5.Text = Convert.ToString(sun);
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)      //入住天数 只能数字
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

 

    }
}
