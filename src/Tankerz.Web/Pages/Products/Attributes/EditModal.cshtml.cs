using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tankerz.ProductWithMultipleAttributeOptions;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Tankerz.Web.Pages.Products.Attributes
{
    public class EditModalModel : TankerzPageModel
    {
        [BindProperty]
        public EditProductMultipleAttributeOptionViewModel ProductAttribute { get; set; }
        public List<SelectListItem> ProductAttributes { get; set; }
        public List<SelectListItem> ProductAttributeOptions { get; set; }

        private readonly IProductWithMultipleAttributeOptionAppService _productWithMultipleAttributeOptionAppService;

        public EditModalModel(IProductWithMultipleAttributeOptionAppService productWithMultipleAttributeOptionAppService)
        {
            _productWithMultipleAttributeOptionAppService = productWithMultipleAttributeOptionAppService;
        }
        public async Task OnGetAsync(int id)
        {
            var productAttributesDto = await _productWithMultipleAttributeOptionAppService.GetAsync(id);

            ProductAttribute = ObjectMapper.Map<ProductWithMultipleAttributeOptionDto, EditProductMultipleAttributeOptionViewModel>(productAttributesDto);

            if (ProductAttribute != null)
            {
                ProductAttribute.ProductId = ProductAttribute.ProductId;

                var productAttributeLookup = await _productWithMultipleAttributeOptionAppService.GetProductAttributeLookupAsync();
                ProductAttributes = productAttributeLookup.Items
                    .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                    .ToList();
                if (productAttributeLookup.Items.Count > 0)
                {
                    var productAttributeOptionLookup = await _productWithMultipleAttributeOptionAppService.GetProductAttributeOptionLookupAsync(ProductAttribute.ProductAttributeId);
                    ProductAttributeOptions = productAttributeOptionLookup.Items
                        .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                        .ToList();
                }
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _productWithMultipleAttributeOptionAppService.UpdateAsync(
                ProductAttribute.Id,
                ObjectMapper.Map<EditProductMultipleAttributeOptionViewModel, CreateUpdateProductWithMultipleAttributeOptionDto>(ProductAttribute)
            );
            return NoContent();
        }
        public class EditProductMultipleAttributeOptionViewModel
        {
            [HiddenInput]
            public int Id { get; set; }
            [HiddenInput]
            public int ProductId { get; set; }

            [SelectItems(nameof(ProductAttributes))]
            [DisplayName("ProductAttribute")]
            public int ProductAttributeId { get; set; }

            [SelectItems(nameof(ProductAttributeOptions))]
            [DisplayName("ProductAttributeOption")]
            public int ProductAttributeOptionId { get; set; }

            public int DisplayOrder { get; set; }
        }
    }
}
