using Help.DBAccessLayer.Factory;
using Help.DBAccessLayer.Model.PagerQueryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help.DBAccessLayer.Business
{
    /// <summary>
    /// BPagerQuery
    /// </summary>
    public class BPagerQuery
    {
        /// <summary>
        /// Pagers the query.
        /// </summary>
        /// <param name="para">The para.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public MPagerReturn PagerQuery(MPagerInParam para)
        {
            string errorstr = this.CheckInParam(para);
            if (!string.IsNullOrEmpty(errorstr))
            {
                throw new Exception(errorstr);
            }

            if (para.PageIndex < 1)
            {
                para.PageIndex = 1;
            }

            if (para.PageSize == 0)
            {
                // 默认每页20条
                para.PageSize = 20;
            }

            var dao = DALFactory.GetPagerQueryDAO(para.DataBaseType);
            MPagerReturn ret = null;
            using (var conn = ConnectionFactory.GetDbConn(para.DataBaseName, para.DataBaseType))
            {
                ret = dao.PagerQuery(conn, para);
            }

            return ret;
        }

        /// <summary>
        /// Checks the in parameter.
        /// </summary>
        /// <param name="para">The para.</param>
        /// <returns></returns>
        private string CheckInParam(MPagerInParam para)
        {
            if (para == null)
            {
                return "分页查询，输入参数为null。";
            }

            if (string.IsNullOrEmpty(para.DataBaseName))
            {
                return "分页查询，数据库名为空。";
            }

            if (para.DataBaseType == Model.MDataBaseType.UNKNOW)
            {
                return "分页查询,数据库类型未知";
            }

            if (string.IsNullOrEmpty(para.FieldNames))
            {
                return "分页查询，无查询字段。";
            }

            if (string.IsNullOrEmpty(para.TableName))
            {
                return "分页查询，表名为空。";
            }

            return string.Empty;
        }
    }
}
