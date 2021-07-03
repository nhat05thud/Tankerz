using Volo.Abp.Domain.Entities.Auditing;

namespace Tankerz.TankerzEntities.TankerzPages
{
    public class TankerzPage : FullAuditedAggregateRoot<int>
    {
        public TankerzPage()
        {
            IsPublish = true;
        }
        public string Banners { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public int Priority { get; set; }
        public bool IsPublish { get; set; }


        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaTag { get; set; }
        public string MetaThumbnail { get; set; }
    }
}
