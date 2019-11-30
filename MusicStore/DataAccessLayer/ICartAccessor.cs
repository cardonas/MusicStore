using System.Collections.Generic;
using DataObjects;

namespace DataAccessLayer
{
    public interface ICartAccessor
    {
        List<InstrumentVm> SelectAllInCart();

        bool InsertCartItem(Instrument instrument);
    }
}
