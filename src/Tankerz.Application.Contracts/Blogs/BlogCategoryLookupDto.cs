using Volo.Abp.Application.Dtos;

namespace Tankerz.Blogs
{
    public class BlogCategoryLookupDto : EntityDto<int>
    {
        public string Name { get; set; }
    }
}
