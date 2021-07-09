using Volo.Abp.Application.Dtos;

namespace Tankerz.ProductAttributes
{
    public class CreateUpdateProductAttributeDto : FullAuditedEntityDto<int>
    {
        public virtual string Name { get; set; }
        public virtual int DisplayOrder { get; set; }
    }
}
