using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tankerz.ProductGroups;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Tankerz.Web.Pages.ProductGroups
{
    public class CreateModalModel : TankerzPageModel
    {
        [BindProperty]
        public CreateProductGroupViewModel ProductGroup { get; set; }

        private readonly IProductGroupAppService _productGroupAppService;

        public CreateModalModel(IProductGroupAppService productGroupAppService)
        {
            _productGroupAppService = productGroupAppService;
        }

        public void OnGet()
        {
            ProductGroup = new CreateProductGroupViewModel();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateProductGroupViewModel, CreateUpdateProductGroupDto>(ProductGroup);
            await _productGroupAppService.CreateAsync(dto);
            return NoContent();
        }

        public class CreateProductGroupViewModel
        {
            public CreateProductGroupViewModel()
            {
                IsPublish = true;
            }
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


            public string MetaTitle { get; set; }
            public string MetaDescription { get; set; }
            public string MetaKeyword { get; set; }
            public string MetaTag { get; set; }
            public string MetaThumbnail { get; set; }
        }
    }
}
