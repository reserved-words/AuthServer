using IdentityServer4.Models;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class ResourceStore : IResourceStore
    {
        public async Task<ApiResource> FindApiResourceAsync(string name)
        {
            return Config.GetApis().SingleOrDefault(a => a.Name == name);
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            return Config.GetApis().Where(a => a.Scopes.Any(sc => scopeNames.Contains(sc.Name)));
        }

        public async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            // ScopeNames?
            return Config.GetIdentityResources();
        }

        public async Task<Resources> GetAllResourcesAsync()
        {
            return new Resources
            {
                IdentityResources = Config.GetIdentityResources().ToList(),
                ApiResources = Config.GetApis().ToList()
            };
        }
    }
}
