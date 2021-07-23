using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Data
{
    public class Connection : IConnection
    {
        private readonly IConfiguration _config;

        public Connection(IConfiguration config)
        {
            this._config = config;
        }

        public string GetStringConnection()
        {
            string connectionString = ConfigurationExtensions.GetConnectionString(this._config, "DeveloperConnection");
            return connectionString;
        }

        public SqlConnection GetSqlConnection(string conString)
        {
            SqlConnection sqlConnection = new SqlConnection(conString);
            return sqlConnection;
        }

    }
}
