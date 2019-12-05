using System.Collections.Generic;
using System.Data.SqlTypes;
using DataObjects;

namespace LogicLayer
{
    public interface ICartManager
    {
        List<InstrumentVm> GetAllInCart();

        bool AddCartItem(Instrument instrument);

        bool DeleteCartItem(string instrumentId);
    }
}
