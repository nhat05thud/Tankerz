using Volo.Abp.Application.Dtos;

namespace Tankerz.ProductWithMultipleAttributeOptions
{
    public class ProductWithMultipleAttributeOptionDto : FullAuditedEntityDto<int>
    {
        public int ProductId { get; set; }
        public string ProductAttributeName { get; set; }
        public int ProductAttributeId { get; set; }
        public string ProductAttributeOptionName { get; set; }
        public int ProductAttributeOptionId { get; set; }
        public int DisplayOrder { get; set; }
    }
}
