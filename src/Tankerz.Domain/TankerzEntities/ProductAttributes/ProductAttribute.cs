using Volo.Abp.Domain.Entities.Auditing;

namespace Tankerz.TankerzEntities.ProductAttributes
{
    public class ProductAttribute : FullAuditedAggregateRoot<int>
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
    }
}
