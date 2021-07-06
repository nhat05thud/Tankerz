using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tankerz.ProductGroups;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Tankerz.Web.Pages.ProductGroups
{
    public class EditModalModel : TankerzPageModel
    {
        [BindProperty]
        public EditProductGroupViewModel ProductGroup { get; set; }

        private readonly IProductGroupAppService _productGroupAppService;

        public EditModalModel(IProductGroupAppService productGroupAppService)
        {
            _productGroupAppService = productGroupAppService;
        }

        public async Task OnGetAsync(int id)
        {
            var productGroupDto = await _productGroupAppService.GetAsync(id);
            ProductGroup = ObjectMapper.Map<ProductGroupDto, EditProductGroupViewModel>(productGroupDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<EditProductGroupViewModel, CreateUpdateProductGroupDto>(ProductGroup);
            await _productGroupAppService.UpdateAsync(ProductGroup.Id, dto);
            return NoContent();
        }

        public class EditProductGroupViewModel
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


            public string MetaTitle { get; set; }
            public string MetaDescription { get; set; }
            public string MetaKeyword { get; set; }
            public string MetaTag { get; set; }
            public string MetaThumbnail { get; set; }
        }
    }
}
