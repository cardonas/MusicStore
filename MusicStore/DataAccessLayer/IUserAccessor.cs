using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public interface IUserAccessor
    {
        Employee AuthenticateUser(string email, string Password);

        bool UpdatePasswordHash(int employeeID, string oldPassowrdHash, string newPasswordHash);

        List<Employee> GetEmployeesByActive(bool active = true);

        List<Customer> GetCustomerByActive(bool active = true);

        bool AddNewEmployee(Employee employee);

        bool UpdateEmployeeInfo(Employee oldEmployee, Employee updatedEmployee);

        bool DeactivateEmployee(int employeeID, string firstName, string lastName);

        bool ReActivateEmployee(int employeeID, string firstName, string lastName);

        bool DeleteEmployee(int employeeID, string firstName, string lastName);

    }
}
