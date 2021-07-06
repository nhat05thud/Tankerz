using Volo.Abp.Application.Services;

namespace Tankerz.Products
{
    public interface IProductAppService : 
        ICrudAppService<
            ProductDto,
            int,
            GetProductListInput,
            CreateUpdateProductDto>
    {
        //Task<ListResultDto<ProductCategoryLookupDto>> GetProductCategoryLookupAsync();
    }
}
