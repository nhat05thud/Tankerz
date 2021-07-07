using Tankerz.TankerzEntities.BlogCategories;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tankerz.TankerzEntities.Blogs
{
    public class Blog : FullAuditedAggregateRoot<int>
    {
        public Blog()
        {
            IsSpecial = false;
            IsPublish = true;
            IsShowOnHomePage = false;
        }
        public virtual string Banners { get; set; }
        public virtual string Image { get; set; }
        public virtual string ListImages { get; set; }
        public virtual string Name { get; set; }
        public virtual string Slug { get; set; }
        public virtual string Description { get; set; }
        public virtual string Content { get; set; }
        public virtual string Tags { get; set; }
        public virtual int DisplayOrder { get; set; }
        public virtual bool IsSpecial { get; set; }
        public virtual bool IsPublish { get; set; }
        public virtual bool IsShowOnHomePage { get; set; }
        public virtual int CategoryId { get; set; }
        public virtual BlogCategory BlogCategory { get; set; }

        public virtual string MetaTitle { get; set; }
        public virtual string MetaDescription { get; set; }
        public virtual string MetaKeyword { get; set; }
        public virtual string MetaTag { get; set; }
        public virtual string MetaThumbnail { get; set; }
    }
}
