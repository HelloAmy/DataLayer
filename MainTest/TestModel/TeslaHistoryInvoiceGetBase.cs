using Help.DBAccessLayer.NPOIDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainTest.TestModel
{
    public abstract class TeslaHistoryInvoiceGetBase
    {
        public void SaveData(string fullFilePath, string fileNameWithoutExtension, string month, int sheetCount)
        {
            for (var i = 0; i < sheetCount; i++)
            {
                Console.WriteLine(fileNameWithoutExtension + "Sheet" + i + " start...");

                this.SaveDataBySheetIndex(fullFilePath, fileNameWithoutExtension + "_Sheet" + i, month, i);
                Console.WriteLine(fileNameWithoutExtension + "Sheet" + i + " end...");
            }
        }

        public void SaveDataBySheetIndex(string fullFilePath, string fileNameWithoutExtension, string month, int sheetIndex)
        {
            NPOIDAO dao = new NPOIDAO();

            List<List<string>> retData = new List<List<string>>();
            using (FileStream fs = File.Open(fullFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                retData = dao.getData(fs, sheetIndex);
            }

            List<string> sqlString = new List<string>();

            string sqlTemplate = @" INSERT INTO A_BD02([EntityCode],[Month],[SerialNumber],[InvoiceType] ,[InvoiceCode],[InvoiceNo],[BuyerName],[BuyerName1],[Remark],[IssuedDateTime],[Amount],[TaxAmount],[TaxRate],[FileName])
 VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13});
";
            foreach (var item in retData)
            {
                if (item[0] == "Entity")
                {
                    continue;
                }

                OutputInvoiceIssuedRecord record = this.GetOutputInvoiceIssuedRecord(item, month);

                string temp = string.Format(sqlTemplate, getStr(record.EntityCode),
                    getStr(record.Month),// month
                    getStr(record.SerialNumber), // SerialNumber
                    getStr(record.InvoiceType),//InvoiceType
                    getStr(record.InvoiceCode),//InvoiceCode
                    getStr(record.InvoiceNo),//InvoiceNo
                    getStr(record.BuyerName),//BuyerName
                    getStr(record.BuyerName1),//BuyerName1
                    getStr(record.Remark),//Remark
                    getStr(record.IssuedDateTime),//IssuedDateTime
                    getOther(record.Amount),//Amount
                    getOther(record.TaxAmount),//TaxAmount
                    getOther(record.TaxRate),//TaxRate,
                    getStr(fileNameWithoutExtension)
                    );


                sqlString.Add(temp);
            }


            List<string> saveSql = sqlString.Take(10000).ToList<string>();

            this.saveFile(saveSql, 0, fileNameWithoutExtension);

            int index = 1;

            while (saveSql.Count == 10000)
            {
                saveSql = sqlString.Skip(index * 10000).Take(10000).ToList<string>();
                this.saveFile(saveSql, index, fileNameWithoutExtension);
                index++;
            }
        }


        public abstract OutputInvoiceIssuedRecord GetOutputInvoiceIssuedRecord(List<string> list, string month);

        public string getOther(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(value.Trim()))
            {
                return "null";
            }
            else
            {
                if (value == "17%")
                {
                    return "0.17";
                }
                else if (value == "6%")
                {
                    return "0.06";
                }
                else
                {
                    return value;
                }
            }
        }


        public bool SaveDB(string saveSql, out int count)
        {
            bool ret = false;
            count = 0;
            try
            {
                using (IDbConnection conn = this.GetDbConn())
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand(saveSql, (SqlConnection)conn);

                    count = comm.ExecuteNonQuery();
                }

                ret = true;
            }
            catch (Exception ex)
            {

            }

            return ret;

        }


        public IDbConnection GetDbConn()
        {
            IDbConnection conn = null;
            string connstr = "Server=HLIU114SEC0,1801;Database=TaxAdmin;User ID=sa;Password=P@ss1234;Trusted_Connection=false;MultipleActiveResultSets=true;";

            conn = new SqlConnection(connstr);
            return conn;
        }

        public string getStr(string value)
        {
            if (value == null)
            {
                return "null";
            }
            else
            {
                var sqlvalue = value;
                if (value.Contains("'"))
                {
                    sqlvalue = value.Replace("'", "''");
                }

                return "N'" + sqlvalue + "'";
            }
        }

        private void saveFile(List<string> sqlString, int index, string fileNameWithoutExtension)
        {
            string str = string.Join("", sqlString.ToArray());
            int count = 0;
            bool ret = this.SaveDB(str, out count);

            var fileName = fileNameWithoutExtension + index + "SQL" + DateTime.Now.ToString("MMddHHmmss") + ".txt";
            if (ret)
            {
                Console.WriteLine(fileNameWithoutExtension + index + ": success count:" + count);
                return;
            }
            else
            {
                Console.WriteLine("Error!!!," + fileName);
            }


            using (FileStream fs2 = new FileStream(fileName, FileMode.Create))
            {
                StreamWriter sw = new StreamWriter(fs2);

                //开始写入
                sw.Write(str);
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
            }
        }

    }
}
