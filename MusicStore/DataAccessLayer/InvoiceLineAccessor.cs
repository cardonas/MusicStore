using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public class InvoiceLineAccessor : IInvoiceLineAccessor
    {
        public bool InsertInvoiceLIne(InvoiceLine invoiceLine)
        {
            bool isInserted;
            var conn = DbConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_invoice_line", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@InvoiceID", invoiceLine.InvoiceID);
            cmd.Parameters.AddWithValue("@InstrumentID", invoiceLine.InstrumentID);
            cmd.Parameters.AddWithValue("@RepairTicketID", ((object)invoiceLine.RepairTicketID) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@RentToOwnID", ((object)invoiceLine.RentToOwnID) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@LineTotal", invoiceLine.LineTotal);

            try
            {
                conn.Open();
                isInserted = 1 == cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isInserted;
        }
    }
}
