using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer
{
    public interface IUserStore
    {
        bool ValidateCredentials(string username, string password);
        User FindByUsername(string username);
        User FindByExternalProvider(string provider, string providerUserId);
    }
}
