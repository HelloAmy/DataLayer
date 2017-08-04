using Help.DBAccessLayer.Business;
using Help.DBAccessLayer.Model.SqlGenerator;
using Help.DBAccessLayer.NOPI;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace MainTest
{
    class Program
    {
        static void Main(string[] args)
        {
            DGenerateDatabaseExcel bll = new DGenerateDatabaseExcel();

            //bll.splitSheets();
            //bll.SplitSheetsXls();
            //bll.SplitSheetsXls("./documents/万能模板.xls", "./documents/");
            //bll.SplitSheets("./documents/权限UI.xlsx", "./documents/");
            //TestExcel();
        }


        public void SplitSheets()
        {
            Excel.Application app = new Excel.Application();

            Excel.Workbook workbook1 = app.Workbooks._Open("./documents/万能模板.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing
                , Type.Missing, Type.Missing, Type.Missing, Type.Missing
                , Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            foreach (var sheet in workbook1.Sheets)
            {
                string name = "./documents/万能模板" + Guid.NewGuid().ToString() + ".xls";
                Excel.Workbook workbook2 = app.Workbooks.Open(name, Type.Missing, Type.Missing, Type.Missing, Type.Missing
               , Type.Missing, Type.Missing, Type.Missing, Type.Missing
               , Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                //Excel.Workbook workbook2 = new Excel.Workbook();
                //workbook2.Worksheets
            }



            Excel.Worksheet sheet1 = workbook1.Worksheets["Sheet1"] as Excel.Worksheet;
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

            DGenerateDatabaseExcel excelbll = new DGenerateDatabaseExcel();
            excelbll.GenerateDatabaseExcel(model);
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
