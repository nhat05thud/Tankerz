using Volo.Abp.Application.Services;

namespace Tankerz.ProductAttributeOptions
{
    public interface IProductAttributeOptionAppService : 
        ICrudAppService<
            ProductAttributeOptionDto,
            int,
            GetProductAttributeOptionListInput,
            CreateUpdateProductAttributeOptionDto>
    {
    }
}
