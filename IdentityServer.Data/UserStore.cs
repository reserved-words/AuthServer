using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace IdentityServer
{
    public class UserStore : IUserStore
    {
        public User FindByExternalProvider(string provider, string providerUserId)
        {
            return Config.GetUsers()
                .SingleOrDefault(u => u.ProviderName == provider
                    && u.ProviderSubjectId == providerUserId);
        }

        public User FindByUsername(string username)
        {
            return Config.GetUsers()
                .SingleOrDefault(u => u.Username == username);
        }

        public bool ValidateCredentials(string username, string password)
        {
            var user = FindByUsername(username);
            return user.Password == password;
        }
    }
}
