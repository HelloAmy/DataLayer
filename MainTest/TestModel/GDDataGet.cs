using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainTest.TestModel
{
    public class GDDataGet : TeslaHistoryInvoiceGetBase
    {
        public override OutputInvoiceIssuedRecord GetOutputInvoiceIssuedRecord(List<string> list, string month)
        {
            OutputInvoiceIssuedRecord ret = new OutputInvoiceIssuedRecord()
            {
                TaxRate = list[10],
                TaxAmount = list[11],
                Amount = list[9],
                BuyerName = list[5],
                BuyerName1 = list[6],
                EntityCode = list[0],
                ID = Guid.NewGuid().ToString(),
                InvoiceCode = list[3],
                InvoiceNo =list[4],
                InvoiceStatus = 0,
                InvoiceType = list[2],
                IssuedDateTime = list[8],
                Month = month,
                Remark = list[1],
                SerialNumber = null
            };

            if (string.IsNullOrEmpty(ret.Remark))
            {
                ret.Remark = list[7];
            }

            return ret;
        }
    }
}
