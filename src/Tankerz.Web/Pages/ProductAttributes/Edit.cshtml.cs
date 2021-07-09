using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tankerz.ProductAttributes;

namespace Tankerz.Web.Pages.ProductAttributes
{
    [Authorize]
    public class EditModel : TankerzPageModel
    {
        [BindProperty]
        public EditProductAttributeViewModel ProductAttribute { get; set; }

        private readonly IProductAttributeAppService _productAttributeAppService;

        public EditModel(IProductAttributeAppService productAttributeAppService)
        {
            _productAttributeAppService = productAttributeAppService;
        }
        public async Task OnGetAsync(int id)
        {
            if (id > 0)
            {
                var productAttributeDto = await _productAttributeAppService.GetAsync(id);
                ProductAttribute = ObjectMapper.Map<ProductAttributeDto, EditProductAttributeViewModel>(productAttributeDto);
            }
            else
            {
                ProductAttribute = new EditProductAttributeViewModel();
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await _productAttributeAppService.UpdateAsync(
                ProductAttribute.Id,
                ObjectMapper.Map<EditProductAttributeViewModel, CreateUpdateProductAttributeDto>(ProductAttribute)
            );

            // return edit page
            return new RedirectToPageResult("Edit", new { id = ProductAttribute.Id });
        }
        public class EditProductAttributeViewModel
        {
            [HiddenInput]
            public int Id { get; set; }
            [Required]
            public virtual string Name { get; set; }
            public virtual int DisplayOrder { get; set; }
        }
    }
}
