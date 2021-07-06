using System.Collections.Generic;
using Tankerz.TankerzEntities.Blogs;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tankerz.TankerzEntities.BlogCategories
{
    public class BlogCategory : FullAuditedAggregateRoot<int>
    {
        public BlogCategory()
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
        public virtual List<Blog> Blogs { get; set; }


        public virtual string MetaTitle { get; set; }
        public virtual string MetaDescription { get; set; }
        public virtual string MetaKeyword { get; set; }
        public virtual string MetaTag { get; set; }
        public virtual string MetaThumbnail { get; set; }
    }
}