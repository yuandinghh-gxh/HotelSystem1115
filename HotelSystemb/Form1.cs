#region System
using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;



/* 本程序使用 MySql.Data.dll 链接Mysql数据库，读取服务器中所有数据库的名称，显示在界面上。HoverTree     */
#endregion

namespace MysqlHoverTree {
    public partial class Form1 : Form
    {
        private MySqlConnection _conn;
        public Form1( )        {
            InitializeComponent();
        }
        private void GetDatabases( )          {
            MySqlDataReader reader = null;
            MySqlCommand cmd = new MySqlCommand( "SHOW DATABASES", _conn );  //显示多个数据库
            try     {
                   reader = cmd.ExecuteReader();
                   listBox_database.Items.Clear();
                while (reader.Read())                {
                    listBox_database.Items.Add( reader.GetString( 0 ) );
                }
            }
            catch (MySqlException ex)           {
                MessageBox.Show( "Failed to populate database list: " + ex.Message );
            }
            finally            {
                if (reader != null) reader.Close();
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (_conn != null)             _conn.Close();
            string h_connString = "server=localhost;user id=root; password=yuanding; port=3306; database=mysql; pooling=false; charset=utf8";           //根据实际修改
            try          {
                 _conn = new MySqlConnection( h_connString );
                _conn.Open(); GetDatabases();                  //     MessageBox.Show( "连接数据库成功!" );
                label1.Text = "连接数据库成功!";
            }
            catch (MySqlException ex)
            {
                MessageBox.Show( "Error connecting to the server: " + ex.Message );
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (_conn != null) _conn.Close();
            string h_connString = "server=localhost;user id=root; password=yuanding; port=3306;         database=mysql;     pooling=false; charset=utf8";           //根据实际修改
            try
            {
                _conn = new MySqlConnection( h_connString );
                _conn.Open(); GetDatabases();                  //     MessageBox.Show( "连接数据库成功!" );
                label1.Text = "连接数据库成功!";

            }
            catch (MySqlException ex)
            {
                MessageBox.Show( "Error connecting to the server: " + ex.Message );
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string str = "Server=localhost; User ID=root; Password=yuanding; Database=mydb; CharSet=utf8";
            MySqlDataAdapter ada;
            MySqlConnection con;
            try        {
                con = new MySqlConnection( str );//实例化链接
                con.Open();//开启连接
                string strcmd = "select * from papercuts";
                MySqlCommand cmd = new MySqlCommand( strcmd, con );
                 ada = new MySqlDataAdapter( cmd );
                DataSet ds = new DataSet();
                ada.Fill( ds );//查询结果填充数据集 
                dataGridView1.DataSource = ds.Tables[0];
                con.Close();//关闭连接
            } 
            catch (MySqlException ex)              {
                MessageBox.Show( "Error connecting to the server: " + ex.Message );
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string str = "Server=localhost; User ID=root; Password=yuanding; Database=mydb; CharSet=utf8";
            try
            {
                MySqlConnection conn = new MySqlConnection( str );
                //_sql = string.Format( "insert into  Deposit values ('{0}','{1}','{2}',{3},{4},'退押金')", DateTime.Now.ToString( "yyyy-MM-dd HH:mm:ss" ), textBox1.Text, textBox7.Text, _truedep, _rentRoomInfoId ); // 记录 第一次 押金
                string strs = DateTime.Now.ToString( "yyyy-MM-dd HH:mm:ss" );
                String sql = " insert into opi  values (4, 'gxhyd', 'dd很便宜', 'dddqq','2018年09月11日 17时37分')";

                MySqlCommand com = new MySqlCommand( sql, conn );
                conn.Open();
                com.ExecuteNonQuery();
                //           com.ExecuteNonQuery();
                conn.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show( "Error connecting to the server: " + ex.Message );
            }
    //        conn.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {

        }
    }
}