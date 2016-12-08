using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help.DBAccessLayer.Model.SqlGenerator
{
    /// <summary>
    /// 表定义
    /// </summary>
    public class MTableDefine
    {
        /// <summary>
        /// 表名（英文）
        /// </summary>
        public string TableName
        {
            get;
            set;
        }

        /// <summary>
        /// 表名（中文）
        /// </summary>
        public string TableNameCH
        {
            get;
            set;
        }

        /// <summary>
        /// 表描述
        /// </summary>
        public string TableDescrption
        {
            get;
            set;
        }

        /// <summary>
        /// 主键
        /// </summary>
        public string PrimaryKey
        {
            get;
            set;
        }

        /// <summary>
        /// 字段定义
        /// </summary>
        public List<MFieldDefine> FieldList
        {
            get;
            set;
        }
    }
}
