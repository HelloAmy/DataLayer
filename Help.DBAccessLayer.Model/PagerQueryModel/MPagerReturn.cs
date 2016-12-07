using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help.DBAccessLayer.Model.PagerQueryModel
{
    public class MPagerReturn
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get;
            set;
        }

        /// <summary>
        /// 页面索引
        /// </summary>
        public int PageIndex
        {
            get;
            set;
        }

        /// <summary>
        /// 总行数
        /// </summary>
        public long RowCount
        {
            get;
            set;
        }

        /// <summary>
        /// 分页数据
        /// </summary>
        public DataTable PageData
        {
            get;
            set;
        }
    }
}
