using System;
using System.Collections.Generic;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    public class CartManager : ICartManager
    {
        private readonly ICartAccessor _cartAccessor;

        public CartManager()
        {
            _cartAccessor = new CartAccessor();
        }

        public List<InstrumentVm> GetAllInCart()
        {
            List<InstrumentVm> instruments;
            try
            {
                instruments = _cartAccessor.SelectAllInCart();
            }
            catch (Exception e)
            {
                throw new ApplicationException("No Items in Cart", e);
            }

            return instruments;
        }

        public bool AddCartItem(Instrument instrument)
        {
            bool isInserted;
            try
            {
                isInserted = _cartAccessor.InsertCartItem(instrument);
            }
            catch (Exception e)
            {
                throw new ApplicationException("Unable to add item to cart", e);
            }
            return isInserted;
        }
    }
}
