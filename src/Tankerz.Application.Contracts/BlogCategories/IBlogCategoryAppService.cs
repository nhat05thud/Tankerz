using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tankerz.BlogCategories
{
    public interface IBlogCategoryAppService :
        ICrudAppService<
            BlogCategoryDto,
            int,
            PagedAndSortedResultRequestDto,
            CreateUpdateBlogCategoryDto> 
    {
    }
}
