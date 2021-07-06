using System.Collections.Generic;
using Tankerz.TankerzEntities.ProductGroups;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tankerz.TankerzEntities.ProductCategories
{
    public class ProductCategory : FullAuditedAggregateRoot<int>
    {
        public ProductCategory()
        {
            IsPublish = true;
            IsShowOnMenu = false;
            IsShowOnHomePage = false;
        }
        public virtual string Banners { get; set; }
        public virtual string Image { get; set; }
        public virtual string ListImages { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual int Priority { get; set; }
        public virtual bool IsPublish { get; set; }
        public virtual bool IsShowOnMenu { get; set; }
        public virtual bool IsShowOnHomePage { get; set; }
        public virtual int ProductGroupId { get; set; }
        public virtual ProductGroup ProductGroup { get; set; }
        public virtual List<ProductCategory> ProductCategories { get; set; }


        public virtual string MetaTitle { get; set; }
        public virtual string MetaDescription { get; set; }
        public virtual string MetaKeyword { get; set; }
        public virtual string MetaTag { get; set; }
        public virtual string MetaThumbnail { get; set; }
    }
}
