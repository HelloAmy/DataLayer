using Help.DBAccessLayer.IDAL;
using Help.DBAccessLayer.Model.SqlGenerator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help.DBAccessLayer.SQLServer
{
    public class DGetSchema : IGetSchema
    {
        public List<Model.MTableDesc> GetTableList(System.Data.IDbConnection conn, string creater)
        {
            throw new NotImplementedException();
        }

        public List<Model.MColumn> GetColumnList(System.Data.IDbConnection conn, string tableName)
        {
            throw new NotImplementedException();
        }

        public MDataBaseDefine GenerateDataBaseDefine(System.Data.IDbConnection conn)
        {
            string queryTableListSql = "SELECT name FROM SYSOBJECTS WHERE TYPE='U';";
            SqlCommand comm = new SqlCommand(queryTableListSql, (SqlConnection)conn);
            SqlDataReader reader = comm.ExecuteReader();

            List<string> tableNameList = new List<string>();

            while (reader.Read())
            {
                string name = reader["name"] == DBNull.Value ? string.Empty : reader["name"].ToString();
                tableNameList.Add(name);
            }

            return null;
        }
    }
}
