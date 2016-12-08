using Help.DBAccessLayer.Model;
using Help.DBAccessLayer.Model.SqlGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help.DBAccessLayer.IDAL
{
    /// <summary>
    /// 生成SQL接口
    /// </summary>
    public interface IGenerator
    {
        /// <summary>
        /// 生成建库建表语句
        /// </summary>
        /// <param name="db">数据库信息</param>
        /// <returns>SQL语句</returns>
        MResult<string> GeneratorSQL(MDataBaseDefine db);

        /// <summary>
        /// 生成建表语句
        /// </summary>
        /// <param name="tableList">表结构列表</param>
        /// <returns>返回</returns>
        MResult<string> GeneratorSQL(List<MTableDefine> tableList);
    }
}
