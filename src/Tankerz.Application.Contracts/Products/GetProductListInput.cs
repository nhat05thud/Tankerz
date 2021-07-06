using Volo.Abp.Application.Dtos;

namespace Tankerz.Products
{
    public class GetProductListInput : PagedAndSortedResultRequestDto
    {
        public int CateId { get; set; }
    }
}
