using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IInstrumentAccessor
    {
        List<InstrumentVM> GetAllInstruments();

        List<string> SelectAllInstrumentType();

        List<string> SelectAllInstrumentFamily();

        List<string> SelectAllInstrumentBrands();

        List<string> SelectAllInstrumentStatusIDs();

        InstrumentTypeVM SelectInstrumentTypeByInstrumentTypeID(string instrumentTypeID);

        bool UpdateInstrumentStatus(Instrument oldInstrument, Instrument newInstrument);

        bool InsertInstrumnet(Instrument instrument);


    }
}
