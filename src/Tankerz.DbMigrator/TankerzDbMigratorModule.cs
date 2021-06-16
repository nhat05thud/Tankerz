using Tankerz.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Tankerz.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(TankerzEntityFrameworkCoreDbMigrationsModule),
        typeof(TankerzApplicationContractsModule)
        )]
    public class TankerzDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
