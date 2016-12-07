using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help.DBAccessLayer.Model
{
    public class MOperateReturn
    {
        public dynamic Data { get; set; }
        public string ErrorMsg { get; set; }
        public bool IsSuccess { get; set; }
        public string Tips { get; set; }
    }
}
