using AutoMapper;
using Tankerz.BlogCategories;
using Tankerz.Blogs;
using Tankerz.ProductAttributeOptions;
using Tankerz.ProductAttributes;
using Tankerz.ProductCategories;
using Tankerz.ProductGroups;
using Tankerz.Products;
using Tankerz.ProductWithMultipleAttributeOptions;
using Tankerz.TankerzEntities.BlogCategories;
using Tankerz.TankerzEntities.Blogs;
using Tankerz.TankerzEntities.ProductAttributeOptions;
using Tankerz.TankerzEntities.ProductAttributes;
using Tankerz.TankerzEntities.ProductCategories;
using Tankerz.TankerzEntities.ProductGroups;
using Tankerz.TankerzEntities.Products;
using Tankerz.TankerzEntities.ProductWithMultipleAttributeOptions;

namespace Tankerz
{
    public class TankerzApplicationAutoMapperProfile : Profile
    {
        public TankerzApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            // blog
            CreateMap<BlogCategory, BlogCategoryDto>();
            CreateMap<CreateUpdateBlogCategoryDto, BlogCategory>();

            // blog category groups
            CreateMap<Blog, BlogDto>();
            CreateMap<CreateUpdateBlogDto, Blog>();
            CreateMap<BlogCategory, BlogCategoryLookupDto>();

            // product groups
            CreateMap<ProductGroup, ProductGroupDto>();
            CreateMap<CreateUpdateProductGroupDto, ProductGroup>();
            
            // product categories
            CreateMap<ProductCategory, ProductCategoryDto>();
            CreateMap<CreateUpdateProductCategoryDto, ProductCategory>();
            CreateMap<ProductGroup, ProductGroupLookupDto>();
            
            // product
            CreateMap<Product, ProductDto>();
            CreateMap<CreateUpdateProductDto, Product>();
            CreateMap<ProductCategory, ProductCategoryLookupDto>();
            
            // product attribute
            CreateMap<ProductAttribute, ProductAttributeDto>();
            CreateMap<CreateUpdateProductAttributeDto, ProductAttribute>();

            // product attribute options
            CreateMap<ProductAttributeOption, ProductAttributeOptionDto>();
            CreateMap<CreateUpdateProductAttributeOptionDto, ProductAttributeOption>();
            
            // product multiple attribute options
            CreateMap<ProductWithMultipleAttributeOption, ProductWithMultipleAttributeOptionDto>();
            CreateMap<CreateUpdateProductWithMultipleAttributeOptionDto, ProductWithMultipleAttributeOption>();
            CreateMap<ProductAttribute, ProductAttributeLookDto>();
            CreateMap<ProductAttributeOption, ProductAttributeLookDto>();

        }
    }
}
