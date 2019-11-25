using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class InstrumentVM : Instrument
    {
        public string RentalTermID { get; set; }
        public decimal RentalFee { get; set; }
        public String RentalFeeString {
            get
            {
                return this.RentalFee.ToString("c");
            }
         }
        public string PrepListDescription { get; set; }
        public string InstrumentFamily { get; set; }
    }
}
