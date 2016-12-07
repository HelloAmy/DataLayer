using Help.DBAccessLayer.Model;
using IBM.Data.DB2;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help.DBAccessLayer.Factory
{
    public class ConnectionFactory
    {
        public static string TRSDbConnString
        {
            get { return ConfigurationManager.ConnectionStrings["TRSDbServer"].ToString(); }
        }

        public static string GetBDConnString(string dbname)
        {
            return ConfigurationManager.ConnectionStrings[dbname].ToString();
        }

        public static IDbConnection GetDbConn(string dbname, MDataBaseType type)
        {
            IDbConnection conn = null;
            string connstr = GetBDConnString(dbname);
            switch (type)
            {
                case MDataBaseType.MYSQL:
                    conn = new MySqlConnection(connstr);
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    break;
                case MDataBaseType.SQLSERVER:
                    conn = new SqlConnection(connstr);
                    break;
                case MDataBaseType.DB2:
                    conn = new DB2Connection(connstr);
                    break;
                case MDataBaseType.UNKNOW:
                    throw new Exception("未知数据库类型，创建数据库链接失败");
            }

            return conn;
        }
    }
}
