using Help.DBAccessLayer.Business;
using Help.DBAccessLayer.Model;
using Help.DBAccessLayer.Model.SqlGenerator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help.DBAccessLayer.Business
{
    /// <summary>
    /// 从Excel中获取数据结构定义
    /// </summary>
    public class BGetTableDefineFromExcel
    {
        /// <summary>
        /// 从Excel中获取数据结构定义
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>结果</returns>
        public MDataBaseDefine GetTableDefineListFromExcel(string path)
        {
            DataSet ds = new BDataBaseDesign().GetExcelDataBaseDesignInfo(path);
            var tableList = this.GetTableDefineList(ds);

            var db = this.GetDataBaseDefine(ds);
            db.TableList = tableList;

            return db;
        }

        private MDataBaseDefine GetDataBaseDefine(DataSet ds)
        {
            MDataBaseDefine model = new MDataBaseDefine();

            DataTable tb = ds.Tables["数据表一栏$"];

            model.DataBaseName = tb.Rows[1][1].ToString();
            string databaseType = tb.Rows[2][1].ToString();

            if (!string.IsNullOrEmpty(databaseType) && databaseType.ToUpper().Trim() == "MYSQL")
            {
                model.DataBaseType = MDataBaseType.MYSQL;
            }
            else
            {
                model.DataBaseType = MDataBaseType.UNKNOW;
            }

            model.ServerAddress = tb.Rows[3][1].ToString();
            model.ReadAccount = tb.Rows[5][1].ToString();
            model.WriteAccount = tb.Rows[5][1].ToString();

            return model;
        }

        /// <summary>
        /// 获取数据表定义列表
        /// </summary>
        /// <param name="ds">DataSet</param>
        /// <returns>结果</returns>
        private List<MTableDefine> GetTableDefineList(DataSet ds)
        {
            List<MTableDefine> tb = new List<MTableDefine>();

            foreach (DataTable item in ds.Tables)
            {
                var ret = GetTableDefine(item);
                if (ret != null)
                {
                    tb.Add(ret);
                }

            }

            return tb;
        }

        /// <summary>
        /// 获取表结构定义
        /// </summary>
        /// <param name="tb">DataTable</param>
        /// <returns>结果</returns>
        private MTableDefine GetTableDefine(DataTable tb)
        {
            MTableDefine ret = new MTableDefine();

            if (tb == null)
            {
                return null;
            }

            if (tb.TableName.Contains("首页") || tb.TableName.Contains("数据表一栏"))
            {
                return null;
            }

            // 估计带超链接，还真不准，先注释掉
            //if (tb.Rows[0][0].ToString() != "返回一览表")
            //{
            //    return null;
            //}

            if (tb.Rows[1][0].ToString() != "TableName")
            {
                return null;
            }

            ret.TableName = tb.Rows[2][0].ToString();
            ret.TableNameCH = tb.Rows[2][1].ToString();
            ret.TableDescrption = tb.Rows[2][2].ToString();
            ret.FieldList = new List<MFieldDefine>();
            for (int i = 5; i < tb.Rows.Count; i++)
            {
                MFieldDefine col = new MFieldDefine();
                col.FieldNameCH = tb.Rows[i][1].ToString();
                col.FieldName = tb.Rows[i][2].ToString();
                col.DataType = tb.Rows[i][3].ToString();

                string tempstr = tb.Rows[i][4].ToString();
                int tempint = 0;
                if (!string.IsNullOrEmpty(tempstr) && int.TryParse(tempstr, out tempint))
                {
                    col.Length = tempint;
                }

                tempstr = tb.Rows[i][5].ToString();
                if (!string.IsNullOrEmpty(tempstr) && tempstr == "○")
                {
                    col.IsNullable = false;
                }

                tempstr = tb.Rows[i][6].ToString();

                if (!string.IsNullOrEmpty(tempstr) && int.TryParse(tempstr, out tempint))
                {
                    col.PrimaryKeyIndex = tempint;
                }

                col.ForeignRelation = tb.Rows[i][7].ToString();

                tempstr = tb.Rows[i][7].ToString();
                if (!string.IsNullOrEmpty(tempstr) && tempstr == "○")
                {
                    col.IsUniqueIndex = true;
                }

                tempstr = tb.Rows[i][8].ToString();
                if (!string.IsNullOrEmpty(tempstr) && int.TryParse(tempstr, out tempint))
                {
                    col.IndexNo = tempint;
                }

                tempstr = tb.Rows[i][9].ToString();
                if (!string.IsNullOrEmpty(tempstr) && tempstr == "○")
                {
                    col.IsAutoIncrement = true;
                }

                col.FieldFormat = tb.Rows[i][10].ToString();
                col.DefaultValue = tb.Rows[i][11].ToString();
                col.ValueConstraint = tb.Rows[i][12].ToString();
                col.ProjectSignificance = tb.Rows[i][13].ToString();
                ret.FieldList.Add(col);
            }

            return ret;
        }
    }
}
