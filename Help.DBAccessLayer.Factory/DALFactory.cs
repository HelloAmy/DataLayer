using Help.DBAccessLayer.DB2DAL;
using Help.DBAccessLayer.IDAL;
using Help.DBAccessLayer.Model;
using Help.DBAccessLayer.MySQLDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help.DBAccessLayer.Factory
{
    public class DALFactory
    {
        public static IGetSchema GetSchemaDAO(MDataBaseType dbType, MDBAccessType accessType)
        {
            switch (dbType)
            {
                case MDataBaseType.DB2:
                    return new DGetSchema();
            }

            throw new Exception(string.Format("未实现的IGetSchema接口:数据库类型:{0},数据库访问类型:{1}", dbType.ToString(), accessType.ToString()));
        }

        /// <summary>
        /// Gets the pager query DAO.
        /// </summary>
        /// <param name="dbtype">The dbtype.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public static IPagerQuery GetPagerQueryDAO(MDataBaseType dbtype)
        {
            switch (dbtype)
            {
                case MDataBaseType.MYSQL:
                    return new DPagerQuery();
            }

            throw new Exception(string.Format("未实现的IPagerQuery接口:数据库类型:{0}", dbtype.ToString()));
        }
    }
}
