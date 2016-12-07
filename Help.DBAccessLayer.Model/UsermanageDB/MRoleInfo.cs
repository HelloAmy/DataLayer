using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help.DBAccessLayer.Model.UsermanageDB
{
    public class MRoleInfo
    {
        public string KeyID { get; set; }
        public string RoleName { get; set; }
        public string RoleAlias { get; set; }
        public sbyte IsValid { get; set; }
        public sbyte IsDelete { get; set; }
        public System.DateTime AddTime { get; set; }
        public System.DateTime ModifyTime { get; set; }
    }
}
