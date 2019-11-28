using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using DataObjects;

namespace DataAccessLayer
{
    [SuppressMessage("ReSharper", "PossibleIntendedRethrow")]
    public class InstrumentAccessor : IInstrumentAccessor
    {
        public List<InstrumentVm> GetAllInstruments()
        {
            List<InstrumentVm> instruments = new List<InstrumentVm>();
            var conn = DbConnection.GetConnection();
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
                        InstrumentVm newInstrument = new InstrumentVm
                        {
                            InstrumentId = reader.GetString(0),
                            InstrumentTypeId = reader.GetString(1),
                            InstrumentFamily = reader.GetString(2),
                            InstrumentStatusId = reader.GetString(3),
                            InstrumentBrandId = reader.GetString(4),
                            Price = reader.GetDecimal(5),
                            RentalTermId = reader.GetString(6),
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

        public bool InsertInstrument(Instrument instrument)
        {
            bool isAdded;

            var conn = DbConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_instrument", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@InstrumentID", instrument.InstrumentId);
            cmd.Parameters.AddWithValue("@InstrumentTypeID", instrument.InstrumentTypeId);
            cmd.Parameters.AddWithValue("@instrumentBrandID", instrument.InstrumentBrandId);
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
            List<string> instrumentBrands = new List<string>();

            var conn = DbConnection.GetConnection();
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
                    var instrumentBrand = reader.GetString(0);
                    instrumentBrands.Add(instrumentBrand);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return instrumentBrands;
        }

        public List<string> SelectAllInstrumentFamily()
        {
            List<string> instrumentFamilies = new List<string>();

            var conn = DbConnection.GetConnection();
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
                    var instrumentFamily = reader.GetString(0);
                    instrumentFamilies.Add(instrumentFamily);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return instrumentFamilies;
        }


        public List<string> SelectAllInstrumentStatusIDs()
        {
            List<string> instrumentStatuses = new List<string>();

            var conn = DbConnection.GetConnection();
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
                    instrumentStatuses.Add(instrumentStatus);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return instrumentStatuses;
        }

        public List<string> SelectAllInstrumentType()
        {
            List<string> instrumentTypes = new List<string>();

            var conn = DbConnection.GetConnection();
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

        public List<InstrumentVm> SelectInstrumentsByStatus(string status)
        {
            List<InstrumentVm> instruments = new List<InstrumentVm>();
            var conn = DbConnection.GetConnection();
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
                        InstrumentVm newInstrument = new InstrumentVm
                        {
                            InstrumentId = reader.GetString(0),
                            InstrumentTypeId = reader.GetString(1),
                            InstrumentFamily = reader.GetString(2),
                            InstrumentStatusId = reader.GetString(3),
                            InstrumentBrandId = reader.GetString(4),
                            Price = reader.GetDecimal(5),
                            RentalTermId = reader.GetString(6),
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


        public InstrumentTypeVm SelectInstrumentTypeByInstrumentTypeId(string instrumentTypeId)
        {
            InstrumentTypeVm instrumentType = new InstrumentTypeVm();
            var conn = DbConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_instrument_type_by_InstrumentTypeID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@InstrumentTypeID", instrumentTypeId);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    instrumentType.InstrumentTypeId = instrumentTypeId;
                    instrumentType.RentalTermId = reader.GetString(0);
                    instrumentType.RentalFee = reader.GetDecimal(1);
                    instrumentType.InstrumentFamilyId = reader.GetString(2);
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

            var conn = DbConnection.GetConnection();
            SqlCommand cmd = new SqlCommand("sp_update_instrumentStatus", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@InstrumentID", oldInstrument.InstrumentId);
            cmd.Parameters.AddWithValue("@OldStatus", oldInstrument.InstrumentStatusId);
            cmd.Parameters.AddWithValue("@OldPrice", oldInstrument.Price);
            cmd.Parameters.AddWithValue("@NewStatus", newInstrument.InstrumentStatusId);
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
