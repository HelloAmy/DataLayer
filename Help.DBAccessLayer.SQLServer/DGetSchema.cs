using Help.DBAccessLayer.IDAL;
using Help.DBAccessLayer.Model.SqlGenerator;
using Help.DBAccessLayer.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help.DBAccessLayer.SQLServer
{
    public class DGetSchema : IGetSchema
    {
        public List<Model.MTableDesc> GetTableList(System.Data.IDbConnection conn, string creater)
        {
            throw new NotImplementedException();
        }

        public List<Model.MColumn> GetColumnList(System.Data.IDbConnection conn, string tableName)
        {
            throw new NotImplementedException();
        }

        public MDataBaseDefine GenerateDataBaseDefine(System.Data.IDbConnection conn)
        {
            MDataBaseDefine database = new MDataBaseDefine();
            database.TableList = new List<MTableDefine>();

            // 获取所有表名
            List<string> tableNameList = this.GetTableNameList(conn);

            foreach (var tableName in tableNameList)
            {
                var table = this.GetTableDefine(conn, tableName);
                database.TableList.Add(table);
            }


            // 填充索引信息
            List<MTableIndex> indexList = this.GetIndexInfo(conn);

            var group = from p in indexList
                        group p by new { p.TableName } into g
                        select new { g.Key };
            foreach (var g in group)
            {
                var tableFind = database.TableList.Find(sa => sa.TableName == g.Key.TableName);
                if (tableFind != null)
                {
                    var columns = tableFind.FieldList;
                    var indexs = indexList.FindAll(sa => sa.TableName == g.Key.TableName);

                    indexs.ForEach(sa =>
                    {
                        var column = columns.Find(p => p.FieldName == sa.ColumnName);
                        if (sa.IsUnique)
                        {
                            column.IsUniqueIndex = true;
                        }
                        else
                        {
                            column.IndexNo = columns.Max(q => q.IndexNo) + 1;
                        }
                    });
                }
            }

            return database;
        }

        private List<string> GetTableNameList(System.Data.IDbConnection conn)
        {
            string queryTableListSql = "SELECT name FROM SYSOBJECTS WHERE TYPE='U' and category=0 order by name;";
            SqlCommand comm = new SqlCommand(queryTableListSql, (SqlConnection)conn);
            SqlDataReader reader = comm.ExecuteReader();

            List<string> tableNameList = new List<string>();

            while (reader.Read())
            {
                string name = reader["name"] == DBNull.Value ? string.Empty : reader["name"].ToString();
                tableNameList.Add(name);
            }

            return tableNameList;
        }

        private MTableDefine GetTableDefine(System.Data.IDbConnection conn, string tableName)
        {
            MTableDefine table = new MTableDefine();
            table.TableName = tableName;
            string sql = string.Format(@"
SELECT 
    TableName       = case when a.colorder=1 then d.name else '' end,
    TableNameCH     = case when a.colorder=1 then isnull(f.value,'') else '' end,
    FieldIndex   = a.colorder,
    FieldName     = a.name,
    IsIdentity       = case when COLUMNPROPERTY( a.id,a.name,'IsIdentity')=1 then 'true'else 'false' end,
    IsPrimaryKey       = case when exists(SELECT 1 FROM sysobjects where xtype='PK' and parent_obj=a.id and name in (
                     SELECT name FROM sysindexes WHERE indid in( SELECT indid FROM sysindexkeys WHERE id = a.id AND colid=a.colid))) then 'true' else 'false' end,
    DataType       = b.name,
    DataTypeLength       = COLUMNPROPERTY(a.id,a.name,'PRECISION'),
    DigitalLength   = isnull(COLUMNPROPERTY(a.id,a.name,'Scale'),0),
    IsNullable     = case when a.isnullable=1 then 'true'else 'false' end,
    DefaultValue     = isnull(e.text,''),
    FieldNameCH   = isnull(g.[value],'')
FROM 
    syscolumns a
left join 
    systypes b 
on 
    a.xusertype=b.xusertype
inner join 
    sysobjects d 
on 
    a.id=d.id  and d.xtype='U' and  d.name<>'dtproperties'
left join 
    syscomments e 
on 
    a.cdefault=e.id
left join 
sys.extended_properties   g 
on 
    a.id=G.major_id and a.colid=g.minor_id  
left join
sys.extended_properties f
on 
    d.id=f.major_id and f.minor_id=0
where 
    d.name='{0}'    --如果只查询指定表,加上此红色where条件，tablename是要查询的表名；去除红色where条件查询说有的表信息
order by 
    a.id,a.colorder;", tableName);

            SqlCommand comm = new SqlCommand(sql, (SqlConnection)conn);
            SqlDataReader reader = comm.ExecuteReader();
            List<MFieldDefine> fieldList = new List<MFieldDefine>();
            while (reader.Read())
            {
                MFieldDefine field = new MFieldDefine();
                table.TableNameCH = reader["TableNameCH"] == DBNull.Value ? string.Empty : reader["TableNameCH"].ToString();
                field.Index = DBUtil.GetDBValueInt(reader, "FieldIndex", 0);
                field.FieldName = DBUtil.GetDBValueStr(reader, "FieldName", string.Empty);
                field.IsAutoIncrement = DBUtil.GetDBValueBool(reader, "IsIdentity", false);

                bool IsPrimaryKey = DBUtil.GetDBValueBool(reader, "IsPrimaryKey", false);
                int maxPrimaryKeyIndex = fieldList != null && fieldList.Count > 0 ? fieldList.Max(sa => sa.PrimaryKeyIndex) : 0;

                if (IsPrimaryKey)
                {
                    field.PrimaryKeyIndex = maxPrimaryKeyIndex + 1;
                }

                field.IsPrimaryKey = IsPrimaryKey;

                field.DataType = DBUtil.GetDBValueStr(reader, "DataType", string.Empty);
                field.Length = DBUtil.GetDBValueInt(reader, "DataTypeLength", 0);
                field.DigitalLength = DBUtil.GetDBValueInt(reader, "DigitalLength", 0);
                field.IsNullable = DBUtil.GetDBValueBool(reader, "IsNullable", false);
                field.DefaultValue = DBUtil.GetDBValueStr(reader, "DefaultValue", string.Empty);

                // 处理默认值
                if (!string.IsNullOrEmpty(field.DefaultValue))
                {
                    string[] twoBracketsSpecial = { "int", "tinyint" };

                    if (twoBracketsSpecial.Contains(field.DataType.ToLower()))
                    {
                        field.DefaultValue = field.DefaultValue.Replace("(", string.Empty).Replace(")", string.Empty);
                    }
                    else
                    {
                        // 只有一个括号
                        field.DefaultValue = field.DefaultValue.Substring(1, field.DefaultValue.Length - 2);
                    }
                }

                field.FieldNameCH = DBUtil.GetDBValueStr(reader, "FieldNameCH", string.Empty);
                fieldList.Add(field);
            }

            table.FieldList = fieldList;




            return table;

        }

        private List<MTableIndex> GetIndexInfo(System.Data.IDbConnection conn)
        {
            string sql = @"SELECT 
    TableId=O.[object_id],
    TableName=O.Name,
    IndexId=ISNULL(KC.[object_id],IDX.index_id),
    IndexName=IDX.Name,
    IndexType=ISNULL(KC.type_desc,''),
    Index_Column_id=IDXC.index_column_id,
    ColumnID=C.Column_id,
    ColumnName=C.Name,
    Sort=CASE INDEXKEY_PROPERTY(IDXC.[object_id],IDXC.index_id,IDXC.index_column_id,'IsDescending')
        WHEN 1 THEN 'DESC' WHEN 0 THEN 'ASC' ELSE '' END,
    PrimaryKey=IDX.is_primary_key,
    IsUnique=IDX.is_unique,
    Ignore_dup_key=IDX.ignore_dup_key,
    Disabled=IDX.is_disabled,
    Fill_factor=IDX.fill_factor,
    Padded=IDX.is_padded
FROM sys.indexes IDX
    INNER JOIN sys.index_columns IDXC
        ON IDX.[object_id]=IDXC.[object_id]
            AND IDX.index_id=IDXC.index_id
    LEFT JOIN sys.key_constraints KC
        ON IDX.[object_id]=KC.[parent_object_id]
            AND IDX.index_id=KC.unique_index_id
    INNER JOIN sys.objects O
        ON O.[object_id]=IDX.[object_id]
    INNER JOIN sys.columns C
        ON O.[object_id]=C.[object_id]
            AND O.type='U'
            AND O.is_ms_shipped=0
            AND IDXC.Column_id=C.Column_id
WHERE IDX.is_primary_key<>1
	 AND O.Name NOT LIKE 'sys%';";

            SqlCommand comm = new SqlCommand(sql, (SqlConnection)conn);
            SqlDataReader reader = comm.ExecuteReader();
            List<MTableIndex> list = new List<MTableIndex>();
            while (reader.Read())
            {
                MTableIndex model = new MTableIndex();
                model.TableName = DBUtil.GetDBValueStr(reader, "TableName");
                model.IndexType = DBUtil.GetDBValueStr(reader, "IndexType");
                model.IndexName = DBUtil.GetDBValueStr(reader, "IndexName");
                model.ColumnName = DBUtil.GetDBValueStr(reader, "ColumnName");
                model.IsUnique = DBUtil.GetDBValueBool(reader, "IsUnique");
                model.Sort = DBUtil.GetDBValueStr(reader, "Sort");

                list.Add(model);
            }

            return list;
        }
    }
}
