using Help.DBAccessLayer.Business;
using Help.DBAccessLayer.Model.SqlGenerator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MSWord = Microsoft.Office.Interop.Word;

namespace MainTest.TestModel
{
    public class GenerateWordDBDesign
    {

        private string _DbConnectionString = "Server=HLIU114SEC0,1801;Database=TaxAdmin;User ID=sa;Password=P@ss1234;Trusted_Connection=false;MultipleActiveResultSets=true;";

        private bool _isMock = true;

        /// <summary>
        /// 由于使用的是COM库，因此有许多变量需要用Missing.Value代替
        /// </summary>
        private Object Nothing = Missing.Value;

        /// <summary>
        /// 写入黑体文本
        /// </summary>
        private object unite = MSWord.WdUnits.wdStory;

        private List<string> useLengthList = new List<string>(){
            "varchar",
            "nvarchar"
        };

        /// <summary>
        /// 获取数据库中的所有表结构信息等，现在的问题是table的cell 的 高度控制不好，内容是上对齐的 20180316
        /// </summary>
        public void getDBInfo()
        {
            MDataBaseDefine res = getData();

            object path;                              //文件路径变量
            MSWord.Application wordApp;
            MSWord.Document wordDoc;

            path = Environment.CurrentDirectory + "\\" + DateTime.Now.ToString("yyyyMMdd") + "_TaxAdminDbDesign.doc";
            wordApp = new MSWord.Application();

            wordApp.Visible = true;//使文档可见

            //如果已存在，则删除
            if (File.Exists((string)path))
            {
                File.Delete((string)path);
            }


            //由于使用的是COM库，因此有许多变量需要用Missing.Value代替
            wordDoc = wordApp.Documents.Add(ref Nothing, ref Nothing, ref Nothing, ref Nothing);

            //页面设置
            wordDoc.PageSetup.PaperSize = MSWord.WdPaperSize.wdPaperA4;//设置纸张样式为A4纸
            wordDoc.PageSetup.Orientation = MSWord.WdOrientation.wdOrientPortrait;//排列方式为垂直方向
            wordDoc.PageSetup.TopMargin = 57.0f;
            wordDoc.PageSetup.BottomMargin = 57.0f;
            wordDoc.PageSetup.LeftMargin = 57.0f;
            wordDoc.PageSetup.RightMargin = 57.0f;
            wordDoc.PageSetup.HeaderDistance = 30.0f;//页眉位置

            wordApp.Selection.ParagraphFormat.LineSpacing = 16f;//设置文档的行间距
            wordApp.Selection.ParagraphFormat.FirstLineIndent = 30;//首行缩进的长度

            wordDoc.Paragraphs.Last.Range.Text = "物理模型设计";

            //将文档的前4个字替换成"哥是替换文字"，并将其颜色设为红色
            object start = 0;
            object end = 6;
            MSWord.Range rang = wordDoc.Range(ref start, ref end);
            rang.Font.Color = MSWord.WdColor.wdColorOrange;
            rang.Font.Name = "微软雅黑";
            rang.Font.Size = 12F;
            rang.Font.Bold = 1;
            rang.Text = "物理模型设计";
            wordDoc.Range(ref start, ref end);



            this.DrawTableCountInfo(wordApp, wordDoc, res);

            wordDoc.Content.InsertAfter("\n");//这一句与下一句的顺序不能颠倒，原因还没搞透
            wordApp.Selection.EndKey(ref unite, ref Nothing);//这一句不加，有时候好像也不出问题，不过还是加了安全
            wordDoc.Paragraphs.Last.Range.Font.Bold = 1;
            wordDoc.Paragraphs.Last.Range.Text = "2.表描述";
            int tableIndex = 1;
            foreach (var item in res.TableList)
            {
                this.DrawTableDetailInfo(wordApp, wordDoc, tableIndex, item);
            }

            wordDoc.Content.InsertAfter("\n");
            //wordDoc.Content.InsertAfter("就写这么多，算了吧！2016.09.27");

            //WdSaveFormat为Word 2003文档的保存格式
            object format = MSWord.WdSaveFormat.wdFormatDocumentDefault;// office 2007就是wdFormatDocumentDefault
            //将wordDoc文档对象的内容保存为DOCX文档
            wordDoc.SaveAs(ref path, ref format, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing);

            wordDoc.Close(ref Nothing, ref Nothing, ref Nothing);
            //关闭wordApp组件对象
            wordApp.Quit(ref Nothing, ref Nothing, ref Nothing);
            Console.WriteLine(path + " 创建完毕！");
            Console.ReadKey();

        }

        private void DrawTableDetailInfo(MSWord.Application wordApp, MSWord.Document wordDoc, int tableIndex, MTableDefine tableDefine)
        {
            wordDoc.Content.InsertAfter("\n");//这一句与下一句的顺序不能颠倒，原因还没搞透
            wordApp.Selection.EndKey(ref unite, ref Nothing);//这一句不加，有时候好像也不出问题，不过还是加了安全
            wordDoc.Paragraphs.Last.Range.Font.Bold = 1;
            wordDoc.Paragraphs.Last.Range.Text = "2." + tableIndex + tableDefine.TableName + "表的卡片";

            wordDoc.Content.InsertAfter("\n");//这一句与下一句的顺序不能颠倒，原因还没搞透
            wordApp.Selection.EndKey(ref unite, ref Nothing); //将光标移动到文档末尾
            wordApp.Selection.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphLeft;

            int tableRow = tableDefine.FieldList.Count + 1;
            int tableColumn = 9;


            //定义一个Word中的表格对象
            MSWord.Table table = wordDoc.Tables.Add(wordApp.Selection.Range,
            tableRow, tableColumn, ref Nothing, ref Nothing);

            //默认创建的表格没有边框，这里修改其属性，使得创建的表格带有边框 
            table.Borders.Enable = 1;//这个值可以设置得很大，例如5、13等等


            //table.Cell(4, 4).Merge(table.Cell(4, 5));//横向合并

            //表格的索引是从1开始的。
            table.Cell(1, 1).Range.Text = "是否主键";
            table.Cell(1, 2).Range.Text = "字段名";
            table.Cell(1, 3).Range.Text = "字段描述";
            table.Cell(1, 4).Range.Text = "数据类型";
            table.Cell(1, 5).Range.Text = "长度";
            table.Cell(1, 6).Range.Text = "可空";
            table.Cell(1, 7).Range.Text = "约束";
            table.Cell(1, 8).Range.Text = "缺省值";
            table.Cell(1, 9).Range.Text = "备注";



            for (int i = 1; i <= tableRow; i++)
            {
                int row = i + 1;
                var field = tableDefine.FieldList[i - 1];

                // 是否主键
                if (field.IsPrimaryKey)
                {
                    table.Cell(row, 1).Range.Text = "是";
                }
                else
                {
                    table.Cell(row, 1).Range.Text = "";
                }


                // 字段名
                table.Cell(row, 2).Range.Text = field.FieldName;


                if (string.IsNullOrEmpty(field.FieldNameCH) && field.FieldName == "ID")
                {
                    // 字段描述
                    table.Cell(row, 3).Range.Text = "主键ID";
                }
                else
                {
                    // 字段描述
                    table.Cell(row, 3).Range.Text = field.FieldNameCH;
                }

                // 数据类型
                table.Cell(row, 4).Range.Text = field.DataType;

                if (this.useLengthList.Contains(field.DataType))
                {
                    // 长度
                    table.Cell(row, 5).Range.Text = field.Length.ToString();
                }
                else
                {
                    table.Cell(row, 5).Range.Text = string.Empty;
                }

                // 是否可空
                if (field.IsNullable)
                {
                    table.Cell(row, 6).Range.Text = "是";
                }
                else
                {
                    table.Cell(row, 6).Range.Text = "否";
                }

                // 约束
                table.Cell(row, 7).Range.Text = field.ValueConstraint;

                // 缺省值
                table.Cell(row, 8).Range.Text = field.DefaultValue;

                // 备注
                table.Cell(row, 9).Range.Text = field.ProjectSignificance;
            }

            //设置table样式
            //table.Rows.HeightRule = MSWord.WdRowHeightRule.wdRowHeightAtLeast;//高度规则是：行高有最低值下限？
            //table.Rows.Height = wordApp.CentimetersToPoints(float.Parse("0.8"));// 

            table.Range.Font.Size = 9F;
            table.Range.Font.Bold = 0;
            table.Range.Font.Name = "微软雅黑";

            table.Range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;//表格文本居中

            table.Range.Cells.VerticalAlignment = MSWord.WdCellVerticalAlignment.wdCellAlignVerticalBottom;//文本垂直贴到底部
            //设置table边框样式
            table.Borders.OutsideLineStyle = MSWord.WdLineStyle.wdLineStyleSingle;//表格外框是双线
            table.Borders.InsideLineStyle = MSWord.WdLineStyle.wdLineStyleSingle;//表格内框是单线

            table.Rows[1].Range.Font.Bold = 1;//加粗
            table.Rows[1].Range.Font.Size = 9F;
            //table.Cell(1, 1).Range.Font.Size = 9F;
            //wordApp.Selection.Cells.Height = 30;//所有单元格的高度

            //除第一行外，其他行的行高都设置为20
            //for (int i = 2; i < tableRow; i++)
            //{
            //    table.Rows[i].Height = 20;
            //}

            //将表格左上角的单元格里的文字（“行” 和 “列”）居右
            //table.Cell(1, 1).Range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphRight;
            //将表格左上角的单元格里面下面的“列”字移到左边，相比上一行就是将ParagraphFormat改成了Paragraphs[2].Format
            //table.Cell(1, 1).Range.Paragraphs[2].Format.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphLeft;

            table.Columns[1].Width = 30;//将第 1列宽度设置为50
            table.Columns[2].Width = 100;//将第 1列宽度设置为50
            table.Columns[3].Width = 90;//将第 1列宽度设置为50
            table.Columns[4].Width = 50;//将第 1列宽度设置为50
            table.Columns[5].Width = 30;//将第 1列宽度设置为50
            table.Columns[6].Width = 30;//将第 1列宽度设置为50
            table.Columns[7].Width = 30;//将第 1列宽度设置为50
            table.Columns[8].Width = 55;//将第 1列宽度设置为50
            table.Columns[9].Width = 75;//将第 1列宽度设置为50
        }

        /// <summary>
        /// 绘制表清单
        /// </summary>
        /// <param name="wordApp"></param>
        /// <param name="wordDoc"></param>
        /// <param name="res"></param>
        private void DrawTableCountInfo(MSWord.Application wordApp, MSWord.Document wordDoc, MDataBaseDefine res)
        {
            wordDoc.Content.InsertAfter("\n");//这一句与下一句的顺序不能颠倒，原因还没搞透
            wordApp.Selection.EndKey(ref unite, ref Nothing);//这一句不加，有时候好像也不出问题，不过还是加了安全
            wordDoc.Paragraphs.Last.Range.Font.Bold = 1;
            wordDoc.Paragraphs.Last.Range.Text = "1.表清单";

            wordDoc.Content.InsertAfter("\n");//这一句与下一句的顺序不能颠倒，原因还没搞透
            wordApp.Selection.EndKey(ref unite, ref Nothing); //将光标移动到文档末尾
            wordApp.Selection.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphLeft;

            int tableRow = res.TableList.Count + 1;
            int tableColumn = 3;


            //定义一个Word中的表格对象
            MSWord.Table table = wordDoc.Tables.Add(wordApp.Selection.Range,
            tableRow, tableColumn, ref Nothing, ref Nothing);

            //默认创建的表格没有边框，这里修改其属性，使得创建的表格带有边框 
            table.Borders.Enable = 1;//这个值可以设置得很大，例如5、13等等

            //表格的索引是从1开始的。
            table.Cell(1, 1).Range.Text = "序号";
            table.Cell(1, 2).Range.Text = "代码";
            table.Cell(1, 3).Range.Text = "描述";

            for (int i = 1; i < tableRow; i++)
            {
                int row = i + 1;
                var tableInfo = res.TableList[i - 1];
                table.Cell(row, 1).Range.Text = i + ".";
                table.Cell(row, 2).Range.Text = tableInfo.TableName;
                table.Cell(row, 3).Range.Text = tableInfo.TableNameCH;
            }

            //设置table样式
            table.Rows.HeightRule = MSWord.WdRowHeightRule.wdRowHeightAtLeast;//高度规则是：行高有最低值下限？
            //table.Rows.Height = wordApp.CentimetersToPoints(float.Parse("0.8"));// 

            table.Range.Font.Size = 11F;
            table.Range.Font.Bold = 0;
            table.Range.Font.Name = "微软雅黑";
            table.Range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;//表格文本居中
            table.Range.Cells.VerticalAlignment = MSWord.WdCellVerticalAlignment.wdCellAlignVerticalBottom;//文本垂直贴到底部
            //设置table边框样式
            table.Borders.OutsideLineStyle = MSWord.WdLineStyle.wdLineStyleSingle;//表格外框是双线
            table.Borders.InsideLineStyle = MSWord.WdLineStyle.wdLineStyleSingle;//表格内框是单线

            table.Rows[1].Range.Font.Bold = 1;//加粗
            table.Rows[1].Range.Font.Size = 11F;
            //table.Cell(1, 1).Range.Font.Size = 11F;
            //wordApp.Selection.Cells.Height = 20;//所有单元格的高度

            //除第一行外，其他行的行高都设置为20
            //for (int i = 2; i < tableRow; i++)
            //{
            //    table.Rows[i].Height = 20;
            //}

            //将表格左上角的单元格里的文字（“行” 和 “列”）居右
            //table.Cell(1, 1).Range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphRight;
            //将表格左上角的单元格里面下面的“列”字移到左边，相比上一行就是将ParagraphFormat改成了Paragraphs[2].Format
            //table.Cell(1, 1).Range.Paragraphs[2].Format.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphLeft;

            table.Columns[1].Width = 90;//将第 1列宽度设置为50

            //将其他列的宽度都设置为75
            for (int i = 2; i <= tableColumn; i++)
            {
                table.Columns[i].Width = 200;
            }

            //添加表头斜线,并设置表头的样式
            //table.Cell(1, 1).Borders[MSWord.WdBorderType.wdBorderDiagonalDown].Visible = true;
            //table.Cell(1, 1).Borders[MSWord.WdBorderType.wdBorderDiagonalDown].Color = MSWord.WdColor.wdColorRed;
            //table.Cell(1, 1).Borders[MSWord.WdBorderType.wdBorderDiagonalDown].LineWidth = MSWord.WdLineWidth.wdLineWidth150pt;

        }

        private MDataBaseDefine getData()
        {
            if (_isMock)
            {
                return new MDataBaseDefine()
                {
                    TableList = new List<MTableDefine>()
                    {
                         new MTableDefine(){
                              FieldList = new List<MFieldDefine>(){
                               new MFieldDefine(){
                                DataType = "varchar",
                                 DefaultValue = string.Empty,
                                  DigitalLength = 3,
                                   FieldFormat = string.Empty,
                                    FieldName = "ID",
                                     FieldNameCH = string.Empty,
                                      ForeignRelation = string.Empty,
                                       IsNullable = false,
                                        Length = 128,
                                         IsAutoIncrement = false,
                                          
                               }
                              },
                               PrimaryKey = "ID",
                                TableDescrption = string.Empty,
                                 TableName = "OutputReconciliationConfiguration",
                                  TableNameCH = string.Empty
                         }
                    }
                };
            }
            else
            {
                var res = new BGetSchema().GenerateDataBaseDefine(this._DbConnectionString);

                return res;
            }
        }
    }
}
