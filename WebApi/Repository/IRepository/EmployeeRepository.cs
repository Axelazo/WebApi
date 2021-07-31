using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Models;
using WebApi.Repository.IRepository;
using System.Data;

namespace WebApi.Repository.IRepository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConnection _connection;
        private readonly string _stringConnection;

        public EmployeeRepository(IConnection connection)
        {
            this._connection = connection;
            this._stringConnection = connection.GetStringConnection();
        }
        public bool InsertEmployee(EmployeeModel employee)
        {
            using (var sqlConnection = _connection.GetSqlConnection(_stringConnection))
            {
                SqlCommand command = new SqlCommand("insert_employee", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@employee_name", employee.Name);
                command.Parameters.AddWithValue("@employee_last_name", employee.LastName);
                command.Parameters.AddWithValue("@employee_dni", employee.Dni);
                command.Parameters.AddWithValue("@employee_age", employee.Age);
                command.Parameters.AddWithValue("@employee_birth_date", employee.BirthDate);
                command.Parameters.AddWithValue("@employee_gender", employee.Gender);
                command.Parameters.AddWithValue("@employee_address", employee.Address);
                command.Parameters.AddWithValue("@employee_position", employee.Position);
                command.Parameters.AddWithValue("@employee_department", employee.Department);

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

        public List<EmployeeModel> ListEmployees()
        {
            List<EmployeeModel> list = new List<EmployeeModel>();

            using (var sqlConnection = _connection.GetSqlConnection(_stringConnection))
            {
                SqlCommand command = new SqlCommand("list_employees", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                try
                {
                    sqlConnection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            EmployeeModel data = new EmployeeModel
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                LastName = reader.GetString(2),
                                Dni = reader.GetString(3),
                                Age = reader.GetInt32(4),
                                BirthDate = reader.GetDateTime(5).ToString("MM'/'dd'/'yyyy"),
                                Gender = reader.GetSingleChar(6),
                                Address = reader.GetString(7),
                                Position = reader.GetInt32(8),
                                Department = reader.GetInt32(9)
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

        public EmployeeModel ViewEmployee(int id)
        {
            EmployeeModel user = new EmployeeModel();

            using (var sqlConnection = _connection.GetSqlConnection(_stringConnection))
            {
                SqlCommand command = new SqlCommand("view_employee", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@employee_id", id);

                try
                {
                    sqlConnection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            EmployeeModel employee = new EmployeeModel
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                LastName = reader.GetString(2),
                                Dni = reader.GetString(3),
                                Age = reader.GetInt32(4),
                                BirthDate = reader.GetDateTime(5).ToString("MM'/'dd'/'yyyy"),
                                Gender = reader.GetSingleChar(6),
                                Address = reader.GetString(7),
                                Position = reader.GetInt32(8),
                                Department = reader.GetInt32(9)
                            };

                            return employee;
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }

            }
            return null;
        }

        public bool ModifyEmployee(EmployeeModel employee)
        {
            using (var sqlConnection = _connection.GetSqlConnection(_stringConnection))
            {
                SqlCommand command = new SqlCommand("modify_employee", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@employee_id", employee.Id);
                command.Parameters.AddWithValue("@employee_name", employee.Name);
                command.Parameters.AddWithValue("@employee_last_name", employee.LastName);
                command.Parameters.AddWithValue("@employee_dni", employee.Dni);
                command.Parameters.AddWithValue("@employee_age", employee.Age);
                command.Parameters.AddWithValue("@employee_birth_date", employee.BirthDate);
                command.Parameters.AddWithValue("@employee_gender", employee.Gender);
                command.Parameters.AddWithValue("@employee_address", employee.Address);
                command.Parameters.AddWithValue("@employee_position", employee.Position);
                command.Parameters.AddWithValue("@employee_department", employee.Department);

                try
                {
                    sqlConnection.Open();
                    if (command.ExecuteNonQuery() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    throw;
                }

            }
        }

        public bool DeleteEmployee(int id)
        {
            using (var sqlConnection = _connection.GetSqlConnection(_stringConnection))
            {
                SqlCommand command = new SqlCommand("delete_employee", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@employee_id", id);

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
}
