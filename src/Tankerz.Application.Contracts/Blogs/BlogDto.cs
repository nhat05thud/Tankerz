using Volo.Abp.Application.Dtos;

namespace Tankerz.Blogs
{
    public class BlogDto : FullAuditedEntityDto<int>
    {
        public string Banners { get; set; }
        public string Image { get; set; }
        public string ListImages { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Tags { get; set; }
        public int Priority { get; set; }
        public bool IsSpecial { get; set; }
        public bool IsPublish { get; set; }
        public bool IsShowOnHomePage { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }


        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaTag { get; set; }
        public string MetaThumbnail { get; set; }
    }
}
