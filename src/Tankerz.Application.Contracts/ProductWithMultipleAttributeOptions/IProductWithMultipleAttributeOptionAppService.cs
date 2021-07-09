using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tankerz.ProductWithMultipleAttributeOptions
{
    public interface IProductWithMultipleAttributeOptionAppService : 
        ICrudAppService<
            ProductWithMultipleAttributeOptionDto,
            int,
            GetProductWithMultipleAttributeOptionListInput,
            CreateUpdateProductWithMultipleAttributeOptionDto>
    {
        Task<ListResultDto<ProductAttributeLookDto>> GetProductAttributeLookupAsync();
        Task<ListResultDto<ProductAttributeLookDto>> GetProductAttributeOptionLookupAsync(int attributeId);
    }
}
