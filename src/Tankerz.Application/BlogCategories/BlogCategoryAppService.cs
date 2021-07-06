using Microsoft.AspNetCore.Authorization;
using Tankerz.TankerzEntities.BlogCategories;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Tankerz.BlogCategories
{
    [Authorize]
    public class BlogCategoryAppService :
        CrudAppService<
            BlogCategory,
            BlogCategoryDto,
            int,
            PagedAndSortedResultRequestDto,
            CreateUpdateBlogCategoryDto>,
        IBlogCategoryAppService
    {
        public BlogCategoryAppService(IRepository<BlogCategory, int> repository)
            : base(repository)
        {

        }
    }
}