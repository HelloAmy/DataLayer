using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help.DBAccessLayer.Model
{
    /// <summary>
    // 数据表描述
    /// </summary>
    public class MTableDesc
    {
        /// <summary>
        /// Gets or sets the name of the table.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        public string TableName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the creator.
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
        /// Gets or sets the create date time.
        /// </summary>
        /// <value>
        /// The create date time.
        /// </value>
        public DateTime CreateDateTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        /// <value>
        /// The remarks.
        /// </value>
        public string Remarks
        {
            get;
            set;
        }

        public List<MColumn> ColumnList
        {
            get;
            set;
        }

        public MTableDesc()
        {
            this.ColumnList = new List<MColumn>();
        }
    }
}
