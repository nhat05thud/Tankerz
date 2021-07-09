using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tankerz.ProductWithMultipleAttributeOptions;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Tankerz.Web.Pages.Products.Attributes
{
    public class CreateModalModel : TankerzPageModel
    {
        [BindProperty]
        public CreateProductMultipleAttributeOptionViewModel ProductAttribute { get; set; }
        public List<SelectListItem> ProductAttributes { get; set; }
        public List<SelectListItem> ProductAttributeOptions { get; set; }

        private readonly IProductWithMultipleAttributeOptionAppService _productWithMultipleAttributeOptionAppService;

        public CreateModalModel(IProductWithMultipleAttributeOptionAppService productWithMultipleAttributeOptionAppService)
        {
            _productWithMultipleAttributeOptionAppService = productWithMultipleAttributeOptionAppService;
        }
        public async Task OnGetAsync(int productid)
        {
            ProductAttribute = new CreateProductMultipleAttributeOptionViewModel();
            if (productid > 0)
            {
                ProductAttribute.ProductId = productid;

                var productAttributeLookup = await _productWithMultipleAttributeOptionAppService.GetProductAttributeLookupAsync();
                ProductAttributes = productAttributeLookup.Items
                    .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                    .ToList();
                if (productAttributeLookup.Items.Count > 0)
                {
                    await this.OnGetOption(productAttributeLookup.Items.First().Id);
                }
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateProductMultipleAttributeOptionViewModel, CreateUpdateProductWithMultipleAttributeOptionDto>(ProductAttribute);

            await _productWithMultipleAttributeOptionAppService.CreateAsync(dto);

            return NoContent();
        }
        public async Task<IActionResult> OnGetOption(int attributeid)
        {
            var productAttributeOptionLookup = await _productWithMultipleAttributeOptionAppService.GetProductAttributeOptionLookupAsync(attributeid);
            ProductAttributeOptions = productAttributeOptionLookup.Items
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();

            return new JsonResult(ProductAttributeOptions);
        }
        public class CreateProductMultipleAttributeOptionViewModel
        {
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
