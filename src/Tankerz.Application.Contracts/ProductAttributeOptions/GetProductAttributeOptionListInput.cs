using Volo.Abp.Application.Dtos;

namespace Tankerz.ProductAttributeOptions
{
    public class GetProductAttributeOptionListInput : PagedAndSortedResultRequestDto
    {
        public int AttributeId { get; set; }
    }
}
