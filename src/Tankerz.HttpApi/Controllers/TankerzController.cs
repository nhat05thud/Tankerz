using Tankerz.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Tankerz.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class TankerzController : AbpController
    {
        protected TankerzController()
        {
            LocalizationResource = typeof(TankerzResource);
        }
    }
}