using Help.DBAccessLayer.Model;
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

    public class OutputInvoiceIssuedRecordImport
    {

        public void getFleName(string path)
        {
            DirectoryInfo root = new DirectoryInfo(path);
            foreach (FileInfo f in root.GetFiles())
            {
                // ? 这里应该去掉隐藏文件
                string str = f.FullName;

                string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(str);// 没有扩展名的文件名 “Default”

                Console.WriteLine(fileNameWithoutExtension + " start...");

                Console.WriteLine(" 需要调用特定方法去处理...");

                //TestReadDataCommonGD(str, fileNameWithoutExtension);

                Console.WriteLine(fileNameWithoutExtension + " end...");
            }
        }

        public void ExcuteImportFiles(string path, string month)
        {
            DirectoryInfo root = new DirectoryInfo(path);
            foreach (FileInfo f in root.GetFiles())
            {
                // ? 这里应该去掉隐藏文件
                string str = f.FullName;

                string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(str);// 没有扩展名的文件名 “Default”

                Console.WriteLine(fileNameWithoutExtension + " start...");

                if ((f.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                {
                    Console.WriteLine(" 隐藏文件不需要处理...");
                }
                else
                {
                    Console.WriteLine(" 需要调用特定方法去处理...");
                    //TestReadData201803(str, fileNameWithoutExtension, month);
                }

                Console.WriteLine(fileNameWithoutExtension + " end...");
            }
        }

        public void TestReadData201803(string fullFilePath, string fileNameWithoutExtension, string month)
        {
            NPOIDAO dao = new NPOIDAO();

            List<List<string>> retData = new List<List<string>>();
            using (FileStream fs = File.Open(fullFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                retData = dao.getData(fs, 0);
            }

            List<string> sqlString = new List<string>();

            string sqlTemplate = @" INSERT INTO OutputInvoiceIssuedRecord201803([EntityCode],[Month],[SerialNumber],[InvoiceType] ,[InvoiceCode],[InvoiceNo],[BuyerName],[BuyerName1],[Remark],[IssuedDateTime],[Amount],[TaxAmount],[TaxRate])
 VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12});
";
            foreach (var item in retData)
            {
                if (item[0] != null && item[0].ToLower() == "entity")
                {
                    continue;
                }

                string temp = string.Format(sqlTemplate, getStr(item[0]),
                    getStr(month),//月份
                    getStr(null), // 序列号
                    getStr(item[2]),//发票类型
                    getStr(item[3]),// fapiao code
                    getStr(item[4]),// fapiao number
                    getStr(item[5]),//buyer
                    getStr(item[6]),//buyer1
                    getStr(item[1]),//remark
                    getStr(item[8]),//IssuedDateTime
                    getOther(item[9]),//Amount
                    getOther(item[11]),//TaxAmount
                    getOther(item[10])//TaxRate
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


        /// <summary>
        /// BD 版本2018 年的导入
        /// </summary>
        /// <param name="fullFilePath"></param>
        /// <param name="fileNameWithoutExtension"></param>
        public void TestReadData2018(string fullFilePath, string fileNameWithoutExtension)
        {
            NPOIDAO dao = new NPOIDAO();

            List<List<string>> retData = new List<List<string>>();
            using (FileStream fs = File.Open(fullFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                retData = dao.getData(fs, 0);
            }


            List<string> sqlString = new List<string>();

            string sqlTemplate = @" INSERT INTO AAA([EntityCode],[Month],[SerialNumber],[InvoiceType] ,[InvoiceCode],[InvoiceNo],[BuyerName],[BuyerName1],[Remark],[IssuedDateTime],[Amount],[TaxAmount],[TaxRate],[FileName])
 VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13});
";
            foreach (var item in retData)
            {
                if (item[0] == "发票代码")
                {
                    continue;
                }

                string temp = string.Format(sqlTemplate, getStr(null),
                    getStr("201801"),// month
                    getStr(null), // SerialNumber
                    getStr("增值税专用票"),//InvoiceType
                    getStr(item[3]),//InvoiceCode
                    getStr(item[4]),//InvoiceNo
                    getStr(item[5]),//BuyerName
                    getStr(item[6]),//BuyerName1
                    getStr(item[1]),//Remark
                    getStr(item[8]),//IssuedDateTime
                    getOther(item[9]),//Amount
                    getOther(item[11]),//TaxAmount
                    getOther(item[10]),//TaxRate,
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

        /// <summary>
        /// 普通版本的GD 文件导入
        /// </summary>
        /// <param name="fullFilePath"></param>
        /// <param name="fileNameWithoutExtension"></param>
        public void TestReadDataCommonGD(string fullFilePath, string fileNameWithoutExtension)
        {
            NPOIDAO dao = new NPOIDAO();

            List<List<string>> retData = new List<List<string>>();
            using (FileStream fs = File.Open(fullFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                retData = dao.getData(fs, 0);
            }


            List<string> sqlString = new List<string>();

            string sqlTemplate = @" INSERT INTO AAA([EntityCode],[Month],[SerialNumber],[InvoiceType] ,[InvoiceCode],[InvoiceNo],[BuyerName],[BuyerName1],[Remark],[IssuedDateTime],[Amount],[TaxAmount],[TaxRate],[FileName])
 VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13});
";
            foreach (var item in retData)
            {
                if (item[0] == "发票代码")
                {
                    continue;
                }

                string temp = string.Format(sqlTemplate, getStr(null),
                    getStr(item[9]),// month
                    getStr(null), // SerialNumber
                    getStr("增值税专用票"),//InvoiceType
                    getStr(item[0]),//InvoiceCode
                    getStr(item[1]),//InvoiceNo
                    getStr(item[2]),//BuyerName
                    getStr(item[8]),//BuyerName1
                    getStr(item[4]),//Remark
                    getStr(item[11]),//IssuedDateTime
                    getOther(item[5]),//Amount
                    getOther(item[7]),//TaxAmount
                    getOther(item[6]),//TaxRate,
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

        /// <summary>
        /// GD 发票 2014 年 到 2015年数据导入
        /// </summary>
        /// <param name="fullFilePath"></param>
        /// <param name="fileNameWithoutExtension"></param>
        public void TestReadData2014TO2015(string fullFilePath, string fileNameWithoutExtension)
        {
            NPOIDAO dao = new NPOIDAO();

            List<List<string>> retData = new List<List<string>>();
            using (FileStream fs = File.Open(fullFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                retData = dao.getData(fs, 0);
            }


            List<string> sqlString = new List<string>();

            string sqlTemplate = @" INSERT INTO AAA([EntityCode],[Month],[SerialNumber],[InvoiceType] ,[InvoiceCode],[InvoiceNo],[BuyerName],[BuyerName1],[Remark],[IssuedDateTime],[Amount],[TaxAmount],[TaxRate],[FileName])
 VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13});
";
            foreach (var item in retData)
            {
                if (item[0] == "Year")
                {
                    continue;
                }

                string temp = string.Format(sqlTemplate, getStr(null),
                    getStr(item[12]),// month
                    getStr(null), // SerialNumber
                    getStr("增值税专用票"),//InvoiceType
                    getStr(null),//InvoiceCode
                    getStr(null),//InvoiceNo
                    getStr(item[2]),//BuyerName
                    getStr(item[11]),//BuyerName1
                    getStr(item[5]),//Remark
                    getStr(item[12]),//IssuedDateTime
                    getOther(item[9]),//Amount
                    getOther(item[8]),//TaxAmount
                    getOther(item[7]),//TaxRate,
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


        /// <summary>
        /// 2018年01月的GD版本导入
        /// </summary>
        /// <param name="fullFilePath"></param>
        /// <param name="fileNameWithoutExtension"></param>
        public void TestReadDataGD201801(string fullFilePath, string fileNameWithoutExtension)
        {
            NPOIDAO dao = new NPOIDAO();

            List<List<string>> retData = new List<List<string>>();
            using (FileStream fs = File.Open(fullFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                retData = dao.getData(fs, 0);
            }


            List<string> sqlString = new List<string>();

            string sqlTemplate = @" INSERT INTO AAA([EntityCode],[Month],[SerialNumber],[InvoiceType] ,[InvoiceCode],[InvoiceNo],[BuyerName],[BuyerName1],[Remark],[IssuedDateTime],[Amount],[TaxAmount],[TaxRate])
 VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12});
";
            foreach (var item in retData)
            {
                if (item[0] == "Entity")
                {
                    continue;
                }

                string temp = string.Format(sqlTemplate, getStr(item[0]),
                    getStr(null),
                    getStr(null),
                    getStr(item[2]),
                    getStr(item[3]),
                    getStr(item[4]),
                    getStr(item[5]),
                    getStr(item[6]),
                    getStr(item[1]),
                    getStr(item[8]),
                    getOther(item[9]),
                    getOther(item[11]),
                    getOther(item[10])
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

        /// <summary>
        /// 普通版本的BD 导入
        /// </summary>
        /// <param name="fullFilePath"></param>
        /// <param name="fileNameWithoutExtension"></param>
        public void TestReadData(string fullFilePath, string fileNameWithoutExtension)
        {
            NPOIDAO dao = new NPOIDAO();

            List<List<string>> retData = new List<List<string>>();
            using (FileStream fs = File.Open(fullFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                retData = dao.getData(fs, 0);
            }


            List<string> sqlString = new List<string>();

            string sqlTemplate = @" INSERT INTO AAA([EntityCode],[Month],[SerialNumber],[InvoiceType] ,[InvoiceCode],[InvoiceNo],[BuyerName],[BuyerName1],[Remark],[IssuedDateTime],[Amount],[TaxAmount],[TaxRate])
 VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12});
";
            foreach (var item in retData)
            {
                if (item[0] == "entity")
                {
                    continue;
                }

                string temp = string.Format(sqlTemplate, getStr(item[0]),
                    getStr(item[2]),
                    getStr(item[3]),
                    getStr(item[4]),
                    getStr(item[5]),
                    getStr(item[6]),
                    getStr(item[7]),
                    getStr(item[8]),
                    getStr(item[9]),
                    getStr(item[10]),
                    getOther(item[11]),
                    getOther(item[13]),
                    getOther(item[12])
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

        public IDbConnection GetDbConn()
        {
            IDbConnection conn = null;
            //string connstr = "Server=HLIU114SEC0,1801;Database=TaxAdmin;User ID=sa;Password=P@ss1234;Trusted_Connection=false;MultipleActiveResultSets=true;";

            string connstr = "Server=cnshaappuwv707,1800;Database=TaxAdmin;User ID=sa;Password=P@ss1234;Trusted_Connection=false;MultipleActiveResultSets=true;";

            conn = new SqlConnection(connstr);
            return conn;
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
                Console.WriteLine(ex.StackTrace.ToString());
            }

            return ret;

        }

        public void UpdateInfo()
        {
            string sql = @"select Distinct [IssuedDateTime] from [TaxAdmin].[dbo].[OutputInvoiceIssuedRecord]
where  len([IssuedDateTime])=5
order by [IssuedDateTime]
";

            List<string> fieldList = new List<string>();
            try
            {
                using (IDbConnection conn = this.GetDbConn())
                {
                    conn.Open();

                    SqlCommand comm = new SqlCommand(sql, (SqlConnection)conn);
                    SqlDataReader reader = comm.ExecuteReader();
                    while (reader.Read())
                    {

                        string temp = reader["IssuedDateTime"] == DBNull.Value ? string.Empty : reader["IssuedDateTime"].ToString();

                        fieldList.Add(temp);
                    }

                }


            }
            catch (Exception ex)
            {

            }


            try
            {
                string templateSql = @"
update [TaxAdmin].[dbo].[OutputInvoiceIssuedRecord] set [IssuedDateTime] =N'{0}'
where  [IssuedDateTime] ='{1}';";


                List<string> executeSql = new List<string>();
                foreach (var item in fieldList)
                {
                    DateTime tempD = DateTime.FromOADate(double.Parse(item));

                    string temp = string.Format(templateSql, tempD.ToString("yyyy-MM-dd"), item);
                    executeSql.Add(temp);
                }

                string finalSql = string.Join("", executeSql.ToArray());

                using (IDbConnection conn = this.GetDbConn())
                {
                    conn.Open();

                    SqlCommand comm = new SqlCommand(finalSql, (SqlConnection)conn);

                    int count = comm.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

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
    }

    public class OutputInvoiceIssuedRecord
    {
        public string ID
        {
            get;
            set;
        }

        public string EntityCode
        {
            get;
            set;
        }

        public string Month
        {
            get;
            set;
        }

        public string SerialNumber
        {
            get;
            set;
        }

        public string InvoiceType
        {
            get;
            set;
        }

        public string InvoiceCode
        {
            get;
            set;
        }

        public string InvoiceNo
        {
            get;
            set;
        }

        public string BuyerName
        {
            get;
            set;
        }

        public string BuyerName1
        {
            get;
            set;
        }

        public string Remark
        {
            get;
            set;
        }

        public string IssuedDateTime
        {
            get;
            set;
        }

        public string Amount
        {
            get;
            set;
        }

        public string TaxAmount
        {
            get;
            set;
        }

        public string TaxRate
        {
            get;
            set;
        }

        public int InvoiceStatus
        {
            get;
            set;
        }
    }
}
