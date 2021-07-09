using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tankerz.BlogCategories;
using Tankerz.Blogs;
using Tankerz.Helper;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Tankerz.Web.Pages.Blogs
{
    public class CreateModel : TankerzPageModel
    {
        [BindProperty]
        public CreateBlogViewModel Blog { get; set; }


        private readonly IBlogAppService _blogAppService;
        private readonly IBlogCategoryAppService _blogCategoryAppService;

        public CreateModel(IBlogAppService blogAppService, IBlogCategoryAppService blogCategoryAppService)
        {
            _blogAppService = blogAppService;
            _blogCategoryAppService = blogCategoryAppService;
        }

        public async Task OnGetAsync(int cateid)
        {
            Blog = new CreateBlogViewModel();

            if (cateid > 0)
            {
                var category = await _blogCategoryAppService.GetAsync(cateid);
                if (category != null)
                {
                    Blog = new CreateBlogViewModel
                    {
                        CategoryId = category.Id,
                        BlogCategoryName = category.Name
                    };
                }
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Blog.Slug = StringHelper.GenerateSlug(Blog.Slug);

            var dto = ObjectMapper.Map<CreateBlogViewModel, CreateUpdateBlogDto>(Blog);
            var blog = await _blogAppService.CreateAsync(dto);

            // return edit page
            return new RedirectToPageResult("Edit", new { id = blog.Id });
        }

        public class CreateBlogViewModel
        {
            public CreateBlogViewModel()
            {
                IsPublish = true;
            }
            [HiddenInput]
            public int CategoryId { get; set; }
            public string BlogCategoryName { get; set; }

            public string Banners { get; set; }
            public string Image { get; set; }
            public string ListImages { get; set; }
            [Required]
            [StringLength(256)]
            public string Name { get; set; }
            [Required]
            public string Slug { get; set; }
            [TextArea]
            public string Description { get; set; }
            [TextArea]
            public string Content { get; set; }
            public string Tags { get; set; }
            public int DisplayOrder { get; set; }
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
