using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help.DBAccessLayer.Model
{
    /// <summary>
    /// 字段描述
    /// </summary>
    public class MColumn
    {
        /// <summary>
        /// Gets or sets 字段名称
        /// </summary>
        /// <value>
        /// The name of the column.
        /// </value>
        public string ColumnName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 创建者
        /// </summary>
        /// <value>
        /// The creator.
        /// </value>
        public string Creator
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the 字段备注.
        /// </summary>
        /// <value>
        /// The remarks.
        /// </value>
        public string Remarks
        {
            get;
            set;
        }

        /// <summary>
        /// 字段类型
        /// </summary>
        /// <value>
        /// The type of the col.
        /// </value>
        public string ColType
        {
            get;
            set;
        }

        /// <summary>
        /// 字段长度
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public int Length
        {
            get;
            set;
        }

        /// <summary>
        /// 字段是否可空
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is nullable; otherwise, <c>false</c>.
        /// </value>
        public bool IsNullable
        {
            get;
            set;
        }

        /// <summary>
        /// 默认值
        /// </summary>
        /// <value>
        /// The default value.
        /// </value>
        public string DefaultValue
        {
            get;
            set;
        }

        /// <summary>
        /// 主键列表
        /// </summary>
        /// <value>
        /// The key seq.
        /// </value>
        public int KeySeq
        {
            get;
            set;
        }
    }
}
