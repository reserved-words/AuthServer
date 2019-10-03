using IdentityServer4.Models;
using IdentityServer4.Stores;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class ClientStore : IClientStore
    {
        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            var client = new Client();

            var connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=auth;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("[dbo].[FindClientById]", connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add(new SqlParameter("@ClientId", clientId));

                    var reader = await command.ExecuteReaderAsync();

                    var dtClient = new DataTable();
                    var dtProperties = new DataTable();
                    var dtSecrets = new DataTable();
                    var dtGrantTypes = new DataTable();
                    var dtScopes = new DataTable();

                    dtClient.Load(reader);
                    dtProperties.Load(reader);
                    dtScopes.Load(reader);
                    dtSecrets.Load(reader);
                    dtGrantTypes.Load(reader);

                    if (dtClient.Rows.Count == 0)
                        throw new AuthenticationException();

                    var rClient = dtClient.Rows[0] as DataRow;
                    client.ClientId = rClient["ClientId"].ToString();
                    client.ClientName = rClient["ClientName"].ToString();
                    client.Enabled = (bool)rClient["Enabled"];
                    client.AllowOfflineAccess = (bool)rClient["AllowOfflineAccess"];
                    client.EnableLocalLogin = (bool)rClient["EnableLocalLogin"];
                    client.RequireConsent = (bool)rClient["RequireConsent"];
                    client.AllowRememberConsent = (bool)rClient["AllowRememberConsent"];
                    client.ClientUri = rClient["ClientUri"].ToString();
                    client.LogoUri = rClient["LogoUri"].ToString();

                    var redirectUri = rClient["RedirectUri"].ToString();
                    if (!string.IsNullOrEmpty(redirectUri))
                    {
                        client.RedirectUris = new List<string> { redirectUri };
                    }

                    var postLogRedirectUri = rClient["PostLogoutRedirectUri"].ToString();
                    if (!string.IsNullOrEmpty(postLogRedirectUri))
                    {
                        client.PostLogoutRedirectUris = new List<string> { postLogRedirectUri };
                    }

                    foreach (DataRow row in dtProperties.Rows)
                    {
                        client.Properties.Add(row["Key"].ToString(), row["Value"].ToString());
                    }

                    foreach (DataRow row in dtSecrets.Rows)
                    {
                        client.ClientSecrets.Add(new Secret(row["Secret"].ToString()));
                    }

                    foreach (DataRow row in dtScopes.Rows)
                    {
                        client.AllowedScopes.Add(row["Scope"].ToString());
                    }

                    foreach (DataRow row in dtGrantTypes.Rows)
                    {
                        client.AllowedGrantTypes.Add(row["GrantType"].ToString());
                    }
                }

                connection.Close();
            }

            return client;
        }
    }
}
