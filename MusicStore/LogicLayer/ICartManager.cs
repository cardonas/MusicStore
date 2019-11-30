using System.Collections.Generic;
using DataObjects;

namespace LogicLayer
{
    public interface ICartManager
    {
        List<InstrumentVm> GetAllInCart();

        bool AddCartItem(Instrument instrument);
    }
}
