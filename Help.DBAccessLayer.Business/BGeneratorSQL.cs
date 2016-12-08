using Help.DBAccessLayer.Factory;
using Help.DBAccessLayer.Model;
using Help.DBAccessLayer.Model.SqlGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help.DBAccessLayer.Business
{
    /// <summary>
    /// 生成SQL业务类
    /// </summary>
    public class BGeneratorSQL
    {
        /// <summary>
        /// 生成SQL语句
        /// </summary>
        /// <param name="db">数据库定义</param>
        /// <returns>结果</returns>
        public MResult<string> GeneratorSQL(MDataBaseDefine db)
        {
            var dao = DALFactory.GetGeneratorSQLDAL(db.DataBaseType);
            var ret = dao.GeneratorSQL(db);
            return ret;
        }
    }
}
