using DataObjects;
using System.Collections.Generic;

namespace LogicLayer
{
    public interface IInstrumentManager
    {
        List<InstrumentVm> GetAllInstrument();

        List<InstrumentVm> GetInstrumentsByStatus(string status);

        List<string> GetAllInstrumentTypes();

        List<string> GetAllInstrumentFamilies();

        List<string> GetAllInstrumentBrands();

        List<string> GetAllInstrumentStatusIDs();

        InstrumentTypeVm GetInstrumentTypeByInstrumentTypeId(string instrumentTypeId);

        bool UpdateInstrumentStatus(Instrument oldInstrument, Instrument newInstrument);

        bool AddInstrument(Instrument instrument);
    }
}
