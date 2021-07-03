﻿using Volo.Abp.Domain.Entities.Auditing;

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
        public string Banners { get; set; }
        public string Images { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public bool IsPublish { get; set; }
        public bool IsShowOnMenu { get; set; }
        public bool IsShowOnHomePage { get; set; }
        public int ProductGroupId { get; set; }


        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaTag { get; set; }
        public string MetaThumbnail { get; set; }
    }
}
