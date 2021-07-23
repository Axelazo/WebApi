using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Data
{
    public interface IConnection
    {
        string GetStringConnection();
        SqlConnection GetSqlConnection(string conString);
    }
}
