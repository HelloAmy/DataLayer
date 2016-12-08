using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help.DBAccessLayer.Model
{
    public class MResult<T>
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess
        {
            get;
            set;
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg
        {
            get;
            set;
        }

        /// <summary>
        /// 结果
        /// </summary>
        public T Result
        {
            get;
            set;
        }
    }
}
