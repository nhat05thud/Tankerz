using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tankerz.Helper;
using Tankerz.ProductCategories;
using Tankerz.Products;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Tankerz.Web.Pages.Products
{
    public class EditModalModel : TankerzPageModel
    {
        [BindProperty]
        public EditProductViewModel Product { get; set; }

        private readonly IProductAppService _productAppService;
        private readonly IProductCategoryAppService _productCategoryAppService;

        public EditModalModel(IProductAppService productAppService, IProductCategoryAppService productCategoryAppService)
        {
            _productAppService = productAppService;
            _productCategoryAppService = productCategoryAppService;
        }
        public async Task OnGetAsync(int id)
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

        public async Task<IActionResult> OnPostAsync()
        {
            Product.Slug = StringHelper.GenerateSlug(Product.Slug);

            await _productAppService.UpdateAsync(
                Product.Id,
                ObjectMapper.Map<EditProductViewModel, CreateUpdateProductDto>(Product)
            );
            return NoContent();
        }

        public class EditProductViewModel
        {
            [HiddenInput]
            public int Id { get; set; }
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
            [Required]
            public string Slug { get; set; }
            [TextArea]
            public string Description { get; set; }
            [TextArea]
            public string Content { get; set; }
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
