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
    public partial class FrmSystemAddGoods : Form
    {
        private string _Spell;//存储商品名称的简拼
        private bool _b;//判断选中是增加还是修改
        private FrmSystemMain _fsm;
        public FrmSystemAddGoods(FrmSystemMain fsm,bool b)
        {
            _fsm = fsm;
            _b = b;
            InitializeComponent();
        }

        private void FrmSystemAddGoods_Load(object sender, EventArgs e)
        {
            skinEngine1.SkinFile = "s (1).ssk";
            //加载下拉列表
            string sql = "select * from GoodsClass";
            DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
            cboGoodsType.DataSource = dt;
            cboGoodsType.DisplayMember = "GoodsClass";
            cboGoodsType.ValueMember = "GoodsClassId";
            if (_b)
            {
                string sql2 = "select * from Goods where GoodsId=" + _fsm.lvGoodsName.SelectedItems[0].Tag;
                DataTable dt2 = SqlHelp.ExcuteAsAdapter(sql2);
                foreach (DataRow row in dt2.Rows)
                {
                    cboGoodsType.SelectedValue = row["GoodsClassId"].ToString();
                    txtGoodsNumber.Text = row["GoodsNumber"].ToString();
                    txtGoodsName.Text = row["GoodsName"].ToString();
                    txtPrice.Text = string.Format("{0:F2}", row["Price"]);
                    txtPriceCost.Text = string.Format("{0:F2}", row["PriceCost"]);
                    txtStockNum.Text = row["StockNum"].ToString();
                    txtStockAlarm.Text = row["StockAlarm"].ToString();
                    txtencash.Text = row["encash"].ToString();
                    if (row["Stock"].ToString() == "Y")
                    {
                        cboStock.Checked = true;
                    }
                    if (row["encashBadge"].ToString() == "Y")
                    {
                        cboencashBadge.Checked = true;
                    }
                    else
                    {
                        txtencash.Enabled = false;
                    }
                }
                Text = "修改商品项目";
                txtGoodsNumber.Enabled = false;
                txtStockNum.Enabled = false;
            }
        }
        #region //禁止输入字符
        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtPriceCost_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtStockNum_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtStockAlarm_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtencash_KeyPress(object sender, KeyPressEventArgs e)
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
        #endregion

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (_b)
            {
                GetBool();
            }
            else
            {
                GetBool();
            }
        }
        /// <summary>
        /// 修改商品
        /// </summary>
        private void UpdateGoods()
        {
            #region //商品兑换积分
            if (cboencashBadge.Checked == true)//允许该商品兑换会员积分
            {
                #region //参加进销存管理
                if (cboStock.Checked == true)//允许参与进销存管理
                {
                    #region //保存后是否关闭
                    if (checkBox3.Checked == true)//保存后不关闭
                    {
                        string sql = string.Format("Update Goods set GoodsName='{0}',Price='{1}',StockNum={2},SpellBrief='{3}',GoodsClassId={4},GoodsNumber='{5}',PriceCost='{6}',StockAlarm={7},encash={8},Stock='Y',encashBadge='Y' where GoodsId={9} ",
                           txtGoodsName.Text,
                           txtPrice.Text,
                           txtStockNum.Text,
                           _Spell,
                           cboGoodsType.SelectedValue,
                           txtGoodsNumber.Text,
                           txtPriceCost.Text,
                           txtStockAlarm.Text,
                           txtencash.Text,
                           _fsm.lvGoodsName.SelectedItems[0].Tag);
                        SqlHelp.ExcuteInsertUpdateDelete(sql);
                        _fsm.AddGoodsName();//调用加载商品信息
                    }
                    else//关闭
                    {
                        string sql = string.Format("Update Goods set GoodsName='{0}',Price='{1}',StockNum={2},SpellBrief='{3}',GoodsClassId={4},GoodsNumber='{5}',PriceCost='{6}',StockAlarm={7},encash={8},Stock='Y',encashBadge='Y' where GoodsId={9} ",
                           txtGoodsName.Text,
                           txtPrice.Text,
                           txtStockNum.Text,
                           _Spell,
                           cboGoodsType.SelectedValue,
                           txtGoodsNumber.Text,
                           txtPriceCost.Text,
                           txtStockAlarm.Text,
                           txtencash.Text,
                           _fsm.lvGoodsName.SelectedItems[0].Tag);
                        SqlHelp.ExcuteInsertUpdateDelete(sql);
                        _fsm.AddGoodsName();//调用加载商品信息
                        Close();
                    }
                    #endregion
                }
                else//不参与进销存管理
                {
                    #region //保存后是否关闭
                    if (checkBox3.Checked == true)//保存后不关闭
                    {
                        string sql = string.Format("Update Goods set GoodsName='{0}',Price='{1}',StockNum={2},SpellBrief='{3}',GoodsClassId={4},GoodsNumber='{5}',PriceCost='{6}',StockAlarm={7},encash={8},Stock='N',encashBadge='Y' where GoodsId={9} ",
                           txtGoodsName.Text,
                           txtPrice.Text,
                           txtStockNum.Text,
                           _Spell,
                           cboGoodsType.SelectedValue,
                           txtGoodsNumber.Text,
                           txtPriceCost.Text,
                           txtStockAlarm.Text,
                           txtencash.Text,
                           _fsm.lvGoodsName.SelectedItems[0].Tag);
                        SqlHelp.ExcuteInsertUpdateDelete(sql);
                        _fsm.AddGoodsName();//调用加载商品信息
                    }
                    else//关闭
                    {
                        string sql = string.Format("Update Goods set GoodsName='{0}',Price='{1}',StockNum={2},SpellBrief='{3}',GoodsClassId={4},GoodsNumber='{5}',PriceCost='{6}',StockAlarm={7},encash={8},Stock='N',encashBadge='Y' where GoodsId={9} ",
                           txtGoodsName.Text,
                           txtPrice.Text,
                           txtStockNum.Text,
                           _Spell,
                           cboGoodsType.SelectedValue,
                           txtGoodsNumber.Text,
                           txtPriceCost.Text,
                           txtStockAlarm.Text,
                           txtencash.Text,
                           _fsm.lvGoodsName.SelectedItems[0].Tag);
                        SqlHelp.ExcuteInsertUpdateDelete(sql);
                        _fsm.AddGoodsName();//调用加载商品信息
                        Close();
                    }
                    #endregion
                }
                #endregion
            }
            else//不允许商品兑换会员积分
            {
                #region //参加进销存管理
                if (cboStock.Checked == true)//允许参与进销存管理
                {
                    #region //保存后是否关闭
                    if (checkBox3.Checked == true)//保存后不关闭
                    {
                        string sql = string.Format("Update Goods set GoodsName='{0}',Price='{1}',StockNum={2},SpellBrief='{3}',GoodsClassId={4},GoodsNumber='{5}',PriceCost='{6}',StockAlarm={7},encash={8},Stock='Y',encashBadge='N' where GoodsId={9} ",
                           txtGoodsName.Text,
                           txtPrice.Text,
                           txtStockNum.Text,
                           _Spell,
                           cboGoodsType.SelectedValue,
                           txtGoodsNumber.Text,
                           txtPriceCost.Text,
                           txtStockAlarm.Text,
                           txtencash.Text,
                           _fsm.lvGoodsName.SelectedItems[0].Tag);
                        SqlHelp.ExcuteInsertUpdateDelete(sql);
                        _fsm.AddGoodsName();//调用加载商品信息
                    }
                    else//关闭
                    {
                        string sql = string.Format("Update Goods set GoodsName='{0}',Price='{1}',StockNum={2},SpellBrief='{3}',GoodsClassId={4},GoodsNumber='{5}',PriceCost='{6}',StockAlarm={7},encash={8},Stock='Y',encashBadge='N' where GoodsId={9} ",
                           txtGoodsName.Text,
                           txtPrice.Text,
                           txtStockNum.Text,
                           _Spell,
                           cboGoodsType.SelectedValue,
                           txtGoodsNumber.Text,
                           txtPriceCost.Text,
                           txtStockAlarm.Text,
                           txtencash.Text,
                           _fsm.lvGoodsName.SelectedItems[0].Tag);
                        SqlHelp.ExcuteInsertUpdateDelete(sql);
                        _fsm.AddGoodsName();//调用加载商品信息
                        Close();
                    }
                    #endregion
                }
                else//不参与进销存管理
                {
                    #region //保存后是否关闭
                    if (checkBox3.Checked == true)//保存后不关闭
                    {
                        string sql = string.Format("Update Goods set GoodsName='{0}',Price='{1}',StockNum={2},SpellBrief='{3}',GoodsClassId={4},GoodsNumber='{5}',PriceCost='{6}',StockAlarm={7},encash={8},Stock='N',encashBadge='N' where GoodsId={9} ",
                           txtGoodsName.Text,
                           txtPrice.Text,
                           txtStockNum.Text,
                           _Spell,
                           cboGoodsType.SelectedValue,
                           txtGoodsNumber.Text,
                           txtPriceCost.Text,
                           txtStockAlarm.Text,
                           txtencash.Text,
                           _fsm.lvGoodsName.SelectedItems[0].Tag);
                        SqlHelp.ExcuteInsertUpdateDelete(sql);
                        _fsm.AddGoodsName();//调用加载商品信息
                    }
                    else//关闭
                    {
                        string sql = string.Format("Update Goods set GoodsName='{0}',Price='{1}',StockNum={2},SpellBrief='{3}',GoodsClassId={4},GoodsNumber='{5}',PriceCost='{6}',StockAlarm={7},encash={8},Stock='N',encashBadge='N' where GoodsId={9} ",
                           txtGoodsName.Text,
                           txtPrice.Text,
                           txtStockNum.Text,
                           _Spell,
                           cboGoodsType.SelectedValue,
                           txtGoodsNumber.Text,
                           txtPriceCost.Text,
                           txtStockAlarm.Text,
                           txtencash.Text,
                           _fsm.lvGoodsName.SelectedItems[0].Tag);
                        SqlHelp.ExcuteInsertUpdateDelete(sql);
                        _fsm.AddGoodsName();//调用加载商品信息
                        Close();
                    }
                    #endregion
                }
                #endregion
            }
            #endregion
        }
        /// <summary>
        /// 判断
        /// </summary>
        private void GetBool()
        {
            if (txtGoodsNumber.Text == "")
            {
                MessageBox.Show("项目编码不能为空!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtGoodsNumber.Focus();
                return;
            }
            else if (txtGoodsName.Text == "")
            {
                MessageBox.Show("项目名称不能为空或“房间费”!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtGoodsName.Focus();
                return;
            }
            else if (txtPrice.Text == "")
            {
                MessageBox.Show("初始预设单价有误", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtPrice.Focus();
                return;
            }
            else if (txtPriceCost.Text == "")
            {
                MessageBox.Show("初始单价成本有误", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtPriceCost.Focus();
                return;
            }
            else if (txtStockNum.Text == "")
            {
                MessageBox.Show("初始库存数量有误", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtStockNum.Focus();
                return;
            }
            else if (txtStockAlarm.Text == "")
            {
                MessageBox.Show("初始报警库存有误", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtStockAlarm.Focus();
                return;
            }
            else if (txtencash.Text == "")
            {
                MessageBox.Show("初始兑换有误", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtencash.Focus();
                return;
            }
            else
            {
                if (_b)
                {
                    GetSpell();//取其简拼
                    UpdateGoods();
                }
                else
                {
                    string sql = "select * from Goods";
                    DataTable dt = SqlHelp.ExcuteAsAdapter(sql);
                    foreach (DataRow row in dt.Rows)
                    {
                        if (txtGoodsName.Text == row["GoodsName"].ToString() || txtGoodsNumber.Text == row["GoodsNumber"].ToString())
                        {
                            MessageBox.Show("存在有此商品或商品编号，请重新填写！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            txtGoodsName.Focus();
                            return;
                        }
                    }
                    GetSpell();//取其简拼
                    AddGoods();
                }
            }
        }
        /// <summary>
        /// 增加商品
        /// </summary>
        private void AddGoods()
        {
            #region //商品兑换积分
            if (cboencashBadge.Checked == true)//允许该商品兑换会员积分
            {
                #region //参加进销存管理
                if (cboStock.Checked == true)//允许参与进销存管理
                {
                    #region //保存后是否关闭
                    if (checkBox3.Checked == true)//保存后不关闭
                    {
                        string sql = string.Format("insert into Goods values ('{0}','{1}',{2},'{3}',{4},'{5}','{6}',{7},{8},'Y','Y') ",
                           txtGoodsName.Text,
                           txtPrice.Text,
                           txtStockNum.Text,
                           _Spell,
                           cboGoodsType.SelectedValue,
                           txtGoodsNumber.Text,
                           txtPriceCost.Text,
                           txtStockAlarm.Text,
                           txtencash.Text);
                        SqlHelp.ExcuteInsertUpdateDelete(sql);
                        _fsm.AddGoodsName();//调用加载商品信息
                    }
                    else//关闭
                    {
                        string sql = string.Format("insert into Goods values ('{0}','{1}',{2},'{3}',{4},'{5}','{6}',{7},{8},'Y','Y') ",
                           txtGoodsName.Text,
                           txtPrice.Text,
                           txtStockNum.Text,
                           _Spell,
                           cboGoodsType.SelectedValue,
                           txtGoodsNumber.Text,
                           txtPriceCost.Text,
                           txtStockAlarm.Text,
                           txtencash.Text);
                        SqlHelp.ExcuteInsertUpdateDelete(sql);
                        _fsm.AddGoodsName();//调用加载商品信息
                        Close();
                    }
                    #endregion
                }
                else//不参与进销存管理
                {
                    #region //保存后是否关闭
                    if (checkBox3.Checked == true)//保存后不关闭
                    {
                        string sql = string.Format("insert into Goods values ('{0}','{1}',{2},'{3}',{4},'{5}','{6}',{7},{8},'N','Y') ",
                           txtGoodsName.Text,
                           txtPrice.Text,
                           txtStockNum.Text,
                           _Spell,
                           cboGoodsType.SelectedValue,
                           txtGoodsNumber.Text,
                           txtPriceCost.Text,
                           txtStockAlarm.Text,
                           txtencash.Text);
                        SqlHelp.ExcuteInsertUpdateDelete(sql);
                        _fsm.AddGoodsName();//调用加载商品信息
                    }
                    else//关闭
                    {
                        string sql = string.Format("insert into Goods values ('{0}','{1}',{2},'{3}',{4},'{5}','{6}',{7},{8},'N','Y') ",
                           txtGoodsName.Text,
                           txtPrice.Text,
                           txtStockNum.Text,
                           _Spell,
                           cboGoodsType.SelectedValue,
                           txtGoodsNumber.Text,
                           txtPriceCost.Text,
                           txtStockAlarm.Text,
                           txtencash.Text);
                        SqlHelp.ExcuteInsertUpdateDelete(sql);
                        _fsm.AddGoodsName();//调用加载商品信息
                        Close();
                    }
                    #endregion
                }
                #endregion
            }
            else//不允许商品兑换会员积分
            {
                #region //参加进销存管理
                if (cboStock.Checked == true)//允许参与进销存管理
                {
                    #region //保存后是否关闭
                    if (checkBox3.Checked == true)//保存后不关闭
                    {
                        string sql = string.Format("insert into Goods values ('{0}','{1}',{2},'{3}',{4},'{5}','{6}',{7},{8},'Y','N') ",
                           txtGoodsName.Text,
                           txtPrice.Text,
                           txtStockNum.Text,
                           _Spell,
                           cboGoodsType.SelectedValue,
                           txtGoodsNumber.Text,
                           txtPriceCost.Text,
                           txtStockAlarm.Text,
                           txtencash.Text);
                        SqlHelp.ExcuteInsertUpdateDelete(sql);
                        _fsm.AddGoodsName();//调用加载商品信息
                    }
                    else//关闭
                    {
                        string sql = string.Format("insert into Goods values ('{0}','{1}',{2},'{3}',{4},'{5}','{6}',{7},{8},'Y','N') ",
                           txtGoodsName.Text,
                           txtPrice.Text,
                           txtStockNum.Text,
                           _Spell,
                           cboGoodsType.SelectedValue,
                           txtGoodsNumber.Text,
                           txtPriceCost.Text,
                           txtStockAlarm.Text,
                           txtencash.Text);
                        SqlHelp.ExcuteInsertUpdateDelete(sql);
                        _fsm.AddGoodsName();//调用加载商品信息
                        Close();
                    }
                    #endregion
                }
                else//不参与进销存管理
                {
                    #region //保存后是否关闭
                    if (checkBox3.Checked == true)//保存后不关闭
                    {
                        string sql = string.Format("insert into Goods values ('{0}','{1}',{2},'{3}',{4},'{5}','{6}',{7},{8},'N','N') ",
                           txtGoodsName.Text,
                           txtPrice.Text,
                           txtStockNum.Text,
                           _Spell,
                           cboGoodsType.SelectedValue,
                           txtGoodsNumber.Text,
                           txtPriceCost.Text,
                           txtStockAlarm.Text,
                           txtencash.Text);
                        SqlHelp.ExcuteInsertUpdateDelete(sql);
                        _fsm.AddGoodsName();//调用加载商品信息
                    }
                    else//关闭
                    {
                        string sql = string.Format("insert into Goods values ('{0}','{1}',{2},'{3}',{4},'{5}','{6}',{7},{8},'N','N') ",
                           txtGoodsName.Text,
                           txtPrice.Text,
                           txtStockNum.Text,
                           _Spell,
                           cboGoodsType.SelectedValue,
                           txtGoodsNumber.Text,
                           txtPriceCost.Text,
                           txtStockAlarm.Text,
                           txtencash.Text);
                        SqlHelp.ExcuteInsertUpdateDelete(sql);
                        _fsm.AddGoodsName();//调用加载商品信息
                        Close();
                    }
                    #endregion
                }
                #endregion
            }
            #endregion
        }
        /// <summary>
        /// 商品名称调Spell方法获取其简拼
        /// </summary>
        private void GetSpell()
        {
            if (txtGoodsName.Text == "")
            {
                return;
            }
            string str = txtGoodsName.Text.Trim();
            for (int i = 0; i < str.Length; i++)
            {
                byte[] ary = Encoding.Default.GetBytes(str.Substring(i,1));//循环截取字符串的每个字符,将其转换为整型数组
                if (ary.Length < 2)//判断输入的是否是汉字
                {
                    _Spell += str.Substring(i, i + 1).ToUpper();//如果是字母就将其转为大写存入变量中
                }
                else
                {
                    int AnsiCode = ((short)ary[0] * 256) + (short)ary[1];//把两个字节中存储的汉字转化为整数
                    if ((AnsiCode < 45217 || AnsiCode > 55289) && AnsiCode != 8)//汉字的编码范围是25217~55289，8是退格键
                    {
                        return;
                    }
                    _Spell = _Spell + Spell.GetChineseSpell(str.Substring(i,1));//调用方法获取其首字母
                }
            }
        }

        private void cboencashBadge_CheckedChanged(object sender, EventArgs e)
        {
            if (_b)
            {
                if (cboencashBadge.Checked == true)
                {
                    txtencash.Enabled = true;
                }
                else
                {
                    txtencash.Enabled = false;
                }
            }
            else
            { 
            }
        }
    }
}
