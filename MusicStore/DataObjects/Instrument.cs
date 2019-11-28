namespace DataObjects
{
    public class Instrument
    {
        public string InstrumentId { get; set; }
        public string InstrumentTypeId { get; set; }
        public string InstrumentStatusId { get; set; }
        public string InstrumentBrandId { get; set; }
        public decimal Price { get; set; }
        public string PriceString => this.Price.ToString("c");
    }
}
