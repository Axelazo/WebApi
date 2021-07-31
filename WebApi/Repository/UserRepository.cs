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

        public bool InsertUser(UserModel user)
        {
            using (var sqlConnection = _connection.GetSqlConnection(_stringConnection))
            {
                SqlCommand command = new SqlCommand("insert_user", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@user_username", user.Username);
                command.Parameters.AddWithValue("@user_email", user.Email);
                command.Parameters.AddWithValue("@user_password", user.Password);

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

        public List<UserModel> ListUsers()
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
                                Id = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                Email = reader.GetString(2)
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

        public UserModel ViewUser(int id)
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
                                Username = reader.GetString(0),
                                Email = reader.GetString(1),
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

        public bool ModifyUser(UserModel user)
        {
            using (var sqlConnection = _connection.GetSqlConnection(_stringConnection))
            {
                SqlCommand command = new SqlCommand("modify_user", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@user_id", user.Id);
                command.Parameters.AddWithValue("@user_username", user.Username);
                command.Parameters.AddWithValue("@user_email", user.Email);
                command.Parameters.AddWithValue("@user_password", user.Password);

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

        public bool DeleteUser(int id)
        {
            using (var sqlConnection = _connection.GetSqlConnection(_stringConnection))
            {
                SqlCommand command = new SqlCommand("delete_user", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
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

        public int LoginUser(string username, string password)
        {
            using (var sqlConnection = _connection.GetSqlConnection(_stringConnection))
            {
                SqlCommand command = new SqlCommand("login_user", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
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
