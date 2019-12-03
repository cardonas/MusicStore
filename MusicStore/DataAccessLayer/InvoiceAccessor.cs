using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class InvoiceAccessor : IInvoiceAccessor
    {
        public int InsertInvoice(int customerId, int employeeId, DateTime transactionDate, decimal total)
        {
            int invoiceId  = 0;

            var conn = DbConnection.GetConnection();
            var cmd = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@CustomerID", customerId);
            cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
            cmd.Parameters.AddWithValue("@TransactionID", transactionDate);
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
    }
}
