using System.Collections.Generic;
using DataObjects;

namespace DataAccessLayer
{
    public interface IInvoiceLineAccessor
    {
        bool InsertInvoiceLIne(InvoiceLine invoiceLine);

        List<InvoiceLineVM> SelectInvoiceLinesByInvoiceID(int invoiceID);
    }
}
