namespace IdentityServer
{
    public interface IProviderStore
    {
        Provider FindProviderById(string id);
    }
}
