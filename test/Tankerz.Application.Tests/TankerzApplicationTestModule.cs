using Volo.Abp.Modularity;

namespace Tankerz
{
    [DependsOn(
        typeof(TankerzApplicationModule),
        typeof(TankerzDomainTestModule)
        )]
    public class TankerzApplicationTestModule : AbpModule
    {

    }
}