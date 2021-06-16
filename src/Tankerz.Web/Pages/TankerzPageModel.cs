using Tankerz.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Tankerz.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class TankerzPageModel : AbpPageModel
    {
        protected TankerzPageModel()
        {
            LocalizationResourceType = typeof(TankerzResource);
        }
    }
}