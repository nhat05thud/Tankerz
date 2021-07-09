using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tankerz.ProductAttributes;

namespace Tankerz.Web.Pages.ProductAttributes
{
    [Authorize]
    public class CreateModel : TankerzPageModel
    {
        [BindProperty]
        public CreateProductAttributeViewModel ProductAttribute { get; set; }

        private readonly IProductAttributeAppService _productAttributeAppService;

        public CreateModel(IProductAttributeAppService productAttributeAppService)
        {
            _productAttributeAppService = productAttributeAppService;
        }
        public void OnGet()
        {
            ProductAttribute = new CreateProductAttributeViewModel();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateProductAttributeViewModel, CreateUpdateProductAttributeDto>(ProductAttribute);
            var productAttribute = await _productAttributeAppService.CreateAsync(dto);

            // return edit page
            return new RedirectToPageResult("Edit", new { id = productAttribute.Id });
        }

        public class CreateProductAttributeViewModel
        {
            [Required]
            public virtual string Name { get; set; }
            public virtual int DisplayOrder { get; set; }
        }
    }
}
