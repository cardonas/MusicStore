using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public class InstrumentAccessor : IInstrumentAccessor
    {
        public List<InstrumentVM> GetAllInstruments()
        {
            List<InstrumentVM> instruments = new List<InstrumentVM>();
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_get_all_Instruments", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        InstrumentVM newInstrument = new InstrumentVM
                        {
                            InstrumentID = reader.GetString(0),
                            InstrumentTypeID = reader.GetString(1),
                            InstrumentFamily = reader.GetString(2),
                            InstrumentStatusID = reader.GetString(3),
                            InstrumentBrandID = reader.GetString(4),
                            Price = reader.GetDecimal(5),
                            RentalTermID = reader.GetString(6),
                            RentalFee = reader.GetDecimal(7),
                            PrepListDescription = reader.GetString(8)
                        };
                        instruments.Add(newInstrument);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return instruments;
        }

        public bool InsertInstrumnet(Instrument instrument)
        {
            bool isAdded;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_instrument", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@InstrumentID", instrument.InstrumentID);
            cmd.Parameters.AddWithValue("@InstrumentTypeID", instrument.InstrumentTypeID);
            cmd.Parameters.AddWithValue("@instrumentBrandID", instrument.InstrumentBrandID);
            cmd.Parameters.AddWithValue("@Price", instrument.Price);

            try
            {
                conn.Open();

                isAdded = 1 == cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isAdded;
        }


        public List<string> SelectAllInstrumentBrands()
        {
            List<string> InstrumentBrands = new List<string>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_get_all_instrument_brands", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var InstrumentBrand = reader.GetString(0);
                    InstrumentBrands.Add(InstrumentBrand);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return InstrumentBrands;
        }

        public List<string> SelectAllInstrumentFamily()
        {
            List<string> InstrumentFamilies = new List<string>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_get_all_Instrument_family", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var InstrumentFamily = reader.GetString(0);
                    InstrumentFamilies.Add(InstrumentFamily);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return InstrumentFamilies;
        }


        public List<string> SelectAllInstrumentStatusIDs()
        {
            List<string> InstrumentStatuses = new List<string>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_all_instrument_Status_IDs", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var instrumentStatus = reader.GetString(0);
                    InstrumentStatuses.Add(instrumentStatus);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return InstrumentStatuses;
        }

        public List<string> SelectAllInstrumentType()
        {
            List<string> instrumentTypes = new List<string>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_get_all_InstrumentType", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var instrumentType = reader.GetString(0);
                    instrumentTypes.Add(instrumentType);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return instrumentTypes;
        }

        public List<InstrumentVM> SelectInstrumentsByStatus(string status)
        {
            List<InstrumentVM> instruments = new List<InstrumentVM>();
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_get_instruments_by_status", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Status", status);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        InstrumentVM newInstrument = new InstrumentVM
                        {
                            InstrumentID = reader.GetString(0),
                            InstrumentTypeID = reader.GetString(1),
                            InstrumentFamily = reader.GetString(2),
                            InstrumentStatusID = reader.GetString(3),
                            InstrumentBrandID = reader.GetString(4),
                            Price = reader.GetDecimal(5),
                            RentalTermID = reader.GetString(6),
                            RentalFee = reader.GetDecimal(7),
                            PrepListDescription = reader.GetString(8)
                        };
                        instruments.Add(newInstrument);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return instruments;
        }


        public InstrumentTypeVM SelectInstrumentTypeByInstrumentTypeID(string instrumentTypeID)
        {
            InstrumentTypeVM instrumentType = new InstrumentTypeVM();
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_instrument_type_by_InstrumentTypeID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@InstrumentTypeID", instrumentTypeID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    instrumentType.InstrumentTypeID = instrumentTypeID;
                    instrumentType.RentalTermID = reader.GetString(0);
                    instrumentType.RentalFee = reader.GetDecimal(1);
                    instrumentType.InstrumentFamilyID = reader.GetString(2);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return instrumentType;
        }

        public bool UpdateInstrumentStatus(Instrument oldInstrument, Instrument newInstrument)
        {
            bool isUpdate;

            var conn = DBConnection.GetConnection();
            SqlCommand cmd = new SqlCommand("sp_update_instrumentStatus", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@InstrumentID", oldInstrument.InstrumentID);
            cmd.Parameters.AddWithValue("@OldStatus", oldInstrument.InstrumentStatusID);
            cmd.Parameters.AddWithValue("@OldPrice", oldInstrument.Price);
            cmd.Parameters.AddWithValue("@NewStatus", newInstrument.InstrumentStatusID);
            cmd.Parameters.AddWithValue("@NewPrice", newInstrument.Price);


            try
            {
                conn.Open();
                isUpdate = 1 == cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isUpdate;
        }
    }
}
