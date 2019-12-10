using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class InvoiceLineVM : InvoiceLine
    {
        public string InstrumentTypeID { get; set; }
        public string InstrumentBrandID { get; set; }
    }
}
