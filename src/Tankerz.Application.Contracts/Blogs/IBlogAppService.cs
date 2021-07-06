using Volo.Abp.Application.Services;

namespace Tankerz.Blogs
{
    public interface IBlogAppService : ICrudAppService<
            BlogDto,
            int,
            GetBlogListInput,
            CreateUpdateBlogDto>
    {
        //Task<ListResultDto<BlogCategoryLookupDto>> GetBlogCategoryLookupAsync();
    }
}
