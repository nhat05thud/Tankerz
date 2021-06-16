﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Layout;
using Volo.Abp.UI.Navigation;

namespace Abp.AspNetCore.Mvc.UI.Theme.Front.Themes.Front.Components.Menu
{
    public class MainNavbarMenuViewComponent : AbpViewComponent
    {
        private readonly IMenuManager _menuManager;
        protected IPageLayout PageLayout { get; }

        public MainNavbarMenuViewComponent(IMenuManager menuManager, IPageLayout pageLayout)
        {
            _menuManager = menuManager;
            PageLayout = pageLayout;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var menu = await _menuManager.GetAsync(StandardMenus.Main);

            if (!PageLayout.Content.MenuItemName.IsNullOrEmpty())
            {
                SetActiveMenuItems(menu.Items, PageLayout.Content.MenuItemName);
            }
            return View("~/Themes/Front/Components/Menu/Default.cshtml", menu);
        }
        protected virtual bool SetActiveMenuItems(ApplicationMenuItemList items, string activeMenuItemName)
        {
            foreach (var item in items)
            {
                if (item.Name == activeMenuItemName || SetActiveMenuItems(item.Items, activeMenuItemName))
                {
                    item.CssClass = "active";
                    return true;
                }
            }

            return false;
        }
    }
}
