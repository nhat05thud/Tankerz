using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Tankerz.EntityFrameworkCore
{
    [DependsOn(
        typeof(TankerzEntityFrameworkCoreModule)
        )]
    public class TankerzEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<TankerzMigrationsDbContext>();
        }
    }
}
