using Volo.Abp.Application.Dtos;

namespace Tankerz.ProductWithMultipleAttributeOptions
{
    public class ProductAttributeLookDto : EntityDto<int>
    {
        public string Name { get; set; }
    }
}
