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
        List<Instrument> GetAllInstrument(bool active = true);
    }
}
