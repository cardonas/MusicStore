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
        List<InstrumentVM> GetAllInstruments(bool active = true);

        List<string> SelectAllInstrumentType();

        List<string> SelectAllInstrumentFamily();

        List<string> SelectAllInstrumentBrands();

        List<string> SelectAllInstrumentStatusIDs();

        InstrumentTypeVM SelectInstrumentTypeByInstrumentTypeID(string instrumentTypeID);
    }
}
