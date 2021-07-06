using Volo.Abp.Application.Dtos;

namespace Tankerz.Products
{
    public class ProductCategoryLookupDto : EntityDto<int>
    {
        public string Name { get; set; }
    }
}
