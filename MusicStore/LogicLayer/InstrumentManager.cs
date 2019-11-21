using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;

namespace LogicLayer
{
    public class InstrumentManager : IInstrumentManager
    {
        private IInstrumentAccessor _instrumentAccessor;
        public InstrumentManager()
        {
            _instrumentAccessor = new InstrumentAccessor();
        }

        public InstrumentManager(InstrumentAccessor instrumentAccessor)
        {
            _instrumentAccessor = instrumentAccessor;
        }

        public List<Instrument> GetAllInstrument(bool active = true)
        {
            try
            {
                return _instrumentAccessor.GetAllInstruments(active);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not found", ex);
            }
        }
    }
}
