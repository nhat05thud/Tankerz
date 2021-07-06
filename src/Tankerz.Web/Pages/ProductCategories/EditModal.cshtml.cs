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
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Tankerz.Web.Pages.ProductCategories
{
    public class EditModalModel : TankerzPageModel
    {
        [BindProperty]
        public EditProductCategoryViewModel ProductCategory { get; set; }
        public List<SelectListItem> ProductGroups { get; set; }

        private readonly IProductCategoryAppService _productCategoryAppService;

        public EditModalModel(IProductCategoryAppService productCategoryAppService)
        {
            _productCategoryAppService = productCategoryAppService;
        }

        public async Task OnGetAsync(int id)
        {
            var productCategoryDto = await _productCategoryAppService.GetAsync(id);
            ProductCategory = ObjectMapper.Map<ProductCategoryDto, EditProductCategoryViewModel>(productCategoryDto);

            var productGroupLookup = await _productCategoryAppService.GetProductGroupLookupAsync();
            ProductGroups = productGroupLookup.Items
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _productCategoryAppService.UpdateAsync(
                ProductCategory.Id,
                ObjectMapper.Map<EditProductCategoryViewModel, CreateUpdateProductCategoryDto>(ProductCategory)
            );
            return NoContent();
        }

        public class EditProductCategoryViewModel
        {
            [HiddenInput]
            public int Id { get; set; }
            public string Banners { get; set; }
            public string Image { get; set; }
            public string ListImages { get; set; }
            [Required]
            [StringLength(256)]
            public string Name { get; set; }
            [TextArea]
            public string Description { get; set; }
            public int Priority { get; set; }
            public bool IsPublish { get; set; }
            public bool IsShowOnMenu { get; set; }
            public bool IsShowOnHomePage { get; set; }
            [SelectItems(nameof(ProductGroups))]
            [DisplayName("ProductGroup")]
            public int ProductGroupId { get; set; }


            public string MetaTitle { get; set; }
            public string MetaDescription { get; set; }
            public string MetaKeyword { get; set; }
            public string MetaTag { get; set; }
            public string MetaThumbnail { get; set; }
        }
    }
}
