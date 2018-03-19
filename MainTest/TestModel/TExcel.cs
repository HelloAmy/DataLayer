using Help.DBAccessLayer.NPOIDAL;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainTest.TestModel
{
    public class TExcel
    {


        public void CreateBDHistorySQL()
        {
            DateTime from = new DateTime(2015, 12, 1);

            DateTime to = new DateTime(2018, 1, 1);

            DateTime forMonth = from;

            string str = @"C:\Julius_J_Zhu\10Tesla\06test\35历史开票记录check\VAT solution\VAT solution";

            while (forMonth != to)
            {
                string month = forMonth.ToString("yyyyMM");
                

                string fileName = month + " BD AX output";

                string fullName = str + @"\" + month + @"\" + fileName + ".xlsx";

                string period = forMonth.Year + "-Period " + forMonth.Month;

                TestGenerateSQL(fullName, fileName,  period);




                forMonth = forMonth.AddMonths(1);

                 
            }
        }



        public void TestGenerateSQL(string fullFilePath, string fileName, string period)
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
                    getStr(item[11]),
                     null,
                    getStr("机动车销售发票"),
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

            this.saveFile(saveSql, 0);

            int index = 1;

            while (saveSql.Count == 10000)
            {
                saveSql = sqlString.Skip(index * 10000).Take(10000).ToList<string>();
                this.saveFileName(fileName, saveSql, index);
                index++;
            }

        }


        private void saveFileName(string name, List<string> sqlString, int index)
        {
            string str = string.Join("", sqlString.ToArray());

            var fileName = index + name + "SQL" + DateTime.Now.ToString("MMddHHmmss") + ".txt";
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


        /// <summary>
        /// 测试读取数据的大小An unhandled exception of type 'System.OutOfMemoryException' occurred in mscorlib.dll
        /// </summary>
        public void TestReadData()
        {
            NPOIDAO dao = new NPOIDAO();


            string fullFilePath = @"C:\Julius_J_Zhu\10Tesla\06test\35历史开票记录check\180306导入数据到数据库\20180116 BD Y2017 开票清单-to Celine.xlsx";

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

            this.saveFile(saveSql, 0);

            int index = 1;

            while (saveSql.Count == 10000)
            {
                saveSql = sqlString.Skip(index * 10000).Take(10000).ToList<string>();
                this.saveFile(saveSql, index);
                index++;
            }

        }

        private void saveFile(List<string> sqlString, int index)
        {
            string str = string.Join("", sqlString.ToArray());

            var fileName = index + "SQL" + DateTime.Now.ToString("MMddHHmmss") + ".txt";
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
                return value;
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
                return "N'" + value + "'";
            }
        }

        public void ReadVoucherDataEEplus()
        {
            string fullFilePath = "C:/Julius_J_Zhu/09DataLayer/MainTest/documents/8大数据测试44.5M.xlsx";

            List<string> exclueFormats = new List<string>() { "General" };

            List<List<string>> dataList = new List<List<string>>();

            StringBuilder specialsb = new StringBuilder();
            using (FileStream fs = File.Open(fullFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (ExcelPackage package = new ExcelPackage(fs))
                {
                    ExcelWorksheet sheet = package.Workbook.Worksheets["report"];
                    for (int m = sheet.Dimension.Start.Row, n = sheet.Dimension.End.Row; m <= n; m++)
                    {
                        List<string> row = new List<string>();
                        for (int j = sheet.Dimension.Start.Column, k = sheet.Dimension.End.Column; j <= k; j++)
                        {
                            var cell = sheet.Cells[m, j];
                            object value = cell.Value;

                            string tempValue = null;


                            if (cell.Value != null)
                            {
                                string format = cell.Style.Numberformat.Format;
                                if (!string.IsNullOrEmpty(format) && !exclueFormats.Contains(format))
                                {
                                    if (this.isDate(format))
                                    {
                                        DateTime temp = cell.GetValue<DateTime>();
                                        tempValue = temp.ToString("yyyy-MM-dd");
                                    }
                                    else if (this.isDateTime(format))
                                    {
                                        DateTime temp = cell.GetValue<DateTime>();
                                        tempValue = temp.ToString("yyyy-MM-dd HH:mm:ss");
                                    }
                                    else
                                    {
                                        tempValue = cell.Value.ToString();
                                    }
                                }
                                else
                                {
                                    tempValue = cell.Value.ToString();
                                }

                                specialsb.Append("Format:").Append(format).Append(";")
                                .Append("Text:").Append(cell.Text).Append(";")
                                .Append("Value:").Append(cell.Value).Append(";")
                                .Append("tempValue:").Append(tempValue).Append(";")
                                .AppendLine()
                               ;
                            }


                            row.Add(tempValue);
                        }

                        // 如果全部是空行，则不要
                        if (row.Any(sa => !string.IsNullOrEmpty(sa)))
                        {
                            row[0] = m + "=>" + row[0];
                            dataList.Add(row);
                        }
                    }
                }
            }


            StringBuilder str = new StringBuilder();

            //str.Append(specialsb.ToString()).AppendLine();
            int index = 0;
            foreach (var item in dataList)
            {
                StringBuilder temp = new StringBuilder();

                temp.Append("Index:" + index).Append(":");
                foreach (var t in item)
                {
                    temp.Append(t).Append(";");
                }
                str.Append(temp).AppendLine();

                index++;
            }


            var fileName = "aaa" + DateTime.Now.ToString("MMddHHmmss") + ".txt";
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

            Console.WriteLine("完毕...");
            Console.Read();
        }


        private bool isDate(string format)
        {
            List<string> dateFormatList = new List<string>()
            {
                "[$-10409]m/d/yyyy",
                "[$-10409]mm-dd-yy",
                "[$-10409]d-mmm-yy",
                "[$-10409]d-mmm",
                "[$-10409]mmm-yy",
                "[$-10409]mmm-yy",
            };

            return dateFormatList.Contains(format);
        }

        private bool isDateTime(string format)
        {
            List<string> dateTimeFormatList = new List<string>()
            {
                "[$-10409]h:mm AM/PM",
                "[$-10409]h:mm:ss AM/PM",
                "[$-10409]h:mm",
                "[$-10409]d-mmm",
                "[$-10409]h:mm:ss",
                "[$-10409]mm:ss",
                "[$-10409][h]:mm:ss",
                "[$-10409]mmss.0",
                @"[$-10409]m/d/yyyy\ h:mm:ss\ AM/PM",
            };

            return dateTimeFormatList.Contains(format);
        }

        public void ReadDataEEplus()
        {
            string fullFilePath = "./documents/1多项目多年份Period测试.xlsx";

            List<string> exclueFormats = new List<string>() { "General" };

            List<List<string>> dataList = new List<List<string>>();

            StringBuilder specialsb = new StringBuilder();
            using (FileStream fs = File.Open(fullFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (ExcelPackage package = new ExcelPackage(fs))
                {
                    for (int i = 1; i <= package.Workbook.Worksheets.Count; ++i)
                    {
                        ExcelWorksheet sheet = package.Workbook.Worksheets[i];
                        for (int m = sheet.Dimension.Start.Row, n = sheet.Dimension.End.Row; m <= n; m++)
                        {
                            List<string> row = new List<string>();

                            for (int j = sheet.Dimension.Start.Column, k = sheet.Dimension.End.Column; j <= k; j++)
                            {
                                var cell = sheet.Cells[m, j];
                                object value = cell.Value;

                                string tempValue = null;

                                if (!string.IsNullOrEmpty(cell.Style.Numberformat.Format) && !exclueFormats.Contains(cell.Style.Numberformat.Format))
                                {
                                    if (cell.Style.Numberformat.Format == "[$-10409]m/d/yyyy")
                                    {
                                        tempValue = cell.Text;
                                    }
                                    else if (cell.Style.Numberformat.Format == @"[$-10409]m/d/yyyy\ h:mm:ss\ AM/PM")
                                    {
                                        DateTime temp = cell.GetValue<DateTime>();
                                        tempValue = temp.ToString("yyyy-MM-dd HH:mm:ss");
                                    }
                                    else if (cell.Style.Numberformat.Format == @"[$-10409]#,##0.00;\(#,##0.00\)")
                                    {
                                        if (cell.Value != null)
                                        {
                                            tempValue = cell.Value.ToString();
                                        }
                                    }
                                    else
                                    {
                                        if (cell.Value != null)
                                        {
                                            tempValue = cell.Value.ToString();
                                        }
                                    }

                                    specialsb.Append("Format:").Append(cell.Style.Numberformat.Format).Append(";")
                                        .Append("Text:").Append(cell.Text).Append(";")
                                        .Append("Value:").Append(cell.Value).Append(";")
                                        .Append("tempValue:").Append(tempValue).Append(";")
                                        .AppendLine()
                                       ;
                                }
                                else
                                {
                                    if (value != null)
                                    {
                                        tempValue = value.ToString();
                                    }
                                }

                                row.Add(tempValue);
                            }



                            dataList.Add(row);
                        }


                        //for (int j = sheet.Dimension.Start.Column, k = sheet.Dimension.End.Column; j <= k; j++)
                        //{
                        //    for (int m = sheet.Dimension.Start.Row, n = sheet.Dimension.End.Row; m <= n; m++)
                        //    {
                        //        string str = sheet.Cells[m, j].Value.ToString();
                        //        if (str != null)
                        //        {
                        //            // do something
                        //        }
                        //    }
                        //}
                    }
                }
            }


            StringBuilder str = new StringBuilder();

            //str.Append(specialsb.ToString()).AppendLine();
            int index = 0;
            foreach (var item in dataList)
            {
                StringBuilder temp = new StringBuilder();

                temp.Append("Index:" + index).Append(":");
                foreach (var t in item)
                {
                    temp.Append(t).Append(";");
                }
                str.Append(temp).AppendLine();

                index++;
            }


            var fileName = "aaa" + DateTime.Now.ToString("MMddHHmmss") + ".txt";
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

            Console.WriteLine("完毕...");
            Console.Read();
        }


        public static string ToName(int index)
        {
            if (index < 0) { throw new Exception("invalid parameter"); }

            List<string> chars = new List<string>();
            do
            {
                if (chars.Count > 0) index--;
                chars.Insert(0, ((char)(index % 26 + (int)'A')).ToString());
                index = (int)((index - index % 26) / 26);
            } while (index > 0);

            return String.Join(string.Empty, chars.ToArray());
        }

        public void ReadOfficeData()
        {
            Stopwatch totalWatch = new Stopwatch();
            totalWatch.Start();
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();

            Microsoft.Office.Interop.Excel.Workbook workbook1 = app.Workbooks._Open(@"C:\Julius_J_Zhu\09DataLayer\MainTest\documents\4Period测试14841.xlsx", Type.Missing, Type.Missing, Type.Missing, Type.Missing
                , Type.Missing, Type.Missing, Type.Missing, Type.Missing
                , Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            StringBuilder str = new StringBuilder();

            List<List<string>> data = new List<List<string>>();
            if (workbook1.Worksheets.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Worksheet sheet = workbook1.Worksheets.get_Item(1);

                int rowsint = sheet.UsedRange.Cells.Rows.Count; //得到行数 
                int columnsint = sheet.UsedRange.Cells.Columns.Count;//得到列数 

                int startColumnIndex = sheet.UsedRange.Cells.Column;
                string startColumn = ToName(sheet.UsedRange.Cells.Column);

                string endColumn = ToName(startColumnIndex + columnsint - 1);

                // 不包括表头
                string[,] arry = new string[rowsint - 1, columnsint];


                List<Microsoft.Office.Interop.Excel.Range> columnList = new List<Microsoft.Office.Interop.Excel.Range>();


                for (var i = startColumnIndex; i < startColumnIndex + columnsint; i++)
                {
                    string columnName = ToName(i);

                    Microsoft.Office.Interop.Excel.Range rng1 = sheet.Cells.get_Range(columnName + "7", columnName + rowsint);

                    columnList.Add(rng1);
                }

                for (int i = 1; i <= rowsint - 2; i++)
                {

                    List<string> tempList = new List<string>();
                    Stopwatch itemWatch = new Stopwatch();
                    itemWatch.Start();
                    for (int j = 0; j < columnsint; j++)
                    {
                        var range = columnList[j];

                        object[,] tempArray = (object[,])range.Value2;
                        var value = tempArray[i, 1] != null ? tempArray[i, 1].ToString() : null;
                        tempList.Add(value);
                    }

                    itemWatch.Stop();

                    Console.WriteLine("行" + i + "总毫秒：" + itemWatch.ElapsedMilliseconds);
                    data.Add(tempList);
                }


                //str.Append(specialsb.ToString()).AppendLine();

            }

            totalWatch.Stop();

            Console.WriteLine("总毫秒：" + totalWatch.ElapsedMilliseconds);

            int index = 0;
            foreach (var item in data)
            {
                StringBuilder temp = new StringBuilder();

                temp.Append("Index:" + index).Append(":");
                foreach (var t in item)
                {
                    temp.Append(t).Append(";");
                }
                str.Append(temp).AppendLine();

                index++;
            }

            var fileName = "Office" + DateTime.Now.ToString("MMddHHmmss") + ".txt";
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

            Console.WriteLine("完毕...");
            Console.Read();
        }


        public class DataModel
        {
            public int Sheet
            {
                get;
                set;
            }

            public List<List<string>> Data
            {
                get;
                set;
            }
        }

        public void ReadOfficeData2()
        {
            Stopwatch totalWatch = new Stopwatch();
            totalWatch.Start();
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();

            Microsoft.Office.Interop.Excel.Workbook workbook1 = app.Workbooks._Open(@"C:\Julius_J_Zhu\09DataLayer\MainTest\documents\4Period测试14841.xlsx", Type.Missing, Type.Missing, Type.Missing, Type.Missing
                , Type.Missing, Type.Missing, Type.Missing, Type.Missing
                , Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            StringBuilder str = new StringBuilder();

            List<DataModel> dataModelList = new List<DataModel>();
            List<string> DateTimeList = new List<string>()
            {
                "Transaction Date",
                "Posting Date Time",
            };

            Dictionary<string, int> dateTimeMap = new Dictionary<string, int>();

            for (int shet = 1; shet <= workbook1.Worksheets.Count; shet++)
            {
                List<List<string>> data = new List<List<string>>();
                //DataModel dataModel = new DataModel()
                //{
                //    Sheet = shet,
                //    Data = data
                //};

                //dataModelList.Add(dataModel);

                Microsoft.Office.Interop.Excel.Worksheet sheet = workbook1.Worksheets.get_Item(shet);

                int rowsint = sheet.UsedRange.Cells.Rows.Count; //得到行数 
                int columnsint = sheet.UsedRange.Cells.Columns.Count;//得到列数 

                int startColumnIndex = sheet.UsedRange.Cells.Column;
                string startColumn = ToName(startColumnIndex);

                string endColumn = ToName(startColumnIndex + columnsint - 1);

                // 不包括表头
                string[,] arry = new string[rowsint - 1, columnsint];


                List<Microsoft.Office.Interop.Excel.Range> columnList = new List<Microsoft.Office.Interop.Excel.Range>();

                List<object[,]> columnDataList = new List<object[,]>();

                // 表头
                Microsoft.Office.Interop.Excel.Range headerRange = sheet.UsedRange.Cells.get_Range(startColumn + "1", endColumn + "1");

                object[,] hearderArray = (object[,])headerRange.Value2;

                List<string> header = new List<string>();

                if (hearderArray != null)
                {
                    for (var i = startColumnIndex; i < startColumnIndex + columnsint; i++)
                    {
                        var value = hearderArray[1, i] != null ? hearderArray[1, i].ToString() : null;
                        header.Add(value);
                    }
                }

                for (var i = startColumnIndex; i < startColumnIndex + columnsint; i++)
                {
                    Stopwatch itemWatch = new Stopwatch();
                    itemWatch.Start();
                    string columnName = ToName(i);

                    Microsoft.Office.Interop.Excel.Range rng1 = sheet.Cells.get_Range(columnName + "2", columnName + rowsint);

                    //columnList.Add(rng1);

                    object[,] tempArray = (object[,])rng1.Value2;

                    columnDataList.Add(tempArray);

                    itemWatch.Stop();

                    Console.WriteLine("列" + i + "总毫秒：" + itemWatch.ElapsedMilliseconds + ";" + rng1.EntireColumn.NumberFormat);
                }

                for (int i = 1; i <= rowsint - 2; i++)
                {
                    List<string> tempList = new List<string>();

                    for (int j = 0; j < columnDataList.Count; j++)
                    {
                        object[,] tempArray = columnDataList[j];
                        var value = tempArray[i, 1] != null ? tempArray[i, 1].ToString() : null;
                        tempList.Add(value);
                    }

                    Console.WriteLine("[" + shet + "," + i + "]:" + string.Join(",", tempList.ToArray()));

                    data.Add(tempList);
                }

                int index = 0;
                foreach (var item in data)
                {
                    StringBuilder temp = new StringBuilder();

                    temp.Append("Sheet:").Append(shet).Append(",");
                    temp.Append("Index:" + index).Append(":");
                    foreach (var t in item)
                    {
                        temp.Append(t).Append(";");
                    }
                    str.Append(temp).AppendLine();

                    index++;
                }

                var fileName = shet + "Office" + DateTime.Now.ToString("MMddHHmmss") + ".txt";
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

                str = new StringBuilder();
            }

            totalWatch.Stop();

            Console.WriteLine("总毫秒：" + totalWatch.ElapsedMilliseconds);

            Console.WriteLine("完毕...");
            Console.Read();
        }


        public void ReadOfficeDataTime()
        {
            Stopwatch totalWatch = new Stopwatch();
            totalWatch.Start();
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();

            Microsoft.Office.Interop.Excel.Workbook workbook1 = app.Workbooks._Open(@"C:\Julius_J_Zhu\10Tesla\06test\01分录的导入分发\1多项目多年份Period测试.xlsx", Type.Missing, Type.Missing, Type.Missing, Type.Missing
                , Type.Missing, Type.Missing, Type.Missing, Type.Missing
                , Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            StringBuilder str = new StringBuilder();

            List<DataModel> dataModelList = new List<DataModel>();
            List<string> DateTimeList = new List<string>()
            {
                "Transaction Date",
                "Posting Date Time",
            };

            List<int> dateTimeColumnIndexList = new List<int>();

            for (int shet = 1; shet <= workbook1.Worksheets.Count; shet++)
            {
                if (shet != 1)
                {
                    continue;
                }

                List<List<string>> data = new List<List<string>>();
                //DataModel dataModel = new DataModel()
                //{
                //    Sheet = shet,
                //    Data = data
                //};

                //dataModelList.Add(dataModel);

                //Microsoft.Office.Interop.Excel.Worksheet sheet = workbook1.Worksheets.get_Item(shet);

                Microsoft.Office.Interop.Excel.Worksheet sheet = workbook1.Worksheets.get_Item("report");

                int rowsint = sheet.UsedRange.Cells.Rows.Count; //得到行数 
                int columnsint = sheet.UsedRange.Cells.Columns.Count;//得到列数 

                int startColumnIndex = sheet.UsedRange.Cells.Column - 1;

                int endColumnIndex = startColumnIndex + columnsint - 1;

                int startRowIndex = sheet.UsedRange.Cells.Row;
                string startColumn = ToName(startColumnIndex);

                string endColumn = ToName(endColumnIndex);

                // 不包括表头
                string[,] arry = new string[rowsint - 1, columnsint];


                List<Microsoft.Office.Interop.Excel.Range> columnList = new List<Microsoft.Office.Interop.Excel.Range>();

                List<object[,]> columnDataList = new List<object[,]>();

                // 表头
                Microsoft.Office.Interop.Excel.Range headerRange = sheet.UsedRange.Rows[6];

                object[,] hearderArray = (object[,])headerRange.Value2;

                List<string> header = new List<string>();

                if (hearderArray != null)
                {
                    for (var i = startColumnIndex; i <= endColumnIndex; i++)
                    {
                        var value = hearderArray[1, i] != null ? hearderArray[1, i].ToString() : null;

                        if (DateTimeList.Any(sa => sa == value))
                        {
                            dateTimeColumnIndexList.Add(i - startColumnIndex);
                        }

                        header.Add(value);
                    }
                }

                for (var i = startColumnIndex; i <= endColumnIndex; i++)
                {
                    Stopwatch itemWatch = new Stopwatch();
                    itemWatch.Start();
                    string columnName = ToName(i);

                    Microsoft.Office.Interop.Excel.Range rng1 = sheet.Cells.get_Range(columnName + "7", columnName + rowsint);

                    //columnList.Add(rng1);

                    object[,] tempArray = (object[,])rng1.Value2;

                    columnDataList.Add(tempArray);

                    itemWatch.Stop();

                    Console.WriteLine("列" + i + "总毫秒：" + itemWatch.ElapsedMilliseconds + ";行数:" + tempArray.Length);
                }

                for (int i = 1; i <= rowsint - 7; i++)
                {
                    List<string> tempList = new List<string>();

                    for (int j = 0; j < columnDataList.Count; j++)
                    {
                        object[,] tempArray = columnDataList[j];
                        var value = tempArray[i, 1] != null ? tempArray[i, 1].ToString() : null;
                        if (dateTimeColumnIndexList.Any(sa => sa == j))
                        {
                            double d = 0;
                            if (!string.IsNullOrEmpty(value) && double.TryParse(value, out d))
                            {
                                DateTime t = DateTime.FromOADate(d);
                                tempList.Add(t.ToString("yyyy-MM-dd HH:mm:ss"));
                            }
                            else
                            {
                                tempList.Add(null);
                            }
                        }
                        else
                        {
                            tempList.Add(value);
                        }

                    }


                    //Console.WriteLine("[" + shet + "," + i + "]:" + string.Join(",", tempList.ToArray()));

                    data.Add(tempList);
                }

                int index = 0;
                foreach (var item in data)
                {
                    StringBuilder temp = new StringBuilder();

                    temp.Append("Sheet:").Append(shet).Append(",");
                    temp.Append("Index:" + index).Append(":");
                    foreach (var t in item)
                    {
                        temp.Append(t).Append(";");
                    }
                    str.Append(temp).AppendLine();

                    index++;
                }

                var fileName = shet + "Office" + DateTime.Now.ToString("MMddHHmmss") + ".txt";
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

                str = new StringBuilder();
            }

            totalWatch.Stop();

            Console.WriteLine("总毫秒：" + totalWatch.ElapsedMilliseconds);

            Console.WriteLine("完毕...");
            Console.Read();
        }
    }
}
