using Help.DBAccessLayer.Business;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();
        }

        private static void Test()
        {

            // Excel 操作文档:http://www.cnblogs.com/wzxwhd/p/5888849.html
            string conn = "Server=CNSHASQLUWV040,1801;Database=TaxClient_temp2;User ID=sa;Password=P@ss1234;Trusted_Connection=false;MultipleActiveResultSets=true;";

            BGetSchema bll = new BGetSchema();
            var ret = bll.GenerateDataBaseDefine(conn);

            string json = JsonConvert.SerializeObject(ret);
        }
    }
}
