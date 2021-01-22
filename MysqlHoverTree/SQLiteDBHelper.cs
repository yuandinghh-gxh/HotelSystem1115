
using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Vcom {
	/// <summary>
	    /// 说明：这是一个针对System.Data.SQLite的数据库常规操作封装的通用类。 
	    /// </summary>
	public class SQLiteDBHelper {
		public string connectionString = string.Empty;
		public static string dataSource = @"D:\HotelSystem1115\Whq\data.db";
		public static SQLiteConnection connection = null;
		public static SQLiteDataAdapter OraDa;
		/// <param name="dbPath">数据库文件路径</param>
		public SQLiteDBHelper(string dbPath) {
			this.connectionString = "Data Source=" + dbPath;
		}
		/// 创建数据库文件
		        /// <param name="dbPath"></param>
		public static void CreateDB(string dbPath) {
			using (SQLiteConnection connection = new SQLiteConnection( "Data Source=" + dbPath )) {
				connection.Open();
				using (SQLiteCommand command = new SQLiteCommand( connection )) {
					command.CommandText = "CREATE TABLE Demo(id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE)";
					command.ExecuteNonQuery();
					command.CommandText = "DROP TABLE Demo";
					command.ExecuteNonQuery();
				}
			}
		}
		/// <summary>
		/// 创建表
		/// </summary>
		/// <param name="dbPath">指定数据库文件</param>
		/// <param name="tableName">表名称</param>
		static public void NewTable(string tableName, string column) {
			string connectionString = string.Format( "Data Source={0};Pooling=true;FailIfMissing=false", dataSource );
			connection = new SQLiteConnection( connectionString );
			if (connection.State != ConnectionState.Open) {
				connection.Open(); column = "(" + column + ")";
				SQLiteCommand cmd = new SQLiteCommand {
					Connection = connection, CommandText = "CREATE TABLE " + tableName + column
				};
				// "(lowtem int NOT NULL,uptemp int NOT NULL,nodecount int NOT NULL,frontstr VARCHAR( 20 ) NULL,rearst VARCHAR( 20 ) NULL,timeopen VARCHAR( 20 ) NULL,startnum int NOT NULL)"
				cmd.ExecuteNonQuery();
			}
			connection.Close();
		}

		/// 打开连接
		        /// <param name="dataSource"></param>
		        /// <param name="password"></param>
		//public void SetDataSource( string password) {
		//    connectionString = string.Format( "Data Source={0};Pooling=true;FailIfMissing=false", dataSource );
		//    connection = new SQLiteConnection( connectionString );
		//    if (!string.IsNullOrEmpty( password )) {        //        connection.SetPassword( password );        //    }           //    connection.Open();        //}
		#region 通用方法
		/// <summary>
		        /// 查询表
		        /// </summary>
		        /// <param name="sqlStr"></param>
		        /// <returns></returns>
		public static DataTable ExecQuery(string sqlStr) {
			string connectionString = string.Format( "Data Source={0};Pooling=true;FailIfMissing=false", dataSource );
			connection = new SQLiteConnection( connectionString );
			DataTable dt = new DataTable();
			try {
				OraDa = new SQLiteDataAdapter( sqlStr, connection );
				OraDa.Fill( dt );
				connection.Close();
				return dt;
			} catch (SQLiteException e) {
				string s = "数据库错误：" + e.Message;
				MessageBox.Show( s, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning ); return null;
			} finally { connection.Close(); }
		}
		/// <summary>
		        /// 增、删、改操作  
		        /// </summary>
		        /// <param name="commandStr">sql语句</param>
		        /// <returns>是否成功</returns>
		public static bool ExecuteCommand(string sqlStr) {
			string connectionString = string.Format( "Data Source={0};Pooling=true;FailIfMissing=false", dataSource );
			connection = new SQLiteConnection( connectionString );
			using (SQLiteCommand cmd = new SQLiteCommand( sqlStr, connection )) {
				try {
					if (cmd.ExecuteNonQuery() > 0) {
						connection.Close();
						return true;
					} else {
						connection.Close();
						return false;
					}
				} catch (Exception ex) {
					string s = "数据库错误：" + ex.Message;
					MessageBox.Show( s, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning );
					connection.Close();
					return false;
				}
			}
		}
		/// <summary>
		        /// 根据查询语句，获取表中记录的条数   select count(*) from t_Developer
		        /// </summary>
		        /// <param name="sqlStr"></param>
		        /// <returns></returns>
		public static int GetRecordCount(string sqlStr) {
			string connectionString = string.Format( "Data Source={0};Pooling=true;FailIfMissing=false", dataSource );
			connection = new SQLiteConnection( connectionString );
			using (SQLiteCommand cmd = new SQLiteCommand( sqlStr, connection )) {
				try {
					cmd.CommandText = sqlStr;
					SQLiteDataReader dr = cmd.ExecuteReader();
					connection.Close();
					if (dr.Read()) {
						return dr.FieldCount;
					}
					return 0;
				} catch (Exception e) {
					string s = "数据库错误：" + e.Message;
					MessageBox.Show( s, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return 0;
				}
			}
		}
		//public bool tabbleIsExist(string tableName) {        //      bool result = false;
		//    if (tableName == null) {
		//        return false;
		//    }
		//    SQLiteDatabase db = null;
		//    Cursor cursor = null;
		//    try {
		//        db = this.getReadableDatabase();
		//        String sql = "select count(*) as c from " + AppConstant.DataBaseName + " where type ='table' and name ='" + tableName.trim() + "' ";
		//        cursor = db.rawQuery( sql, null );
		//        if (cursor.moveToNext()) {
		//            int count = cursor.getInt( 0 );
		//            if (count > 0) {
		//                //          result = true;
		//                return true;
		//            }
		//        }

		//    } catch (Exception e) {
		//        // TODO: handle exception
		//        return false;
		//    }
		#endregion
	}
}


