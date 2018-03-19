using NPOI.HSSF.UserModel;
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
