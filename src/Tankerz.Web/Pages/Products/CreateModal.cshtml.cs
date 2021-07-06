using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tankerz.ProductCategories;
using Tankerz.Products;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Tankerz.Web.Pages.Products
{
    public class CreateModalModel : TankerzPageModel
    {
        [BindProperty]
        public CreateProductViewModel Product { get; set; }

        private readonly IProductAppService _productAppService;
        private readonly IProductCategoryAppService _productCategoryAppService;

        public CreateModalModel(IProductAppService productAppService, IProductCategoryAppService productCategoryAppService)
        {
            _productAppService = productAppService;
            _productCategoryAppService = productCategoryAppService;
        }

        public async Task OnGetAsync(int id)
        {
            Product = new CreateProductViewModel();

            if (id > 0)
            {
                var category = await _productCategoryAppService.GetAsync(id);

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
            var dto = ObjectMapper.Map<CreateProductViewModel, CreateUpdateProductDto>(Product);
            await _productAppService.CreateAsync(dto);
            return NoContent();
        }

        public class CreateProductViewModel
        {
            public CreateProductViewModel()
            {
                IsPublish = true;
            }
            public int ProductCategoryId { get; set; }
            public string ProductCategoryName { get; set; }
            public string Banners { get; set; }
            public string Image { get; set; }
            public string ListImages { get; set; }
            public decimal Price { get; set; }
            public decimal OldPrice { get; set; }
            [Required]
            [StringLength(256)]
            public string Name { get; set; }
            [TextArea]
            public string Description { get; set; }
            [TextArea]
            public string Content { get; set; }
            public int Priority { get; set; }
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
