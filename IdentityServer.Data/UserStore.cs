using IdentityServer4.Models;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;

namespace IdentityServer
{
    public class UserStore : IUserStore
    {
        private readonly IDataFetcher _dataFetcher;
        private readonly IDataUpdater _dataUpdater;

        public UserStore(IDataFetcher dataFetcher, IDataUpdater dataUpdater)
        {
            _dataFetcher = dataFetcher;
            _dataUpdater = dataUpdater;
        }

        public void AddExternalProviderId(User user, string provider, string providerUserId)
        {
            _dataUpdater.Execute("AddUserExternalProvider",
                new SqlParameter("@SubjectId", user.SubjectId),
                new SqlParameter("@ProviderId", provider),
                new SqlParameter("@ProviderUserId", providerUserId));
        }

        public User FindByEmail(string email)
        {
            var dataTables = Enumerable.Range(0, 2).Select(i => new DataTable()).ToArray();

            _dataFetcher.Fill(dataTables, "FindUserByEmail",
                new SqlParameter("@Email", email));

            return GetUserFromDataTables(dataTables);
        }

        public User FindByExternalProvider(string provider, string providerUserId)
        {
            var dataTables = Enumerable.Range(0, 2).Select(i => new DataTable()).ToArray();

            _dataFetcher.Fill(dataTables, "FindUserByExternalProvider", 
                new SqlParameter("@ProviderId", provider),
                new SqlParameter("@ProviderUserId", providerUserId));

            return GetUserFromDataTables(dataTables);
        }

        public User FindByUsername(string username)
        {
            var dataTables = Enumerable.Range(0, 2).Select(i => new DataTable()).ToArray();

            _dataFetcher.Fill(dataTables, "FindUserByUsername", new SqlParameter("@Username", username));

            return GetUserFromDataTables(dataTables);
        }

        public bool ValidateCredentials(string username, string password)
        {
            var user = FindByUsername(username);
            return user.Password == password.Sha256();
        }

        private User GetUserFromDataTables(DataTable[] dataTables)
        {
            var dtUser = dataTables[0];
            var dtClaims = dataTables[1];

            if (dtUser.Rows.Count == 0)
                return null;

            var rUser = dtUser.Rows[0] as DataRow;

            return new User
            {
                SubjectId = rUser["SubjectId"].ToString(),
                Username = rUser["Username"].ToString(),
                Password = rUser["Password"].ToString(),
                Claims = dtClaims.Rows.OfType<DataRow>()
                    .Select(r => new Claim(r["Type"].ToString(), r["Value"].ToString()))
                    .ToArray()
            };
        }
    }
}
