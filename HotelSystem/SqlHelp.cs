using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace HotelSystem1115
{
    class SqlHelp
    {
        //      private static string connStr = @"Data Source=.\STUDENT;Initial Catalog=dbHote112;Integrated Security=True";
        //      private static string connStr = @"Data Source=.\SQLEXPRESS;Initial Catalog=dbHote112;Integrated Security=True";
        //    public static string connStr = @"Data Source=.\SQLEXPRESS;AttachDbFilename=F:\酒店管理\dbHote112.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True";
        public static string connStr = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\C++C#\HOTELSYSTEM1115\DBHOTE113.MDF;Integrated Security = True; Connect Timeout = 30";

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
            try
            {
                conn.Open();
                o = com.ExecuteScalar();
            }
            catch (SqlException)             {
            }
            finally
            {
                conn.Close();
            }
            return o;
        }
        /// <summary>
        /// 适配器查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable ExcuteAsAdapter(string sql)
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand com = new SqlCommand(sql, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                adapter.Fill(dt);
            }
            catch (SqlException)
            {
                
            }
            finally
            {
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
        public static SqlDataReader ExcuteAsReader(string sql)
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand com = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataReader reader = com.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }
    }
}
