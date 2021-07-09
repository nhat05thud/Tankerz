using AutoMapper;
using Tankerz.BlogCategories;
using Tankerz.Blogs;
using Tankerz.ProductAttributeOptions;
using Tankerz.ProductAttributes;
using Tankerz.ProductCategories;
using Tankerz.ProductGroups;
using Tankerz.Products;
using Tankerz.ProductWithMultipleAttributeOptions;
using static Tankerz.Web.Pages.BlogCategories.CreateModalModel;
using static Tankerz.Web.Pages.BlogCategories.EditModalModel;
using static Tankerz.Web.Pages.Blogs.CreateModel;
using static Tankerz.Web.Pages.Blogs.EditModel;
using static Tankerz.Web.Pages.ProductAttributes.CreateModel;
using static Tankerz.Web.Pages.ProductAttributes.EditModel;
using static Tankerz.Web.Pages.ProductAttributes.Options.CreateModalModel;
using static Tankerz.Web.Pages.ProductAttributes.Options.EditModalModel;
using static Tankerz.Web.Pages.ProductCategories.CreateModalModel;
using static Tankerz.Web.Pages.ProductCategories.EditModalModel;
using static Tankerz.Web.Pages.ProductGroups.CreateModalModel;
using static Tankerz.Web.Pages.ProductGroups.EditModalModel;
using static Tankerz.Web.Pages.Products.Attributes.CreateModalModel;
using static Tankerz.Web.Pages.Products.Attributes.EditModalModel;
using static Tankerz.Web.Pages.Products.CreateModel;
using static Tankerz.Web.Pages.Products.EditModel;

namespace Tankerz.Web
{
    public class TankerzWebAutoMapperProfile : Profile
    {
        public TankerzWebAutoMapperProfile()
        {
            //Define your AutoMapper configuration here for the Web project.

            // blog category
            CreateMap<CreateBlogCategoryViewModel, CreateUpdateBlogCategoryDto>();
            CreateMap<BlogCategoryDto, EditBlogCategoryViewModel>();
            CreateMap<EditBlogCategoryViewModel, CreateUpdateBlogCategoryDto>();

            // blog 
            CreateMap<BlogDto, CreateBlogViewModel>();
            CreateMap<BlogDto, EditBlogViewModel>();
            CreateMap<CreateBlogViewModel, CreateUpdateBlogDto>();
            CreateMap<EditBlogViewModel, CreateUpdateBlogDto>();

            // product group
            CreateMap<ProductGroupDto, CreateProductGroupViewModel>();
            CreateMap<ProductGroupDto, EditProductGroupViewModel>();
            CreateMap<CreateProductGroupViewModel, CreateUpdateProductGroupDto>();
            CreateMap<EditProductGroupViewModel, CreateUpdateProductGroupDto>();

            // product category
            CreateMap<ProductCategoryDto, CreateProductCategoryViewModel>();
            CreateMap<ProductCategoryDto, EditProductCategoryViewModel>();
            CreateMap<CreateProductCategoryViewModel, CreateUpdateProductCategoryDto>();
            CreateMap<EditProductCategoryViewModel, CreateUpdateProductCategoryDto>();

            // product
            CreateMap<ProductDto, CreateProductViewModel>();
            CreateMap<ProductDto, EditProductViewModel>();
            CreateMap<CreateProductViewModel, CreateUpdateProductDto>();
            CreateMap<EditProductViewModel, CreateUpdateProductDto>();

            // productAttribute
            CreateMap<ProductAttributeDto, CreateProductAttributeViewModel>();
            CreateMap<ProductAttributeDto, EditProductAttributeViewModel>();
            CreateMap<CreateProductAttributeViewModel, CreateUpdateProductAttributeDto>();
            CreateMap<EditProductAttributeViewModel, CreateUpdateProductAttributeDto>();
            
            // productAttributeOptions
            CreateMap<ProductAttributeOptionDto, CreateProductAttributeOptionViewModel>();
            CreateMap<ProductAttributeOptionDto, EditProductAttributeOptionViewModel>();
            CreateMap<CreateProductAttributeOptionViewModel, CreateUpdateProductAttributeOptionDto>();
            CreateMap<EditProductAttributeOptionViewModel, CreateUpdateProductAttributeOptionDto>();

            // ProductWithMultipleAttributeOption
            CreateMap<ProductWithMultipleAttributeOptionDto, CreateProductMultipleAttributeOptionViewModel>();
            CreateMap<ProductWithMultipleAttributeOptionDto, EditProductMultipleAttributeOptionViewModel>();
            CreateMap<CreateProductMultipleAttributeOptionViewModel, CreateUpdateProductWithMultipleAttributeOptionDto>();
            CreateMap<EditProductMultipleAttributeOptionViewModel, CreateUpdateProductWithMultipleAttributeOptionDto>();

        }
    }
}
