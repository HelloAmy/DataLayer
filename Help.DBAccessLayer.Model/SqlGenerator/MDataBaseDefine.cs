using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help.DBAccessLayer.Model.SqlGenerator
{
    /// <summary>
    /// 数据库定义
    /// </summary>
    public class MDataBaseDefine
    {
        /// <summary>
        /// 数据库定义
        /// </summary>
        public string DataBaseName
        {
            get;
            set;
        }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public MDataBaseType DataBaseType
        {
            get;
            set;
        }

        /// <summary>
        /// 服务器地址
        /// </summary>
        public string ServerAddress
        {
            get;
            set;
        }

        /// <summary>
        /// 读写账号
        /// </summary>
        public string ReadAccount
        {
            get;
            set;
        }

        /// <summary>
        /// 写账号
        /// </summary>
        public string WriteAccount
        {
            get;
            set;
        }

        /// <summary>
        /// 表列表
        /// </summary>
        public List<MTableDefine> TableList
        {
            get;
            set;
        }
    }
}
