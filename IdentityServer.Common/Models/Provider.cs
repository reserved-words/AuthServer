using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer
{
    public class Provider
    {
        public string Id { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
