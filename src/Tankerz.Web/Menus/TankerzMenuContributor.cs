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
            context.Menu.AddItem(
                new ApplicationMenuItem(
                        TankerzMenus.AdminManagement, 
                        l["Menu:AdminManagement"],
                        null,
                        icon: "fas fa-users-cog",
                        order: 1
                    )
                    .AddItem(new ApplicationMenuItem(
                        TankerzMenus.BlogCategory,
                            l["Menu:BlogCategory"],
                            "~/BlogCategories",
                            icon: "fas fa-newspaper",
                            order: 0
                        )
                    )
                    .AddItem(new ApplicationMenuItem(
                            TankerzMenus.ProductGroup,
                            l["Menu:ProductGroup"],
                            "~/ProductGroups",
                            icon: "fas fa-newspaper",
                            order: 0
                        )
                    )
                    .AddItem(new ApplicationMenuItem(
                            TankerzMenus.ProductCategory,
                            l["Menu:ProductCategory"],
                            "~/ProductCategories",
                            icon: "fas fa-newspaper",
                            order: 0
                        )
                    )
            );

            if (MultiTenancyConsts.IsEnabled)
            {
                administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 98);
            }
            else
            {
                administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
            }

            administration.SetSubItemOrder(IdentityMenuNames.GroupName, 99);
            administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 100);
        }
    }
}
