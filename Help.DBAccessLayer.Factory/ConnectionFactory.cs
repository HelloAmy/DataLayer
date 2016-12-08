using Help.DBAccessLayer.Model;
using IBM.Data.DB2;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
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

        /// <summary>
        /// Gets the excel OLE database connection.
        /// </summary>
        /// <param name="strExcelPath">The string excel path.</param>
        /// <returns></returns>
        public static IDbConnection GetExcelOleDbConnection(string strExcelPath)
        {
            //获取文件扩展名
            string strExtension = System.IO.Path.GetExtension(strExcelPath);
            string strFileName = System.IO.Path.GetFileName(strExcelPath);
            // Excel的连接
            OleDbConnection objConn = null;
            switch (strExtension)
            {
                case ".xls":
                    objConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strExcelPath + ";" + "Extended Properties=\"Excel 8.0;HDR=NO;IMEX=1;\"");
                    break;
                case ".xlsx":
                    objConn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strExcelPath + ";" + "Extended Properties=\"Excel 12.0;HDR=NO;IMEX=1;\"");
                    break;
                default:
                    objConn = null;
                    break;
            }

            if (objConn == null)
            {
                return null;
            }

            objConn.Open();

            return objConn;
        }
    }
}
