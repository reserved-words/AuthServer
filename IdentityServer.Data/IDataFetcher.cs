using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace IdentityServer
{
    public interface IDataFetcher
    {
        DataTable Fetch(string procedureName, params SqlParameter[] parameters);
        Task<DataTable> FetchAsync(string procedureName, params SqlParameter[] parameters);
        void Fill(DataTable[] dataTables, string procedureName, params SqlParameter[] parameters);
        Task FillAsync(DataTable[] dataTables, string procedureName, params SqlParameter[] parameters);
    }
}
