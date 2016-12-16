using Help.DBAccessLayer.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help.DBAccessLayer.SQLServer
{
    public class DGenerator : IGenerator
    {
        public Model.MResult<string> GeneratorSQL(Model.SqlGenerator.MDataBaseDefine db)
        {
            throw new NotImplementedException();
        }

        public Model.MResult<string> GeneratorSQL(List<Model.SqlGenerator.MTableDefine> tableList)
        {
            throw new NotImplementedException();
        }
    }
}
