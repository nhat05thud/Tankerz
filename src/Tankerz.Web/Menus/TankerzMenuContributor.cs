using System.Threading.Tasks;
using Tankerz.Localization;
using Tankerz.MultiTenancy;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace Tankerz.Web.Menus
{
    public class TankerzMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
        }

        private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            var administration = context.Menu.GetAdministration();
            var l = context.GetLocalizer<TankerzResource>();

            context.Menu.Items.Insert(
                0,
                new ApplicationMenuItem(
                    TankerzMenus.Home,
                    l["Menu:Home"],
                    "~/",
                    icon: "fas fa-home",
                    order: 0
                )
            );
            context.Menu.Items.Insert(
                1,
                new ApplicationMenuItem(
                    TankerzMenus.Blog,
                    l["Menu:Blog"],
                    "~/Blogs",
                    icon: "fas fa-newspaper",
                    order: 0
                )
            );
            context.Menu.Items.Insert(
                1,
                new ApplicationMenuItem(
                    TankerzMenus.BlogCategory,
                    l["Menu:BlogCategory"],
                    "~/BlogCategories",
                    icon: "fas fa-newspaper",
                    order: 0
                )
            );

            if (MultiTenancyConsts.IsEnabled)
            {
                administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
            }
            else
            {
                administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
            }

            administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
            administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);
        }
    }
}
