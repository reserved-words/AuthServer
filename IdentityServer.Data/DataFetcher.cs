using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class DataFetcher : IDataFetcher
    {
        public void Fill(DataTable[] dataTables, string procedureName, params SqlParameter[] parameters)
        {
            using (var connection = GetConnection())
            {
                SqlDataReader reader = null;

                try
                {
                    using (var command = GetCommand(connection, procedureName, parameters))
                    {
                        reader = command.ExecuteReader();
                        Fill(reader, dataTables);
                    }
                }
                finally
                {
                    CloseConnection(connection, reader);
                }
            }
        }

        public DataTable Fetch(string procedureName, params SqlParameter[] parameters)
        {
            var dt = new DataTable();
            Fill(new DataTable[] { dt }, procedureName, parameters);
            return dt;
        }

        public async Task FillAsync(DataTable[] dataTables, string procedureName, params SqlParameter[] parameters)
        {
            using (var connection = GetConnection())
            {
                SqlDataReader reader = null;

                try
                {
                    using (var command = GetCommand(connection, procedureName, parameters))
                    {
                        reader = await command.ExecuteReaderAsync();
                        Fill(reader, dataTables);
                    }
                }
                finally
                {
                    CloseConnection(connection, reader);
                }
            }
        }

        public async Task<DataTable> FetchAsync(string procedureName, params SqlParameter[] parameters)
        {
            var dt = new DataTable();
            await FillAsync(new DataTable[] { dt }, procedureName, parameters);
            return dt;
        }

        private void CloseConnection(SqlConnection connection, SqlDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                reader.Close();
            }

            connection.Close();
        }

        private void Fill(SqlDataReader reader, DataTable[] dataTables)
        {
            foreach (var dt in dataTables)
            {
                dt.Load(reader);
            }
        }

        private SqlCommand GetCommand(SqlConnection connection, string procedureName, params SqlParameter[] parameters)
        {
            var command = new SqlCommand($"[dbo].[{procedureName}]", connection) { CommandType = CommandType.StoredProcedure };
            command.Parameters.AddRange(parameters);
            return command;
        }

        private SqlConnection GetConnection()
        {
            var connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=auth;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True";
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
