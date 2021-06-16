using Volo.Abp.AspNetCore.Mvc.UI.Theming;
using Volo.Abp.DependencyInjection;

namespace Abp.AspNetCore.Mvc.UI.Theme.Front
{
    [ThemeName(Name)]
    public class FrontTheme : ITheme, ITransientDependency
    {
        public const string Name = "Front";

        public virtual string GetLayout(string name, bool fallbackToDefault = true)
        {
            switch (name)
            {
                case StandardLayouts.Application:
                    return "~/Themes/Front/Layouts/Application.cshtml";
                case StandardLayouts.Account:
                    return "~/Themes/Front/Layouts/Account.cshtml";
                case StandardLayouts.Empty:
                    return "~/Themes/Front/Layouts/Empty.cshtml";
                default:
                    return fallbackToDefault ? "~/Themes/Front/Layouts/Application.cshtml" : null;
            }
        }
    }
}
