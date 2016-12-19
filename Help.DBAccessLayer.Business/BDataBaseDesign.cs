using Help.DBAccessLayer.Factory;
using Help.DBAccessLayer.IDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help.DBAccessLayer.Business
{
    public class BDataBaseDesign
    {
        public DataSet GetExcelDataBaseDesignInfo(string strExcelPath)
        {
            //数据表
            DataSet ds = new DataSet();

            IExcelInfo dao = DALFactory.GetExcelInfoDAL();

            using (var objConn = ConnectionFactory.GetExcelOleDbConnection(strExcelPath))
            {
                // 返回Excel的架构，包括各个sheet表的名称,类型，创建时间和修改时间等    
                DataTable dtSheetName = ((OleDbConnection)objConn).GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" });

                List<string> sheetList = this.GetTableNameList(dtSheetName);

                ds = dao.GetDataSet(objConn, sheetList);
            }

            return ds;
        }

        private List<string> GetTableNameList(DataTable dtSheetName)
        {
            string[] strTableNames = new string[dtSheetName.Rows.Count];
            for (int k = 0; k < dtSheetName.Rows.Count; k++)
            {
                strTableNames[k] = dtSheetName.Rows[k]["TABLE_NAME"].ToString();
            }

            return strTableNames.ToList();
        }
    }
}
