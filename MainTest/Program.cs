using Help.DBAccessLayer.Business;
using Help.DBAccessLayer.Model;
using Help.DBAccessLayer.Model.SqlGenerator;
using Help.DBAccessLayer.NOPI;
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

namespace MainTest
{
    class Program
    {
        static void Main(string[] args)
        {

            Program p = new Program();
            p.ImportDataParse();
            //DGenerateDatabaseExcel bll = new DGenerateDatabaseExcel();
            //bll.SplitSheetXlsx("./documents/WP03.xlsx", "./documents/");
            //bll.ReadXlsx();
            //bll.SplitSheetsXls();
            //bll.SplitSheetsXls("./documents/万能模板.xls", "./documents/");
            //bll.SplitSheets("./documents/权限UI.xlsx", "./documents/");
            //TestExcel();
        }


        public void ImportDataParse()
        {
            DataImportModel dataImportModel;

            string fileFullPath = "./documents/ShipmentList.xlsx";
            using (FileStream fs = File.Open(fileFullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                dataImportModel = NPOIHelper.RenderTBDataTableFromExcel(fs, 0, 3, true, 1000);
                dataImportModel.selectedSheetIndex = 0;
            }

            List<OutputInvoiceImportShipmentDto> list = new List<OutputInvoiceImportShipmentDto>();

            try
            {

                if (dataImportModel.dataList != null && dataImportModel.dataList.Count > 0)
                {
                    for (int row = 3; row < dataImportModel.dataList.Count; row++)
                    {
                        OutputInvoiceImportShipmentDto model = new OutputInvoiceImportShipmentDto();
                        model.ID = string.Empty;
                        var rowData = dataImportModel.dataList[row];
                        this.ResetColumnIndex();
                        model.PGIweek = this.GetNextColumn(rowData);
                        model.Company = this.GetNextColumn(rowData);
                        model.CompanyVloolup = this.GetNextColumn(rowData);
                        model.ConfirmationNumber = this.GetNextColumn(rowData);
                        model.VIN = this.GetNextColumn(rowData);
                        model.CarType = this.GetNextColumn(rowData);
                        model.IsMarketingCar = this.GetNextColumn(rowData);
                        model.TaxableSubtotal = this.getNextColumnDecimal(rowData);
                        model.VehiclePriceafterDiscounts = this.getNextColumnDecimal(rowData);
                        model.VAT = this.getNextColumnDecimal(rowData);
                        model.Revenue = this.getNextColumnDecimal(rowData);
                        model.Discounts = this.GetNextColumn(rowData);
                        model.Subsidy = this.GetNextColumn(rowData);
                        model.Cost = this.getNextColumnDecimal(rowData);
                        model.GDRevenue = this.getNextColumnString(rowData);
                        model.GDVAT = this.getNextColumnString(rowData);
                        model.GDCost = this.getNextColumnString(rowData);
                        model.Inventory = this.getNextColumnDecimal(rowData);
                        model.OrginalbeforeIntercompany = this.GetNextColumn(rowData);
                        model.Notes = this.GetNextColumn(rowData);

                        string result = JsonConvert.SerializeObject(model);
                    }

                }
            }
            catch (Exception ex)
            {

            }
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
