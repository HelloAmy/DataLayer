using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help.DBAccessLayer.Model
{

    public class DataImportModel
    {
        public List<string> sheetNameList { get; set; }

        public List<List<string>> dataList { get; set; }

        public List<string> mappingResult { get; set; }

        public int selectedSheetIndex { get; set; }

        public int lastRowIndex { get; set; }
        public bool Result { get; set; }
        public string ResultMsg { get; set; }
    }

    public class SheetDto
    {
        public string fileName
        {
            get;
            set;
        }

        public string sheetName
        {
            get;
            set;
        }

        public List<CellDto> CellList
        {
            get;
            set;
        }
    }


    public class CellDto
    {
        public int rowIndex
        {
            get;
            set;
        }

        public int colomnIndex
        {
            get;
            set;
        }

        public string Value
        {
            get;
            set;
        }

        public bool IsLocked
        {
            get;
            set;
        }
    }
}
