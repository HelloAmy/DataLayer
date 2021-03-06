﻿using Help.DBAccessLayer.Model;
using Help.DBAccessLayer.Model.SqlGenerator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help.DBAccessLayer.IDAL
{
    public interface IGetSchema
    {
        List<MTableDesc> GetTableList(IDbConnection conn, string creater);

        List<MColumn> GetColumnList(IDbConnection conn, string tableName);

        MDataBaseDefine GenerateDataBaseDefine(System.Data.IDbConnection conn);
    }
}
