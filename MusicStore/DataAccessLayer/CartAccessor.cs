using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataObjects;

namespace DataAccessLayer
{
    public class CartAccessor : ICartAccessor
    {
        public List<InstrumentVm> SelectAllInCart()
        {
            List<InstrumentVm> instruments = new List<InstrumentVm>();
            var conn = DbConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_all_in_cart", conn)
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
                            InstrumentStatusId = reader.GetString(3),
                            InstrumentBrandId = reader.GetString(4),
                            Price = reader.GetDecimal(5),
                            RentalTermId = reader.GetString(6),
                            RentalFee = reader.GetDecimal(7),
                        };
                        instruments.Add(newInstrument);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return instruments;
        }

        public bool InsertCartItem(Instrument instrument)
        {
            bool isInserted;

            var conn = DbConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_cart_item", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@InstrumentID", instrument.InstrumentId);
            cmd.Parameters.AddWithValue("@InstrumentTypeID", instrument.InstrumentTypeId);
            cmd.Parameters.AddWithValue("@InstrumentStatusID", instrument.InstrumentStatusId);
            cmd.Parameters.AddWithValue("@InstrumentBrandID", instrument.InstrumentBrandId);
            cmd.Parameters.AddWithValue("@Price", instrument.Price);

            try
            {
                conn.Open();
                isInserted = 2 == cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
            return isInserted;
        }
    }
}
