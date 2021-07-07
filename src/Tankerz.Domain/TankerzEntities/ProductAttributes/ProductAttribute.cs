using System.Collections.Generic;
using Tankerz.TankerzEntities.ProductAttributeOptions;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tankerz.TankerzEntities.ProductAttributes
{
    public class ProductAttribute : FullAuditedAggregateRoot<int>
    {
        public virtual string Name { get; set; }
        public virtual int DisplayOrder { get; set; }
        public virtual List<ProductAttributeOption> ProductAttributeOptions { get; set; }
    }
}
