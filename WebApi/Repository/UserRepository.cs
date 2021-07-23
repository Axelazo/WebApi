using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Models;
using WebApi.Repository.IRepository;
using System.Data;

namespace WebApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConnection _connection;
        private readonly string _stringConnection;

        public UserRepository(IConnection connection)
        {
            this._connection = connection;
            this._stringConnection = connection.GetStringConnection();
        }

        public bool insertUser(UserModel user)
        {
            using (var sqlConnection = _connection.GetSqlConnection(_stringConnection))
            {
                SqlCommand command = new SqlCommand("insert_user", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@user_username", user.username);
                command.Parameters.AddWithValue("@user_email", user.email);
                command.Parameters.AddWithValue("@user_password", user.password);

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

        public List<UserModel> listUsers()
        {
            List<UserModel> list = new List<UserModel>();

            using (var sqlConnection = _connection.GetSqlConnection(_stringConnection))
            {
                SqlCommand command = new SqlCommand("list_users", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                try
                {
                    sqlConnection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            UserModel data = new UserModel
                            {
                                id = reader.GetInt32(0),
                                username = reader.GetString(1),
                                email = reader.GetString(2)
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

        public UserModel viewUser(int id)
        {
            UserModel user = new UserModel();

            using (var sqlConnection = _connection.GetSqlConnection(_stringConnection))
            {
                SqlCommand command = new SqlCommand("view_user", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@user_id", id);

                try
                {
                    sqlConnection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            user = new UserModel
                            {
                                username = reader.GetString(0),
                                email = reader.GetString(1),
                            };

                            return user;
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

        public bool modifyUser(UserModel user)
        {
            using (var sqlConnection = _connection.GetSqlConnection(_stringConnection))
            {
                SqlCommand command = new SqlCommand("modify_user", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@user_id", user.id);
                command.Parameters.AddWithValue("@user_username", user.username);
                command.Parameters.AddWithValue("@user_email", user.email);
                command.Parameters.AddWithValue("@user_password", user.password);

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

        public bool deleteUser(int id)
        {
            using (var sqlConnection = _connection.GetSqlConnection(_stringConnection))
            {
                SqlCommand command = new SqlCommand("delete_user", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@user_id", id);

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

        public int loginUser(string username, string password)
        {
            using (var sqlConnection = _connection.GetSqlConnection(_stringConnection))
            {
                SqlCommand command = new SqlCommand("login_user", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@user_username", username);
                command.Parameters.AddWithValue("@user_password", password);

                SqlParameter output = new SqlParameter();
                output.ParameterName = "@response_message";
                output.SqlDbType = SqlDbType.Int;
                output.Direction = ParameterDirection.Output;
                command.Parameters.Add(output);


                

                try
                {
                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                    int r = (int)output.Value;
                    return r;
                }
                catch (Exception)
                {
                    throw;
                }

            }
        }
    }
}
