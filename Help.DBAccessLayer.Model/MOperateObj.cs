using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help.DBAccessLayer.Model
{
    public class MOperateObj<T>
    {
        /// <summary>
        /// 操作类型
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public MOperateType Type { get; set; }

        /// <summary>
        /// 待操作对象
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public T Data
        {
            get;
            set;
        }
    }
}
