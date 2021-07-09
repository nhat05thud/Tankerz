using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tankerz.ProductAttributeOptions;
using Tankerz.ProductAttributes;

namespace Tankerz.Web.Pages.ProductAttributes.Options
{
    public class CreateModalModel : TankerzPageModel
    {
        [BindProperty]
        public CreateProductAttributeOptionViewModel ProductAttributeOption { get; set; }

        private readonly IProductAttributeOptionAppService _productAttributeOptionAppService;
        private readonly IProductAttributeAppService _productAttributeAppService;

        public CreateModalModel(IProductAttributeOptionAppService productAttributeOptionAppService,
            IProductAttributeAppService productAttributeAppService)
        {
            _productAttributeOptionAppService = productAttributeOptionAppService;
            _productAttributeAppService = productAttributeAppService;
        }

        public async Task OnGetAsync(int attributeid)
        {
            ProductAttributeOption = new CreateProductAttributeOptionViewModel();

            if (attributeid > 0)
            {
                var productAttribute = await _productAttributeAppService.GetAsync(attributeid);
                if (productAttribute != null)
                {
                    ProductAttributeOption = new CreateProductAttributeOptionViewModel
                    {
                        ProductAttributeId = productAttribute.Id
                    };
                }
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateProductAttributeOptionViewModel, CreateUpdateProductAttributeOptionDto>(ProductAttributeOption);
            await _productAttributeOptionAppService.CreateAsync(dto);

            return NoContent();
        }

        public class CreateProductAttributeOptionViewModel
        {
            [HiddenInput]
            public int ProductAttributeId { get; set; }
            [Required]
            public string Name { get; set; }
            [Required]
            public string Value { get; set; }
            public int DisplayOrder { get; set; }
        }
    }
}
