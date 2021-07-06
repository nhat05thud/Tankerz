using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tankerz.ProductCategories;

namespace Tankerz.Web.Pages.Products
{
    [Authorize]
    public class IndexModel : TankerzPageModel
    {
        [BindProperty]
        public ProductCateViewModel Product { get; set; }

        private readonly IProductCategoryAppService _productCategoryAppService;

        public IndexModel(IProductCategoryAppService productCategoryAppService)
        {
            _productCategoryAppService = productCategoryAppService;
        }

        public async Task OnGetAsync(int cateid)
        {
            Product = new ProductCateViewModel();

            if (cateid > 0)
            {
                var category = await _productCategoryAppService.GetAsync(cateid);
                Product.Name = category.Name ?? "";
            }
        }
        public class ProductCateViewModel
        {
            public string Name { get; set; }
        }
    }
}
