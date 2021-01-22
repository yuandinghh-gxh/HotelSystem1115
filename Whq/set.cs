using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.IO.Ports;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.Rendering;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace Whq {
	#region ϵͳ����
	public partial class set : OfficeForm {
		private int select = 1, cc, index;
		private string sql;
		private DataTable dt = new DataTable();
		private List<string> loginname = new List<string>();
		private string md5;
		private string loginnamestr;
		private Int32 nodecount, startnum, samplingcyc;
		private bool hadscan = false;    //      private string column = "";        private string cabinetnumstr="";
		private ListViewItem item = new ListViewItem();
		private BindingSource bindingSource1 = new BindingSource();
		private bool addcomparisontable = false;

		#endregion
		private void templistdisplay( ) {
			// sql = "SELECT count( *) FROM sqlite_master WHERE type = 'table' AND name = 'comparison' ";
			//dt  =  SQLiteDBHelper.ExecQuery( sql );      //      if (dt.    false) {    //table no exist
			if (addcomparisontable) {
				dt = SQLiteDBHelper.ExecQuery( "Select * From comparison" );
				if (dt.Rows.Count > 0) {  //del  recode
					sql = " DELETE FROM  comparison";
					SQLiteDBHelper.ExecQuery( sql );
				}
			}
			if (AppInfo.supply == "3") threephase.Enabled = true;
			if (AppInfo.nodecount == 0) return;   //û���ýڵ�
			cc = AppInfo.startnum;              //templist.Items.Clear();
			for (int ii = 0; ii < AppInfo.nodecount; ii++) {
				if (AppInfo.supply == "3") {
					for (int j = 0; j < 3; j++) {
						item = new ListViewItem();
						sql = cc.ToString(); sql = textBoxX5.Text + sql;
						switch (j) {
							case 0: sql = sql + "A��"; break;
							case 1: sql = sql + "B��"; break;
							default: sql = sql + "C��"; break;
						}
						if (addcomparisontable) {
							sql = string.Format( "insert into comparison  values(null,' ','{0}'   ) ", sql );
							SQLiteDBHelper.ExecQuery( sql );
						}
					}
				} else {
					if (addcomparisontable) {
						sql = string.Format( "insert into comparison  values(null,' ','{0}'   ) ", sql );
						SQLiteDBHelper.ExecQuery( sql );
					}
				}
				cc++;
			}
			//      dataGridViewX1.AutoGenerateColumns = true;               //      dataGridViewX1.Dock = DockStyle.Fill;
			dt = SQLiteDBHelper.ExecQuery( "Select * From comparison" );
			dataGridViewX1.Rows.Clear();
			int indexs = 0; // AppInfo.dict.Clear();
			foreach (DataRow row in dt.Rows) {      //2.��ֵ������                    comparisondata cda = new comparisondata();
				AppInfo.scdstr[indexs].id = Convert.ToInt32( row["id"].ToString() );
				AppInfo.scdstr[indexs].tempnum = row["tempnum"].ToString();
				AppInfo.scdstr[indexs].cabinetnum = row["cabinetnum"].ToString();
				dataGridViewX1.Rows.Add();
				dataGridViewX1.Rows[indexs].Cells["tempnum"].Value = row["tempnum"];
				dataGridViewX1.Rows[indexs].Cells["cabinetnum"].Value = row["cabinetnum"];
				indexs++;
				//AppInfo.cda.id = Convert.ToInt32( row["id"].ToString() );				//sql = row["tempnum"].ToString();
				//AppInfo.cda.cabinetnum = row["cabinetnum"].ToString();				//index = dataGridViewX1.Rows.Add();
				//AppInfo.dict.Add( sql, AppInfo.cda );		index = dataGridViewX1.Rows.Add();    //     AppInfo.dict.Add( index, cda );
			}              //bindingSource1.DataSource = SQLiteDBHelper.ExecQuery( "Select * From comparison" );            //dataGridViewX1.DataSource = bindingSource1;
			dataGridViewX1.EditMode = DataGridViewEditMode.EditOnEnter;
			//AppInfo.cda = AppInfo.dict[AppInfo.cda.tempnum];
		}
		//      }
		public set( ) {
			#region  ���õ� ��ʼ��
			InitializeComponent();
			comboBoxEx1.Items.AddRange( new object[] { eStyle.Office2013, eStyle.OfficeMobile2014, eStyle.Office2010Blue,
				eStyle.Office2010Silver, eStyle.Office2010Black, eStyle.VisualStudio2010Blue, eStyle.VisualStudio2012Light,
				eStyle.VisualStudio2012Dark, eStyle.Office2007Blue, eStyle.Office2007Silver, eStyle.Office2007Black} );
			comboBoxEx1.SelectedIndex = 0;
			comboBoxEx1.Items.AddRange( new object[] { eStyle.Office2013, eStyle.OfficeMobile2014, eStyle.Office2010Blue,
				eStyle.Office2010Silver, eStyle.Office2010Black, eStyle.VisualStudio2010Blue, eStyle.VisualStudio2012Light,
				eStyle.VisualStudio2012Dark, eStyle.Office2007Blue, eStyle.Office2007Silver, eStyle.Office2007Black} );
			comboBoxEx1.SelectedIndex = 0;
			displayuser();
			#endregion
			//         AppInfo.comset = false;
			labelX3.Text = "���ǳ�������Ա";
			if (AppInfo.AdminId != 1) {
				labelX3.Text = "������ͨ�û�"; buttonX1.Enabled = false; buttonX3.Enabled = false;
			}
			textBoxX4.Text = AppInfo.nodecount.ToString(); textBoxX5.Text = AppInfo.frontstr;   //textBoxX6.Text = AppInfo.rearst;
			textBoxX7.Text = AppInfo.startnum.ToString(); textBoxX8.Text = AppInfo.lowtemp.ToString(); textBoxX9.Text = AppInfo.uptemp.ToString();
			textBoxX10.Text = AppInfo.samplingcyc.ToString();
			samplingcyc = AppInfo.samplingcyc; nodecount = AppInfo.nodecount; startnum = AppInfo.startnum; cc = startnum;
			addcomparisontable = false;
			templistdisplay();
			sql = "select  *  from parameters  ";
			dt = SQLiteDBHelper.ExecQuery( sql );
			if (!AppInfo.comset) {
				if (!hadscan) buttonX11_Click( null, null );    //scan com port
			} else {      //          comboBoxEx2.SelectedItem = AppInfo.com;
				comboBoxEx2.Text = AppInfo.com;
			}
		}
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
		/// <summary>
		/// Updates the Dock property of SideNav control since when Close/Maximize/Splitter functionality is enabled
		/// the Dock cannot be set to fill since control needs ability to resize itself.
		/// </summary>
		private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e) {
			if (comboBoxEx1.SelectedItem == null) return;
			eStyle style = (eStyle)comboBoxEx1.SelectedItem;
			if (styleManager1.ManagerStyle != style)
				styleManager1.ManagerStyle = style;
		}
		private void buttonX5_Click(object sender, EventArgs e) {
			Close();
		}
		//    var System = new Set(); Dialog);      itemPanel1.Visible = true; listView1.Visible = false;
		//��ӹ���Ա
		private void buttonX1_Click(object sender, EventArgs e) {
			textBoxX1.Text = ""; textBoxX2.Text = ""; textBoxX3.Text = "";
			textBoxX1.Focus(); buttonX4.Text = "ȷ�����˺�"; select = 1;
			comboBox1.Text = "��ͨ�û�";
		}
		//ȷ����� and   modification
		private void buttonX4_Click(object sender, EventArgs e) {   //ȷ�����
			bool t = true;
			switch (select) {
				case 1:
					if (AppInfo.AdminId != 1) {
						MessageBox.Show( "��ӹ���Ա�г����û���ɣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning );
						break;
					}
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
					if (AppInfo.AdminId != 1) {
						MessageBox.Show( "��ӹ���Ա�г����û���ɣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning );
						break;
					}
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
		//��ص���  ��ʼ�ַ� �ȼ�� ����
		private void buttonX7_Click(object sender, EventArgs e) {
			if (AppInfo.AdminId != 1) {
				MessageBox.Show( "�����û���ɣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning ); return;
			}
			if (textBoxX4.Text == "") {
				MessageBox.Show( "�������ؽڵ�����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning ); return;
			}
			try {
				nodecount = Convert.ToInt32( textBoxX4.Text.ToString() ); samplingcyc = Convert.ToInt32( textBoxX10.Text.ToString() );
				startnum = Convert.ToInt32( textBoxX7.Text.ToString() );
			} catch (FormatException) {
				MessageBox.Show( "���ؽڵ㡢��ʼ��š������������������֣�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning ); return;
			}
			if (samplingcyc < 60) {
				MessageBox.Show( "�������ڲ���С��60�룡", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning ); return;
			}
			AppInfo.supply = "2";
			if (threephase.Enabled) AppInfo.supply = "3";
			if ((samplingcyc != AppInfo.samplingcyc) || (textBoxX5.Text != AppInfo.frontstr) || (nodecount != AppInfo.nodecount) || (startnum != AppInfo.startnum)) {
				sql = string.Format( "update parameters set nodecount ={0},samplingcyc ={1} ,startnum ={2},frontstr='{3}',supply='{4}'", nodecount, samplingcyc, startnum, textBoxX5.Text, AppInfo.supply );
				try {
					SQLiteDBHelper.ExecQuery( sql );
					AppInfo.nodecount = nodecount; AppInfo.startnum = startnum; AppInfo.samplingcyc = samplingcyc;
					AppInfo.frontstr = textBoxX5.Text;
				} catch (Exception ex) {
					string ss = ex.Message;
					MessageBox.Show( ss, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				}
				addcomparisontable = true;
				templistdisplay();
				//            if (!AppInfo.comparison) {
				////                sql = " DROP TABLE comparison"; SQLiteDBHelper.ExecQuery( sql );            SQLiteDBHelper.NewTable( "comparison", column );
				//                MessageBox.Show( "�ɹ��ı����¶ȣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				//            }   else {  //had creat

				//            }
			}
		}
		//�¶ȸߵ� ȷ������
		private void buttonX8_Click(object sender, EventArgs e) {
			if (AppInfo.AdminId != 1) {
				MessageBox.Show( "�����û���ɣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning ); return;
			}
			int low = Convert.ToInt32( textBoxX8.Text );  // = AppInfo.lowtem.ToString();   
			int up = Convert.ToInt32( textBoxX9.Text );
			if ((low != AppInfo.lowtemp) || (up != AppInfo.uptemp)) {
				sql = string.Format( "update parameters set  lowtem ={0},uptemp ={1},controltemp={2} ", low, up, '1' );
				SQLiteDBHelper.ExecQuery( sql );
				AppInfo.lowtemp = low; AppInfo.uptemp = up;
				MessageBox.Show( "�ɹ��ı����¶ȣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning );
			}
		}
		private void supply( ) {
			AppInfo.supply = "2";
			if (threephase.Checked) {
				AppInfo.supply = "3";
			}
		}
		private void radioButton1_CheckedChanged(object sender, EventArgs e) {
			supply();
		}
		private void radioButton2_CheckedChanged(object sender, EventArgs e) {
			supply();
		}
		// ϵͳ��ʼ��
		private void buttonX9_Click(object sender, EventArgs e) {
			if (AppInfo.AdminId != 1) {
				MessageBox.Show( "�ɳ�������Ա������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			MessageBoxButtons buttons = MessageBoxButtons.YesNo;
			var result = MessageBox.Show( "���ز���������������ݣ��ؽ����ݿ�������ݣ���ʼ�����û��˺������Ϊadmin", "��ѡ��", buttons );
			if (result == DialogResult.Yes) {
				try {
					sql = "DROP TABLE IF EXISTS Users"; SQLiteDBHelper.ExecQuery( sql );
					sql = "UserName VARCHAR (30), LoginName    VARCHAR( 30 ), PassWord     VARCHAR( 32 ),AdminId      INT( 1 ),  CreationDate TEXT( 30 )";
					SQLiteDBHelper.NewTable( "comparison", sql );
					sql = " DROP TABLE comparison"; SQLiteDBHelper.ExecQuery( sql );
					sql = " id INTEGER PRIMARY KEY AUTOINCREMENT, tempnum varchar(10), cabinetnum varchar(30)";
					SQLiteDBHelper.NewTable( "comparison", sql );
					sql = "INSERT INTO Users (UserName, LoginName, PassWord, AdminId, CreationDate) VALUES ('admin','admin',21232f297a57a5a743894a0e4a801fc3',1,null)";
					SQLiteDBHelper.ExecQuery( sql );                        //AppInfo.UserId = true; 
					AppInfo.UserName = "admin"; AppInfo.Login = true; AppInfo.LoginName = "admin";

				} catch (Exception ex) {
					string exs = "�������ݿ���� " + ex.Message;
					MessageBox.Show( exs, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning ); return;
				}
				sql = " DROP TABLE SystemLog"; SQLiteDBHelper.ExecQuery( sql );
				sql = "HandleTime [datetime] NOT NULL, operator [nvarchar] (20) NOT NULL, Abstract [nvarchar] (50) NOT NULL, opertorContent [nvarchar] (100) NOT NULL;";
				try {
					SQLiteDBHelper.NewTable( "SystemLog", sql );
				} catch (Exception ex) {
					string exs = "�������ݿ���� " + ex.Message;
					MessageBox.Show( exs, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning ); return;
				}
				MessageBox.Show( "�ɹ��������ݿ����Ӧ�ı�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning );
			}
		}
		//���ݱ���
		private void buttonX10_Click(object sender, EventArgs e) {
			if (AppInfo.AdminId != 1) {
				MessageBox.Show( "�ɳ�������Ա������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
		}
		//ɨ���������п�
		private void buttonX11_Click(object sender, EventArgs e) {
			hadscan = false;
			string[] comstr = new string[10];
			int c = 0;
			foreach (string com in SerialPort.GetPortNames()) {  //�Զ���ȡ���п�����
				comstr[c] = com; c++;
				comboBoxEx2.Items.Add( com );
			}
			if (c == 0) {
				MessageBox.Show( "��ļ����û���ִ��ڣ��밲װ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			hadscan = true;
			comboBoxEx2.Text = comstr[0];
		}
		private void buttonX13_Click(object sender, EventArgs e) {  // ��װWiFi

		}
		private void textBoxX4_MouseMove(object sender, MouseEventArgs e) {
			labelX18.Visible = true; labelX18.Text = "���ѡ������磬ϵͳ�Զ�������";
		}
		private void labelX2_MouseEnter(object sender, EventArgs e) {
			labelX18.Visible = true; labelX18.Text = "���ѡ������磬ϵͳ�Զ�������";
		}
		private void textBoxX4_MouseLeave(object sender, EventArgs e) {
			labelX18.Visible = false; labelX18.Text = "";
		}
		private void labelX2_MouseLeave(object sender, EventArgs e) {
			labelX18.Visible = false; labelX18.Text = "";
		}
		private void textBoxX10_MouseEnter(object sender, EventArgs e) {
			labelX18.Visible = true; labelX18.Text = "�������ڲ���С��60�룡";
		}
		private void labelX14_MouseEnter(object sender, EventArgs e) {
			labelX18.Visible = true; labelX18.Text = "�������ڲ���С��60�룡";
		}
		private void textBoxX10_MouseMove(object sender, MouseEventArgs e) {
			labelX18.Visible = true; labelX18.Text = "�������ڲ���С��60�룡";
		}
		private void labelX14_MouseLeave(object sender, EventArgs e) {
			labelX18.Visible = false; labelX18.Text = "";
		}
		private void textBoxX10_MouseLeave(object sender, EventArgs e) {
			labelX18.Visible = false; labelX18.Text = "";
		}
		private void textBoxX10_TextChanged(object sender, EventArgs e) {

		}
		//�������Ӳ����
		private void buttonX15_Click(object sender, EventArgs e) {

		}

		private void sideNavItem3_Click(object sender, EventArgs e) {

		}

		private void sideNavItem4_Click(object sender, EventArgs e) {

		}

		//��Ӽ���Ż����Ӧ��
		private void buttonX14_Click_1(object sender, EventArgs e) {

		}

		//��ӱ�Ŷ�Ӧ��
		private void buttonX14_Click(object sender, EventArgs e) {

		}
		//ȷ���޸�  ����� �����
		private void buttonX16_Click(object sender, EventArgs e) {
			string s1, s2, tempnum, cabinetnum, st;                  //         int i = dict.Count;//            for (int j = 0; j < i; j++) {
			int id;
			//        Dictionary<int, comparisondata>.ValueCollection cd2 = AppInfo.dict.Values;   //Dictionary<string, comparisondata> kvp = dict.Values;
			int j = 0;
			for (int i = 0; i < AppInfo.scdstrCount; i++) {
				s1 = AppInfo.scdstr[i].cabinetnum;
				s2 = AppInfo.scdstr[i].tempnum;
				id = AppInfo.scdstr[i].id;
				tempnum = (string)dataGridViewX1.Rows[j].Cells["tempnum"].Value;
				cabinetnum = (string)dataGridViewX1.Rows[j].Cells["cabinetnum"].Value;
				if (s1 != cabinetnum || s2 != tempnum) {
					try {
						st = "S" + tempnum;
						sql = string.Format( "CREATE TABLE IF NOT EXISTS {0}(stime [datetime] ,   sampledata  varchar(20) ) ", st );  //������
						SQLiteDBHelper.ExecQuery( sql );    //crearte  TABLE  sample data  by temp number
						sql = string.Format( "UPDATE  comparison set   tempnum='{0}',  cabinetnum='{1}'  where id = {2} ", tempnum, cabinetnum, id );
						SQLiteDBHelper.ExecQuery( sql );
					} catch (Exception ex) {
						string exs = "�����������ݿ���� " + ex.Message;
						MessageBox.Show( exs, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning ); return;
					}
				}
				j++;
			}
			sql = string.Format( "UPDATE  parameters set   comparison='1'" );
			SQLiteDBHelper.ExecQuery( sql );
			MessageBox.Show( "�ɹ�����Ӳ������źͲ���������ձ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning );
		}
		/*	sql = " DROP TABLE detection"; SQLiteDBHelper.ExecQuery( sql );   //creat tabale
			//if (SQLiteDBHelper.GetRecordCount( "detection" ) >0 ) {   //have recode
			//}            //column += " data VARCHAR( 20 )";
			//SQLiteDBHelper.NewTable( "detection", column );            //MessageBox.Show( "�ɹ���Ӽ�����ݿ⣡", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning );  */

		//ȷ�����пڲ���
		private void buttonX12_Click(object sender, EventArgs e) {
			if (!AppInfo.comset) {
				AppInfo.com = comboBoxEx2.SelectedItem.ToString();
				AppInfo.serialPort = new SerialPort {
					PortName = AppInfo.com,   // "COM1";
					BaudRate = 115200,
					Parity = Parity.None,
					StopBits = StopBits.One,
					DataBits = 8           //SerialPort serialPort = new SerialPort("COM1", 19200, Parity.Odd, StopBits.Two);  
				};					
				AppInfo.serialPort.Open();           
				AppInfo.serialPort.WriteLine( "send com" );          //��������  
				AppInfo.serialPort.Close();
				AppInfo.comset = true;
				labelX15.Text = "ʹ�ô��п�:" + AppInfo.com + ",�����ʣ�115200������λ8������żУ��λ��ֹͣλ1";
			} else {
				MessageBox.Show( "���п��Ѿ��򿪣�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning );
			}
		}

	}
}
