using System;

namespace DataObjects
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumberString => $"{double.Parse(this.PhoneNumber):(###) ###-####}";
        public string Email { get; set; }
    }
}
