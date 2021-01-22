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
    public partial class FrmSystemAdmin : Form
    {
        private FrmSystemMain _fsm;
        private bool _b;
        public FrmSystemAdmin(FrmSystemMain fsm ,bool b)
        {
            this._fsm = fsm;
            this._b = b;
            InitializeComponent();
        }

        private void FrmSystemAdmin_Load(object sender, EventArgs e)
        {
            string sql = "select * from Phpdom";
            DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
            this.listView1.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                ListViewItem item = new ListViewItem();
                this.listView1.Items.Add(item);
                item.Tag = row["PhpdomId"];
                item.Text = row["PhpdomName"].ToString();
            }
            //选择了修改
            if (this._b)
            {
                if (this._fsm.lvadmin.SelectedItems[0].Text == "超级管理员")
                {
                    this.button1.Enabled = false;
                }


                this.textBox1.Text = this._fsm.lvadmin.SelectedItems[0].Text;
                foreach (ListViewItem item in this.listView1.Items)
                {
                    //根据操作员Id查询此操作员拥有的权限
                    string sql1 = string.Format("select IsHave from AdminPhpdom where AdminId={0} and PhpdomId={1}", this._fsm.lvadmin.SelectedItems[0].Tag,item.Tag);
                    string IsHave = SqlHelp.ExcuteScalar(sql1).ToString();
                    if (IsHave.Trim() == "Y")
                    {
                        item.Checked = true;
                    }
                    else
                    {
                        item.Checked = false;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this._b)
            {
                this.Update();
            }
            else
            {
                this.Add();
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        private new void Update()
        {
            if (this.textBox1.Text == "")
            {
                MessageBox.Show("权限组名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                foreach (ListViewItem item in this._fsm.lvadmin.Items)
                {
                    if (this.textBox1.Text == item.Text&&this.textBox1.Text!=this._fsm.lvadmin.SelectedItems[0].Text)
                    {
                        MessageBox.Show("以存在相同名称的权限组!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        this.textBox1.Focus();
                        return;
                    }
                }
                //修改权限组名称
                string sql = string.Format("Update Admin set AdminName='{0}' where AdminId={1}", this.textBox1.Text, this._fsm.lvadmin.SelectedItems[0].Tag);
                SqlHelp.ExcuteInsertUpdateDelete(sql);
                //循环遍历listview修改权限设计
                foreach (ListViewItem item in this.listView1.Items)
                {
                    string IsHave = item.Checked ? "Y" : "N";
                    string sql2 = string.Format("Update AdminPhpdom set IsHave='{0}' where AdminId={1} and PhpdomId={2}", IsHave, this._fsm.lvadmin.SelectedItems[0].Tag, item.Tag);
                    SqlHelp.ExcuteInsertUpdateDelete(sql2);
                }
                this._fsm.AddAdmin();
                this.Close();
            }
        }
        /// <summary>
        /// 增加
        /// </summary>
        private void Add()
        {
            if (this.textBox1.Text == "")
            {
                MessageBox.Show("权限组名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                foreach (ListViewItem item in this._fsm.lvadmin.Items)
                {
                    if (this.textBox1.Text == item.Text)
                    {
                        MessageBox.Show("以存在相同名称的权限组!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        this.textBox1.Focus();
                        return;
                    }
                }
                //添加权限组
                string sql = string.Format("insert into Admin values ('{0}','','')", this.textBox1.Text);
                SqlHelp.ExcuteInsertUpdateDelete(sql);
                string sql2 = string.Format("select AdminId from Admin where AdminName='{0}'", this.textBox1.Text);
                int AdminId = Convert.ToInt32(SqlHelp.ExcuteScalar(sql2));//添加的操作员Id
                //添加权限设置
                foreach (ListViewItem item in this.listView1.Items)
                {
                    string IsHave = item.Checked ? "Y" : "N";//选择为Y，否则N
                    string sql3 = string.Format("insert into AdminPhpdom values ({0},{1},'{2}','','')", AdminId, item.Tag, IsHave);
                    SqlHelp.ExcuteInsertUpdateDelete(sql3);
                }
                this._fsm.AddAdmin();
                this.Close();
            }
        }
    }
}
