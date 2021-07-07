using Tankerz.TankerzEntities.ProductAttributes;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tankerz.TankerzEntities.ProductAttributeOptions
{
    public class ProductAttributeOption : FullAuditedAggregateRoot<int>
    {
        public virtual string Name { get; set; }
        public virtual string Value { get; set; }
        public virtual int DisplayOrder { get; set; }
        public virtual ProductAttribute ProductAttribute { get; set; }
    }
}
