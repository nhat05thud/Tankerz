using Tankerz.TankerzEntities.ProductAttributeOptions;
using Tankerz.TankerzEntities.ProductAttributes;
using Tankerz.TankerzEntities.Products;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tankerz.TankerzEntities.ProductWithMultipleAttributeOptions
{
    public class ProductWithMultipleAttributeOption : FullAuditedAggregateRoot<int>
    {
        public virtual int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual int ProductAttributeId { get; set; }
        public virtual int ProductAttributeOptionId { get; set; }
        public virtual ProductAttributeOption ProductAttributeOption { get; set; }
        public virtual int DisplayOrder { get; set; }
    }
}
