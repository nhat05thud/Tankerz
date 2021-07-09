using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tankerz.Helper;
using Tankerz.ProductCategories;
using Tankerz.Products;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Tankerz.Web.Pages.Products
{
    [Authorize]
    public class CreateModel : TankerzPageModel
    {
        [BindProperty]
        public CreateProductViewModel Product { get; set; }

        private readonly IProductAppService _productAppService;
        private readonly IProductCategoryAppService _productCategoryAppService;

        public CreateModel(IProductAppService productAppService, IProductCategoryAppService productCategoryAppService)
        {
            _productAppService = productAppService;
            _productCategoryAppService = productCategoryAppService;
        }

        public async Task OnGetAsync()
        {
            var cateId = Request.Query["cateid"];
            Product = new CreateProductViewModel();
            if (int.TryParse(cateId, out int intValue))
            {
                var category = await _productCategoryAppService.GetAsync(int.Parse(cateId));

                if (category != null)
                {
                    Product = new CreateProductViewModel
                    {
                        ProductCategoryId = category.Id,
                        ProductCategoryName = category.Name
                    };
                }
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Product.Slug = StringHelper.GenerateSlug(Product.Slug);

            var dto = ObjectMapper.Map<CreateProductViewModel, CreateUpdateProductDto>(Product);
            var product = await _productAppService.CreateAsync(dto);
            
            // return edit page
            return new RedirectToPageResult("Edit", new { id = product.Id });
        }

        public class CreateProductViewModel
        {
            public CreateProductViewModel()
            {
                IsPublish = true;
            }
            [HiddenInput]
            public int ProductCategoryId { get; set; }
            public string ProductCategoryName { get; set; }
            public string Banners { get; set; }
            public string Image { get; set; }
            public string ListImages { get; set; }
            [Required]
            public float Price { get; set; }
            public float OldPrice { get; set; }
            [Required]
            [StringLength(256)]
            public string Name { get; set; }
            [Required]
            public string Slug { get; set; }
            [TextArea]
            public string Description { get; set; }
            [TextArea]
            public string Content { get; set; }
            public string Tags { get; set; }
            public int DisplayOrder { get; set; }
            public bool IsSpecial { get; set; }
            public bool IsPublish { get; set; }
            public bool IsShowOnHomePage { get; set; }


            public string MetaTitle { get; set; }
            public string MetaDescription { get; set; }
            public string MetaKeyword { get; set; }
            public string MetaTag { get; set; }
            public string MetaThumbnail { get; set; }
        }
    }
}
