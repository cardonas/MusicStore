using DataObjects;

namespace DataAccessLayer
{
    public interface IInvoiceLineAccessor
    {
        bool InsertInvoiceLIne(InvoiceLine invoiceLine);
    }
}
