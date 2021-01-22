using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebMemcache
{
    public class SqlHelper
    {
        static string connStr = ConfigurationManager.ConnectionStrings["Demo"].ConnectionString;


        private static object _obj = new object();

        public static SqlConnection Connection
        {
            get
            {
                SqlConnection connection = null;

                if (connection == null)
                {
                    lock (_obj)
                    {
                        if (connection == null)
                        {
                            connection = new SqlConnection(connStr);
                        }
                    }
                }
                return connection;
            }
        }

    }
}