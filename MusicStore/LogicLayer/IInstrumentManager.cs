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
        List<InstrumentVM> GetAllInstrument(bool active = true);

        List<string> GetAllInstrumentTypes();

        List<string> GetAllInstrumentFamilies();

        List<string> GetAllInstrumentBrands();

        List<string> GetAllInstrumentStatusIDs();

        InstrumentTypeVM GetInstrumentTypeByInstrumentTypeID(string instrumentTypeID);
    }
}
