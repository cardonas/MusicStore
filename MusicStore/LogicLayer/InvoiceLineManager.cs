using System;
using System.Collections.Generic;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    public class InvoiceLineManager : IInvoiceLineManager
    {
        private readonly IInvoiceLineAccessor _invoiceLineAccessor;

        public InvoiceLineManager() => _invoiceLineAccessor= new InvoiceLineAccessor();

        public bool AddInvoiceLine(InvoiceLine invoiceLIne)
        {
            bool isAdded;
            try
            {
                isAdded = _invoiceLineAccessor.InsertInvoiceLIne(invoiceLIne);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to add Invoice LIne", ex);
            }

            return isAdded;
        }

        public List<InvoiceLineVM> GetAllInvoiceLInesByInvoiceID(int invoiceID)
        {
            try
            {
                return _invoiceLineAccessor.SelectInvoiceLinesByInvoiceID(invoiceID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No Items found for this invoice", ex);
            }
        }
    }
}
