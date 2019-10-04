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
        private readonly IDataFetcher _dataFiller;

        public ClientStore(IDataFetcher dataFiller)
        {
            _dataFiller = dataFiller;
        }

        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            var client = new Client();

            var dataTables = Enumerable.Range(0, 5).Select(i => new DataTable()).ToArray();

            await _dataFiller.FillAsync(dataTables, "FindClientById", new SqlParameter("@ClientId", clientId));

            var dtClient = dataTables[0];
            var dtProperties = dataTables[1];
            var dtScopes = dataTables[2];
            var dtSecrets = dataTables[3];
            var dtGrantTypes = dataTables[4];

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

            return client;
        }
    }
}
