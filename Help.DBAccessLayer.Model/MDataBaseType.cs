using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help.DBAccessLayer.Model
{
    public enum MDataBaseType
    {
        /// <summary>
        /// 未知数据库
        /// </summary>
        UNKNOW = 0,

        /// <summary>
        /// The mysql
        /// </summary>
        MYSQL = 1,

        /// <summary>
        /// The sqlserver
        /// </summary>
        SQLSERVER = 2,

        /// <summary>
        /// DB2
        /// </summary>
        DB2 = 3
    }
}
