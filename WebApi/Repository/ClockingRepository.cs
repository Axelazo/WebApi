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

        public bool CreateClocking(int employeeId)
        {
            using (var sqlConnection = _connection.GetSqlConnection(_stringConnection))
            {
                DateTime createdAt = DateTime.Now;
                SqlCommand command = new SqlCommand("insert_clocking", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@clocking_employee_id", employeeId);
                command.Parameters.AddWithValue("@clocking_created_at", createdAt);

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
            using (var sqlConnection = _connection.GetSqlConnection(_stringConnection))
            {
                SqlCommand command = new SqlCommand("delete_clocking", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@department_id", id);

                try
                {
                    sqlConnection.Open();
                    if (command.ExecuteNonQuery() > 0)
                    {
                        return true;
                    }

                    return false;
                }
                catch (Exception)
                {
                    throw;
                }

            }
        }

        public bool InsertClocking(InsertClockingModel insertClocking/*DateTime dateTime, int employeeId, int input*/)
        {
            if(insertClocking.Input == 1)
            {
                using (var sqlConnection = _connection.GetSqlConnection(_stringConnection))
                {
                    SqlCommand command = new SqlCommand("insert_clocking_in", sqlConnection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@clocking_employee_id", insertClocking.EmployeeId);
                    command.Parameters.AddWithValue("@clocking_id", insertClocking.Id);
                    command.Parameters.AddWithValue("@clocking_clock_in", insertClocking.ClockIn);

                    try
                    {
                        sqlConnection.Open();
                        if (command.ExecuteNonQuery() > 0)
                        {
                            return true;
                        }

                        return false;
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                }
            }
            else
            {
                using (var sqlConnection = _connection.GetSqlConnection(_stringConnection))
                {
                    SqlCommand command = new SqlCommand("insert_clocking_out", sqlConnection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@clocking_employee_id", insertClocking.EmployeeId);
                    command.Parameters.AddWithValue("@clocking_id", insertClocking.Id);
                    command.Parameters.AddWithValue("@clockin_clock_out", insertClocking.ClockOut);

                    try
                    {
                        sqlConnection.Open();
                        if (command.ExecuteNonQuery() > 0)
                        {
                            return true;
                        }

                        return false;
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                }
            }
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
                                Id = reader.GetInt32(0),
                                EmployeeId = reader.GetInt32(1),
                                ClockIn = reader.GetDateTime(2),
                                ClockOut = reader.GetDateTime(3),
                                CreatedAt = reader.GetDateTime(4)
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
