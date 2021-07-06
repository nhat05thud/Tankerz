using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tankerz.TankerzEntities.ProductCategories;
using Tankerz.TankerzEntities.ProductGroups;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Tankerz.ProductCategories
{
    [Authorize]
    public class ProductCategoryAppService :
        CrudAppService<
            ProductCategory,
            ProductCategoryDto,
            int,
            PagedAndSortedResultRequestDto,
            CreateUpdateProductCategoryDto>,
        IProductCategoryAppService
    {
        private readonly IRepository<ProductGroup, int> _productGroupRepository;
        public ProductCategoryAppService(IRepository<ProductCategory, int> repository, IRepository<ProductGroup, int> productGroupRepository)
            : base(repository)
        {
            _productGroupRepository = productGroupRepository;
        }

        public override async Task<ProductCategoryDto> GetAsync(int id)
        {
            //Get the IQueryable<Book> from the repository
            var queryable = await Repository.GetQueryableAsync();

            //Prepare a query to join books and authors
            var query = from productCategory in queryable
                        join productGroup in _productGroupRepository on productCategory.ProductGroupId equals productGroup.Id
                        where productCategory.Id == id
                        select new { productCategory, productGroup };

            //Execute the query and get the book with author
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(ProductCategory), id);
            }

            var productCategoryDto = ObjectMapper.Map<ProductCategory, ProductCategoryDto>(queryResult.productCategory);

            return productCategoryDto;
        }

        public override async Task<PagedResultDto<ProductCategoryDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            //Get the IQueryable<Book> from the repository
            var queryable = await Repository.GetQueryableAsync();

            //Prepare a query to join books and authors
            var query = from productCategory in queryable
                        join productGroup in _productGroupRepository on productCategory.ProductGroupId equals productGroup.Id
                        select new { productCategory, productGroup };

            //Paging
            query = query
                .OrderBy(x => x.productCategory.Priority)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            //Convert the query result to a list of BookDto objects
            var productCategoryDtos = queryResult.Select(x =>
            {
                var productCategoryDto = ObjectMapper.Map<ProductCategory, ProductCategoryDto>(x.productCategory);
                productCategoryDto.ProductGroupName = x.productGroup.Name;
                return productCategoryDto;
            }).ToList();

            //Get the total count with another query
            var totalCount = productCategoryDtos.Count();

            return new PagedResultDto<ProductCategoryDto>(
                totalCount,
                productCategoryDtos
            );
        }
        public async Task<ListResultDto<ProductGroupLookupDto>> GetProductGroupLookupAsync()
        {
            var productGroups = await _productGroupRepository.GetListAsync();

            return new ListResultDto<ProductGroupLookupDto>(
                ObjectMapper.Map<List<ProductGroup>, List<ProductGroupLookupDto>>(productGroups)
            );
        }
    }
}
