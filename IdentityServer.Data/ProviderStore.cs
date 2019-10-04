using System.Data;
using System.Data.SqlClient;

namespace IdentityServer
{
    public class ProviderStore : IProviderStore
    {
        private readonly IDataFetcher _dataFiller;

        public ProviderStore(IDataFetcher dataFiller)
        {
            _dataFiller = dataFiller;
        }

        public Provider FindProviderById(string id)
        {
            var dtProvider = _dataFiller.Fetch("FindProviderById", new SqlParameter("@Id", id));

            var provider = new Provider();

            if (dtProvider.Rows.Count == 0)
                return null;

            var rProvider = dtProvider.Rows[0] as DataRow;
            provider.Id = rProvider["Id"].ToString();
            provider.ClientId = rProvider["ClientId"].ToString();
            provider.ClientSecret = rProvider["ClientSecret"].ToString();

            return provider;
        }
    }
}
