using Volo.Abp.Application.Dtos;

namespace Tankerz.ProductAttributeOptions
{
    public class CreateUpdateProductAttributeOptionDto : FullAuditedEntityDto<int>
    {
        public int ProductAttributeId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int DisplayOrder { get; set; }
    }
}
