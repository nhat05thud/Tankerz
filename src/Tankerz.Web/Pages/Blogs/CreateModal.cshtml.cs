using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tankerz.BlogCategories;
using Tankerz.Blogs;
using Tankerz.Helper;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Tankerz.Web.Pages.Blogs
{
    public class CreateModalModel : TankerzPageModel
    {
        [BindProperty]
        public CreateBlogViewModel Blog { get; set; }


        private readonly IBlogAppService _blogAppService;
        private readonly IBlogCategoryAppService _blogCategoryAppService;

        public CreateModalModel(IBlogAppService blogAppService, IBlogCategoryAppService blogCategoryAppService)
        {
            _blogAppService = blogAppService;
            _blogCategoryAppService = blogCategoryAppService;
        }

        public async Task OnGetAsync(int id)
        {
            Blog = new CreateBlogViewModel();

            if (id > 0)
            {
                var category = await _blogCategoryAppService.GetAsync(id);
                if (category != null)
                {
                    Blog = new CreateBlogViewModel
                    {
                        BlogCategoryId = category.Id,
                        BlogCategoryName = category.Name
                    };
                }
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Blog.Slug = StringHelper.GenerateSlug(Blog.Slug);

            var dto = ObjectMapper.Map<CreateBlogViewModel, CreateUpdateBlogDto>(Blog);
            await _blogAppService.CreateAsync(dto);
            return NoContent();
        }

        public class CreateBlogViewModel
        {
            public CreateBlogViewModel()
            {
                IsPublish = true;
            }
            public int BlogCategoryId { get; set; }
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
