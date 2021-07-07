using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tankerz.TankerzEntities.ProductCategories;
using Tankerz.TankerzEntities.Products;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Tankerz.Products
{
    [Authorize]
    public class ProductAppService :
        CrudAppService<
            Product,
            ProductDto,
            int,
            GetProductListInput,
            CreateUpdateProductDto>,
        IProductAppService
    {
        private readonly IRepository<ProductCategory, int> _productCategoryRepository;
        public ProductAppService(IRepository<Product, int> repository, IRepository<ProductCategory, int> productCategoryRepository)
            : base(repository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public override async Task<ProductDto> GetAsync(int id)
        {
            //Get the IQueryable<Book> from the repository
            var queryable = await Repository.GetQueryableAsync();

            //Prepare a query to join books and authors
            var query = from product in queryable
                        join productCategory in _productCategoryRepository on product.ProductCategoryId equals productCategory.Id
                        where productCategory.Id == id
                        select new { product, productCategory };

            //Execute the query and get the book with author
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Product), id);
            }

            var productCategoryDto = ObjectMapper.Map<Product, ProductDto>(queryResult.product);

            return productCategoryDto;
        }

        public override async Task<PagedResultDto<ProductDto>> GetListAsync(GetProductListInput input)
        {
            if (input.CateId > 0)
            {
                var queryable = await Repository.GetQueryableAsync();

                var query = from product in queryable
                            join productCategory in _productCategoryRepository on product.ProductCategoryId equals productCategory.Id
                            where input.CateId > 0 && input.CateId == productCategory.Id
                            select new { product, productCategory };

                //Paging
                query = query
                    .OrderBy(x => x.product.DisplayOrder)
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount);

                //Execute the query and get a list
                var queryResult = await AsyncExecuter.ToListAsync(query);

                var productDtos = queryResult.Select(x =>
                {
                    var productDto = ObjectMapper.Map<Product, ProductDto>(x.product);
                    productDto.ProductCategoryName = x.productCategory.Name;
                    return productDto;
                }).ToList();

                //Get the total count with another query
                var totalCount = productDtos.Count();

                return new PagedResultDto<ProductDto>(
                    totalCount,
                    productDtos
                );
            }
            return new PagedResultDto<ProductDto>();
        }
        //public async Task<ListResultDto<ProductCategoryLookupDto>> GetProductCategoryLookupAsync()
        //{
        //    var productCategories = await _productCategoryRepository.GetListAsync();

        //    return new ListResultDto<ProductCategoryLookupDto>(
        //        ObjectMapper.Map<List<ProductCategory>, List<ProductCategoryLookupDto>>(productCategories)
        //    );
        //}
    }
}
