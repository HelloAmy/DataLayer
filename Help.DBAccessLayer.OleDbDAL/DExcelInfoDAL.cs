using Help.DBAccessLayer.IDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help.DBAccessLayer.OleDbDAL
{
    public class DExcelInfoDAL : IExcelInfo
    {
        public DataSet GetDataSet(IDbConnection conn, List<string> sheetNameList)
        {
            //数据表
            DataSet ds = new DataSet();
            foreach (var item in sheetNameList)
            {
                string strSql = "select * from [" + item + "]";

                // 获取Excel指定Sheet表中的信息
                OleDbCommand objCmd = new OleDbCommand(strSql, (OleDbConnection)conn);
                OleDbDataAdapter myData = new OleDbDataAdapter(strSql, (OleDbConnection)conn);

                // 填充数据
                myData.Fill(ds, item);
            }

            return ds;
        }
 
    }
}
