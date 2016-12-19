using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help.DBAccessLayer.Model.SqlGenerator
{
    public class MTableIndex
    {
        public string TableName
        {
            get;
            set;
        }

        public string IndexType
        {
            get;
            set;
        }

        public string IndexName
        {
            get;
            set;
        }

        public string ColumnName
        {
            get;
            set;
        }

        public bool IsUnique
        {
            get;
            set;
        }

        public string Sort
        {
            get;
            set;
        }
    }
}
