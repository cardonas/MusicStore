﻿using System;
using System.Collections.Generic;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    public class InvoiceManager : IInvoiceManager
    {
        private readonly IInvoiceAccessor _invoiceAccessor;

        public InvoiceManager()
        {
            _invoiceAccessor = new InvoiceAccessor();
        }

        public int CreateInvoice(int customerId, int employeeId, DateTime transactionDate, decimal total)
        {
            try
            {
              return _invoiceAccessor.InsertInvoice(customerId, employeeId, transactionDate, total);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to create a new invoice", ex);
            }
        }

        public List<Invoice> GetAllInvoices()
        {
            try
            {
                return _invoiceAccessor.SelectAllInvoices();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There are no Invoices", ex);
            }
        }
    }
}
