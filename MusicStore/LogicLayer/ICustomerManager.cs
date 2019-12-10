using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    public interface ICustomerManager
    {
        List<Customer> GetCustomersByEmailLike(string query);
        List<Customer> GetAllCustomers(bool active);
        bool EditCustomerDetails(Customer oldCustomer, Customer newCustomer);
        bool AddCustomer(Customer customer);
        Customer GetCustomerByEmail(string email);
    }
}
