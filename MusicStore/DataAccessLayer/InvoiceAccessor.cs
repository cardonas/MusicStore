using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataObjects;

namespace DataAccessLayer
{
    public class InvoiceAccessor : IInvoiceAccessor
    {
        public int InsertInvoice(int customerId, int employeeId, DateTime transactionDate, decimal total)
        {
            int invoiceId;

            var conn = DbConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_invoice", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@CustomerID", customerId);
            cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
            cmd.Parameters.AddWithValue("@TransactionDate", transactionDate);
            cmd.Parameters.AddWithValue("@Total", total);

            try
            {
                conn.Open();
                invoiceId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return invoiceId;
        }

        public List<Invoice> SelectAllInvoices()
        {
            List<Invoice> invoices = new List<Invoice>();

            var conn = DbConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_all_invoices", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    invoices.Add(new Invoice()
                    {
                        InvoiceId = reader.GetInt32(0),
                        CustomerFirstName = reader.GetString(1),
                        CustomerLastName = reader.GetString(2),
                        EmployeeFirstName = reader.GetString(3),
                        EmployeeLastName = reader.GetString(4),
                        TransactionDate = reader.GetDateTime(5),
                        Total = reader.GetDecimal(6)
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return invoices;
        }
    }
}
