using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tankerz.ProductAttributes
{
    public interface IProductAttributeAppService : 
        ICrudAppService<
            ProductAttributeDto,
            int,
            PagedAndSortedResultRequestDto,
            CreateUpdateProductAttributeDto>
    {
    }
}
