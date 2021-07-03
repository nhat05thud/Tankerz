using Tankerz.TankerzEntities.BlogCategories;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Tankerz.BlogCategories
{
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