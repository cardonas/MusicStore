﻿using System;
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
    }
}