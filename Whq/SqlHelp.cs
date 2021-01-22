using System;
using System.Data;
using System.Data.SqlClient;

namespace Whq {
    class SqlHelp      {
        //      private static string connStr = @"Data Source=.\SQLEXPRESS;Initial Catalog=dbHote112;Integrated Security=True";
            public static string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog = Data; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        internal static void ExcuteInsertUpdateDelete(object sql3) {
            throw new NotImplementedException();
        }

        public static void ExcuteInsertUpdateDelete(string sql)    // 增删改
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand com = new SqlCommand(sql, conn);
            conn.Open();
            com.ExecuteNonQuery();
            conn.Close();
        }
   
        public static object ExcuteScalar(string sql)     // 单值查询
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand com = new SqlCommand(sql, conn);
            object o = new object() ;
            try            {
                conn.Open();
                o = com.ExecuteScalar();
            }
            catch (SqlException)             {
            }
            finally            {
                conn.Close();
            }
            return o;
        }
        /// <summary>
        /// 适配器查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable ExcuteAsAdapter(string sql)        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand com = new SqlCommand(sql, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            try            {
                conn.Open();
                adapter.Fill(dt);
            }
            catch (SqlException)            {
                
            }
            finally            {
                conn.Close();
            }
            //adapter.Fill(dt);
            return dt;
        }
        /// <summary>
        /// 读取器查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static SqlDataReader ExcuteAsReader(string sql)        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand com = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataReader reader = com.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }
    }
}
