using Tankerz.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Tankerz
{
    [DependsOn(
        typeof(TankerzEntityFrameworkCoreTestModule)
        )]
    public class TankerzDomainTestModule : AbpModule
    {

    }
}