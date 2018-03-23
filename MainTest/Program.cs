﻿using Help.DBAccessLayer.Business;
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

namespace MainTest
{
    class Program
    {
        static X509Certificate serverCertificate = null;

        static void Main(string[] args)
        {
            GetDBDesign();
            Console.WriteLine("结束");
            Console.Read();
            //new TExcel().ReadVoucherDataEEplus();
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
            MTest model = new MTest()
            {
                VATType = "121212",
                Name = "名字",
            };


            string json = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }


        private static void TestExcel()
        {
            string json = "{\"DataBaseName\":\"TaxClient\",\"DataBaseType\":0,\"ServerAddress\":\"localhost\",\"ReadAccount\":\"\",\"WriteAccount\":\"\",\"TableList\":[{\"TableName\":\"C_UserHistoricalPassword\",\"TableNameCH\":\"\",\"TableDescrption\":null,\"PrimaryKey\":null,\"FieldList\":[{\"FieldFormat\":null,\"Index\":1,\"FieldNameCH\":\"\",\"FieldName\":\"UserID\",\"DataType\":\"bigint\",\"Length\":19,\"IsNullable\":false,\"PrimaryKeyIndex\":1,\"ForeignRelation\":null,\"IsUniqueIndex\":false,\"IndexNo\":1,\"IsAutoIncrement\":false,\"DefaultValue\":\"\",\"ValueConstraint\":null,\"ProjectSignificance\":null,\"DigitalLength\":0},{\"FieldFormat\":null,\"Index\":2,\"FieldNameCH\":\"\",\"FieldName\":\"Password\",\"DataType\":\"nvarchar\",\"Length\":50,\"IsNullable\":false,\"PrimaryKeyIndex\":0,\"ForeignRelation\":null,\"IsUniqueIndex\":false,\"IndexNo\":0,\"IsAutoIncrement\":false,\"DefaultValue\":\"\",\"ValueConstraint\":null,\"ProjectSignificance\":null,\"DigitalLength\":0},{\"FieldFormat\":null,\"Index\":3,\"FieldNameCH\":\"\",\"FieldName\":\"UpdateTime\",\"DataType\":\"datetime\",\"Length\":23,\"IsNullable\":false,\"PrimaryKeyIndex\":2,\"ForeignRelation\":null,\"IsUniqueIndex\":false,\"IndexNo\":0,\"IsAutoIncrement\":false,\"DefaultValue\":\"\",\"ValueConstraint\":null,\"ProjectSignificance\":null,\"DigitalLength\":3}]},{\"TableName\":\"A_RoleMenu\",\"TableNameCH\":\"\",\"TableDescrption\":null,\"PrimaryKey\":null,\"FieldList\":[{\"FieldFormat\":null,\"Index\":1,\"FieldNameCH\":\"\",\"FieldName\":\"RoleMenuID\",\"DataType\":\"uniqueidentifier\",\"Length\":16,\"IsNullable\":false,\"PrimaryKeyIndex\":1,\"ForeignRelation\":null,\"IsUniqueIndex\":false,\"IndexNo\":0,\"IsAutoIncrement\":false,\"DefaultValue\":\"\",\"ValueConstraint\":null,\"ProjectSignificance\":null,\"DigitalLength\":0},{\"FieldFormat\":null,\"Index\":2,\"FieldNameCH\":\"bigint\",\"FieldName\":\"RoleID\",\"DataType\":\"bigint\",\"Length\":19,\"IsNullable\":false,\"PrimaryKeyIndex\":0,\"ForeignRelation\":null,\"IsUniqueIndex\":false,\"IndexNo\":0,\"IsAutoIncrement\":false,\"DefaultValue\":\"\",\"ValueConstraint\":null,\"ProjectSignificance\":null,\"DigitalLength\":0},{\"FieldFormat\":null,\"Index\":3,\"FieldNameCH\":\"int\",\"FieldName\":\"MenuID\",\"DataType\":\"int\",\"Length\":10,\"IsNullable\":false,\"PrimaryKeyIndex\":0,\"ForeignRelation\":null,\"IsUniqueIndex\":false,\"IndexNo\":0,\"IsAutoIncrement\":false,\"DefaultValue\":\"\",\"ValueConstraint\":null,\"ProjectSignificance\":null,\"DigitalLength\":0},{\"FieldFormat\":null,\"Index\":4,\"FieldNameCH\":\"\",\"FieldName\":\"OrgID\",\"DataType\":\"uniqueidentifier\",\"Length\":16,\"IsNullable\":true,\"PrimaryKeyIndex\":0,\"ForeignRelation\":null,\"IsUniqueIndex\":false,\"IndexNo\":0,\"IsAutoIncrement\":false,\"DefaultValue\":\"\",\"ValueConstraint\":null,\"ProjectSignificance\":null,\"DigitalLength\":0}]}]}";
            MDataBaseDefine model = JsonConvert.DeserializeObject<MDataBaseDefine>(json);


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
