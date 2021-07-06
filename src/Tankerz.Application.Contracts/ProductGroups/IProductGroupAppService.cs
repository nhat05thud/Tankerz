using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tankerz.ProductGroups
{
    public interface IProductGroupAppService :
        ICrudAppService<
            ProductGroupDto,
            int,
            PagedAndSortedResultRequestDto,
            CreateUpdateProductGroupDto>
    {
    }
}
