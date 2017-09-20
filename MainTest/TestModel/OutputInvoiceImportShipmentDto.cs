using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainTest.TestModel
{
    public class OutputInvoiceImportShipmentDto
    {
        public string ID { get; set; }
        public string PGIweek { get; set; }
        public string Company { get; set; }
        public string CompanyVloolup { get; set; }
        public string ConfirmationNumber { get; set; }
        public string VIN { get; set; }
        public string CarType { get; set; }
        public string IsMarketingCar { get; set; }
        public Nullable<decimal> TaxableSubtotal { get; set; }
        public Nullable<decimal> VehiclePriceafterDiscounts { get; set; }
        public Nullable<decimal> VAT { get; set; }
        public Nullable<decimal> Revenue { get; set; }
        public string Discounts { get; set; }
        public string Subsidy { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public string GDRevenue { get; set; }
        public string GDVAT { get; set; }
        public string GDCost { get; set; }
        public Nullable<decimal> Inventory { get; set; }
        public string OrginalbeforeIntercompany { get; set; }
        public string Notes { get; set; }
        public System.DateTime CreateTime { get; set; }
        public System.DateTime UpdateTime { get; set; }
    }
}
