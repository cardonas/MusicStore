using System.Collections.Generic;
using DataObjects;

namespace DataAccessLayer
{
    public interface IUserAccessor
    {
        Employee AuthenticateUser(string email, string password);

        bool UpdatePasswordHash(int employeeId, string oldPasswordHash, string newPasswordHash);

        List<Employee> GetEmployeesByActive(bool active = true);

        List<Customer> GetCustomerByActive(bool active = true);

        bool AddNewEmployee(Employee employee);

        bool UpdateEmployeeInfo(Employee oldEmployee, Employee updatedEmployee);

        bool DeactivateEmployee(int employeeId, string firstName, string lastName);

        bool ReActivateEmployee(int employeeId, string firstName, string lastName);

        bool DeleteEmployee(int employeeId, string firstName, string lastName);

        bool UpdatePasswordHash(string email, string newPasswordHash);

    }
}
