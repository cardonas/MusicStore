namespace DataObjects
{
    public class InstrumentVm : Instrument
    {
        public string RentalTermId { get; set; }
        public decimal RentalFee { get; set; }
        public string RentalFeeString => this.RentalFee.ToString("c");
        public string PrepListDescription { get; set; }
        public string InstrumentFamily { get; set; }
    }
}
