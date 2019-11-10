using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace IdentityServer.Data
{
    public class DataUpdater : IDataUpdater
    {
        private readonly IConfiguration _config;

        public DataUpdater(IConfiguration config)
        {
            _config = config;
        }

        private void CloseConnection(SqlConnection connection)
        {
            connection.Close();
        }

        private SqlCommand GetCommand(SqlConnection connection, string procedureName, params SqlParameter[] parameters)
        {
            var command = new SqlCommand($"[dbo].[{procedureName}]", connection) { CommandType = CommandType.StoredProcedure };
            command.Parameters.AddRange(parameters);
            return command;
        }

        private SqlConnection GetConnection()
        {
            var connection = new SqlConnection(_config.GetConnectionString("AuthDatabase"));
            connection.Open();
            return connection;
        }

        public void Execute(string procedureName, params SqlParameter[] parameters)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    using (var command = GetCommand(connection, procedureName, parameters))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                finally
                {
                    CloseConnection(connection);
                }
            }
        }

        public async Task ExecuteAsync(string procedureName, params SqlParameter[] parameters)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    using (var command = GetCommand(connection, procedureName, parameters))
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                }
                finally
                {
                    CloseConnection(connection);
                }
            }
        }
    }
}
