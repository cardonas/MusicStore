using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    public class CustomerManager : ICustomerManager
    {
        private readonly ICustomerAccessor _customerAccessor;

        public CustomerManager()
        {
          _customerAccessor = new CustomerAccessor(); 
        }

        public List<Customer> GetCustomersByEmailLike(string partialEmail)
        {
            try
            {
                return _customerAccessor.SelectCustomerByEmailLike(partialEmail);
            }
            catch (Exception ex)
            {
                
                throw new ApplicationException("No customers found.", ex);
            }
        }

        public List<Customer> GetAllCustomers(bool active)
        {
            try
            {
                return _customerAccessor.SelectAllCustomers(active);
            }
            catch (Exception ex)
            {
                
                throw new ApplicationException("No customers found.", ex);
            }
        }

        public bool EditCustomerDetails(Customer oldCustomer, Customer newCustomer)
        {
            bool updateSuccess;
            try
            {
                updateSuccess = _customerAccessor.UpdateCustomerProfile(oldCustomer, newCustomer);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to update customer details", ex);
            }

            return updateSuccess;
        }

        public bool AddCustomer(Customer customer)
        {
            bool addCustomer;
            try
            {
                addCustomer = _customerAccessor.InsertCustomer(customer);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to update customer details", ex);
            }

            return addCustomer;
        }

        public Customer GetCustomerByEmail(string email)
        {
            
            try
            {
              return  _customerAccessor.SelectCustomerByEmail(email);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No Customer Found with that email", ex);
            }
        }
    }
}
