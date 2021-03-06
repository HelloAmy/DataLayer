﻿using Help.DBAccessLayer.Factory;
using Help.DBAccessLayer.Model;
using Help.DBAccessLayer.Model.SqlGenerator;
using IBM.Data.DB2;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help.DBAccessLayer.Business
{
    public class BGetSchema
    {
        public List<MTableDesc> GetTableList(string creator)
        {
            string connstr = ConnectionFactory.TRSDbConnString;

            List<MTableDesc> ret = null;

            using (DB2Connection conn = new DB2Connection(connstr))
            {
                conn.Open();
                var dao = DALFactory.GetSchemaDAO(MDataBaseType.DB2, MDBAccessType.WRITE);

                ret = dao.GetTableList(conn, creator);
            }

            return ret;
        }

        public List<MColumn> GetColumnList(string tableName)
        {
            string connstr = ConnectionFactory.TRSDbConnString;
            List<MColumn> ret = new List<MColumn>();
            using (DB2Connection conn = new DB2Connection(connstr))
            {
                conn.Open();
                var dao = DALFactory.GetSchemaDAO(MDataBaseType.DB2, MDBAccessType.WRITE);

                ret = dao.GetColumnList(conn, tableName);
            }

            return ret;
        }

        public MDataBaseDefine GenerateDataBaseDefine(string connstr)
        {
            MDataBaseDefine ret = null;
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                var dao = DALFactory.GetSchemaDAO(MDataBaseType.SQLSERVER, MDBAccessType.READONLY);
                ret = dao.GenerateDataBaseDefine(conn);
            }

            return ret;
        }
    }
}
