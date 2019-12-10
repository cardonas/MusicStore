using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public interface IInvoiceAccessor
    {
        int InsertInvoice(int customerId, int employeeId, DateTime transactionDate, decimal total);

        List<Invoice> SelectAllInvoices();
    }
}
