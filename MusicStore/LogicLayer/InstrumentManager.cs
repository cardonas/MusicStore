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

        public bool AddInstrument(Instrument instrument)
        {
            bool isAdded;
            try
            {
                isAdded = _instrumentAccessor.InsertInstrumnet(instrument);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Instrument Not Added");
            }
            return isAdded;
        }


        public List<InstrumentVM> GetAllInstrument()
        {
            try
            {
                return _instrumentAccessor.GetAllInstruments();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not found", ex);
            }
        }

        public List<string> GetAllInstrumentBrands()
        {
            try
            {
                return _instrumentAccessor.SelectAllInstrumentBrands();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("No Instrument Brands Found", ex);
            }
        }

        public List<string> GetAllInstrumentFamilies()
        {
            try
            {
                return _instrumentAccessor.SelectAllInstrumentFamily();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("No Instrument Families Found", ex);
            }
        }


        public List<string> GetAllInstrumentStatusIDs()
        {
            try
            {
                return _instrumentAccessor.SelectAllInstrumentStatusIDs();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("No Instrument Statuses Found", ex);
            }
        }

        public List<string> GetAllInstrumentTypes()
        {
            try
            {
                return _instrumentAccessor.SelectAllInstrumentType();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("No Instrument Types Found", ex);
            }
        }

        public List<InstrumentVM> GetInstrumentsByStatus(string status)
        {
            try
            {
                return _instrumentAccessor.SelectInstrumentsByStatus(status);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not found", ex);
            }
        }

        public InstrumentTypeVM GetInstrumentTypeByInstrumentTypeID(string instrumentTypeID)
        {
            try
            {
                return _instrumentAccessor.SelectInstrumentTypeByInstrumentTypeID(instrumentTypeID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No Instrument types with " + instrumentTypeID, ex);
            }
        }

        public bool UpdateInstrumentStatus(Instrument oldInstrument, Instrument newInstrument)
        {
            bool isUpdated;
            try
            {
                isUpdated = _instrumentAccessor.UpdateInstrumentStatus(oldInstrument, newInstrument);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to update Instrument Status" , ex);
            }

            return isUpdated;

        }
    }
}
