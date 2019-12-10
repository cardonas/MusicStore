namespace DataObjects
{
    public class InvoiceLine
    {
        public int InvoiceLineID { get; set; }
        public string InstrumentID { get; set; }
        public int InvoiceID { get; set; }
        public int? RepairTicketID { get; set; }
        public int? RentToOwnID { get; set; }
        public decimal LineTotal { get; set; }
        public string LineTotalString => this.LineTotal.ToString("c");
    }
}
