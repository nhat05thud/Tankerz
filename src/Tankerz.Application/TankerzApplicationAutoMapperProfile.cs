using AutoMapper;
using Tankerz.BlogCategories;
using Tankerz.Blogs;
using Tankerz.TankerzEntities.BlogCategories;
using Tankerz.TankerzEntities.Blogs;

namespace Tankerz
{
    public class TankerzApplicationAutoMapperProfile : Profile
    {
        public TankerzApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            CreateMap<BlogCategory, BlogCategoryDto>();
            CreateMap<CreateUpdateBlogCategoryDto, BlogCategory>();

            CreateMap<Blog, BlogDto>();
            CreateMap<CreateUpdateBlogDto, Blog>();
            CreateMap<BlogCategory, BlogCategoryLookupDto>();



        }
    }
}
