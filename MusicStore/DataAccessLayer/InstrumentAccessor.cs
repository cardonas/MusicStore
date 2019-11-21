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
        public List<Instrument> GetAllInstruments(bool active = true)
        {
            List<Instrument> instruments = new List<Instrument>();
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_get_all_Instruments", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Active", active);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Instrument newInsturment = new Instrument();
                        newInsturment.InstrumentID = reader.GetString(0);
                        newInsturment.InstrumentTypeID = reader.GetString(1);
                        newInsturment.InstrumentStatusID = reader.GetString(2);
                        newInsturment.Brand = reader.GetString(3);
                        newInsturment.Price = reader.GetDecimal(4);
                        instruments.Add(newInsturment);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return instruments;
        }
    }
}
