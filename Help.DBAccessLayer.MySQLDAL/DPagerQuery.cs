using Help.DBAccessLayer.IDAL;
using Help.DBAccessLayer.Model.PagerQueryModel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help.DBAccessLayer.MySQLDAL
{
    public class DPagerQuery : IPagerQuery
    {
        public MPagerReturn PagerQuery(IDbConnection conn, MPagerInParam para)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT COUNT(1)");
            sb.AppendFormat(" FROM {0}", para.TableName);
            if (!string.IsNullOrEmpty(para.Condition))
            {
                sb.AppendFormat(" WHERE {0}", para.Condition);
            }

            string countsql = sb.ToString();
            sb = new StringBuilder();
            sb.AppendFormat("SELECT {0}", para.FieldNames);
            sb.AppendFormat(" FROM {0}", para.TableName);
            if (!string.IsNullOrEmpty(para.Condition))
            {
                sb.AppendFormat(" WHERE {0}", para.Condition);
            }

            if (!string.IsNullOrEmpty(para.Sort))
            {
                sb.AppendFormat(" ORDER BY {0}", para.Sort);
            }

            int start = (para.PageIndex - 1) * para.PageSize;
            int end = para.PageIndex * para.PageSize;
            sb.AppendFormat(" LIMIT {0},{1}", start, end);

            string sql = sb.ToString();

            List<MySqlParameter> list = new List<MySqlParameter>();
            foreach (var item in para.Parameters)
            {
                MySqlParameter model = new MySqlParameter()
                {
                    DbType = item.DbType,
                    ParameterName = item.ParameterName,
                    Value = item.Value,
                };

                list.Add(model);
            }

            object count = MySqlHelper.ExecuteScalar((MySqlConnection)conn, countsql, list.ToArray());

            MPagerReturn ret = new MPagerReturn();
            ret.RowCount = long.Parse(count.ToString());

            DataSet ds = MySqlHelper.ExecuteDataset((MySqlConnection)conn, sql, list.ToArray());

            if (ds != null && ds.Tables.Count > 0)
            {
                ret.PageData = ds.Tables[0];
            }

            int total = (int)(ret.RowCount / para.PageSize);
            int yu = (int)(ret.RowCount % para.PageSize);
            total = yu == 0 ? total : total + 1;
            ret.PageCount = total;
            ret.PageIndex = para.PageIndex;

            return ret;
        }
    }
}
