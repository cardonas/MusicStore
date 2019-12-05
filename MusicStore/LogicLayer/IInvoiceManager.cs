using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IInvoiceManager
    {
        int CreateInvoice(int customerId, int employeeId, DateTime transactionDate, decimal total);
    }
}
