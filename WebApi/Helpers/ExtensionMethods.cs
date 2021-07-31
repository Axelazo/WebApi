using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
internal static class ExtensionMethods
{
    internal static char GetSingleChar(this SqlDataReader reader, int columnIndex)
    {
        System.Data.SqlTypes.SqlChars val = reader.GetSqlChars(columnIndex);
        if (val.Length != 1)
        {
            throw new ApplicationException(
                "Expected value to be 1 char long, but was "
                + val.Length.ToString() + " chars long.");
        }
        return val[0];
    }
}