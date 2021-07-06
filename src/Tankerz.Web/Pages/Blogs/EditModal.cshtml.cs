using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tankerz.BlogCategories;
using Tankerz.Blogs;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Tankerz.Web.Pages.Blogs
{
    public class EditModalModel : TankerzPageModel
    {
        [BindProperty]
        public EditBlogViewModel Blog { get; set; }
        public List<SelectListItem> BlogCategories { get; set; }

        private readonly IBlogAppService _blogAppService;
        private readonly IBlogCategoryAppService _blogCategoryAppService;

        public EditModalModel(IBlogAppService blogAppService, IBlogCategoryAppService blogCategoryAppService)
        {
            _blogAppService = blogAppService;
            _blogCategoryAppService = blogCategoryAppService;
        }

        public async Task OnGetAsync(int id)
        {
            var blogDto = await _blogAppService.GetAsync(id);
            Blog = ObjectMapper.Map<BlogDto, EditBlogViewModel>(blogDto);

            var category = await _blogCategoryAppService.GetAsync(blogDto.CategoryId);
            if (category != null)
            {
                Blog.BlogCategoryName = category.Name;
            }
            else
            {
                Blog.BlogCategoryName = "Null --- Category";
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _blogAppService.UpdateAsync(
                Blog.Id,
                ObjectMapper.Map<EditBlogViewModel, CreateUpdateBlogDto>(Blog)
            );
            return NoContent();
        }

        public class EditBlogViewModel
        {
            [HiddenInput]
            public int Id { get; set; }
            public int BlogCategoryId { get; set; }
            public string BlogCategoryName { get; set; }
            public string Banners { get; set; }
            public string Image { get; set; }
            public string ListImages { get; set; }
            [Required]
            [StringLength(256)]
            public string Name { get; set; }
            [TextArea]
            public string Description { get; set; }
            [TextArea]
            public string Content { get; set; }
            public string Tags { get; set; }
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
