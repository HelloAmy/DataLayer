using Help.DBAccessLayer.Business;
using Help.DBAccessLayer.Model.SqlGenerator;
using Newtonsoft.Json;
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
        /// <summary>
        /// 数据库链接字符串
        /// </summary>
        private string _DbConnectionString = "";

        /// <summary>
        /// 数据库名称
        /// </summary>
        private string dataBaseName = "master";

        /// <summary>
        /// 是否使用测试数据
        /// </summary>
        private readonly bool _isMock = true;

        /// <summary>
        /// 由于使用的是COM库，因此有许多变量需要用Missing.Value代替
        /// </summary>
        private Object Nothing = Missing.Value;

        /// <summary>
        /// 写入黑体文本
        /// </summary>
        private object unite = MSWord.WdUnits.wdStory;

        /// <summary>
        /// 有长度的类型列表
        /// </summary>
        private readonly List<string> useLengthList = new List<string>(){
            "varchar",
            "nvarchar",
        };

        /// <summary>
        /// 有小数的类型列表
        /// </summary>
        private readonly List<string> digitalList = new List<string>()
        {
             "decimal",
            "numeric",
        };

        /// <summary>
        /// 获取数据库中的所有表结构信息等，现在的问题是table的cell 的 高度控制不好，内容是上对齐的 20180316
        /// </summary>
        public void Generate()
        {
            MDataBaseDefine res = getData();

            object path;                              //文件路径变量
            MSWord.Application wordApp;
            MSWord.Document wordDoc;

            path = Environment.CurrentDirectory + "\\" + DateTime.Now.ToString("yyyyMMdd") + "_" + this.dataBaseName + ".doc";
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

            this.AddHeading1("物理模型设计", wordDoc);
            this.DrawTableCountInfo(wordApp, wordDoc, res);

            wordDoc.Content.InsertAfter("\n");//这一句与下一句的顺序不能颠倒，原因还没搞透
            wordApp.Selection.EndKey(ref unite, ref Nothing);//这一句不加，有时候好像也不出问题，不过还是加了安全

            this.AddHeading2("2 表描述", wordDoc);
            int tableIndex = 1;
            foreach (var item in res.TableList)
            {
                this.DrawTableDetailInfo(wordApp, wordDoc, tableIndex, item);
                tableIndex++;
            }

            wordDoc.Content.InsertAfter("\n");

            // WdSaveFormat为Word 2003文档的保存格式
            // office 2007就是wdFormatDocumentDefault
            object format = MSWord.WdSaveFormat.wdFormatDocumentDefault;
            //将wordDoc文档对象的内容保存为DOCX文档
            wordDoc.SaveAs(ref path, ref format, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing);

            wordDoc.Close(ref Nothing, ref Nothing, ref Nothing);
            //关闭wordApp组件对象
            wordApp.Quit(ref Nothing, ref Nothing, ref Nothing);
            Console.WriteLine(path + " 创建完毕！");
            Console.ReadKey();

        }

        /// <summary>
        /// 绘制表详细
        /// </summary>
        /// <param name="wordApp">Word App</param>
        /// <param name="wordDoc">Word Doc</param>
        /// <param name="tableIndex">表格序号</param>
        /// <param name="tableDefine">表定义对象</param>
        private void DrawTableDetailInfo(MSWord.Application wordApp, MSWord.Document wordDoc, int tableIndex, MTableDefine tableDefine)
        {
            // 这一句与下一句的顺序不能颠倒，原因还没搞透
            // wordDoc.Content.InsertAfter("\n");

            // 这一句不加，有时候好像也不出问题，不过还是加了安全
            wordApp.Selection.EndKey(ref unite, ref Nothing);

            this.AddHeading4("2." + tableIndex + " " + tableDefine.TableName + "表的卡片", wordDoc);

            // 这一句与下一句的顺序不能颠倒，原因还没搞透
            // wordDoc.Content.InsertAfter("\n"); 
            // 将光标移动到文档末尾
            wordApp.Selection.EndKey(ref unite, ref Nothing);
            wordApp.Selection.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphLeft;

            int tableRow = tableDefine.FieldList.Count + 2;
            int tableColumn = 9;


            // 定义一个Word中的表格对象
            MSWord.Table table = wordDoc.Tables.Add(wordApp.Selection.Range,
            tableRow, tableColumn, ref Nothing, ref Nothing);

            // 默认创建的表格没有边框，这里修改其属性，使得创建的表格带有边框 
            // 这个值可以设置得很大，例如5、13等等
            table.Borders.Enable = 1;

            // 设置 每一列的 宽度
            table.Columns[1].Width = 30;
            table.Columns[2].Width = 100;
            table.Columns[3].Width = 90;
            table.Columns[4].Width = 50;
            table.Columns[5].Width = 30;
            table.Columns[6].Width = 30;
            table.Columns[7].Width = 30;
            table.Columns[8].Width = 55;
            table.Columns[9].Width = 75;

            // 横向合并
            table.Cell(1, 1).Merge(table.Cell(1, 9));
            table.Cell(1, 1).Range.Text = tableDefine.TableName;
            table.Cell(1, 1).Range.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray25;

            //表格的索引是从1开始的。
            table.Cell(2, 1).Range.Text = "是否主键";
            table.Cell(2, 2).Range.Text = "字段名";
            table.Cell(2, 3).Range.Text = "字段描述";
            table.Cell(2, 4).Range.Text = "数据类型";
            table.Cell(2, 5).Range.Text = "长度";
            table.Cell(2, 6).Range.Text = "可空";
            table.Cell(2, 7).Range.Text = "约束";
            table.Cell(2, 8).Range.Text = "缺省值";
            table.Cell(2, 9).Range.Text = "备注";


            for (int i = 3; i <= tableRow; i++)
            {
                int row = i;
                var field = tableDefine.FieldList[i - 3];

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

                if (this.digitalList.Contains(field.DataType))
                {
                    table.Cell(row, 5).Range.Text = field.Length.ToString() + "," + field.DigitalLength;
                }

                else if (this.useLengthList.Contains(field.DataType))
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

            table.Range.Font.Size = 9.5F;
            table.Range.Font.Bold = 0;
            table.Range.Font.Name = "新宋体";

            table.Range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphLeft;//表格文本居中

            table.Range.Cells.VerticalAlignment = MSWord.WdCellVerticalAlignment.wdCellAlignVerticalBottom;//文本垂直贴到底部

            // 设置table边框样式
            table.Borders.OutsideLineStyle = MSWord.WdLineStyle.wdLineStyleSingle;//表格外框是双线
            table.Borders.InsideLineStyle = MSWord.WdLineStyle.wdLineStyleSingle;//表格内框是单线

            // 加粗
            table.Rows[1].Range.Font.Bold = 1;
            table.Rows[1].Range.Font.Size = 9.5F;

            // 加粗
            table.Rows[2].Range.Font.Bold = 1;
            table.Rows[2].Range.Font.Size = 9.5F;
        }

        /// <summary>
        /// 添加标题1
        /// </summary>
        /// <param name="s"></param>
        /// <param name="wordDoc"></param>
        private void AddHeading1(string s, MSWord.Document wordDoc)
        {
            //Word段落
            Microsoft.Office.Interop.Word.Paragraph p;

            p = wordDoc.Content.Paragraphs.Add(ref Nothing);

            //设置段落中的内容文本
            p.Range.Text = s;

            //设置为一号标题
            object style = Microsoft.Office.Interop.Word.WdBuiltinStyle.wdStyleHeading1;

            p.set_Style(ref style);

            //添加到末尾
            p.Range.InsertParagraphAfter();  //在应用 InsertParagraphAfter 方法之后，所选内容将扩展至包括新段落。
        }

        /// <summary>
        /// 添加标题2
        /// </summary>
        /// <param name="s"></param>
        /// <param name="wordDoc"></param>
        private void AddHeading2(string s, MSWord.Document wordDoc)
        {
            // Word段落
            Microsoft.Office.Interop.Word.Paragraph p;

            p = wordDoc.Content.Paragraphs.Add(ref Nothing);

            // 设置段落中的内容文本
            p.Range.Text = s;

            //设置为一号标题
            object style = Microsoft.Office.Interop.Word.WdBuiltinStyle.wdStyleHeading2;
            p.set_Style(ref style);

            // 添加到末尾
            // 在应用 InsertParagraphAfter 方法之后，所选内容将扩展至包括新段落。
            p.Range.InsertParagraphAfter();
        }

        /// <summary>
        /// 添加标题4
        /// </summary>
        /// <param name="s"></param>
        /// <param name="wordDoc"></param>
        private void AddHeading4(string s, MSWord.Document wordDoc)
        {
            //Word段落
            Microsoft.Office.Interop.Word.Paragraph p;

            p = wordDoc.Content.Paragraphs.Add(ref Nothing);
            //设置段落中的内容文本
            p.Range.Text = s;
            //设置为一号标题
            object style = Microsoft.Office.Interop.Word.WdBuiltinStyle.wdStyleHeading4;
            p.set_Style(ref style);
            //添加到末尾
            p.Range.InsertParagraphAfter();  //在应用 InsertParagraphAfter 方法之后，所选内容将扩展至包括新段落。
        }


        /// <summary>
        /// 绘制表清单
        /// </summary>
        /// <param name="wordApp"></param>
        /// <param name="wordDoc"></param>
        /// <param name="res"></param>
        private void DrawTableCountInfo(MSWord.Application wordApp, MSWord.Document wordDoc, MDataBaseDefine res)
        {
            // 这一句与下一句的顺序不能颠倒，原因还没搞透
            //wordDoc.Content.InsertAfter("\n");

            // 这一句不加，有时候好像也不出问题，不过还是加了安全
            wordApp.Selection.EndKey(ref unite, ref Nothing);

            this.AddHeading2("1 表清单", wordDoc);

            //将光标移动到文档末尾
            wordApp.Selection.EndKey(ref unite, ref Nothing);
            wordApp.Selection.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphLeft;

            int tableRow = res.TableList.Count + 1;
            int tableColumn = 3;


            // 定义一个Word中的表格对象
            MSWord.Table table = wordDoc.Tables.Add(wordApp.Selection.Range,
            tableRow, tableColumn, ref Nothing, ref Nothing);

            // 默认创建的表格没有边框，这里修改其属性，使得创建的表格带有边框 
            table.Borders.Enable = 1;//这个值可以设置得很大，例如5、13等等

            // 表格的索引是从1开始的。
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

            // 设置table样式
            // 高度规则是：行高有最低值下限？
            table.Rows.HeightRule = MSWord.WdRowHeightRule.wdRowHeightAtLeast;
            //table.Rows.Height = wordApp.CentimetersToPoints(float.Parse("0.8"));// 

            table.Range.Font.Size = 11F;
            table.Range.Font.Bold = 0;
            table.Range.Font.Name = "新宋体";

            // 表格文本居左
            table.Range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;

            // 文本垂直贴到底部
            table.Range.Cells.VerticalAlignment = MSWord.WdCellVerticalAlignment.wdCellAlignVerticalBottom;

            // 设置table边框样式
            table.Borders.OutsideLineStyle = MSWord.WdLineStyle.wdLineStyleSingle;//表格外框是双线
            table.Borders.InsideLineStyle = MSWord.WdLineStyle.wdLineStyleSingle;//表格内框是单线

            // 加粗
            table.Rows[1].Range.Font.Bold = 1;
            table.Rows[1].Range.Font.Size = 11F;

            //将第 1列宽度设置为90
            table.Columns[1].Width = 90;

            //将其他列的宽度都设置为75
            for (int i = 2; i <= tableColumn; i++)
            {
                table.Columns[i].Width = 200;
            }
        }

        /// <summary>
        /// 读取数据库及表结构信息
        /// </summary>
        /// <returns></returns>
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
                                          IsPrimaryKey = true,
                                          
                               },
                               new MFieldDefine(){
                                DataType = "varchar",
                                 DefaultValue = string.Empty,
                                  DigitalLength = 3,
                                   FieldFormat = string.Empty,
                                    FieldName = "UserName",
                                     FieldNameCH = "用户名称",
                                      ForeignRelation = string.Empty,
                                       IsNullable = false,
                                        Length = 128,
                                         IsAutoIncrement = false,
                                          IsPrimaryKey = false,
                                          
                               }
                              },
                               PrimaryKey = "ID",
                                TableDescrption = string.Empty,
                                 TableName = "User",
                                  TableNameCH = string.Empty
                         }
                    }
                };
            }
            else
            {
                var res = new BGetSchema().GenerateDataBaseDefine(this._DbConnectionString);

                string json = JsonConvert.SerializeObject(res);

                // 存个表

                string fileName = @"C:\Julius_J_Zhu\09DataLayer\MainTest\documents\" + this.dataBaseName + "_JSON_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";

                this.saveFileName(fileName, json);

                // 去除 含有 下划线的临时表
                res.TableList = res.TableList.FindAll(sa => !sa.TableName.Contains("_"));


                return res;
            }
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="name">文件名</param>
        /// <param name="str">文件内容</param>
        private void saveFileName(string name, string str)
        {
            using (FileStream fs2 = new FileStream(name, FileMode.Create))
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
    }
}
