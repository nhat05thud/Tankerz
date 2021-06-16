using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Tankerz.Web
{
    [Dependency(ReplaceServices = true)]
    public class TankerzBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Tankerz";
    }
}
