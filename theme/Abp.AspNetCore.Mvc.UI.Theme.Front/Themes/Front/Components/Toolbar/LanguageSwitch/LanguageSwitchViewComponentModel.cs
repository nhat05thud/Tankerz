using System.Collections.Generic;
using Volo.Abp.Localization;

namespace Abp.AspNetCore.Mvc.UI.Theme.Front.Themes.Front.Components.Toolbar.LanguageSwitch
{
    public class LanguageSwitchViewComponentModel
    {
        public LanguageInfo CurrentLanguage { get; set; }

        public List<LanguageInfo> OtherLanguages { get; set; }
    }
}