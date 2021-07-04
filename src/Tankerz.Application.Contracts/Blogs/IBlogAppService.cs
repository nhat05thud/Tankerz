using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tankerz.Blogs
{
    public interface IBlogAppService : ICrudAppService<
            BlogDto,
            int,
            PagedAndSortedResultRequestDto,
            CreateUpdateBlogDto>
    {
        Task<ListResultDto<BlogCategoryLookupDto>> GetBlogCategoryLookupAsync();
    }
}
