using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace Whq
{
    public partial class Form1 : OfficeForm
    {
        private int select = 1;
        private string sql;
        private DataTable dt = new DataTable();
        private List<string> loginname = new List<string>();
        private string md5;
        private string loginnamestr;
        public Form1() 
        {
            InitializeComponent();
            comboBoxEx1.Items.AddRange(new object[] { eStyle.Office2013, eStyle.OfficeMobile2014, eStyle.Office2010Blue,
                eStyle.Office2010Silver, eStyle.Office2010Black, eStyle.VisualStudio2010Blue, eStyle.VisualStudio2012Light, 
                eStyle.VisualStudio2012Dark, eStyle.Office2007Blue, eStyle.Office2007Silver, eStyle.Office2007Black});
            comboBoxEx1.SelectedIndex = 0;
            comboBoxEx1.Items.AddRange( new object[] { eStyle.Office2013, eStyle.OfficeMobile2014, eStyle.Office2010Blue,
                eStyle.Office2010Silver, eStyle.Office2010Black, eStyle.VisualStudio2010Blue, eStyle.VisualStudio2012Light,
                eStyle.VisualStudio2012Dark, eStyle.Office2007Blue, eStyle.Office2007Silver, eStyle.Office2007Black} );
            comboBoxEx1.SelectedIndex = 0;
            displayuser();
            labelX3.Text = "���ǳ�������Ա";
            if (AppInfo.AdminId != 1) {
                labelX3.Text = "������ͨ�û�"; buttonX1.Enabled = false; buttonX3.Enabled = false;
            }
        }

        //display user table 
        private void displayuser( ) {
            sql = "select * from Users "; dt = SQLiteDBHelper.ExecQuery( sql );
            userlistView.Items.Clear();
            foreach (DataRow row in dt.Rows) {
                ListViewItem item = new ListViewItem();
                userlistView.Items.Add( item );
                //            item.Tag = row["UserId"];
                item.Text = row["UserName"].ToString();
                sql = row["LoginName"].ToString();
                loginname.Add( sql );
                item.SubItems.Add( sql );
                item.SubItems.Add( "*****" );
                string c = row["Adminid"].ToString();
                if (c == "1") {
                    item.SubItems.Add( "�����û�" );
                } else {
                    item.SubItems.Add( "��ͨ�û�" );
                }
            }
        }
        private void disploginstr(string strlogin) {  //display  user item
            sql = sql = string.Format( "select * from Users where LoginName = '{0}'", strlogin );
            dt = SQLiteDBHelper.ExecQuery( sql );
            textBoxX1.Text = dt.Rows[0][0].ToString(); textBoxX2.Text = dt.Rows[0][1].ToString();
            textBoxX3.Text = "*****"; comboBox1.Text = "��ͨ�û�";
            if (dt.Rows[0][3].ToString() == "1") comboBox1.Text = "��������Ա";
        }

        //private void switchButton1_ValueChanged(object sender, EventArgs e)
        //{
        //    sideNav1.EnableClose = switchButton1.Value;
        //    UpdateSideNavDock();
        //}

        //private void switchButton2_ValueChanged(object sender, EventArgs e)
        //{
        //    sideNav1.EnableMaximize = switchButton2.Value;
        //    UpdateSideNavDock();
        //}

        //private void switchButton3_ValueChanged(object sender, EventArgs e)
        //{
        //    sideNav1.EnableSplitter = switchButton3.Value;
        //    UpdateSideNavDock();
        //}

        /// <summary>
        /// Updates the Dock property of SideNav control since when Close/Maximize/Splitter functionality is enabled
        /// the Dock cannot be set to fill since control needs ability to resize itself.
        /// </summary>
        private void UpdateSideNavDock()
        {
            if (sideNav1.EnableClose || sideNav1.EnableMaximize || sideNav1.EnableSplitter)
            {
                if (sideNav1.Dock != DockStyle.Left)
                {
                    sideNav1.Dock = DockStyle.Left;
                    sideNav1.Width = this.ClientRectangle.Width - 32;
                    ToastNotification.Close(this); // Closes any toast messages if already open
                    ToastNotification.Show(this, "With current settings SideNav control must be able to resize itself so its Dock is set to Left.", 4000);
                }
            }
            else if (sideNav1.Dock != DockStyle.Fill)
            {
                sideNav1.Dock = DockStyle.Fill;
                ToastNotification.Close(this); // Closes any toast messages if already open
                ToastNotification.Show(this, "SideNav control Dock is set to Fill.", 2000);
            }
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void labelX13_MarkupLinkClick(object sender, MarkupLinkClickEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.devcomponents.com/kb2/?p=1687");
        }

        

        private void buttonX5_Click(object sender, EventArgs e) {
            Close();
        }
        //    var System = new Set(); Dialog);      itemPanel1.Visible = true; listView1.Visible = false;
        private void buttonX1_Click(object sender, EventArgs e) {  //��ӹ���Ա
                                                               
            textBoxX1.Text = ""; textBoxX2.Text = ""; textBoxX3.Text = "";
            textBoxX1.Focus(); buttonX4.Text = "ȷ�����˺�"; select = 1;
            comboBox1.Text = "��ͨ�û�";
        }
        private void buttonX4_Click(object sender, EventArgs e) {   //ȷ�����
            bool t = true;
            switch (select) {
                case 1:
                    if (textBoxX1.Text == "" || textBoxX2.Text == "" || textBoxX3.Text == "") {
                        MessageBox.Show( "�û������벻��Ϊ�գ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                        break;
                    }
                    foreach (string s in loginname) {
                        if (s.Equals( textBoxX2.Text ) == true) {
                            MessageBox.Show( "��½���Ѿ����ڣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                            textBoxX2.Text = ""; t = false; break;
                        }
                    }
                    if (t == false) break;
                    md5 = Md5.getMd5Hash( textBoxX3.Text.Trim() );
                    DateTime currentTime = DateTime.Now;
                    string nowtime = currentTime.ToString();
                    int adminid = 2;
                    string str = comboBox1.SelectedItem.ToString();
                    if (str == "��������Ա") { adminid = 1; }
                    sql = string.Format( "insert into Users values('{0}','{1}','{2}',{3},'{4}')",
                      textBoxX1.Text.Trim(), textBoxX2.Text.Trim(), md5, adminid, nowtime );
                    SQLiteDBHelper.ExecQuery( sql ); //SqlHelp.ExcuteInsertUpdateDelete( sql );
                    displayuser(); break;
                case 2:
                    if (textBoxX2.Text == "" || textBoxX2.Text.Length <= 2) {
                        MessageBox.Show( "�������벻��Ϊ�ջ������������ַ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                        break;
                    }
                    md5 = Md5.getMd5Hash( textBoxX3.Text.Trim() );
                    sql = string.Format( "update Users set  PassWord ='{0}' where LoginName  = '{1}'", md5, loginnamestr );
                    SQLiteDBHelper.ExecQuery( sql );     //    SqlHelp.ExcuteInsertUpdateDelete( sql );
                    MessageBox.Show( "�����������ɹ��޸ģ�����Σ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                    break;
                case 3:
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    var result = MessageBox.Show( "ȷ��ɾ���˺ţ� ", "��ѡ��", buttons );
                    if (result == DialogResult.Yes) {
                        sql = string.Format( "delete from Users where LoginName='{0}'", loginnamestr );
                        SQLiteDBHelper.ExecQuery( sql );     //  SqlHelp.ExcuteInsertUpdateDelete( sql );
                    }
                    displayuser(); break;
                default:
                    break;
            }
        }

        private void buttonX3_Click(object sender, EventArgs e) {    //ɾ������Ա
            try {
                string site = userlistView.SelectedItems[0].Text;
                loginnamestr = userlistView.SelectedItems[0].SubItems[1].Text;
            } catch (ArgumentOutOfRangeException) {
                MessageBox.Show( "��ûѡ���˺ţ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return;
            }
            disploginstr( loginnamestr );
            select = 3; textBoxX1.Focus(); buttonX4.Text = "ɾ������Ա";
        }

        private void buttonX2_Click(object sender, EventArgs e) {   //�޸�����
            try {
                string site = userlistView.SelectedItems[0].Text;
                loginnamestr = userlistView.SelectedItems[0].SubItems[1].Text;
            } catch (ArgumentOutOfRangeException) {
                MessageBox.Show( "��ûѡ���˺ţ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return;
            }
            disploginstr( loginnamestr );
            textBoxX3.Focus(); textBoxX3.Text = ""; buttonX4.Text = "�޸�����"; select = 2;
        }

        private void sideNavPanel2_Paint(object sender, PaintEventArgs e) {
            textBoxX4.Focus();
        }

        private void buttonX7_Click(object sender, EventArgs e) {
            Int32 c;


            if (textBoxX4.Text == "") {
                MessageBox.Show( "�������ؽڵ�����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return;
            }
            try {
                c = Convert.ToInt32( textBoxX4.Text.ToString() );
            } catch (System.FormatException) {
                MessageBox.Show( "���������֣�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return;
            }
            int cc = c;
            for (int i = 0; i < c; i++) {
                sql = cc.ToString();
                sql = textBoxX5.Text + ' ' + sql + textBoxX6.Text; cc++;
                ListViewItem item = new ListViewItem();
                templist.Items.Add( item );                  //         item.Tag = row["UserId"];
                item.Text = sql;
                item.SubItems.Add( "20 C" );
                item.SubItems.Add( "����" );
                item.SubItems.Add( "1 Сʱ" );
                item.SubItems.Add( "0" );
                item.SubItems.Add( "0" );
            }
        }
        private void buttonX8_Click(object sender, EventArgs e) {
            sql = "select * from Users where UserId=1";
            DataTable dt = SQLiteDBHelper.ExecQuery( sql ); //SqlHelp.ExcuteAsAdapter( sql );
        }

    }

}
