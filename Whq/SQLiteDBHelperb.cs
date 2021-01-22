using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;

namespace Whq {
    public class SQLiteDBHelperb {
        public string connectionString = string.Empty;
        public static string dataSource = @"F:\HotelSystem1115\Whq\data.db";
        public static SQLiteConnection connection = null;
        /// <param name="dbPath">数据库文件路径</param>
        public SQLiteDBHelperb(string dbPath) {
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
        /// 打开连接
                /// <param name="dataSource"></param>
                /// <param name="password"></param>
        public void SetDataSource(string password) {
            connectionString = string.Format( "Data Source={0};Pooling=true;FailIfMissing=false", dataSource );
            connection = new SQLiteConnection( connectionString );
            if (!string.IsNullOrEmpty( password )) {
                connection.SetPassword( password );
            }
            connection.Open();
        }

        #region 通用方法
        /// <summary>
                   /// 查询表
                /// </summary>
                /// <param name="sqlStr"></param>
                /// <returns>DataTable</returns>
        public static DataTable ExecQuery(string sqlStr) {
            string connectionString = string.Format( "Data Source={0};Pooling=true;FailIfMissing=false", dataSource );
            connection = new SQLiteConnection( connectionString );
            DataTable dt = new DataTable();
            try {
                SQLiteDataAdapter OraDa = new SQLiteDataAdapter( sqlStr, connection );
                OraDa.Fill( dt );
                connection.Close();
                return dt;
            } catch (SQLiteException e) {
                string s = e.Message;
                return null;
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
                    ex.ToString();
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
                    e.ToString();
                    return 0;
                }
            }
        }
        #endregion
    }
}
