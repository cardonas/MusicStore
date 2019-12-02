using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace LogicLayer
{
    public class UserManager : IUserManager
    {
        private readonly IUserAccessor _userAccessor;

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
           
                bool isAdded;

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
            Employee result;
            var passwordHash = HashPassword(password);
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
            bool isDeactivated;
            try
            {
                isDeactivated = _userAccessor.DeactivateEmployee(employee.EmployeeId, employee.FirstName, employee.LastName);
            }
            catch (Exception ex )
            {

                throw new ApplicationException("Unable to deactivate Employee: " + employee.EmployeeId, ex);
            }
            return isDeactivated;
        }

        public bool DeleteEmployee(Employee employee)
        {
            bool isDeleted;
            try
            {
                isDeleted = _userAccessor.DeleteEmployee(employee.EmployeeId, employee.FirstName, employee.LastName);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Unable to delete Employee: " + employee.EmployeeId, ex);
            }
            return isDeleted;
        }

        public List<Customer> GetCustomersByActive(bool active = true)
        {
            try
            {
                return _userAccessor.SelectCustomerByActive(active);
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
                return _userAccessor.SelectEmployeesByActive(active);
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Data not found", ex);
            }
        }

        public bool ReActivateEmployee(Employee employee)
        {
            bool isReactivated;
            try
            {
                isReactivated = _userAccessor.ReActivateEmployee(employee.EmployeeId, employee.FirstName, employee.LastName);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Unable to Re-activate Employee: " + employee.EmployeeId, ex);
            }
            return isReactivated;
        }

        public bool UpdateEmployeeInfo(Employee oldEmployee, Employee updatedEmployee)
        {
            bool isUpdated;
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
            bool isUpdated;

            string newPasswordHash = HashPassword(newPassword);
            string oldPasswordHash = HashPassword(oldPassword);

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
            bool isUpdated;

            string newPasswordHash = HashPassword(newPassword);

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

        private string HashPassword(string source)
        {
            string result;
            byte[] data;
            using (SHA256 sha256Hash = SHA256.Create())
            {
                data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(source));
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
