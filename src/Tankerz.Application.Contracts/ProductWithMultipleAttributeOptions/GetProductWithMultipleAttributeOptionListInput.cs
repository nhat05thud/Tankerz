using Volo.Abp.Application.Dtos;

namespace Tankerz.ProductWithMultipleAttributeOptions
{
    public class GetProductWithMultipleAttributeOptionListInput : PagedAndSortedResultRequestDto
    {
        public int ProductId { get; set; }
    }
}
