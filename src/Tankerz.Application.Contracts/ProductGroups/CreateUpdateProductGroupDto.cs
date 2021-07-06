using Volo.Abp.Application.Dtos;

namespace Tankerz.ProductGroups
{
    public class CreateUpdateProductGroupDto : FullAuditedEntityDto<int>
    {
        public string Banners { get; set; }
        public string Image { get; set; }
        public string ListImages { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public bool IsPublish { get; set; }
        public bool IsShowOnMenu { get; set; }
        public bool IsShowOnHomePage { get; set; }


        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaTag { get; set; }
        public string MetaThumbnail { get; set; }
    }
}
