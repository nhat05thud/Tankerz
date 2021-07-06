using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tankerz.ProductCategories
{
    public interface IProductCategoryAppService :
        ICrudAppService<
            ProductCategoryDto,
            int,
            PagedAndSortedResultRequestDto,
            CreateUpdateProductCategoryDto>
    {
        Task<ListResultDto<ProductGroupLookupDto>> GetProductGroupLookupAsync();
    }
}
