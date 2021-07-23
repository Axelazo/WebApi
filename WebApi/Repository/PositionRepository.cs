using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Repository.IRepository;

namespace WebApi.Repository
{
    public class PositionRepository : IPositionRepository
    {
        private readonly IConnection _connection;
        private readonly string _stringConnection;
        
        public PositionRepository(IConnection connection)
        {
            this._connection = connection;
            this._stringConnection = connection.GetStringConnection();
        }

        //Inserta una posicion
        public bool insertPosition(PositionModel position)
        {
            using (var sqlConnection = _connection.GetSqlConnection(_stringConnection))
            {
                SqlCommand command = new SqlCommand("insert_position", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@position_name", position.name);
                command.Parameters.AddWithValue("@position_description", position.description);

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

        //Lista las posiciones
        public List<PositionModel> listPositions()
        {
            List<PositionModel> list = new List<PositionModel>();

            using (var sqlConnection = _connection.GetSqlConnection(_stringConnection))
            {
                SqlCommand command = new SqlCommand("list_positions", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                try
                {
                    sqlConnection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            PositionModel data = new PositionModel
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

        //Muestra una posicion
        public PositionModel viewPosition(int id)
        {
            PositionModel position = new PositionModel();

            using (var sqlConnection = _connection.GetSqlConnection(_stringConnection))
            {
                SqlCommand command = new SqlCommand("view_position", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@position_id", id);

                try
                {
                    sqlConnection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                             position = new PositionModel
                            {
                                id = reader.GetInt32(0),
                                name = reader.GetString(1),
                                description = reader.GetString(2)
                            };

                            return position;
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

        //Modficia una posicion
        public PositionModel modifyPosition(PositionModel position)
        {
            using (var sqlConnection = _connection.GetSqlConnection(_stringConnection))
            {
                SqlCommand command = new SqlCommand("modify_position", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@position_id", position.id);
                command.Parameters.AddWithValue("@position_name", position.name);
                command.Parameters.AddWithValue("@position_description", position.description);

                try
                {
                    sqlConnection.Open();
                    if (command.ExecuteNonQuery() > 0) {
                        return position;
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

        //Elimina una posicion
        public bool deletePosition(int id)
        {
            using (var sqlConnection = _connection.GetSqlConnection(_stringConnection))
            {
                SqlCommand command = new SqlCommand("delete_position", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@position_id", id);

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
