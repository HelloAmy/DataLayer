using Help.DBAccessLayer.Business;
using Help.DBAccessLayer.Model;
using Help.DBAccessLayer.Model.SqlGenerator;
using MainTest.TestModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Security.Cryptography.X509Certificates;
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Authentication;
using System.Net;
using Microsoft.International.Formatters;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Data;
using System.Data.OleDb;
using Help.DBAccessLayer.Factory;
using Help.DBAccessLayer.IDAL;
using System.Xml.Linq;
using System.Xml;
using MainTest.RedLetterQuery;


namespace MainTest
{
    public enum HelpStaus
    {
        Yes = 1, No = 0
    }

    class Program
    {
        static X509Certificate serverCertificate = null;

        static void Main(string[] args)
        {
            new TExcel().TestCreateExcel();
            Console.WriteLine("结束");
            Console.Read();
        }


        private static void TestDisplayDecimal()
        {
            // 结论1：string.Format("{0:N2}", d) 与  Math.Round(d, 2).ToString() 【不总是相等】
            // 结论2:string.Format("{0:N2}", d) 与 RoundChinese(d, 2).ToString() 【总是相等】

            for (var i = 0; i <= 30; i++)
            {
                var d = 2.14M + i / 1000M;
                bool ret = string.Format("{0:N2}", d) == Math.Round(d, 2).ToString();
                if (!ret)
                {
                    Console.WriteLine(d + ":" + string.Format("{0:N2}", d) + "," + Math.Round(d, 2) + "," + ret);
                }
            }

            Console.WriteLine("-----------------------");

            for (var i = 0; i <= 30; i++)
            {
                var d = 2.14M + i / 1000M;
                bool ret = string.Format("{0:N2}", d) == RoundChinese(d, 2).ToString();

                Console.WriteLine(d + ":" + string.Format("{0:N2}", d) + "," + RoundChinese(d, 2).ToString() + "," + ret);
            }
        }

        /// <summary>
        /// 中国版的四舍五入
        /// </summary>
        /// <param name="value"></param>
        /// <param name="digit"></param>
        /// <returns></returns>
        public static decimal RoundChinese(decimal value, int digit)
        {
            double vt = Math.Pow(10, digit);
            //1.乘以倍数 + 0.5
            decimal vx = value * (decimal)vt + 0.5M;
            //2.向下取整
            decimal temp = Math.Floor(vx);
            //3.再除以倍数
            return (temp / (decimal)vt);
        }


        public static void Des()
        {
            Regex r = new Regex("<mw>.*</mw>");
            string filePath = @"C:\Julius_J_Zhu\09DataLayer\MainTest\documents\log.txt";
            string str = ReadFile(filePath);

            var collection = r.Matches(str);

            StringBuilder ret = new StringBuilder();

            var index = 1;

            foreach (var mat in collection)
            {
                string value = mat.ToString();

                if (value != null)
                {

                    string realValue = value.Replace("<mw>", string.Empty).Replace("</mw>", string.Empty);

                    string resultValue = DBQuery.AesDecrypt(realValue);

                    str = str.Replace(value, "<mw>" + resultValue + "</mw>");
                }

                index++;
            }

            r = new Regex("<json>.*</json>");
            collection = r.Matches(str);

            foreach (var mat in collection)
            {
                string value = mat.ToString();

                if (value != null)
                {

                    string realValue = value.Replace("<json>", string.Empty).Replace("</json>", string.Empty);

                    string resultValue = DBQuery.AesDecrypt(realValue);

                    str = str.Replace(value, "<json>" + resultValue + "</json>");
                }
            }

            string fileName = "log" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";

            SaveFile(str, fileName);

            // 写入文件
        }

        public static void TestBwJDCInvoiceData()
        {
            string filePath = @"C:\Julius_J_Zhu\09DataLayer\MainTest\documents\test.xml";
            string str = ReadFile(filePath);
            BaiWangYbjszOutput wrraper = (BaiWangYbjszOutput)XmlSerializer.LoadFromXml(str, typeof(BaiWangYbjszOutput));
            //string json = DBQuery.AesDecrypt(wrraper.json);
            //string json = wrraper.json;
            //var res = DeserializeTaxControlQueryInfo<BaiWangVehicleFpplcxObj>(json);
        }

        private void TestDeserializeTaxControlQueryInfo()
        {
            string filePath = @"C:\Julius_J_Zhu\09DataLayer\MainTest\documents\test.xml";
            string str = ReadFile(filePath);
            DBQueryOutputObj wrraper = new DBQueryOutputObj();
            wrraper = (DBQueryOutputObj)XmlSerializer.LoadFromXml(str, typeof(DBQueryOutputObj));
            var res = DeserializeTaxControlQueryInfo<BWRedLetterMX>(wrraper.json);
            Console.WriteLine("结束");
            Console.Read();
        }

        private static List<T> DeserializeTaxControlQueryInfo<T>(string strData)
        {
            dynamic queryObj = JsonConvert.DeserializeObject(strData);

            // 处理这种情况 [{},{}]
            if (queryObj is Newtonsoft.Json.Linq.JArray)
            {
                return JsonConvert.DeserializeObject<List<T>>(strData);
            }

            // { 'data':[]} 处理这种数据的情况，报 out of index 的bug
            if (queryObj.data != null && queryObj.data is Newtonsoft.Json.Linq.JArray)
            {
                var dataList = queryObj.data as Newtonsoft.Json.Linq.JArray;

                if (dataList.Count == 0)
                {
                    return new List<T>();
                }
            }

            var content = queryObj.data != null && queryObj.data[0] is Newtonsoft.Json.Linq.JArray ? queryObj.data[0] : queryObj.data;
            var queryList = JsonConvert.DeserializeObject<List<T>>(Convert.ToString(content));
            return queryList;
        }

        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="path">文件路径和文件名</param>
        /// <returns>文件内容</returns>
        public static string ReadFile(string path)
        {
            string str = string.Empty;
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    str = reader.ReadToEnd();
                    return str;
                }
            }
            catch (Exception ex)
            {
                Console.Write("读文件错误:" + ex.Message.ToString());
            }

            return str;
        }


        public static double ToJulianDate(DateTime date)
        {
            return date.ToOADate() + 2415018.5;
        }

        public static DateTime FromJulianDate(double julianDate)
        {
            return DateTime.FromOADate(julianDate - 2415018.5);
        }

        private static void TestShowDecimal()
        {
            decimal result = 0.17M * 100;
            decimal result2 = 0.175M * 100;
            Console.WriteLine(GetPercent(result));
            Console.WriteLine(GetPercent(result2));
            Console.WriteLine(GetPercent(111111.1700032M));
        }

        private static string GetPercent(decimal result)
        {
            string ret = string.Format("{0:0.##}", result) + "%";
            return ret;
        }


        private static void GetDBDesign()
        {
            new GenerateWordDBDesign().Generate();
        }

        public static void teslaHistoryInvoiceimport()
        {
            //new TExcel().TestReadData();

            //string str = @"C:\Julius_J_Zhu\10Tesla\06test\35历史开票记录check\gd\导入";
            //new OutputInvoiceIssuedRecordImport().getFleName(str);

            //string str2018 = @"C:\Julius_J_Zhu\10Tesla\06test\35历史开票记录check\bd\2018格式";
            //new OutputInvoiceIssuedRecordImport().getFleName(str2018);

            //string fulName = @"C:\Julius_J_Zhu\10Tesla\06test\35历史开票记录check\bd\201607 BD Tax output.xlsx";
            //string Name = "201607 BD Tax output.xlsx";
            //new OutputInvoiceIssuedRecordImport().TestReadData(fulName, Name);


            //string fulName = @"C:\Julius_J_Zhu\10Tesla\06test\35历史开票记录check\gd\导入\2014-2015 GD tax output.xlsx";
            //string Name = "2014-2015 GD tax output.xlsx";
            //new OutputInvoiceIssuedRecordImport().TestReadData2014TO2015(fulName, Name);

            //string fulName = @"C:\Julius_J_Zhu\10Tesla\06test\35历史开票记录check\gd\导入\special\201801 GD Tax output.xls";
            //string Name = "201801 GD Tax output";
            //new OutputInvoiceIssuedRecordImport().TestReadDataGD201801(fulName, Name);

            //DateTime d = DateTime.FromOADate(41813);

            //new OutputInvoiceIssuedRecordImport().UpdateInfo();

            // 201803的数据
            new OutputInvoiceIssuedRecordImport().ExcuteImportFiles(@"C:\Julius_J_Zhu\10Tesla\06test\35历史开票记录check\Invoice 汇总\Invoice 汇总", "2018-03");
        }


        public static DateTime? ParseNullableDateTimeValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            else
            {
                DateTime temp = DateTime.MinValue;
                if (DateTime.TryParse(value, out temp))
                {
                    return temp;
                }
                else
                {
                    return null;
                }
            }
        }

        //参数value为要处理的浮点数，参数digit为要保留的小数点位数
        public static decimal Round(decimal value, int digit)
        {
            double vt = Math.Pow(10, digit);
            //1.乘以倍数 + 0.5
            decimal vx = value * (decimal)vt + 0.5M;
            //2.向下取整 
            decimal temp = Math.Floor(vx);
            //3.再除以倍数
            return (temp / (decimal)vt);
        }

        static void RuleNo()
        {
            string maxRuleNo = "RN11";
            string regex = @"^[a-zA-Z]*\d+$";

            string prefix = @"[^\d]*";

            string index = @"\d+";

            Match mat = Regex.Match(maxRuleNo, prefix);

            if (mat.Success)
            {
                string prefixStr = mat.Value;
            }

            Match mat2 = Regex.Match(maxRuleNo, index);

            if (mat.Success && mat2.Value != null)
            {
                string indexStr = mat2.Value.ToString();

                int tempNo = 0;
                var indexNo = int.TryParse(indexStr, out tempNo);


            }
        }

        static void testExcel()
        {
            //            结论：放弃，因为数据量大了，还是会报Out Of Memory错误的。
            //44.5M的文件是不会报Out of Memory错误的。
            //两个44.5M是会报错的Out of Memory错误。
            //用OleDb 读数据，是不会报Out of Memory错误

            DataSet ds = new DataSet();
            IExcelInfo dao = DALFactory.GetExcelInfoDAL();
            string strExcelPath = "./documents/8大数据测试44.5M.xlsx";
            using (var objConn = ConnectionFactory.GetExcelOleDbConnection(strExcelPath))
            {
                // 返回Excel的架构，包括各个sheet表的名称,类型，创建时间和修改时间等    
                ds = dao.GetDataSet(objConn, new List<string>() { "工作表1$", });
            }

            if (ds.Tables.Count > 0)
            {
                var rowsCount = ds.Tables[0].Rows.Count;
            }

        }


        static void MoneyParse()
        {
            decimal d = 999999999999999M;

            Console.WriteLine(InternationalNumericFormatter.FormatWithCulture("Ln", d, null, new System.Globalization.CultureInfo("zh-CHS")));

            Console.Read();
        }

        private int _columnIndex = -1;
        private void ResetColumnIndex()
        {
            this._columnIndex = -1;
        }

        private string GetNextColumn(List<string> row)
        {
            this._columnIndex++;
            if (row.Count > this._columnIndex && this._columnIndex >= 0)
            {
                return row[this._columnIndex];
            }

            return string.Empty;
        }


        private decimal? getNextColumnDecimal(List<string> row)
        {
            string ret = this.GetNextColumn(row);
            decimal temp = 0M;
            if (decimal.TryParse(ret, out temp))
            {
                return Math.Round(temp * 100) / 100;
            }

            return null;
        }

        private string getNextColumnString(List<string> row)
        {
            string ret = this.GetNextColumn(row);
            decimal temp = 0M;
            if (decimal.TryParse(ret, out temp))
            {

                decimal retDecimal = Math.Round(temp * 100) / 100;
                return retDecimal.ToString();
            }
            else
            {
                return ret;
            }
        }

        private static void TestJsonSerializeObject()
        {
            //MTest model = new MTest()
            //{
            //    VATType = "121212",
            //    Name = "名字",
            //};


            //string json = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }


        private static void TestExcel()
        {
            string json = "{\"DataBaseName\":\"TaxClient\",\"DataBaseType\":0,\"ServerAddress\":\"localhost\",\"ReadAccount\":\"\",\"WriteAccount\":\"\",\"TableList\":[{\"TableName\":\"C_UserHistoricalPassword\",\"TableNameCH\":\"\",\"TableDescrption\":null,\"PrimaryKey\":null,\"FieldList\":[{\"FieldFormat\":null,\"Index\":1,\"FieldNameCH\":\"\",\"FieldName\":\"UserID\",\"DataType\":\"bigint\",\"Length\":19,\"IsNullable\":false,\"PrimaryKeyIndex\":1,\"ForeignRelation\":null,\"IsUniqueIndex\":false,\"IndexNo\":1,\"IsAutoIncrement\":false,\"DefaultValue\":\"\",\"ValueConstraint\":null,\"ProjectSignificance\":null,\"DigitalLength\":0},{\"FieldFormat\":null,\"Index\":2,\"FieldNameCH\":\"\",\"FieldName\":\"Password\",\"DataType\":\"nvarchar\",\"Length\":50,\"IsNullable\":false,\"PrimaryKeyIndex\":0,\"ForeignRelation\":null,\"IsUniqueIndex\":false,\"IndexNo\":0,\"IsAutoIncrement\":false,\"DefaultValue\":\"\",\"ValueConstraint\":null,\"ProjectSignificance\":null,\"DigitalLength\":0},{\"FieldFormat\":null,\"Index\":3,\"FieldNameCH\":\"\",\"FieldName\":\"UpdateTime\",\"DataType\":\"datetime\",\"Length\":23,\"IsNullable\":false,\"PrimaryKeyIndex\":2,\"ForeignRelation\":null,\"IsUniqueIndex\":false,\"IndexNo\":0,\"IsAutoIncrement\":false,\"DefaultValue\":\"\",\"ValueConstraint\":null,\"ProjectSignificance\":null,\"DigitalLength\":3}]},{\"TableName\":\"A_RoleMenu\",\"TableNameCH\":\"\",\"TableDescrption\":null,\"PrimaryKey\":null,\"FieldList\":[{\"FieldFormat\":null,\"Index\":1,\"FieldNameCH\":\"\",\"FieldName\":\"RoleMenuID\",\"DataType\":\"uniqueidentifier\",\"Length\":16,\"IsNullable\":false,\"PrimaryKeyIndex\":1,\"ForeignRelation\":null,\"IsUniqueIndex\":false,\"IndexNo\":0,\"IsAutoIncrement\":false,\"DefaultValue\":\"\",\"ValueConstraint\":null,\"ProjectSignificance\":null,\"DigitalLength\":0},{\"FieldFormat\":null,\"Index\":2,\"FieldNameCH\":\"bigint\",\"FieldName\":\"RoleID\",\"DataType\":\"bigint\",\"Length\":19,\"IsNullable\":false,\"PrimaryKeyIndex\":0,\"ForeignRelation\":null,\"IsUniqueIndex\":false,\"IndexNo\":0,\"IsAutoIncrement\":false,\"DefaultValue\":\"\",\"ValueConstraint\":null,\"ProjectSignificance\":null,\"DigitalLength\":0},{\"FieldFormat\":null,\"Index\":3,\"FieldNameCH\":\"int\",\"FieldName\":\"MenuID\",\"DataType\":\"int\",\"Length\":10,\"IsNullable\":false,\"PrimaryKeyIndex\":0,\"ForeignRelation\":null,\"IsUniqueIndex\":false,\"IndexNo\":0,\"IsAutoIncrement\":false,\"DefaultValue\":\"\",\"ValueConstraint\":null,\"ProjectSignificance\":null,\"DigitalLength\":0},{\"FieldFormat\":null,\"Index\":4,\"FieldNameCH\":\"\",\"FieldName\":\"OrgID\",\"DataType\":\"uniqueidentifier\",\"Length\":16,\"IsNullable\":true,\"PrimaryKeyIndex\":0,\"ForeignRelation\":null,\"IsUniqueIndex\":false,\"IndexNo\":0,\"IsAutoIncrement\":false,\"DefaultValue\":\"\",\"ValueConstraint\":null,\"ProjectSignificance\":null,\"DigitalLength\":0}]}]}";
            MDataBaseDefine model = JsonConvert.DeserializeObject<MDataBaseDefine>(json);


        }

        private static void SaveFile(string str, string fileName)
        {
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


        public class MTest
        {
            [JsonProperty("vATType")]
            public string VATType
            {
                get;
                set;
            }

            public string Name
            {
                get;
                set;
            }
        }

        private static void Test()
        {

            // Excel 操作文档:http://www.cnblogs.com/wzxwhd/p/5888849.html
            string conn = "Server=CNSHASQLUWV040,1801;Database=TaxClient_temp2;User ID=sa;Password=P@ss1234;Trusted_Connection=false;MultipleActiveResultSets=true;";

            BGetSchema bll = new BGetSchema();
            var ret = bll.GenerateDataBaseDefine(conn);

            string json = JsonConvert.SerializeObject(ret);
        }
    }
}
