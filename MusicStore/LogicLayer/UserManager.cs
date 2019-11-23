using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class UserManager : IUserManager
    {
        private IUserAccessor _userAccessor;

        public UserManager()
        {
            _userAccessor = new UserAccessor();
        }

        public UserManager(IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
        }

        public bool AddNewEmployee(Employee employee)
        {
           
                bool isAdded = false;

                try
                {
                    isAdded = _userAccessor.AddNewEmployee(employee);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Failed to Add Employee", ex);
                }
            return isAdded;
            
        }

        public Employee AuthenticateUser(string email, string password)
        {
            Employee result = null;
            var passwordHash = hashPassword(password);
            password = null;
            try
            {
                result = _userAccessor.AuthenticateUser(email, passwordHash);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Login failed", ex);
            }
            return result;
        }

        public bool DeactivateEmployee(Employee employee)
        {
            bool isDeactivated = false;
            try
            {
                isDeactivated = _userAccessor.DeactivateEmployee(employee.EmployeeID, employee.FirstName, employee.LastName);
            }
            catch (Exception ex )
            {

                throw new ApplicationException("Unable to deactivate Employee: " + employee.EmployeeID, ex);
            }
            return isDeactivated;
        }

        public bool DeleteEmployee(Employee employee)
        {
            bool isDeleted = false;
            try
            {
                isDeleted = _userAccessor.DeleteEmployee(employee.EmployeeID, employee.FirstName, employee.LastName);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Unable to delete Employee: " + employee.EmployeeID, ex);
            }
            return isDeleted;
        }

        public List<Customer> GetCustomersByActive(bool active = true)
        {
            try
            {
                return _userAccessor.GetCustomerByActive(active);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not found", ex);
            }
        }

        public List<Employee> GetEmployeesByActive(bool active = true)
        {
            try
            {
                return _userAccessor.GetEmployeesByActive(active);
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Data not found", ex);
            }
        }

        public bool ReActivateEmployee(Employee employee)
        {
            bool isReactivated = false;
            try
            {
                isReactivated = _userAccessor.ReActivateEmployee(employee.EmployeeID, employee.FirstName, employee.LastName);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Unable to Re-activate Employee: " + employee.EmployeeID, ex);
            }
            return isReactivated;
        }

        public bool UpdateEmployeeInfo(Employee oldEmployee, Employee updatedEmployee)
        {
            bool isUpdated = false;
            try
            {
                isUpdated = _userAccessor.UpdateEmployeeInfo(oldEmployee, updatedEmployee);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Updated Failed", ex);
            }
            return isUpdated;
        }

        public bool UpdatePassword(int employeeId, string oldPassword, string newPassword)
        {
            bool isUpdated = false;

            string newPasswordHash = hashPassword(newPassword);
            string oldPasswordHash = hashPassword(oldPassword);

            try
            {
                isUpdated = _userAccessor.UpdatePasswordHash(employeeId, oldPasswordHash, newPasswordHash);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Updated Failed", ex);
            }
            return isUpdated;
        }

        public bool UpdatePassword(string email, string newPassword)
        {
            bool isUpdated = false;

            string newPasswordHash = hashPassword(newPassword);

            try
            {
                isUpdated = _userAccessor.UpdatePasswordHash(email, newPasswordHash);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Updated Failed", ex);
            }
            return isUpdated;
        }

        private string hashPassword(string source)
        {
            string result;
            byte[] data;
            using (SHA256 sha256hash = SHA256.Create())
            {
                data = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(source));
            }

            var s = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }
            result = s.ToString().ToUpper();
            return result;
        }
    }
}
