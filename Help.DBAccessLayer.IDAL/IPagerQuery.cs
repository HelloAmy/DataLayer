using Help.DBAccessLayer.Model.PagerQueryModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help.DBAccessLayer.IDAL
{
    /// <summary>
    ///  分页查询接口
    /// </summary>
    public interface IPagerQuery
    {
        /// <summary>
        /// Pagers the query.
        /// </summary>
        /// <param name="conn">The connection.</param>
        /// <param name="para">The para.</param>
        /// <returns></returns>
        MPagerReturn PagerQuery(IDbConnection conn, MPagerInParam para);
    }
}
