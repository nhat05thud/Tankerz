using System;
using System.Globalization;
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
    public class EditModel : TankerzPageModel
    {
        [BindProperty]
        public EditProductViewModel Product { get; set; }

        private readonly IProductAppService _productAppService;
        private readonly IProductCategoryAppService _productCategoryAppService;

        public EditModel(IProductAppService productAppService, IProductCategoryAppService productCategoryAppService)
        {
            _productAppService = productAppService;
            _productCategoryAppService = productCategoryAppService;
        }
        public async Task OnGetAsync(int id)
        {
            if (id > 0)
            {
                var productDto = await _productAppService.GetAsync(id);
                Product = ObjectMapper.Map<ProductDto, EditProductViewModel>(productDto);

                var category = await _productCategoryAppService.GetAsync(productDto.ProductCategoryId);
                if (category != null)
                {
                    Product.ProductCategoryName = category.Name;
                }
                else
                {
                    Product.ProductCategoryName = "Null --- Category";
                }
            }
            else
            {
                Product = new EditProductViewModel();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Product.Slug = StringHelper.GenerateSlug(Product.Slug);

            await _productAppService.UpdateAsync(
                Product.Id,
                ObjectMapper.Map<EditProductViewModel, CreateUpdateProductDto>(Product)
            );

            // return edit page
            return new RedirectToPageResult("Edit", new { id = Product.Id });
        }

        public class EditProductViewModel
        {
            [HiddenInput]
            public int Id { get; set; }
            [HiddenInput]
            public int ProductCategoryId { get; set; }
            public string ProductCategoryName { get; set; }
            public string Banners { get; set; }
            public string Image { get; set; }
            public string ListImages { get; set; }
            [Required]
            [DataType(DataType.Currency)]
            public float Price { get; set; }
            [DataType(DataType.Currency)]
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
