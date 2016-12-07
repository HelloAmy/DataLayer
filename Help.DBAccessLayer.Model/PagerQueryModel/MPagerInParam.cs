using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help.DBAccessLayer.Model.PagerQueryModel
{
    /// <summary>
    /// 分页服务入参
    /// </summary>
    public class MPagerInParam
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MPagerInParam"/> class.
        /// </summary>
        public MPagerInParam()
        {
            this.Parameters = new List<SqlParameter>();
        }

        /// <summary>
        /// 数据库类型.
        /// </summary>
        /// <value>
        /// The type of the data base.
        /// </value>
        public MDataBaseType DataBaseType
        {
            get;
            set;
        }

        /// <summary>
        /// 页数
        /// </summary>
        public int PageIndex
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>
        /// The size of the page.
        /// </value>
        public int PageSize
        {
            get;
            set;
        }

        /// <summary>
        /// 数据库名
        /// </summary>
        public string DataBaseName
        {
            get;
            set;
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get;
            set;
        }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldNames
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the condition.
        /// </summary>
        /// <value>
        /// The condition.
        /// </value>
        public string Condition
        {
            get;
            set;
        }

        /// <summary>
        /// 条件
        /// </summary>
        public string Sort
        {
            get;
            set;
        }

        /// <summary>
        /// 查询参数
        /// </summary>
        public List<SqlParameter> Parameters
        {
            get;
            set;
        }
    }
}
