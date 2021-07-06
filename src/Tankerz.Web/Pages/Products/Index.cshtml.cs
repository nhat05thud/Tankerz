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

        public async Task OnGetAsync()
        {
            Product = new ProductCateViewModel();
            if (int.TryParse(Request.Query["category"], out int numValue))
            {
                var category = await _productCategoryAppService.GetAsync(int.Parse(Request.Query["category"]));
                Product.Name = category.Name;
            }
        }
        public class ProductCateViewModel
        {
            public string Name { get; set; }
        }
    }
}
