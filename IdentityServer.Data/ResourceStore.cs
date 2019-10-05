using IdentityServer4.Models;
using IdentityServer4.Stores;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class ResourceStore : IResourceStore
    {
        private readonly IDataFetcher _dataFetcher;

        public ResourceStore(IDataFetcher dataFetcher)
        {
            _dataFetcher = dataFetcher;
        }

        public async Task<ApiResource> FindApiResourceAsync(string name)
        {
            var dataTables = Enumerable.Range(0, 5).Select(i => new DataTable()).ToArray();

            await _dataFetcher.FillAsync(dataTables, "FindApiResourceByName", new SqlParameter("@Name", name));

            var dtResource = dataTables[0];
            var dtClaims = dataTables[1];
            var dtProperties = dataTables[2];
            var dtScopes = dataTables[3];
            var dtScopeClaims = dataTables[3];
            var dtSecrets = dataTables[4];

            var rResource = dtResource.Rows[0] as DataRow;
            
            var resource = new ApiResource
            {
                Name = rResource["Name"].ToString(),
                DisplayName = rResource["Name"].ToString()
            };

            foreach (DataRow row in dtClaims.Rows)
            {
                resource.UserClaims.Add(row["Claim"].ToString());
            }

            foreach (DataRow row in dtProperties.Rows)
            {
                resource.Properties.Add(row["Key"].ToString(), row["Value"].ToString());
            }

            foreach (DataRow row in dtScopes.Rows)
            {
                resource.Scopes.Add(new Scope
                {
                    Name = row["ScopeName"].ToString(),
                    DisplayName = row["DisplayName"].ToString(),
                    Description = row["Description"].ToString(),
                    Required = (bool)row["Required"],
                    Emphasize = (bool)row["Emphasize"],
                    ShowInDiscoveryDocument = (bool)row["ShowInDiscoveryDocument"]
                });
            }

            foreach (DataRow row in dtScopeClaims.Rows)
            {
                var scopeName = row["ScopeName"].ToString();
                var scope = resource.Scopes.Single(s => s.Name == scopeName);
                scope.UserClaims.Add(row["Claim"].ToString());
            }

            foreach (DataRow row in dtSecrets.Rows)
            {
                resource.ApiSecrets.Add(new Secret(row["Secret"].ToString()));
            }

            return resource;
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var list = $"|{string.Join("|", scopeNames)}|";
            var dataTable = await _dataFetcher.FetchAsync("FindApiResourcesByScope", new SqlParameter("@Scopes", list));

            var resources = new List<ApiResource>();

            foreach (DataRow row in dataTable.Rows)
            {
                var resourceName = row["ResourceName"].ToString();

                var resource = resources.SingleOrDefault(r => r.Name == resourceName);

                if (resource == null)
                {
                    resource = new ApiResource
                    {
                        Name = resourceName,
                        DisplayName = row["ResourceDisplayName"].ToString()
                    };
                    resources.Add(resource);
                }

                resource.Scopes.Add(new Scope
                {
                    Name = row["ScopeName"].ToString(),
                    DisplayName = row["ScopeDisplayName"].ToString(),
                    Description = row["ScopeDescription"].ToString(),
                    Required = (bool)row["ScopeRequired"],
                    Emphasize = (bool)row["ScopeEmphasize"],
                    ShowInDiscoveryDocument = (bool)row["ScopeShowInDiscoveryDocument"]
                });
            }

            return resources;
        }

        public async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            // ScopeNames?
            return await Task.Run(() => Config.GetIdentityResources());
        }

        public async Task<Resources> GetAllResourcesAsync()
        {
            return new Resources
            {
                IdentityResources = Config.GetIdentityResources().ToList(),
                ApiResources = await GetApiResources()
            };
        }

        private async Task<ICollection<ApiResource>> GetApiResources()
        {
            var dataTable = await _dataFetcher.FetchAsync("GetApiResources");

            return dataTable.Rows
                .OfType<DataRow>()
                .Select(r => new ApiResource
                {
                    Name = r["Name"].ToString(),
                    DisplayName = r["DisplayName"].ToString()
                })
                .ToList();
        }
    }
}
