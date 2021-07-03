using AutoMapper;
using Tankerz.BlogCategories;
using static Tankerz.Web.Pages.BlogCategories.CreateModalModel;

namespace Tankerz.Web
{
    public class TankerzWebAutoMapperProfile : Profile
    {
        public TankerzWebAutoMapperProfile()
        {
            //Define your AutoMapper configuration here for the Web project.
            CreateMap<CreateBlogCategoryViewModel, CreateUpdateBlogCategoryDto>();
        }
    }
}
