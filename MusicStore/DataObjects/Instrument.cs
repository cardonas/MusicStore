using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Instrument
    {
        public string InstrumentID { get; set; }
        public string InstrumentTypeID { get; set; }
        public string InstrumentStatusID { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public string PriceString
        {
            get
            {
                return this.Price.ToString("c");
            }
        }
    }
}
