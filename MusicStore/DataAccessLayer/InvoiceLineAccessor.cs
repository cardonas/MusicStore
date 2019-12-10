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

        public List<InvoiceLineVM> SelectInvoiceLinesByInvoiceID(int invoiceID)
        {
            List<InvoiceLineVM> invoiceLines = new List<InvoiceLineVM>();
            var conn = DbConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_invoice_lines_by_invoiceID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@InvoiceID", invoiceID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    invoiceLines.Add(new InvoiceLineVM(){ 
                        InvoiceLineID = reader.GetInt32(0),
                        InvoiceID = invoiceID,
                        InstrumentID = reader.GetString(2),
                        InstrumentTypeID = reader.GetString(3),
                        InstrumentBrandID = reader.GetString(4),
                        RepairTicketID = reader.IsDBNull(5) ? (int?) null : reader.GetInt32(5),
                        RentToOwnID = reader.IsDBNull(6) ? (int?) null : reader.GetInt32(6),
                        LineTotal = reader.GetDecimal(7)
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return invoiceLines;
        }
    }
}
