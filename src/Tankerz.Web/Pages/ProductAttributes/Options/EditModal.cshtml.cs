using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tankerz.ProductAttributeOptions;
using Tankerz.ProductAttributes;

namespace Tankerz.Web.Pages.ProductAttributes.Options
{
    public class EditModalModel : TankerzPageModel
    {
        [BindProperty]
        public EditProductAttributeOptionViewModel ProductAttributeOption { get; set; }

        private readonly IProductAttributeOptionAppService _productAttributeOptionAppService;
        private readonly IProductAttributeAppService _productAttributeAppService;

        public EditModalModel(IProductAttributeOptionAppService productAttributeOptionAppService,
            IProductAttributeAppService productAttributeAppService)
        {
            _productAttributeOptionAppService = productAttributeOptionAppService;
            _productAttributeAppService = productAttributeAppService;
        }

        public async Task OnGetAsync(int id)
        {
            var productAttributeOptionDto = await _productAttributeOptionAppService.GetAsync(id);
            ProductAttributeOption = ObjectMapper.Map<ProductAttributeOptionDto, EditProductAttributeOptionViewModel>(productAttributeOptionDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<EditProductAttributeOptionViewModel, CreateUpdateProductAttributeOptionDto>(ProductAttributeOption);
            await _productAttributeOptionAppService.UpdateAsync(ProductAttributeOption.Id, dto);

            return NoContent();
        }

        public class EditProductAttributeOptionViewModel
        {
            [HiddenInput]
            public int ProductAttributeId { get; set; }
            [HiddenInput]
            public int Id { get; set; }
            [Required]
            public string Name { get; set; }
            [Required]
            public string Value { get; set; }
            public int DisplayOrder { get; set; }
        }
    }
}
