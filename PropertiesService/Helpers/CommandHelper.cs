using System;
using System.Data;
using PropertyService.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace PropertyService.Helpers
{
    public class CommandHelper : ICommandHelper
    {
        private readonly SqlOptions _sqlOptions;

        public CommandHelper(IOptions<SqlOptions> options)
        {
            _sqlOptions = options.Value;
        }

        public IDataReader ExecuteQuery(IDbCommand command)
        {
            command.Connection = GetConnection();
            command.Connection.Open();
            return command.ExecuteReader();
        }

        public SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(_sqlOptions.ConnectionString);
            return connection;
        }
    }
}
