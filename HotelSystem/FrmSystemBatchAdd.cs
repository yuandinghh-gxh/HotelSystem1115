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
    public partial class FrmSystemBatchAdd : Form
    {
        private FrmSystemMain _fsm;
        public FrmSystemBatchAdd(FrmSystemMain fsm)
        {
            _fsm = fsm;
            InitializeComponent();
        }
        /// <summary>
        /// 禁止输入字符
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtRoomFormer_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtRoomAfter_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            bool b = true;//判断是否存在此房间
            int IRoomFormer = 0;//存储房间起始
            int IRoomAfter = 0;//存储房间结束
            string RoomFormer = string.Empty;//存储房间起始编号
            string RoomAfter = string.Empty;//存储房间终止编号
            if (txtRoomFormer.Text == "")
            {
                MessageBox.Show("请输入起始编号!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtRoomFormer.Focus();
                return;
            }
            else if (txtRoomAfter.Text == "")
            {
                MessageBox.Show("请输入房间终止编号!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtRoomAfter.Focus();
                return;
            }
            else
            {
                int txt = Convert.ToInt32(txtRoomFormer.Text.Substring(0, 1));//截取用户输入的第一位数是否为0
                if (txt == 0)
                {
                    //为0则把0去掉
                    RoomFormer = txtRoomFormer.Text.Substring(1);
                    IRoomFormer = Convert.ToInt32(RoomFormer);
                }
                else
                {
                    RoomFormer = txtRoomFormer.Text;
                    IRoomFormer = Convert.ToInt32(RoomFormer);
                }
                txt = Convert.ToInt32(txtRoomAfter.Text.Substring(0, 1));//截取用户输入的第一位数是否为0
                if (txt == 0)
                {
                    //为0则把0去掉
                    RoomAfter = txtRoomAfter.Text.Substring(1);
                    IRoomAfter = Convert.ToInt32(RoomAfter);
                }
                else
                {
                    RoomAfter = txtRoomAfter.Text;
                    IRoomAfter = Convert.ToInt32(RoomAfter);
                }
                if(IRoomFormer>IRoomAfter)
                {
                    MessageBox.Show("起始房间号不能大于终止房间号！","提示信息",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                    return;
                }
                else
                {
                    
                    if (rdbFormer.Checked == true)//如果选择置前
                    {
                        string sql3 = "select RoomName from Room";
                        DataTable dt = SqlHelp.ExcuteAsAdapter(sql3);
                        foreach (DataRow row in dt.Rows)
                        {
                            for (int i = IRoomFormer; i <= IRoomAfter; i++)
                            {
                                if ((txtbadge.Text + IRoomFormer.ToString()) == row["RoomName"].ToString())
                                {
                                    MessageBox.Show("中途存在此房间，不能继续添加！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                    b = false;
                                    return;
                                }
                            }
                        }
                        #region //把房间号作为电话号同时添加
                        if (b)
                        {
                            if (checkBox1.Checked == true)//把房间号作为电话号同时添加
                            {
                                #region //把房间号作为门码锁添加
                                if (checkBox2.Checked == true)//把房间号作为门码锁添加
                                {
                                    if (txtPhontion.Text == "")//当所在区域为空用无代替
                                    {
                                        for (int i = IRoomFormer; i <= IRoomAfter; i++)
                                        {
                                            string sql = string.Format("insert into Room values ('{0}',{1},1,'无','40.00',null,'{2}','{3}')",
                                                txtbadge.Text + IRoomFormer.ToString(),
                                                cboRoomType.SelectedValue,
                                                txtbadge.Text + IRoomFormer.ToString(),
                                                txtbadge.Text + IRoomFormer.ToString()
                                                );
                                            SqlHelp.ExcuteInsertUpdateDelete(sql);
                                            IRoomFormer++;
                                            _fsm.AddListView2();//调用刷新
                                            Close();
                                        }
                                    }
                                    else
                                    {
                                        for (int i = IRoomFormer; i <= IRoomAfter; i++)
                                        {
                                            string sql = string.Format("insert into Room values ('{0}',{1},1,'{2}','40.00',null,'{3}','{4}')",
                                                    txtbadge.Text + IRoomFormer.ToString(),
                                                    cboRoomType.SelectedValue,
                                                    txtPhontion.Text,
                                                    txtbadge.Text + IRoomFormer.ToString(),
                                                    txtbadge.Text + IRoomFormer.ToString()
                                                    );
                                            SqlHelp.ExcuteInsertUpdateDelete(sql);
                                            IRoomFormer++;
                                            _fsm.AddListView2();//调用刷新
                                            Close();
                                        }
                                    }
                                }
                                else
                                {
                                    if (txtPhontion.Text == "")//当所在区域为空用无代替
                                    {
                                        for (int i = IRoomFormer; i <= IRoomAfter; i++)
                                        {
                                            string sql = string.Format("insert into Room values ('{0}',{1},1,'无','40.00',null,'{2}',null)",
                                                txtbadge.Text + IRoomFormer.ToString(),
                                                cboRoomType.SelectedValue,
                                                txtbadge.Text + IRoomFormer.ToString()
                                                );
                                            SqlHelp.ExcuteInsertUpdateDelete(sql);
                                            IRoomFormer++;
                                            _fsm.AddListView2();//调用刷新
                                            Close();
                                        }
                                    }
                                    else
                                    {
                                        for (int i = IRoomFormer; i <= IRoomAfter; i++)
                                        {
                                            string sql = string.Format("insert into Room values ('{0}',{1},1,'{2}','40.00',null,'{3}',null,'','')",
                                                    txtbadge.Text + IRoomFormer.ToString(),
                                                    cboRoomType.SelectedValue,
                                                    txtPhontion.Text,
                                                    txtbadge.Text + IRoomFormer.ToString()
                                                    );
                                            SqlHelp.ExcuteInsertUpdateDelete(sql);
                                            IRoomFormer++;
                                            _fsm.AddListView2();//调用刷新
                                            Close();
                                        }
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                #region //把房间号作为门码锁添加
                                if (checkBox2.Checked == true)//把房间号作为门码锁添加
                                {
                                    if (txtPhontion.Text == "")//当所在区域为空用无代替
                                    {
                                        for (int i = IRoomFormer; i <= IRoomAfter; i++)
                                        {
                                            string sql = string.Format("insert into Room values ('{0}',{1},1,'无','40.00',null,null,'{2}')",
                                                txtbadge.Text + IRoomFormer.ToString(),
                                                cboRoomType.SelectedValue,
                                                txtbadge.Text + IRoomFormer.ToString()
                                                );
                                            SqlHelp.ExcuteInsertUpdateDelete(sql);
                                            IRoomFormer++;
                                            _fsm.AddListView2();//调用刷新
                                            Close();
                                        }
                                    }
                                    else
                                    {
                                        for (int i = IRoomFormer; i <= IRoomAfter; i++)
                                        {
                                            string sql = string.Format("insert into Room values ('{0}',{1},1,'{2}','40.00',null,null,'{3}')",
                                                    txtbadge.Text + IRoomFormer.ToString(),
                                                    cboRoomType.SelectedValue,
                                                    txtPhontion.Text,
                                                    txtbadge.Text + IRoomFormer.ToString()
                                                    );
                                            SqlHelp.ExcuteInsertUpdateDelete(sql);
                                            IRoomFormer++;
                                            _fsm.AddListView2();//调用刷新
                                            Close();
                                        }
                                    }
                                }
                                else
                                {
                                    if (txtPhontion.Text == "")//当所在区域为空用无代替
                                    {
                                        for (int i = IRoomFormer; i <= IRoomAfter; i++)
                                        {
                                            string sql = string.Format("insert into Room values ('{0}',{1},1,'无','40.00',null,null,null)",
                                                txtbadge.Text + IRoomFormer.ToString(),
                                                cboRoomType.SelectedValue
                                                );
                                            SqlHelp.ExcuteInsertUpdateDelete(sql);
                                            IRoomFormer++;
                                            _fsm.AddListView2();//调用刷新
                                            Close();
                                        }
                                    }
                                    else
                                    {
                                        for (int i = IRoomFormer; i <= IRoomAfter; i++)
                                        {
                                            string sql = string.Format("insert into Room values ('{0}',{1},1,'{2}','40.00',null,null,null)",
                                                    txtbadge.Text + IRoomFormer.ToString(),
                                                    cboRoomType.SelectedValue,
                                                    txtPhontion.Text
                                                    );
                                            SqlHelp.ExcuteInsertUpdateDelete(sql);
                                            IRoomFormer++;
                                            _fsm.AddListView2();//调用刷新
                                            Close();
                                        }
                                    }
                                }
                                #endregion
                            }
                        }
                        #endregion

                    }
                    else
                    {
                        //选择置后
                        string sql3 = "select RoomName from Room";
                        DataTable dt = SqlHelp.ExcuteAsAdapter(sql3);
                        foreach (DataRow row in dt.Rows)
                        {
                            for (int i = IRoomFormer; i <= IRoomAfter; i++)
                            {
                                if ((IRoomFormer.ToString() + txtbadge.Text )== row["RoomName"].ToString())
                                {
                                    MessageBox.Show("中途存在此房间，不能继续添加！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                    b = false;
                                    return;
                                }
                            }
                        }
                        if (b)
                        {
                            #region //把房间号作为电话号同时添加
                            if (checkBox1.Checked == true)//把房间号作为电话号同时添加
                            {
                                #region //把房间号作为门码锁添加
                                if (checkBox2.Checked == true)//把房间号作为门码锁添加
                                {
                                    if (txtPhontion.Text == "")//当所在区域为空用无代替
                                    {
                                        for (int i = IRoomFormer; i <= IRoomAfter; i++)
                                        {
                                            string sql = string.Format("insert into Room values ('{0}',{1},1,'无','40.00',null,'{2}','{3}')",
                                                IRoomFormer.ToString() + txtbadge.Text,
                                                cboRoomType.SelectedValue,
                                                IRoomFormer.ToString() + txtbadge.Text,
                                                IRoomFormer.ToString() + txtbadge.Text
                                                );
                                            SqlHelp.ExcuteInsertUpdateDelete(sql);
                                            IRoomFormer++;
                                            _fsm.AddListView2();//调用刷新
                                            Close();
                                        }
                                    }
                                    else
                                    {
                                        for (int i = IRoomFormer; i <= IRoomAfter; i++)
                                        {
                                            string sql = string.Format("insert into Room values ('{0}',{1},1,'{2}','40.00',null,'{3}','{4}')",
                                                    IRoomFormer.ToString() + txtbadge.Text,
                                                    cboRoomType.SelectedValue,
                                                    txtPhontion.Text,
                                                    IRoomFormer.ToString() + txtbadge.Text,
                                                    IRoomFormer.ToString() + txtbadge.Text
                                                    );
                                            SqlHelp.ExcuteInsertUpdateDelete(sql);
                                            IRoomFormer++;
                                            _fsm.AddListView2();//调用刷新
                                            Close();
                                        }
                                    }
                                }
                                else
                                {
                                    if (txtPhontion.Text == "")//当所在区域为空用无代替
                                    {
                                        for (int i = IRoomFormer; i <= IRoomAfter; i++)
                                        {
                                            string sql = string.Format("insert into Room values ('{0}',{1},1,'无','40.00',null,'{2}',null)",
                                                IRoomFormer.ToString() + txtbadge.Text,
                                                cboRoomType.SelectedValue,
                                                IRoomFormer.ToString() + txtbadge.Text
                                                );
                                            SqlHelp.ExcuteInsertUpdateDelete(sql);
                                            IRoomFormer++;
                                            _fsm.AddListView2();//调用刷新
                                            Close();
                                        }
                                    }
                                    else
                                    {
                                        for (int i = IRoomFormer; i <= IRoomAfter; i++)
                                        {
                                            string sql = string.Format("insert into Room values ('{0}',{1},1,'{2}','40.00',null,'{3}',null)",
                                                    IRoomFormer.ToString() + txtbadge.Text,
                                                    cboRoomType.SelectedValue,
                                                    txtPhontion.Text,
                                                    IRoomFormer.ToString() + txtbadge.Text
                                                    );
                                            SqlHelp.ExcuteInsertUpdateDelete(sql);
                                            IRoomFormer++;
                                            _fsm.AddListView2();//调用刷新
                                            Close();
                                        }
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                #region //把房间号作为门码锁添加
                                if (checkBox2.Checked == true)//把房间号作为门码锁添加
                                {
                                    if (txtPhontion.Text == "")//当所在区域为空用无代替
                                    {
                                        for (int i = IRoomFormer; i <= IRoomAfter; i++)
                                        {
                                            string sql = string.Format("insert into Room values ('{0}',{1},1,'无','40.00',null,null,'{2}')",
                                                IRoomFormer.ToString() + txtbadge.Text,
                                                cboRoomType.SelectedValue,
                                                IRoomFormer.ToString() + txtbadge.Text
                                                );
                                            SqlHelp.ExcuteInsertUpdateDelete(sql);
                                            IRoomFormer++;
                                            _fsm.AddListView2();//调用刷新
                                            Close();
                                        }
                                    }
                                    else
                                    {
                                        for (int i = IRoomFormer; i <= IRoomAfter; i++)
                                        {
                                            string sql = string.Format("insert into Room values ('{0}',{1},1,'{2}','40.00',null,null,'{3}')",
                                                    IRoomFormer.ToString() + txtbadge.Text,
                                                    cboRoomType.SelectedValue,
                                                    txtPhontion.Text,
                                                    IRoomFormer.ToString() + txtbadge.Text
                                                    );
                                            SqlHelp.ExcuteInsertUpdateDelete(sql);
                                            IRoomFormer++;
                                            _fsm.AddListView2();//调用刷新
                                            Close();
                                        }
                                    }
                                }
                                else
                                {
                                    if (txtPhontion.Text == "")//当所在区域为空用无代替
                                    {
                                        for (int i = IRoomFormer; i <= IRoomAfter; i++)
                                        {
                                            string sql = string.Format("insert into Room values ('{0}',{1},1,'无','40.00',null,null,null)",
                                                IRoomFormer.ToString() + txtbadge.Text,
                                                cboRoomType.SelectedValue
                                                );
                                            SqlHelp.ExcuteInsertUpdateDelete(sql);
                                            IRoomFormer++;
                                            _fsm.AddListView2();//调用刷新
                                            Close();
                                        }
                                    }
                                    else
                                    {
                                        for (int i = IRoomFormer; i <= IRoomAfter; i++)
                                        {
                                            string sql = string.Format("insert into Room values ('{0}',{1},1,'{2}','40.00',null,null,null)",
                                                    IRoomFormer.ToString() + txtbadge.Text,
                                                    cboRoomType.SelectedValue,
                                                    txtPhontion.Text
                                                    );
                                            SqlHelp.ExcuteInsertUpdateDelete(sql);
                                            IRoomFormer++;
                                            _fsm.AddListView2();//调用刷新
                                            Close();
                                        }
                                    }
                                }
                                #endregion
                            }
                        }
                        #endregion
                    }
                }
            }
        }

        private void FrmSystemBatchAdd_Load(object sender, EventArgs e)
        {
            rdbFormer.Checked = true;
            //加载下拉列表
            string sql = "select * from RoomType";
            DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
            cboRoomType.DataSource = dt;
            cboRoomType.DisplayMember = "RoomTypeName";
            cboRoomType.ValueMember = "RoomTypeId";
        }
    }
}
