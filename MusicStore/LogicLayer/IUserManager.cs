using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IUserManager
    {
        Employee AuthenticateUser(string email, string password);

        bool UpdatePassword(int employeeId, string oldPassowrd, string newPassword);

        bool UpdatePassword(string email, string newPassword);

        List<Employee> GetEmployeesByActive(bool active = true);

        List<Customer> GetCustomersByActive(bool active = true);

        bool AddNewEmployee(Employee employee);

        bool UpdateEmployeeInfo(Employee oldEmployee, Employee updatedEmployee);

        bool DeactivateEmployee(Employee employee);

        bool ReActivateEmployee(Employee employee);

        bool DeleteEmployee(Employee employee);

    }
}
