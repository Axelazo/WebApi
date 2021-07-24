using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Models;
using WebApi.Repository.IRepository;

namespace WebApi.Repository
{
    public class ClockingRepository : IClockingRepository
    {
        private readonly IConnection _connection;
        private readonly string _stringConnection;

        public ClockingRepository(IConnection connection)
        {
            this._connection = connection;
            this._stringConnection = connection.GetStringConnection();
        }

        public bool CreateClocking(ClockingModel clocking)
        {
            using (var sqlConnection = _connection.GetSqlConnection(_stringConnection))
            {
                SqlCommand command = new SqlCommand("insert_clocking", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@clocking_employee_id", clocking.employee_id);
                command.Parameters.AddWithValue("@clocking_created_at", clocking.created_at);

                try
                {
                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }

            }
        }

        public bool DeleteClocking(int id)
        {
            throw new NotImplementedException();
        }

        public bool InsertClocking(DateTime dateTime, int input)
        {
            throw new NotImplementedException();
        }

        public List<ClockingModel> ListEmployeeClockings(int employee_id)
        {
            List<ClockingModel> list = new List<ClockingModel>();

            using (var sqlConnection = _connection.GetSqlConnection(_stringConnection))
            {
                SqlCommand command = new SqlCommand("list_employee_clockings", sqlConnection);
                command.Parameters.AddWithValue("@clocking_employee_id", employee_id);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                try
                {
                    sqlConnection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ClockingModel data = new ClockingModel
                            {
                                id = reader.GetInt32(0),
                                employee_id = reader.GetInt32(1),
                                clock_in = reader.GetDateTime(2),
                                clock_out = reader.GetDateTime(3),
                                created_at = reader.GetDateTime(4)
                            };

                            list.Add(data);
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return list;
        }

        public ClockingModel ViewClocking(int id)
        {
            throw new NotImplementedException();
        }
        
        public bool ModifyClocking(int id)
        {
            throw new NotImplementedException();

        }
    }
}
