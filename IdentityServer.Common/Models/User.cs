using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer
{
    public class User
    {
        public string SubjectId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ProviderName { get; set; }
        public string ProviderSubjectId { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Claim> Claims { get; set; }
    }
}
