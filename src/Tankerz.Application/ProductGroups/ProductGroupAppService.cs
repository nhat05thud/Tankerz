using Tankerz.TankerzEntities.ProductGroups;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Tankerz.ProductGroups
{
    public class ProductGroupAppService :
        CrudAppService<
            ProductGroup,
            ProductGroupDto,
            int,
            PagedAndSortedResultRequestDto,
            CreateUpdateProductGroupDto>,
        IProductGroupAppService
    {
        public ProductGroupAppService(IRepository<ProductGroup, int> repository)
            : base(repository)
        {

        }
    }
}
