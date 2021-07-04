using AutoMapper;
using Tankerz.BlogCategories;
using Tankerz.Blogs;
using static Tankerz.Web.Pages.BlogCategories.CreateModalModel;
using static Tankerz.Web.Pages.BlogCategories.EditModalModel;
using static Tankerz.Web.Pages.Blogs.CreateModalModel;
using static Tankerz.Web.Pages.Blogs.EditModalModel;

namespace Tankerz.Web
{
    public class TankerzWebAutoMapperProfile : Profile
    {
        public TankerzWebAutoMapperProfile()
        {
            //Define your AutoMapper configuration here for the Web project.
            CreateMap<CreateBlogCategoryViewModel, CreateUpdateBlogCategoryDto>();

            CreateMap<BlogCategoryDto, EditBlogCategoryViewModel>();
            CreateMap<EditBlogCategoryViewModel, CreateUpdateBlogCategoryDto>();


            CreateMap<BlogDto, CreateBlogViewModel>();
            CreateMap<BlogDto, EditBlogViewModel>();
            CreateMap<CreateBlogViewModel, CreateUpdateBlogDto>();
            CreateMap<EditBlogViewModel, CreateUpdateBlogDto>();
        }
    }
}
