using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help.DBAccessLayer.Util
{
    public class DBUtil
    {
        public static string GetDBValueStr(IDataReader reader, string columnName, string defaultValue = "")
        {
            if (reader[columnName] == DBNull.Value)
            {
                return defaultValue;
            }
            else
            {
                string value = string.Empty;
                value = reader[columnName].ToString();
                return value;
            }
        }

        public static int GetDBValueInt(IDataReader reader, string columnName, int defaultValue = -1)
        {
            if (reader[columnName] == DBNull.Value)
            {
                return defaultValue;
            }
            else
            {
                string value = string.Empty;
                value = reader[columnName].ToString();
                return Convert.ToInt32(value);
            }
        }

        public static DateTime GetDBValueDateTime(IDataReader reader, string columnName, string defaultValue = "0000-01-01")
        {
            if (reader[columnName] == DBNull.Value)
            {
                return Convert.ToDateTime(defaultValue);
            }
            else
            {
                string value = string.Empty;
                value = reader[columnName].ToString();
                return Convert.ToDateTime(value);
            }
        }

        public static bool GetDBValueBool(IDataReader reader, string columnName, bool defaultValue = false)
        {
            if (reader[columnName] == DBNull.Value)
            {
                return defaultValue;
            }
            else
            {
                string value = string.Empty;
                value = reader[columnName].ToString();

                string[] yesParas = { "1", "Y", "TRUE" };

                if (!string.IsNullOrEmpty(value) && yesParas.Contains(value.ToUpper()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
