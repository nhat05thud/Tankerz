using System;
using System.Collections.Generic;
using System.Text;
using Tankerz.Localization;
using Volo.Abp.Application.Services;

namespace Tankerz
{
    /* Inherit your application services from this class.
     */
    public abstract class TankerzAppService : ApplicationService
    {
        protected TankerzAppService()
        {
            LocalizationResource = typeof(TankerzResource);
        }
    }
}
