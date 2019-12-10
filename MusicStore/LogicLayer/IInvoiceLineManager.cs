using System.Collections.Generic;
using DataObjects;

namespace LogicLayer
{
    public interface IInvoiceLineManager
    {
        bool AddInvoiceLine(InvoiceLine invoiceLIne);
        List<InvoiceLineVM> GetAllInvoiceLInesByInvoiceID(int invoiceID);
    }
}
