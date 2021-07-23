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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IConnection _connection;
        private readonly string _stringConnection;

        public DepartmentRepository(IConnection connection)
        {
            this._connection = connection;
            this._stringConnection = connection.GetStringConnection();
        }

        public bool insertDepartment(DepartmentModel position)
        {
            using (var sqlConnection = _connection.GetSqlConnection(_stringConnection))
            {
                SqlCommand command = new SqlCommand("insert_department", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@department_name", position.name);
                command.Parameters.AddWithValue("@department_description", position.description);

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

        public DepartmentModel viewDepartment(int id)
        {
            DepartmentModel department = new DepartmentModel();

            using (var sqlConnection = _connection.GetSqlConnection(_stringConnection))
            {
                SqlCommand command = new SqlCommand("view_department", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@department_id", id);

                try
                {
                    sqlConnection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            department = new DepartmentModel
                            {
                                id = reader.GetInt32(0),
                                name = reader.GetString(1),
                                description = reader.GetString(2)
                            };

                            return department;
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }

            }

            return department;
        }

        public List<DepartmentModel> listDepartments()
        {
            List<DepartmentModel> list = new List<DepartmentModel>();

            using (var sqlConnection = _connection.GetSqlConnection(_stringConnection))
            {
                SqlCommand command = new SqlCommand("list_departments", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                try
                {
                    sqlConnection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            DepartmentModel data = new DepartmentModel
                            {
                                id = reader.GetInt32(0),
                                name = reader.GetString(1),
                                description = reader.GetString(2)
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

        public DepartmentModel modifyDepartment(DepartmentModel department)
        {
            using (var sqlConnection = _connection.GetSqlConnection(_stringConnection))
            {
                SqlCommand command = new SqlCommand("modify_department", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@department_id", department.id);
                command.Parameters.AddWithValue("@department_name", department.name);
                command.Parameters.AddWithValue("@department_description", department.description);

                try
                {
                    sqlConnection.Open();
                    if (command.ExecuteNonQuery() > 0) {
                        return department;
                    } else
                    {
                        return null;
                    }
                    return null;
                }
                catch (Exception)
                {
                    throw;
                }

            }
        }

        public bool deleteDepartment(int id)
        {
            using (var sqlConnection = _connection.GetSqlConnection(_stringConnection))
            {
                SqlCommand command = new SqlCommand("delete_department", sqlConnection);
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





    }
}
