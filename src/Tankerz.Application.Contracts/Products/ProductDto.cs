using Volo.Abp.Application.Dtos;

namespace Tankerz.Products
{
    public class ProductDto : FullAuditedEntityDto<int>
    {
        public string Banners { get; set; }
        public string Image { get; set; }
        public string ListImages { get; set; }
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public int Priority { get; set; }
        public bool IsSpecial { get; set; }
        public bool IsPublish { get; set; }
        public bool IsShowOnHomePage { get; set; }
        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }


        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaTag { get; set; }
        public string MetaThumbnail { get; set; }
    }
}
