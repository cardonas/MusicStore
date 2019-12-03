using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public interface ICustomerAccessor
    {
        List<Customer> SelectCustomerByEmailLike(string query);
        List<Customer> SelectAllCustomers(bool active);
        bool UpdateCustomerProfile(Customer oldCustomer, Customer newCustomer);
    }
}
