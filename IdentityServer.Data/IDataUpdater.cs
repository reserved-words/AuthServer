using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace IdentityServer
{
    public interface IDataUpdater
    {
        void Execute(string procedureName, params SqlParameter[] parameters);
        Task ExecuteAsync(string procedureName, params SqlParameter[] parameters);
    }
}
