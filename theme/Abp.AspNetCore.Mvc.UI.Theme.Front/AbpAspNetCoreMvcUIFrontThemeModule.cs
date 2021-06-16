using Abp.AspNetCore.Mvc.UI.Theme.AdminLTE.Localization;
using Abp.AspNetCore.Mvc.UI.Theme.Front.Bundling;
using Abp.AspNetCore.Mvc.UI.Theme.Front.Toolbars;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using Volo.Abp.AspNetCore.Mvc.UI.Theming;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Abp.AspNetCore.Mvc.UI.Theme.Front
{
    [DependsOn(
        typeof(AbpAspNetCoreMvcUiThemeSharedModule),
        typeof(AbpAspNetCoreMvcUiMultiTenancyModule)
        )]
    public class AbpAspNetCoreMvcUiFrontThemeModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(AbpAspNetCoreMvcUiFrontThemeModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpThemingOptions>(options =>
            {
                options.Themes.Add<FrontTheme>();

                if (options.DefaultThemeName == null)
                {
                    options.DefaultThemeName = FrontTheme.Name;
                }
            });

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AbpAspNetCoreMvcUiFrontThemeModule>("Abp.AspNetCore.Mvc.UI.Theme.Front");
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<FrontResource>("en")
                    .AddVirtualJson("/Localization/Front");
            });

            Configure<AbpToolbarOptions>(options =>
            {
                options.Contributors.Add(new FrontThemeMainTopToolbarContributor());
            });

            Configure<RazorPagesOptions>(options =>
            {
                options.Conventions.AuthorizePage("/index");
            });

            Configure<AbpBundlingOptions>(options =>
            {
                options
                    .StyleBundles
                    .Add(FrontThemeBundles.Styles.Global, bundle =>
                    {
                        bundle
                            .AddBaseBundles(StandardBundles.Styles.Global)
                            .AddContributors(typeof(FrontThemeGlobalStyleContributor));
                    });

                options
                    .ScriptBundles
                    .Add(FrontThemeBundles.Scripts.Global, bundle =>
                    {
                        bundle
                            .AddBaseBundles(StandardBundles.Scripts.Global)
                            .AddContributors(typeof(FrontThemeGlobalScriptContributor));
                    });
            });
        }
    }
}
