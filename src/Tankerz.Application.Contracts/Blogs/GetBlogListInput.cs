using Volo.Abp.Application.Dtos;

namespace Tankerz.Blogs
{
    public class GetBlogListInput : PagedAndSortedResultRequestDto
    {
        public int CateId { get; set; }
    }
}
