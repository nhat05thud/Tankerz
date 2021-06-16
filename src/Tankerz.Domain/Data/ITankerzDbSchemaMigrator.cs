using System.Threading.Tasks;

namespace Tankerz.Data
{
    public interface ITankerzDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
