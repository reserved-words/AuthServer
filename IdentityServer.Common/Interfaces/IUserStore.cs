using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer
{
    public interface IUserStore
    {
        bool ValidateCredentials(string username, string password);
        User FindByUsername(string username);
        User FindByExternalProvider(string provider, string providerUserId);
        void AddExternalProviderId(User user, string provider, string providerUserId);
        User FindByEmail(string email);
    }
}
