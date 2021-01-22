using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SQLiteTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string a = "2";
            if (a=="1")
            {
                goto there;
            }
            else
            {
                goto here;
            }
        
         

            #region 用SQL

            //创建连接字符串
            here:
            string constr = @"Data Source=F:\C++C#\HotelSystem1115\Whq\data.db;";
            //创建连接对象
            using (SQLiteConnection con = new SQLiteConnection(constr))
            {

                //创建SQL语句
                string sql = "select * from users";
                //创建命令对象
                using (SQLiteCommand cmd = new SQLiteCommand(sql, con))
                {

                    con.Open();
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine(reader.GetInt32(0) + " , " + reader.GetString(1));

                            }
                        }
                    }
                }
            }
            #endregion


            #region 用EF
            there:
            using (var context = new testEntities())
            {
                //foreach (var mt in context.mytable)
                //{
                //    Console.WriteLine(mt.value);
                //}

            }

            #endregion
            Console.Read();
        }
    }
}
