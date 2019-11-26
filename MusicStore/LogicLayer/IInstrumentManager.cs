using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IInstrumentManager
    {
        List<InstrumentVM> GetAllInstrument();

        List<InstrumentVM> GetInstrumentsByStatus(string status);

        List<string> GetAllInstrumentTypes();

        List<string> GetAllInstrumentFamilies();

        List<string> GetAllInstrumentBrands();

        List<string> GetAllInstrumentStatusIDs();

        InstrumentTypeVM GetInstrumentTypeByInstrumentTypeID(string instrumentTypeID);

        bool UpdateInstrumentStatus(Instrument oldInstrument, Instrument newInstrument);

        bool AddInstrument(Instrument instrument);
    }
}
