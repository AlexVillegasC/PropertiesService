using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace PropertyService.Helpers
{
    public interface ICommandHelper
    {
        SqlConnection GetConnection();

        IDataReader ExecuteQuery(IDbCommand command);
    }
}
