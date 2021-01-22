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
    public partial class FrmSystemAddClient : Form
    {
        private string _s;
        private bool _b1;//选中增加还是修改
        private bool _b;//选中是客户来源还是销售人员
        private FrmSystemMain _fsm;
        public FrmSystemAddClient(FrmSystemMain fsm,string s,bool b,bool b1)
        {
            this._b1 = b1;
            this._b = b;
            this._fsm = fsm;
            this._s = s;
            InitializeComponent();
        }

        private void FrmAddClient_Load(object sender, EventArgs e)
        {
            this.label1.Text = this._s;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this._b)
            { //选中了客户来源
                if (this._b1)
                {
                    //选中了修改
                    if (this.textBox1.Text == "")
                    {
                        this.Close();
                    }
                    else
                    {
                        string sql = string.Format("Update Client set clientName='{0}' where clientId={1}", this.textBox1.Text, this._fsm.lvClient.SelectedItems[0].Tag);
                        SqlHelp.ExcuteInsertUpdateDelete(sql);
                        this._fsm.Client();
                        this.Close();
                    }
                }
                else
                {
                    //选中了增加
                    if (this.textBox1.Text == "")
                    {
                        this.Close();
                    }
                    else
                    {
                        string sql = string.Format("insert into Client values('{0}','','')", this.textBox1.Text);
                        SqlHelp.ExcuteInsertUpdateDelete(sql);
                        this._fsm.Client();
                        this.Close();
                    }
                }
            }
            else
            {
                if (this._b1)                 //选中了营销人员
                {
                    if (this.textBox1.Text == "")    //选中了修改
                    {
                        this.Close();
                    }
                    else
                    {
                        string sql = string.Format("Update Sellperson set SellpersonName='{0}' where SellpersonId={1}", this.textBox1.Text, this._fsm.lvSellperson.SelectedItems[0].Tag);
                        SqlHelp.ExcuteInsertUpdateDelete(sql);
                        this._fsm.Sellperson();
                        this.Close();
                    }
                }
                else
                {
                    //选中了增加
                    if (this.textBox1.Text == "")
                    {
                        this.Close();
                    }
                    else
                    {
                        string sql = string.Format("insert into Sellperson values('{0}')", this.textBox1.Text);
                        SqlHelp.ExcuteInsertUpdateDelete(sql);
                        this._fsm.Sellperson();
                        this.Close();
                    }
                }
            }
        }
    }
}
