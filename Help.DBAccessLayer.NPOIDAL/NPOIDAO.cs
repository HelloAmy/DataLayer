using NPOI.HSSF.UserModel;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help.DBAccessLayer.NPOIDAL
{
    public class NPOIDAO
    {
        public List<List<string>> getData(Stream ExcelFileStream, int SheetIndex)
        {
            List<List<string>> retData = new List<List<string>>();
            IWorkbook workbook = WorkbookFactory.Create(ExcelFileStream);

            int count = workbook.NumberOfSheets;
            var sheet = workbook.GetSheetAt(SheetIndex);

            try
            {
                XSSFFormulaEvaluator eva = new XSSFFormulaEvaluator(workbook);
                var numberOfSheets = workbook.NumberOfSheets;

                //判断文件是否为空
                if (sheet.PhysicalNumberOfRows == 0)
                {
                    return retData;
                }
                else
                {
                    int cellCount = 0;

                    for (int i = 0; i <= sheet.LastRowNum; i++)
                    {
                        var row = sheet.GetRow(i);
                        if (row != null && row.Cells.Count > cellCount)
                        {
                            cellCount = row.LastCellNum;
                        }
                    }

                    var rowList = new List<string>();


                    int RowStart = sheet.FirstRowNum;
                    for (int i = RowStart; i <= sheet.LastRowNum; i++)
                    {

                        var row = sheet.GetRow(i);
                        if (row != null)
                        {
                            rowList = GetOutputInvoiceRowValueList(eva, row, cellCount);

                            retData.Add(rowList);

                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                ExcelFileStream.Close();
                workbook = null;
            }


            return retData;
        }


        public static bool AppendExcel(DataTable dt, string fileName)
        {
            bool result = true;
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);//读
            //POIFSFileSystem ps = new POIFSFileSystem(fs);
            IWorkbook workbook = new XSSFWorkbook(fs);
            ISheet sheet = workbook.GetSheetAt(0);//获取工作表

            FileStream fsAppend = new FileStream(fileName, FileMode.Open, FileAccess.Write, FileShare.ReadWrite);//读
            var lastRow = sheet.LastRowNum;
            var _doubleCellStyle = workbook.CreateCellStyle();
            _doubleCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.00");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row = sheet.CreateRow((lastRow + i + 1));
                var dataRow = dt.Rows[i];
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    var cellValue = dataRow[j];
                    string value = string.Empty;
                    if (cellValue is decimal)
                    {
                        var temp = Convert.ToDouble(dataRow[j]);
                        // value = temp.ToString("#0.##");
                        var cell = row.CreateCell(j);
                        cell.SetCellType(CellType.Numeric);
                        cell.SetCellValue(temp);
                        //transfer
                        row.Cells[j].CellStyle = _doubleCellStyle;
                    }
                    else if (cellValue is DateTime)
                    {
                        value = dataRow[j] == null ? "" : Convert.ToDateTime(dataRow[j]).ToString("yyyy-MM-dd");
                        row.CreateCell(j).SetCellValue(value);
                    }
                    else
                    {
                        value = dataRow[j] == null ? "" : dataRow[j].ToString();
                        row.CreateCell(j).SetCellValue(value);

                    }
                }
            }

            //fs.Write();
            fsAppend.Flush();
            workbook.Write(fsAppend);//写入文件
            workbook = null;
            fsAppend.Close();
            return result;
        }

        public static void WriteExcel(DataTable dt, string fileName, List<string> headers = null)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = "temp";
            }

            IWorkbook book = new XSSFWorkbook();
            ISheet sheet = book.CreateSheet("Sheet1");
            IRow rowTitle = sheet.CreateRow(0);

            #region 设置列头
            var columnNumber = 0;
            if (headers != null)
            {
                columnNumber = headers.Count;
                for (int i = 0; i < headers.Count; i++)
                {
                    rowTitle.CreateCell(i).SetCellValue(headers[i]);
                    sheet.AutoSizeColumn(i);//i：根据标题的个数设置自动列宽  
                }
            }
            else
            {
                columnNumber = dt.Columns.Count;
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    var val = dt.Columns[i] == null ? "" : dt.Columns[i].ToString();
                    rowTitle.CreateCell(i).SetCellValue(val);
                    sheet.AutoSizeColumn(i);//i：根据标题的个数设置自动列宽  
                }
            }
            #endregion

            var _doubleCellStyle = book.CreateCellStyle();
            _doubleCellStyle.DataFormat = book.CreateDataFormat().GetFormat("#,##0.00");
            var VerificationResultColumnCount = -1;
            var CommentsColumnCount = -1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row = sheet.CreateRow(i + 1);
                var dataRow = dt.Rows[i];
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    var cellValue = dataRow[j];
                    string value = string.Empty;
                    if (cellValue is decimal)
                    {
                        var temp = Convert.ToDouble(dataRow[j]);
                        // value = temp.ToString("#0.##");
                        var cell = row.CreateCell(j);
                        cell.SetCellType(CellType.Numeric);
                        cell.SetCellValue(temp);
                        //transfer
                        row.Cells[j].CellStyle = _doubleCellStyle;
                    }
                    else if (cellValue is DateTime)
                    {
                        value = dataRow[j] == null ? "" : Convert.ToDateTime(dataRow[j]).ToString("yyyy-MM-dd");
                        row.CreateCell(j).SetCellValue(value);
                    }
                    else
                    {
                        value = dataRow[j] == null ? "" : dataRow[j].ToString();
                        row.CreateCell(j).SetCellValue(value);
                        var cs = book.CreateCellStyle();
                        if (dt.Columns[j].ColumnName == "VerificationResult")
                        {
                            cs.WrapText = true;
                            row.Cells[j].CellStyle = cs;
                            VerificationResultColumnCount = j;
                        }
                        if (dt.Columns[j].ColumnName == "Comments")
                        {
                            CommentsColumnCount = j;
                        }

                    }
                }
            }

            #region 设置列宽
            for (int i = 0; i < columnNumber; i++)
            {
                sheet.AutoSizeColumn(i);//i：根据标题的个数设置自动列宽  
            }

            if (VerificationResultColumnCount > -1)
            {
                sheet.SetColumnWidth(VerificationResultColumnCount, 55 * 256);
            }

            if (CommentsColumnCount > -1)
            {
                sheet.SetColumnWidth(CommentsColumnCount, 55 * 256);
            }
            #endregion

            string customFileName = fileName + ".xlsx";//客户端保存的文件名 

            using (FileStream dumpFile = new FileStream(customFileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                book.Write(dumpFile);
                book.Close();
            };
            
        }

        private List<string> GetOutputInvoiceRowValueList(XSSFFormulaEvaluator eva, IRow row, int cellCount)
        {

            var rowList = new List<string>();

            for (int j = 0; j < cellCount; j++)
            {
                try
                {
                    if (row.GetCell(j) != null)
                    {
                        rowList.Add(getStringCellValue(eva, row.GetCell(j)));
                    }
                    else
                    {
                        rowList.Add(null);
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return rowList;
        }


        private String getStringCellValue(XSSFFormulaEvaluator eva, ICell cell)
        {
            // 获取单元格数据内容为字符串类型的数据  
            String strCell = "";
            switch (cell.CellType)
            {
                case CellType.Numeric:
                    if (HSSFDateUtil.IsCellDateFormatted(cell))
                    {

                        if (cell.DateCellValue != null)
                        {
                            //lynn:因为有些数据是有时分秒的，并且vourcher需要这个去判断时间                        
                            strCell = cell.DateCellValue.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else
                        {
                            strCell = null;
                        }
                    }
                    else
                    {
                        //var kk = cell.ToString();  
                        strCell = cell.NumericCellValue.ToString();
                    }
                    break;
                case CellType.Formula:
                    var tempType = eva.Evaluate(cell);
                    if (tempType.CellType == CellType.Numeric)
                    {
                        strCell = tempType.NumberValue.ToString();
                    }
                    else
                    {
                        strCell = tempType.StringValue;
                    }

                    break;
                default:
                    strCell = cell.ToString();
                    break;
            }
            return strCell;
        }
    }
}
