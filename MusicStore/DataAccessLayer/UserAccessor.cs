using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using DataObjects;

namespace DataAccessLayer
{
    public class UserAccessor : IUserAccessor
    {
        public bool AddNewEmployee(Employee employee)
        {
            bool addSuccess;

            var conn = DbConnection.GetConnection();
            var cmd = new SqlCommand("sp_create_employee", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
            cmd.Parameters.AddWithValue("@LastName", employee.LastName);
            cmd.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);

            try
            {
                conn.Open();

                addSuccess = (1 == cmd.ExecuteNonQuery());

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return addSuccess;
        }

        public Employee AuthenticateUser(string username, string passwordHash)
        {
            Employee result;

            var conn = DbConnection.GetConnection();


            var cmd = new SqlCommand("sp_authenticate_employee", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Email", username);
            cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);

            try
            {
                conn.Open();

                if (1 == Convert.ToInt32(cmd.ExecuteScalar()))
                {
                    result = GetUserByEmail(username);
                }
                else
                {
                    throw new ApplicationException("User not found.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public bool DeactivateEmployee(int employeeId, string firstName, string lastName)
        {
            bool deactivateSuccess;
            var conn = DbConnection.GetConnection();
            var cmd = new SqlCommand("sp_deactivate_employee", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
            cmd.Parameters.AddWithValue("@FirstName", firstName);
            cmd.Parameters.AddWithValue("@LastName", lastName);

            try
            {
                conn.Open();
                deactivateSuccess = (1 == cmd.ExecuteNonQuery());
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }



            return deactivateSuccess;
        }

        public bool DeleteEmployee(int employeeId, string firstName, string lastName)
        {
            bool deactivateSuccess;
            var conn = DbConnection.GetConnection();
            var cmd = new SqlCommand("sp_delete_employee", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
            cmd.Parameters.AddWithValue("@FirstName", firstName);
            cmd.Parameters.AddWithValue("@LastName", lastName);

            try
            {
                conn.Open();
                deactivateSuccess = (0 < cmd.ExecuteNonQuery());
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }



            return deactivateSuccess;
        }

        public List<Customer> SelectCustomerByActive(bool active = true)
        {
            List<Customer> users = new List<Customer>();
            var conn = DbConnection.GetConnection();
            var cmd = new SqlCommand("sp_selectall_customers", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@Active", active);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var user = new Customer
                        {
                            CustomerId = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            PhoneNumber = reader.GetString(3),
                            Email = reader.GetString(4)
                        };

                        users.Add(user);
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return users;
        }

        public List<Employee> SelectEmployeesByActive(bool active = true)
        {
            List<Employee> users = new List<Employee>();
            var conn = DbConnection.GetConnection();
            var cmd = new SqlCommand("sp_selectall_employees", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@Active", active);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var user = new Employee
                        {
                            EmployeeId = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            PhoneNumber = reader.GetString(3),
                            Email = reader.GetString(4)
                        };

                        users.Add(user);
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return users;
        }

        public bool ReActivateEmployee(int employeeId, string firstName, string lastName)
        {
            bool reactivateSuccess;
            var conn = DbConnection.GetConnection();
            var cmd = new SqlCommand("sp_reactivate_employee", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
            cmd.Parameters.AddWithValue("@FirstName", firstName);
            cmd.Parameters.AddWithValue("@LastName", lastName);

            try
            {
                conn.Open();
                reactivateSuccess = (1 == cmd.ExecuteNonQuery());
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }



            return reactivateSuccess;
        }

        public bool UpdateEmployeeInfo(Employee oldEmployee, Employee updatedEmployee)
        {
            bool updateSuccess;

            var conn = DbConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_employee_profile", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@EmployeeID", oldEmployee.EmployeeId);

            cmd.Parameters.AddWithValue("@OldFirstName", oldEmployee.FirstName);
            cmd.Parameters.AddWithValue("@OldLastName", oldEmployee.LastName);
            cmd.Parameters.AddWithValue("@OldEmail", oldEmployee.Email);
            cmd.Parameters.AddWithValue("@OldPhoneNumber", oldEmployee.PhoneNumber);

            cmd.Parameters.AddWithValue("@NewFirstName", updatedEmployee.FirstName);
            cmd.Parameters.AddWithValue("@NewLastName", updatedEmployee.LastName);
            cmd.Parameters.AddWithValue("@NewEmail", updatedEmployee.Email);
            cmd.Parameters.AddWithValue("@NewPhoneNumber", updatedEmployee.PhoneNumber);

            try
            {
                conn.Open();

                updateSuccess = (1 == cmd.ExecuteNonQuery());

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return updateSuccess;
        }

        public bool UpdatePasswordHash(int employeeId, string oldPasswordHash, string newPasswordHash)
        {
            bool updateSuccess;
            var conn = DbConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_employee_password", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
            cmd.Parameters.AddWithValue("@OldPasswordHash", oldPasswordHash);
            cmd.Parameters.AddWithValue("@NewPasswordHash", newPasswordHash);

            //cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);
            //cmd.Parameters.Add("@OldPasswordHash", SqlDbType.NVarChar, 100);
            //cmd.Parameters.Add("@NewPasswordHash", SqlDbType.NVarChar, 100);

            //cmd.Parameters["@EmployeeID"].Value = employeeId;
            //cmd.Parameters["@OldPasswordHash"].Value = oldPasswordHash;
            //cmd.Parameters["@NewPasswordHash"].Value = newPasswordHash;

            try
            {
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                updateSuccess = (rows == 1);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return updateSuccess;
        }

        public bool UpdatePasswordHash(string email, string newPasswordHash)
        {
            bool updateSuccess;
            var conn = DbConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_employee_password_by_email", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@NewPasswordHash", newPasswordHash);

            //cmd.Parameters.Add("@Email", SqlDbType.Int);
            //cmd.Parameters.Add("@NewPasswordHash", SqlDbType.NVarChar, 100);

            //cmd.Parameters["@Email"].Value = email;
            //cmd.Parameters["@NewPasswordHash"].Value = newPasswordHash;

            try
            {
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                updateSuccess = (rows == 1);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return updateSuccess;
        }

        private static Employee GetUserByEmail(string email)
        {
            Employee user;
            var conn = DbConnection.GetConnection();
            var cmd1 = new SqlCommand("sp_retrieve_employee_by_email", conn);
            var cmd2 = new SqlCommand("sp_select_all_roles_for_employeeID", conn);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@Email", email);
            cmd2.Parameters.Add("@EmployeeID", SqlDbType.Int);

            try
            {
                conn.Open();

                var reader1 = cmd1.ExecuteReader();

                if (reader1.Read())
                {
                    user = new Employee
                    {
                        EmployeeId = reader1.GetInt32(0),
                        FirstName = reader1.GetString(1),
                        LastName = reader1.GetString(2),
                        PhoneNumber = reader1.GetString(3),
                        Email = email
                    };
                }
                else
                {
                    throw new ApplicationException("User not found");
                }
                reader1.Close();
                cmd2.Parameters["@EmployeeID"].Value = user.EmployeeId;
                var reader2 = cmd2.ExecuteReader();
                List<string> roles = new List<string>();
                while (reader2.Read())
                {
                    string role = reader2.GetString(0);
                    roles.Add(role);
                }
                user.Roles = roles;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return user;
        }
    }
}
