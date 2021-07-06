using Volo.Abp.Application.Dtos;

namespace Tankerz.ProductCategories
{
    public class ProductGroupLookupDto : EntityDto<int>
    {
        public string Name { get; set; }
    }
}
