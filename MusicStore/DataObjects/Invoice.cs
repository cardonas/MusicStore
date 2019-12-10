using System;

namespace DataObjects
{
    public class Invoice 
    {
        public int InvoiceId { get; set; }
        public string CustomerFirstName{ get; set; }
        public string CustomerLastName{ get; set; }
        public string Customer => this.CustomerLastName + ", " + this.CustomerFirstName;
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public string Employee => this.EmployeeLastName + ", " + this.EmployeeFirstName;
        public DateTime TransactionDate { get; set; }
        public decimal Total { get; set; }
        public string TotalString => this.Total.ToString("c");
    }
}
