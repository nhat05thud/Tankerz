using Volo.Abp.Application.Dtos;

namespace Tankerz.ProductCategories
{
    public class CreateUpdateProductCategoryDto : FullAuditedEntityDto<int>
    {
        public string Banners { get; set; }
        public string Image { get; set; }
        public string ListImages { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsPublish { get; set; }
        public bool IsShowOnMenu { get; set; }
        public bool IsShowOnHomePage { get; set; }
        public int ProductGroupId { get; set; }
        public string ProductGroupName { get; set; }


        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaTag { get; set; }
        public string MetaThumbnail { get; set; }
    }
}
