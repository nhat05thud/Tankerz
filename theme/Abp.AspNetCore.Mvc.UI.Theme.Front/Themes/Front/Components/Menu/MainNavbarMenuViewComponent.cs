using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tankerz.ProductGroups;
using Tankerz.TankerzEntities.BlogCategories;
using Tankerz.TankerzEntities.ProductCategories;
using Tankerz.TankerzEntities.ProductGroups;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Layout;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.UI.Navigation;

namespace Abp.AspNetCore.Mvc.UI.Theme.Front.Themes.Front.Components.Menu
{
    public class MainNavbarMenuViewComponent : AbpViewComponent
    {
        private readonly IMenuManager _menuManager;
        private readonly IRepository<ProductGroup, int> _productGroupRepository;
        private readonly IRepository<ProductCategory, int> _productCategoryRepository;
        private readonly IRepository<BlogCategory, int> _blogCategoryRepository;
        protected IPageLayout PageLayout { get; }

        public MainNavbarMenuViewComponent(
                IMenuManager menuManager, 
                IPageLayout pageLayout,
                IRepository<ProductGroup, int> productGroupRepository,
                IRepository<ProductCategory, int> productCategoryRepository,
                IRepository<BlogCategory, int> blogCategoryRepository
            )
        {
            _menuManager = menuManager;
            PageLayout = pageLayout;
            _productGroupRepository = productGroupRepository;
            _productCategoryRepository = productCategoryRepository;
            _blogCategoryRepository = blogCategoryRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var menu = await _menuManager.GetAsync(StandardMenus.Main);
            // default menu index
            var menuIndex = 1;
            // add dynamic productgroup menu
            var productGroups = await _productGroupRepository.GetListAsync();
            var productGroupsWithOrder = productGroups.OrderBy(x => x.Priority).ToList();

            foreach (var group in productGroupsWithOrder)
            {
                var productGroupMenus = new ApplicationMenuItem(group.Name, group.Name, null, "fas fa-tshirt", menuIndex);

                var catetegories = await _productCategoryRepository.GetListAsync(x => x.ProductGroupId == group.Id);

                foreach (var cate in catetegories)
                {
                    productGroupMenus.AddItem(
                        new ApplicationMenuItem(
                            cate.Name,
                            cate.Name,
                            "~/Products?cateid=" + cate.Id,
                            "fas fa-th-large",
                            0
                        )
                    );
                }
                menu.Items.Insert(menuIndex, productGroupMenus);
                menuIndex++;
            }

            // add dynamic blogcategory menu
            var blogCategories = await _blogCategoryRepository.GetListAsync();
            var blogCategoriesWithOrder = blogCategories.OrderBy(x => x.Priority).ToList();
            foreach (var blogCate in blogCategoriesWithOrder)
            {
                var productGroupMenus = new ApplicationMenuItem(
                    blogCate.Name,
                    blogCate.Name, 
                    "~/Blogs?cateid=" + blogCate.Id, 
                    "fas fa-newspaper", 
                    0
                );

                menu.Items.Insert(menuIndex, productGroupMenus);
                menuIndex++;
            }
            // ======================================== //
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
