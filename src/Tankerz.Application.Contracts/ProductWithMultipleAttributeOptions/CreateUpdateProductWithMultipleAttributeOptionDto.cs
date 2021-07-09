using Volo.Abp.Application.Dtos;

namespace Tankerz.ProductWithMultipleAttributeOptions
{
    public class CreateUpdateProductWithMultipleAttributeOptionDto : FullAuditedEntityDto<int>
    {
        public int ProductId { get; set; }
        public int ProductAttributeId { get; set; }
        public int ProductAttributeOptionId { get; set; }
        public int DisplayOrder { get; set; }
    }
}
