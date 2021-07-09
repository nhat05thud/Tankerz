using Tankerz.TankerzEntities.ProductAttributes;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Tankerz.ProductAttributes
{
    public class ProductAttributeAppService :
        CrudAppService<
            ProductAttribute,
            ProductAttributeDto,
            int,
            PagedAndSortedResultRequestDto,
            CreateUpdateProductAttributeDto>,
        IProductAttributeAppService
    {
        public ProductAttributeAppService(IRepository<ProductAttribute, int> repository)
            : base(repository)
        {

        }
    }
}
