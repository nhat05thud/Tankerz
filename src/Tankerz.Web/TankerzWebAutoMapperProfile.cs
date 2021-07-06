using AutoMapper;
using Tankerz.BlogCategories;
using Tankerz.Blogs;
using Tankerz.ProductCategories;
using Tankerz.ProductGroups;
using Tankerz.Products;
using static Tankerz.Web.Pages.BlogCategories.CreateModalModel;
using static Tankerz.Web.Pages.BlogCategories.EditModalModel;
using static Tankerz.Web.Pages.Blogs.CreateModalModel;
using static Tankerz.Web.Pages.Blogs.EditModalModel;
using static Tankerz.Web.Pages.ProductCategories.CreateModalModel;
using static Tankerz.Web.Pages.ProductCategories.EditModalModel;
using static Tankerz.Web.Pages.ProductGroups.CreateModalModel;
using static Tankerz.Web.Pages.ProductGroups.EditModalModel;
using static Tankerz.Web.Pages.Products.CreateModalModel;
using static Tankerz.Web.Pages.Products.EditModalModel;

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

            // product category
            CreateMap<ProductDto, CreateProductViewModel>();
            CreateMap<ProductDto, EditProductViewModel>();
            CreateMap<CreateProductViewModel, CreateUpdateProductDto>();
            CreateMap<EditProductViewModel, CreateUpdateProductDto>();
        }
    }
}
