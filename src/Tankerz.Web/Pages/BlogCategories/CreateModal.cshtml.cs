using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tankerz.BlogCategories;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Tankerz.Web.Pages.BlogCategories
{
    public class CreateModalModel : TankerzPageModel
    {
        [BindProperty]
        public CreateBlogCategoryViewModel BlogCategory { get; set; }

        private readonly IBlogCategoryAppService _blogCategoryAppService;

        public CreateModalModel(IBlogCategoryAppService blogCategoryAppService)
        {
            _blogCategoryAppService = blogCategoryAppService;
        }

        public void OnGet()
        {
            BlogCategory = new CreateBlogCategoryViewModel();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateBlogCategoryViewModel, CreateUpdateBlogCategoryDto>(BlogCategory);
            await _blogCategoryAppService.CreateAsync(dto);
            return NoContent();
        }

        public class CreateBlogCategoryViewModel
        {
            public CreateBlogCategoryViewModel()
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
