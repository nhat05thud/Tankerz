using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tankerz.BlogCategories;

namespace Tankerz.Web.Pages.Blogs
{
    [Authorize]
    public class IndexModel : TankerzPageModel
    {
        [BindProperty]
        public BlogCateViewModel Blog { get; set; }

        private readonly IBlogCategoryAppService _blogCategoryAppService;

        public IndexModel(IBlogCategoryAppService blogCategoryAppService)
        {
            _blogCategoryAppService = blogCategoryAppService;
        }

        public async Task OnGetAsync(int cateid)
        {
            Blog = new BlogCateViewModel();

            if (cateid > 0)
            {
                var category = await _blogCategoryAppService.GetAsync(cateid);
                Blog.Name = category.Name ?? "";
            }
        }
        public class BlogCateViewModel
        {
            public string Name { get; set; }
        }
    }
}
