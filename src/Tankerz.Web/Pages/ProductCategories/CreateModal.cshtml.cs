using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tankerz.Helper;
using Tankerz.ProductCategories;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Tankerz.Web.Pages.ProductCategories
{
    public class CreateModalModel : TankerzPageModel
    {
        [BindProperty]
        public CreateProductCategoryViewModel ProductCategory { get; set; }

        public List<SelectListItem> ProductGroups { get; set; }


        private readonly IProductCategoryAppService _productCategoryAppService;

        public CreateModalModel(IProductCategoryAppService productCategoryAppService)
        {
            _productCategoryAppService = productCategoryAppService;
        }

        public async Task OnGetAsync()
        {
            ProductCategory = new CreateProductCategoryViewModel();

            var productGroupLookup = await _productCategoryAppService.GetProductGroupLookupAsync();
            ProductGroups = productGroupLookup.Items
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ProductCategory.Slug = StringHelper.GenerateSlug(ProductCategory.Slug);

            var dto = ObjectMapper.Map<CreateProductCategoryViewModel, CreateUpdateProductCategoryDto>(ProductCategory);
            await _productCategoryAppService.CreateAsync(dto);
            return NoContent();
        }

        public class CreateProductCategoryViewModel
        {
            public CreateProductCategoryViewModel()
            {
                IsPublish = true;
            }
            [SelectItems(nameof(ProductGroups))]
            [DisplayName("ProductGroup")]
            public int ProductGroupId { get; set; }

            public string Banners { get; set; }
            public string Image { get; set; }
            public string ListImages { get; set; }
            [Required]
            [StringLength(256)]
            public string Name { get; set; }
            [Required]
            public string Slug { get; set; }
            [TextArea]
            public string Description { get; set; }
            public int DisplayOrder { get; set; }
            public bool IsPublish { get; set; }
            public bool IsShowOnMenu { get; set; }
            public bool IsShowOnHomePage { get; set; }


            public string MetaTitle { get; set; }
            public string MetaDescription { get; set; }
            public string MetaKeyword { get; set; }
            public string MetaTag { get; set; }
            public string MetaThumbnail { get; set; }
        }
    }
}
