using System.Data;
using System.Data.SqlClient;
using System.Security.Authentication;

namespace IdentityServer
{
    public class ProviderStore : IProviderStore
    {
        public Provider FindProviderById(string id)
        {
            var provider = new Provider();

            var connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=auth;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("[dbo].[FindProviderById]", connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add(new SqlParameter("@Id", id));

                    var reader = command.ExecuteReader();

                    var dtProvider = new DataTable();
 
                    dtProvider.Load(reader);

                    if (dtProvider.Rows.Count == 0)
                        return null;

                    var rProvider = dtProvider.Rows[0] as DataRow;
                    provider.Id = rProvider["Id"].ToString();
                    provider.ClientId = rProvider["ClientId"].ToString();
                    provider.ClientSecret = rProvider["ClientSecret"].ToString();
                }

                connection.Close();
            }

            return provider;
        }
    }
}
