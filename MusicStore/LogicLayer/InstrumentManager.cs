using System;
using System.Collections.Generic;
using DataObjects;
using DataAccessLayer;

namespace LogicLayer
{
    public class InstrumentManager : IInstrumentManager
    {
        private readonly IInstrumentAccessor _instrumentAccessor;
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
                isAdded = _instrumentAccessor.InsertInstrument(instrument);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Instrument Not Added", ex);
            }
            return isAdded;
        }


        public List<InstrumentVm> GetAllInstrument()
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

        public List<InstrumentVm> GetInstrumentsByStatus(string status)
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

        public InstrumentTypeVm GetInstrumentTypeByInstrumentTypeId(string instrumentTypeId)
        {
            try
            {
                return _instrumentAccessor.SelectInstrumentTypeByInstrumentTypeId(instrumentTypeId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No Instrument types with " + instrumentTypeId, ex);
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
