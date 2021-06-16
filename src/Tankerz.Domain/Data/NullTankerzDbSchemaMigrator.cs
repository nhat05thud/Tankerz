using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Tankerz.Data
{
    /* This is used if database provider does't define
     * ITankerzDbSchemaMigrator implementation.
     */
    public class NullTankerzDbSchemaMigrator : ITankerzDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}