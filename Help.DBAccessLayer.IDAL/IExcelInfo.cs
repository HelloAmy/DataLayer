using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help.DBAccessLayer.IDAL
{
    public interface IExcelInfo
    {
        DataSet GetDataSet(IDbConnection conn, List<string> sheetNameList);
    }
}
