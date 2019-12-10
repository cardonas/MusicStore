using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataObjects;

namespace DataAccessLayer
{
    public class CustomerAccessor : ICustomerAccessor
    {
        public List<Customer> SelectCustomerByEmailLike(string query)
        {
            List<Customer> customers = new List<Customer>();

            var conn = DbConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_customer_by_email_like", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Email", query);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    customers.Add(new Customer()
                    {
                        CustomerId = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        PhoneNumber = reader.GetString(3),
                        Email = reader.GetString(4)
                    });
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return customers;
        }

        public List<Customer> SelectAllCustomers(bool active)
        {
            List<Customer> customers = new List<Customer>();

            var conn = DbConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_all_customers", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Active", active);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    customers.Add(new Customer()
                    {
                        CustomerId = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        PhoneNumber = reader.GetString(3),
                        Email = reader.GetString(4)
                    });
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return customers;
        }

        public bool UpdateCustomerProfile(Customer oldCustomer, Customer newCustomer)
        {
            bool isUpdated;
            var conn = DbConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_customer_profile", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@CustomerID", oldCustomer.CustomerId);
            cmd.Parameters.AddWithValue("@OldFirstName", oldCustomer.FirstName);
            cmd.Parameters.AddWithValue("@OldLastName", oldCustomer.LastName);
            cmd.Parameters.AddWithValue("@OldPhoneNumber", oldCustomer.PhoneNumber);
            cmd.Parameters.AddWithValue("@OldEmail", oldCustomer.Email);

            cmd.Parameters.AddWithValue("@NewFirstName", newCustomer.FirstName);
            cmd.Parameters.AddWithValue("@NewLastName", newCustomer.LastName);
            cmd.Parameters.AddWithValue("@NewPhoneNumber", newCustomer.PhoneNumber);
            cmd.Parameters.AddWithValue("@NewEmail", newCustomer.Email);

            try
            {
                conn.Open();
                isUpdated = 1 == cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return isUpdated;
        }

        public bool InsertCustomer(Customer customer)
        {
            bool isAdded;
            var conn = DbConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_customer", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
            cmd.Parameters.AddWithValue("@LastName", customer.LastName);
            cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
            cmd.Parameters.AddWithValue("@Email", customer.Email);

            try
            {
                conn.Open();
                isAdded = 1 == cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return isAdded;
        }

        public Customer SelectCustomerByEmail(string email)
        {
            Customer customer = null;
            var conn = DbConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_customer_by_email", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Email", email);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    customer = new Customer()
                    {
                        CustomerId = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        PhoneNumber = reader.GetString(3),
                        Email = email
                    };
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

            return customer;
        }
    }
}
