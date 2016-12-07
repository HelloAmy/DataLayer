using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help.DBAccessLayer.Model
{
    /// <summary>
    /// 数据库访问类型
    /// </summary>
    public enum MDBAccessType
    {
        /// <summary>
        /// The unknown
        /// </summary>
        UNKNOWN = 0,

        /// <summary>
        /// The readonly
        /// </summary>
        READONLY = 1,

        /// <summary>
        /// The write
        /// </summary>
        WRITE = 2,
    }
}
