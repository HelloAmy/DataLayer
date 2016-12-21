﻿using Help.DBAccessLayer.Model.SqlGenerator;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help.DBAccessLayer.NOPI
{
    public class DGenerateDatabaseExcel
    {
        public void GenerateDatabaseExcel(MDataBaseDefine database)
        {
            string fileName = @"D:\01code\02mine\04DataLayer\MainTest\" + database.DataBaseName + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            IWorkbook workbook = new HSSFWorkbook();
            this.SetFirstPageWorkSheet(workbook, database);
            this.SetTableIndexPage(workbook, database);

            foreach (var table in database.TableList)
            {
                this.SetTableNamePageList(workbook, table, fileName);
            }

            //表格制作完成后，保存
            //创建一个文件流对象
            using (FileStream fs = File.Open(fileName, FileMode.OpenOrCreate))
            {
                workbook.Write(fs);

                //最后记得关闭对象
                workbook.Close();
            }
        }

        private void SetFirstPageWorkSheet(IWorkbook workbook, MDataBaseDefine database)
        {
            ISheet firstPageSheet = workbook.CreateSheet("首页");

            IRow row = firstPageSheet.CreateRow(15);
            ICell cell = row.CreateCell(5);

            IFont font1 = workbook.CreateFont();
            font1.FontName = "微软雅黑";
            font1.FontHeightInPoints = 36;

            cell.CellStyle = this.GetCellStyle(workbook, font1, null, FillPattern.NoFill, null, HorizontalAlignment.Center, VerticalAlignment.Center, false);

            cell.SetCellValue(string.Format("【{0}-{1}-{2}】", database.DataBaseName + "数据库", database.DataBaseName, database.DataBaseType.ToString()));

            row = firstPageSheet.CreateRow(17);
            cell = row.CreateCell(5);
            cell.CellStyle = this.GetCellStyle(workbook, font1, null, FillPattern.NoFill, null, HorizontalAlignment.Center, VerticalAlignment.Center, false);
            cell.SetCellValue("数据库项目定义");

            for (int j = 0; j <= 6; j++)
            {
                firstPageSheet.AutoSizeColumn(j);
            }
        }

        private void SetTableIndexPage(IWorkbook workbook, MDataBaseDefine database)
        {
            ISheet sheet = workbook.CreateSheet("数据表一栏");

            IRow row = sheet.CreateRow(0);
            ICell cell = row.CreateCell(0);
            cell.CellStyle = this.GetCellStyle(workbook, null, "微软雅黑", 10, false);
            cell.SetCellValue("Table表一栏");

            row = sheet.CreateRow(1);
            cell = row.CreateCell(0);
            cell.CellStyle = this.GetCellStyle(workbook, null);
            cell.SetCellValue("库名");
            cell = row.CreateCell(1);
            cell.CellStyle = this.GetCellStyle(workbook, null);
            cell.SetCellValue("");

            row = sheet.CreateRow(2);
            cell = row.CreateCell(0);
            cell.SetCellValue("库类型");
            cell.CellStyle = this.GetCellStyle(workbook, null);
            cell = row.CreateCell(1);
            cell.CellStyle = this.GetCellStyle(workbook, null);
            cell.SetCellValue(database.DataBaseName);

            row = sheet.CreateRow(3);
            cell = row.CreateCell(0);
            cell.CellStyle = this.GetCellStyle(workbook, null);
            cell.SetCellValue("服务器地址");
            cell = row.CreateCell(1);
            cell.CellStyle = this.GetCellStyle(workbook, null);
            cell.SetCellValue(database.ServerAddress);

            row = sheet.CreateRow(4);
            cell = row.CreateCell(0);
            cell.CellStyle = this.GetCellStyle(workbook, null);
            cell.SetCellValue("建库时间");
            cell = row.CreateCell(1);
            cell.CellStyle = this.GetCellStyle(workbook, null);
            cell.SetCellValue("");

            row = sheet.CreateRow(5);
            cell = row.CreateCell(0);
            cell.CellStyle = this.GetCellStyle(workbook, null);
            cell.SetCellValue("读账号");
            cell = row.CreateCell(1);
            cell.CellStyle = this.GetCellStyle(workbook, null);
            cell.SetCellValue(database.ReadAccount);

            row = sheet.CreateRow(6);
            cell = row.CreateCell(0);
            cell.CellStyle = this.GetCellStyle(workbook, null);
            cell.SetCellValue("写账号");
            cell = row.CreateCell(1);
            cell.CellStyle = this.GetCellStyle(workbook, null);
            cell.SetCellValue(database.WriteAccount);

            row = sheet.CreateRow(7);
            cell = row.CreateCell(4);
            cell.CellStyle = this.GetCellStyle(workbook, null, "宋体", 10, false);
            cell.SetCellValue("○修改，●新增，◎使用");

            HSSFColor gray = new HSSFColor.Grey40Percent();
            row = sheet.CreateRow(8);
            cell = row.CreateCell(0);
            cell.CellStyle = this.GetCellStyle(workbook, gray);
            cell.SetCellValue("#");
            cell = row.CreateCell(1);
            cell.CellStyle = this.GetCellStyle(workbook, gray);
            cell.SetCellValue("一级分类");
            cell = row.CreateCell(2);
            cell.CellStyle = this.GetCellStyle(workbook, gray);
            cell.SetCellValue("二级分类");
            cell = row.CreateCell(3);
            cell.CellStyle = this.GetCellStyle(workbook, gray);
            cell.SetCellValue("TableName");
            cell = row.CreateCell(4);
            cell.CellStyle = this.GetCellStyle(workbook, gray);
            cell.SetCellValue("表名");
            cell = row.CreateCell(5);
            cell.CellStyle = this.GetCellStyle(workbook, gray);
            cell.SetCellValue("备注");

            int i = 9;
            foreach (var table in database.TableList)
            {
                row = sheet.CreateRow(i);
                cell = row.CreateCell(0);
                cell.CellStyle = this.GetCellStyle(workbook, null);
                cell.SetCellValue(i - 8);
                cell = row.CreateCell(1);
                cell.CellStyle = this.GetCellStyle(workbook, null);
                cell.SetCellValue(table.TableNameCH);
                cell = row.CreateCell(2);
                cell.CellStyle = this.GetCellStyle(workbook, null);
                cell.SetCellValue(table.TableNameCH);
                cell = row.CreateCell(3);
                HSSFHyperlink link = new HSSFHyperlink(HyperlinkType.Document);//建一个HSSFHyperlink实体，指明链接类型为URL（这里是枚举，可以根据需求自行更改）
                link.Address = "#" + table.TableName + "!A1";
                cell.Hyperlink = link;
                cell.SetCellValue(table.TableName);
                this.SetHyperLinkCellStyle(workbook, cell);
                cell = row.CreateCell(4);
                cell.CellStyle = this.GetCellStyle(workbook, null);
                cell.SetCellValue(table.TableNameCH);
                cell = row.CreateCell(5);
                cell.CellStyle = this.GetCellStyle(workbook, null);
                cell.SetCellValue(table.TableNameCH);
                i++;
            }

            for (int j = 0; j <= 6; j++)
            {
                sheet.AutoSizeColumn(j);
            }
        }

        private void SetTableNamePageList(IWorkbook workbook, MTableDefine table, string filename)
        {
            ISheet sheet = workbook.CreateSheet(table.TableName);

            // 第0行：返回一览表
            IRow row = sheet.CreateRow(0);
            ICell cell = row.CreateCell(0);
            cell.SetCellValue("返回一览表");
            HSSFHyperlink link = new HSSFHyperlink(HyperlinkType.Document);//建一个HSSFHyperlink实体，指明链接类型为URL（这里是枚举，可以根据需求自行更改）
            link.Address = "#数据表一栏!A1";
            cell.Hyperlink = link;
            this.SetHyperLinkCellStyle(workbook, cell, false);

            HSSFColor gold = new HSSFColor.Gold();

            // 第一行、表介绍的的表头
            row = sheet.CreateRow(1);
            cell = row.CreateCell(0);
            cell.CellStyle = this.GetCellStyle(workbook, gold);
            cell.SetCellValue("TableName");

            cell = row.CreateCell(1);
            cell.CellStyle = this.GetCellStyle(workbook, gold);
            cell.SetCellValue("表明");

            cell = row.CreateCell(2);
            cell.CellStyle = this.GetCellStyle(workbook, gold);
            cell.SetCellValue("表说明");

            // 表介绍的内容
            row = sheet.CreateRow(2);
            cell = row.CreateCell(0);
            cell.CellStyle = this.GetCellStyle(workbook, null);
            cell.SetCellValue(table.TableName);
            cell = row.CreateCell(1);
            cell.CellStyle = this.GetCellStyle(workbook, null);
            cell.SetCellValue(table.TableNameCH);
            cell = row.CreateCell(2);
            cell.CellStyle = this.GetCellStyle(workbook, null);
            cell.SetCellValue(table.TableNameCH);

            // 一、表定义
            row = sheet.CreateRow(3);
            cell = row.CreateCell(0);
            cell.CellStyle = this.GetCellStyle(workbook, null, "微软雅黑", 10, false);
            cell.SetCellValue("一、表定义");

            // 表字段定义表头
            row = sheet.CreateRow(4);
            cell = row.CreateCell(0);
            cell.CellStyle = this.GetCellStyle(workbook, gold);
            cell.SetCellValue("序号");
            cell = row.CreateCell(1);
            cell.CellStyle = this.GetCellStyle(workbook, gold);
            cell.SetCellValue("字段中文名");
            cell = row.CreateCell(2);
            cell.CellStyle = this.GetCellStyle(workbook, gold);
            cell.SetCellValue("字段英文名");
            cell = row.CreateCell(3);
            cell.CellStyle = this.GetCellStyle(workbook, gold);
            cell.SetCellValue("数据类型");
            cell = row.CreateCell(4);
            cell.CellStyle = this.GetCellStyle(workbook, gold);
            cell.SetCellValue("位数");
            cell = row.CreateCell(5);
            cell.CellStyle = this.GetCellStyle(workbook, gold);
            cell.SetCellValue("非空");
            cell = row.CreateCell(6);
            cell.CellStyle = this.GetCellStyle(workbook, gold);
            cell.SetCellValue("主键");
            cell = row.CreateCell(7);
            cell.CellStyle = this.GetCellStyle(workbook, gold);
            cell.SetCellValue("外部关系");
            cell = row.CreateCell(8);
            cell.CellStyle = this.GetCellStyle(workbook, gold);
            cell.SetCellValue("唯一索引");
            cell = row.CreateCell(9);
            cell.CellStyle = this.GetCellStyle(workbook, gold);
            cell.SetCellValue("索引");
            cell = row.CreateCell(10);
            cell.CellStyle = this.GetCellStyle(workbook, gold);
            cell.SetCellValue("自增");
            cell = row.CreateCell(11);
            cell.CellStyle = this.GetCellStyle(workbook, gold);
            cell.SetCellValue("默认值");
            cell = row.CreateCell(12);
            cell.CellStyle = this.GetCellStyle(workbook, gold);
            cell.SetCellValue("字段格式");
            cell = row.CreateCell(13);
            cell.CellStyle = this.GetCellStyle(workbook, gold);
            cell.SetCellValue("值约束");
            cell = row.CreateCell(14);
            cell.CellStyle = this.GetCellStyle(workbook, gold);
            cell.SetCellValue("项目意义");

            int i = 5;

            // 字段定义
            foreach (var field in table.FieldList)
            {
                row = sheet.CreateRow(i);
                cell = row.CreateCell(0);
                cell.CellStyle = this.GetCellStyle(workbook, null);
                cell.SetCellValue(field.Index);

                cell = row.CreateCell(1);
                cell.CellStyle = this.GetCellStyle(workbook, null);
                cell.SetCellValue(field.FieldNameCH);

                cell = row.CreateCell(2);
                cell.CellStyle = this.GetCellStyle(workbook, null);
                cell.SetCellValue(field.FieldName);

                cell = row.CreateCell(3);
                cell.CellStyle = this.GetCellStyle(workbook, null);
                cell.SetCellValue(field.DataType);

                cell = row.CreateCell(4);
                cell.CellStyle = this.GetCellStyle(workbook, null);
                cell.SetCellValue(field.Length);

                cell = row.CreateCell(5);
                cell.CellStyle = this.GetCellStyle(workbook, null, "宋体");
                if (!field.IsNullable)
                {
                    cell.SetCellValue("○");
                }
                else
                {
                    cell.SetCellValue(string.Empty);
                }

                cell = row.CreateCell(6);
                cell.CellStyle = this.GetCellStyle(workbook, null);
                if (field.PrimaryKeyIndex > 0)
                {
                    cell.SetCellValue(field.PrimaryKeyIndex);
                }
                else
                {
                    cell.SetCellValue(string.Empty);
                }

                // 外部关系先不填写
                cell = row.CreateCell(7);
                cell.CellStyle = this.GetCellStyle(workbook, null);
                cell.SetCellValue(string.Empty);

                cell = row.CreateCell(8);
                cell.CellStyle = this.GetCellStyle(workbook, null, "宋体");
                if (field.IsUniqueIndex)
                {
                    cell.SetCellValue("○");
                }
                else
                {
                    cell.SetCellValue(string.Empty);
                }

                cell = row.CreateCell(9);
                cell.CellStyle = this.GetCellStyle(workbook, null);
                if (field.IndexNo > 0)
                {
                    cell.SetCellValue(field.IndexNo);
                }
                else
                {
                    cell.SetCellValue(string.Empty);
                }

                cell = row.CreateCell(10);
                cell.CellStyle = this.GetCellStyle(workbook, null, "宋体");
                if (field.IsAutoIncrement)
                {
                    cell.SetCellValue("○");
                }
                else
                {
                    cell.SetCellValue(string.Empty);
                }

                cell = row.CreateCell(11);
                cell.CellStyle = this.GetCellStyle(workbook, null);
                cell.SetCellValue(field.DefaultValue);

                // 字段格式，暂时不写
                cell = row.CreateCell(12);
                cell.CellStyle = this.GetCellStyle(workbook, null);
                cell.SetCellValue(string.Empty);

                // 值约束
                cell = row.CreateCell(13);
                cell.CellStyle = this.GetCellStyle(workbook, null);
                cell.SetCellValue(field.ValueConstraint);

                // 项目意义
                cell = row.CreateCell(14);
                cell.CellStyle = this.GetCellStyle(workbook, null);
                cell.SetCellValue(string.Empty);
                i++;
            }

            for (int j = 0; j <= 14; j++)
            {
                sheet.AutoSizeColumn(j);
            }
        }

        /// <summary>
        /// 获取单元格样式
        /// </summary>
        /// <param name="hssfworkbook">Excel操作类</param>
        /// <param name="font">单元格字体</param>
        /// <param name="fillForegroundColor">图案的颜色</param>
        /// <param name="fillPattern">图案样式</param>
        /// <param name="fillBackgroundColor">单元格背景</param>
        /// <param name="ha">垂直对齐方式</param>
        /// <param name="va">垂直对齐方式</param>
        /// <returns></returns>
        private ICellStyle GetCellStyle(IWorkbook hssfworkbook, IFont font, HSSFColor fillForegroundColor, FillPattern fillPattern, HSSFColor fillBackgroundColor, HorizontalAlignment ha, VerticalAlignment va, bool haveBorder = true)
        {
            ICellStyle cellstyle = hssfworkbook.CreateCellStyle();
            cellstyle.FillPattern = fillPattern;
            cellstyle.Alignment = ha;
            cellstyle.VerticalAlignment = va;
            if (fillForegroundColor != null)
            {
                cellstyle.FillForegroundColor = fillForegroundColor.Indexed;
            }
            else
            {
                cellstyle.FillPattern = FillPattern.NoFill;
            }

            if (fillBackgroundColor != null)
            {
                cellstyle.FillBackgroundColor = fillBackgroundColor.Indexed;
            }

            if (font != null)
            {
                cellstyle.SetFont(font);
            }

            //有边框
            if (haveBorder)
            {
                cellstyle.BorderBottom = BorderStyle.Thin;
                cellstyle.BorderLeft = BorderStyle.Thin;
                cellstyle.BorderRight = BorderStyle.Thin;
                cellstyle.BorderTop = BorderStyle.Thin;
            }

            return cellstyle;
        }

        private ICellStyle GetCellStyle(IWorkbook hssfworkbook, HSSFColor fillBackgroundColor, string fontName = "微软雅黑", short fontsize = 10, bool ishaveBorder = true)
        {
            IFont font1 = hssfworkbook.CreateFont();
            font1.FontName = fontName;
            font1.FontHeightInPoints = fontsize;
            return this.GetCellStyle(hssfworkbook, font1, fillBackgroundColor, FillPattern.SolidForeground, null, HorizontalAlignment.Left, VerticalAlignment.Center, ishaveBorder);
        }

        private void SetHyperLinkCellStyle(IWorkbook hssfworkbook, ICell cell, bool haveBorder = true)
        {
            ICellStyle linkStyle = hssfworkbook.CreateCellStyle();
            if (haveBorder)
            {
                linkStyle.BorderBottom = BorderStyle.Thin;
                linkStyle.BorderLeft = BorderStyle.Thin;
                linkStyle.BorderRight = BorderStyle.Thin;
                linkStyle.BorderTop = BorderStyle.Thin;
            }

            IFont cellFont = hssfworkbook.CreateFont();
            cellFont.FontName = "微软雅黑";
            cellFont.Underline = FontUnderlineType.Single;
            cellFont.Color = new HSSFColor.Blue().Indexed;
            linkStyle.SetFont(cellFont);
            cell.CellStyle = linkStyle;
        }

    }
}
