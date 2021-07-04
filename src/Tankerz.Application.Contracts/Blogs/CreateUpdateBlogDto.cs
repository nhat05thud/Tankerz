using System;
using System.Collections.Generic;
using System.Text;

namespace Tankerz.Blogs
{
    public class CreateUpdateBlogDto
    {
        public virtual string Banners { get; set; }
        public virtual string Images { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string Content { get; set; }
        public virtual string Tags { get; set; }
        public virtual int Priority { get; set; }
        public virtual bool IsSpecial { get; set; }
        public virtual bool IsPublish { get; set; }
        public virtual bool IsShowOnHomePage { get; set; }
        public virtual int CategoryId { get; set; }


        public virtual string MetaTitle { get; set; }
        public virtual string MetaDescription { get; set; }
        public virtual string MetaKeyword { get; set; }
        public virtual string MetaTag { get; set; }
        public virtual string MetaThumbnail { get; set; }
    }
}
