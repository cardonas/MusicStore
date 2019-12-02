using DataObjects;
using System.Collections.Generic;

namespace DataAccessLayer
{
    public interface IInstrumentAccessor
    {
        List<InstrumentVm> SelectAllInstruments();

        List<string> SelectAllInstrumentType();

        List<string> SelectAllInstrumentFamily();

        List<string> SelectAllInstrumentBrands();

        List<string> SelectAllInstrumentStatusIDs();

        InstrumentTypeVm SelectInstrumentTypeByInstrumentTypeId(string instrumentTypeId);

        bool UpdateInstrumentStatus(Instrument oldInstrument, Instrument newInstrument);

        bool InsertInstrument(Instrument instrument);

        List<InstrumentVm> SelectInstrumentsByStatus(string status);

    }
}
