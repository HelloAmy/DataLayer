using Help.DBAccessLayer.IDAL;
using Help.DBAccessLayer.Model;
using Help.DBAccessLayer.Model.SqlGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help.DBAccessLayer.SQLServer
{
    public class DGenerator : IGenerator
    {
        public MResult<string> GeneratorSQL(MDataBaseDefine db)
        {
            // 1、进行数据验证
            string errorMsg = string.Empty;
            if (!this.CheckGeneratorSQL(db, out errorMsg))
            {
                return new MResult<string>()
                {
                    ErrorMsg = errorMsg,
                    IsSuccess = false,
                    Result = string.Empty,
                };
            }

            string databaseSQL = this.GetCreateDataBaseSQL(db);

            StringBuilder tableSQL = new StringBuilder();
            foreach (var table in db.TableList)
            {
                string str = this.GetCreateTableSQL(table);
                tableSQL.Append(str).AppendLine();
            }

            return new MResult<string>
            {
                IsSuccess = true,
                ErrorMsg = string.Empty,
                Result = databaseSQL.ToString() + tableSQL.ToString()
            };
        }

        public MResult<string> GeneratorSQL(List<MTableDefine> tableList)
        {
            string errorMsg = string.Empty;
            if (!this.CheckGeneratorSQL(tableList, out errorMsg))
            {
                return new MResult<string>()
                {
                    ErrorMsg = errorMsg,
                    IsSuccess = false,
                    Result = string.Empty,
                };
            }

            StringBuilder tableSQL = new StringBuilder();
            foreach (var table in tableList)
            {
                string str = this.GetCreateTableSQL(table);
                tableSQL.Append(str).AppendLine();
            }

            return new MResult<string>
            {
                IsSuccess = true,
                ErrorMsg = string.Empty,
                Result = tableSQL.ToString()
            };
        }

        public MResult<Dictionary<string, string>> GenneratorModelList(MDataBaseDefine db, string namespaceStr)
        {
            MResult<Dictionary<string, string>> ret = new MResult<Dictionary<string, string>>();

            return ret;
        }

        public string GeneratorModel(MTableDefine table, string namespaceStr)
        {
            StringBuilder sb = new StringBuilder();



            return sb.ToString();
        }

        private bool CheckGeneratorSQL(MDataBaseDefine db, out string errorMsg)
        {
            if (db == null)
            {
                errorMsg = "待处理的数据为空";
                return false;
            }

            if (string.IsNullOrEmpty(db.DataBaseName))
            {
                errorMsg = "数据库名称为空";
                return false;
            }

            string msg = string.Empty;

            if (!this.CheckGeneratorSQL(db.TableList, out msg))
            {
                errorMsg = msg;
                return false;
            }

            errorMsg = string.Empty;
            return true;
        }

        private bool CheckGeneratorSQL(List<MTableDefine> tableList, out string errorMsg)
        {
            if (tableList == null || tableList.Count == 0)
            {
                errorMsg = "数据库表的个数为空";
                return false;
            }

            foreach (var table in tableList)
            {
                if (string.IsNullOrEmpty(table.TableName))
                {
                    errorMsg = "表名为空";
                    return false;
                }

                if (table.FieldList == null || table.FieldList.Count == 0)
                {
                    errorMsg = string.Format("表:{0},字段个数为空", table.TableName);
                    return false;
                }

                if (!table.FieldList.Exists(sa => sa.PrimaryKeyIndex > 0))
                {
                    errorMsg = string.Format("表:{0},无主键，不符合数据库规范", table.TableName);
                    return false;
                }

                if (table.FieldList.Exists(sa => string.IsNullOrEmpty(sa.FieldName)))
                {

                    errorMsg = string.Format("表:{0},存在字段名为空的字段，不能正确建表", table.TableName);
                    return false;
                }

                if (table.FieldList.Exists(sa => sa.IsNullable == true))
                {
                    var nullableFied = (from p in table.FieldList
                                        where p.IsNullable == true
                                        select p.FieldName).ToList<string>();

                    errorMsg = string.Format("表:{0},字段：{1}可空，不符合数据库规范", table.TableName, string.Join(",", nullableFied.ToArray()));

                    return false;
                }

                if (table.FieldList.Exists(sa => string.IsNullOrEmpty(sa.DataType)))
                {
                    var field = (from p in table.FieldList
                                 where string.IsNullOrEmpty(p.DataType)
                                 select p.FieldName).ToList<string>();

                    errorMsg = string.Format("表:{0},字段：{1}没有定义数据类型，会生成错误的SQL语句", table.TableName, string.Join(",", field.ToArray()));
                    return false;
                }

                if (table.FieldList.Exists(sa => string.IsNullOrEmpty(sa.FieldNameCH)))
                {
                    var field = (from p in table.FieldList
                                 where string.IsNullOrEmpty(p.FieldNameCH)
                                 select p.FieldName).ToList<string>();
                    errorMsg = string.Format("表:{0},字段：{1}没有中文名，不符合数据库规范", table.TableName, string.Join(",", field.ToArray()));
                    return false;
                }


                if (table.FieldList.Exists(sa => sa.IsUniqueIndex == true) && table.FieldList.FindAll(sa => sa.IsUniqueIndex).Count > 1)
                {
                    var field = (from p in table.FieldList
                                 where p.IsUniqueIndex
                                 select p.FieldName).ToList<string>();
                    errorMsg = string.Format("表:{0},多个字段建立了唯一索引，字段列表：{1}，建表失败！", table.TableName, string.Join(",", field.ToArray()));
                    return false;
                }
            }

            errorMsg = string.Empty;
            return true;
        }

        private string GetCreateDataBaseSQL(MDataBaseDefine db)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("/* 建立数据库{0} */", db.DataBaseName).AppendLine();
            sb.AppendFormat("CREATE DATABASE {0} DEFAULT charset utf8 collate utf8_general_ci;", db.DataBaseName).AppendLine();

            return sb.ToString();
        }

        private string GetCreateTableSQL(MTableDefine tb)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("/*建立数表{0}*/", tb.TableName).AppendLine();

            sb.AppendFormat("IF NOT EXISTS (select 1 from sys.tables where name = '{0}' and type = 'U')", tb.TableName);
            sb.Append("BEGIN");
            sb.AppendFormat("CREATE TABLE DBO.{0}", tb.TableName).AppendLine();
            sb.Append("(");

            // 字段
            foreach (var field in tb.FieldList)
            {
                if (field.FieldName.ToUpper() == "MODIFYTIME" && field.DefaultValue == "CURRENT_TIMESTAMP")
                {
                    sb.AppendFormat("{0} {1} NOT NULL DEFAULT {2} ON UPDATE {2} COMMENT '{3}',", field.FieldName, field.DataType, field.DefaultValue, field.FieldNameCH + ";" + field.ValueConstraint).AppendLine();
                }
                else if (field.DataType.ToUpper() == "DATETIME")
                {
                    sb.AppendFormat("{0} {1} NOT NULL DEFAULT {2} COMMENT '{3}',", field.FieldName, field.DataType, field.DefaultValue, field.FieldNameCH + ";" + field.ValueConstraint).AppendLine();
                }
                else
                {
                    if (field.Length == 0)
                    {
                        // 没有长度
                        sb.AppendFormat("{0} {1} NOT NULL DEFAULT '{2}' COMMENT '{3}',", field.FieldName, field.DataType, field.DefaultValue, field.FieldNameCH + ";" + field.ValueConstraint).AppendLine();
                    }
                    else
                    {
                        sb.AppendFormat("{0} {1}({2}) NOT NULL DEFAULT '{3}' COMMENT '{4}',", field.FieldName, field.DataType, field.Length, field.DefaultValue, field.FieldNameCH + ";" + field.ValueConstraint).AppendLine();
                    }
                }
            }

            var primarkeys = (from p in tb.FieldList
                              where p.PrimaryKeyIndex > 0
                              orderby p.PrimaryKeyIndex ascending
                              select p.FieldName);

            string primaryKeyStr = string.Join(",", primarkeys.ToArray());

            // 主键
            sb.AppendFormat("PRIMARY KEY({0}),", primaryKeyStr).AppendLine();


            // 唯一索引
            if (tb.FieldList.Exists(sa => sa.IsUniqueIndex == true))
            {
                var uniqueIndex = (from sa in tb.FieldList
                                   where sa.IsUniqueIndex
                                   select sa.FieldName);

                sb.AppendFormat("UNIQUE  INDEX  {0}_{1} ({1}),", tb.TableName, uniqueIndex.ToArray()[0]).AppendLine();
            }

            // 其他索引列，如果有的话
            if (tb.FieldList.Exists(sa => sa.IndexNo > 0))
            {
                var indexNoList = (from sa in tb.FieldList
                                   where sa.IndexNo > 0
                                   orderby sa.IndexNo ascending
                                   select sa.FieldName);

                string indexNoStr = string.Join(",", indexNoList.ToArray());

                string indexNoListName = string.Join("_", indexNoList.ToArray());

                sb.AppendFormat("INDEX  {0}_{1} ({2}),", tb.TableName, indexNoListName, indexNoStr).AppendLine();
            }

            // 去掉最后一个逗号
            sb = new StringBuilder(sb.ToString().Trim());
            sb = sb.Remove(sb.Length - 1, 1);
            sb.AppendFormat(");", tb.TableNameCH);

            sb.Append("END");



            return sb.ToString();
        }
    }
}
