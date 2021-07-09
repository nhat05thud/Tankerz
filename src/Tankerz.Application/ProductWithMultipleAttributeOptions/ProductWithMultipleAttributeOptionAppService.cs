using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankerz.ProductAttributeOptions;
using Tankerz.TankerzEntities.ProductAttributeOptions;
using Tankerz.TankerzEntities.ProductAttributes;
using Tankerz.TankerzEntities.Products;
using Tankerz.TankerzEntities.ProductWithMultipleAttributeOptions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Tankerz.ProductWithMultipleAttributeOptions
{
    public class ProductWithMultipleAttributeOptionAppService :
        CrudAppService<
            ProductWithMultipleAttributeOption,
            ProductWithMultipleAttributeOptionDto,
            int,
            GetProductWithMultipleAttributeOptionListInput,
            CreateUpdateProductWithMultipleAttributeOptionDto>,
        IProductWithMultipleAttributeOptionAppService
    {
        private readonly IRepository<Product, int> _productsRepository;
        private readonly IRepository<ProductAttribute, int> _productAttributesRepository;
        private readonly IRepository<ProductAttributeOption, int> _productAttributeOptionsRepository;
        public ProductWithMultipleAttributeOptionAppService(
            IRepository<ProductWithMultipleAttributeOption, int> repository,
            IRepository<Product, int> productsRepository,
            IRepository<ProductAttribute, int> productAttributesRepository,
            IRepository<ProductAttributeOption, int> productAttributeOptionsRepository) : base(repository)
        {
            _productsRepository = productsRepository;
            _productAttributesRepository = productAttributesRepository;
            _productAttributeOptionsRepository = productAttributeOptionsRepository;
        }
        public override async Task<ProductWithMultipleAttributeOptionDto> GetAsync(int id)
        {
            //Get the IQueryable<Book> from the repository
            var queryable = await Repository.GetQueryableAsync();

            //Prepare a query to join books and authors
            var query = from productWithMultipleAttributeOption in queryable
                        join product in _productsRepository on productWithMultipleAttributeOption.ProductId equals product.Id
                        where productWithMultipleAttributeOption.Id == id
                        select new { productWithMultipleAttributeOption, product };

            //Execute the query and get the book with author
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(ProductWithMultipleAttributeOption), id);
            }

            var productWithMultipleAttributeOptionDto = ObjectMapper.Map<ProductWithMultipleAttributeOption, ProductWithMultipleAttributeOptionDto>(queryResult.productWithMultipleAttributeOption);

            return productWithMultipleAttributeOptionDto;
        }

        public override async Task<PagedResultDto<ProductWithMultipleAttributeOptionDto>> GetListAsync(GetProductWithMultipleAttributeOptionListInput input)
        {
            //Get the IQueryable<Book> from the repository
            var queryable = await Repository.GetQueryableAsync();

            //Prepare a query to join books and authors
            var query = from productWithMultipleAttributeOption in queryable
                        join product in _productsRepository on productWithMultipleAttributeOption.ProductId equals product.Id
                        join productAttribute in _productAttributesRepository on productWithMultipleAttributeOption.ProductAttributeId equals productAttribute.Id
                        join productAttributeOption in _productAttributeOptionsRepository on productWithMultipleAttributeOption.ProductAttributeOptionId equals productAttributeOption.Id
                        where input.ProductId > 0 && input.ProductId == product.Id
                        select new { productWithMultipleAttributeOption, product, productAttribute, productAttributeOption };

            //Paging
            query = query
                .OrderBy(x => x.productWithMultipleAttributeOption.DisplayOrder)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            //Convert the query result to a list of BookDto objects
            var productWithMultipleAttributeOptionDtos = queryResult.Select(x =>
            {
                var productWithMultipleAttributeOptionDto = new ProductWithMultipleAttributeOptionDto { 
                    Id = x.productWithMultipleAttributeOption.Id,
                    ProductId = x.productWithMultipleAttributeOption.ProductId,
                    DisplayOrder = x.productWithMultipleAttributeOption.DisplayOrder,
                    ProductAttributeId = x.productWithMultipleAttributeOption.ProductAttributeId,
                    ProductAttributeOptionId = x.productWithMultipleAttributeOption.ProductAttributeOptionId,
                    ProductAttributeName = x.productAttribute.Name,
                    ProductAttributeOptionName = x.productAttributeOption.Name
                };
                return productWithMultipleAttributeOptionDto;
            }).ToList();

            var totalCount = productWithMultipleAttributeOptionDtos.Count();

            return new PagedResultDto<ProductWithMultipleAttributeOptionDto>(
                totalCount,
                productWithMultipleAttributeOptionDtos
            );
        }

        public async Task<ListResultDto<ProductAttributeLookDto>> GetProductAttributeLookupAsync()
        {
            var productAttributes = await _productAttributesRepository.GetListAsync();

            return new ListResultDto<ProductAttributeLookDto>(
                ObjectMapper.Map<List<ProductAttribute>, List<ProductAttributeLookDto>>(productAttributes)
            );
        }
        public async Task<ListResultDto<ProductAttributeLookDto>> GetProductAttributeOptionLookupAsync(int attributeId)
        {
            var queryable = await _productAttributeOptionsRepository.GetQueryableAsync();

            var query = from productAttributeOption in queryable
                        join productAttribute in _productAttributesRepository on productAttributeOption.ProductAttributeId equals productAttribute.Id
                        where attributeId > 0 && productAttributeOption.ProductAttributeId == attributeId
                        select new { productAttributeOption, productAttribute };

            query = query
                .OrderBy(x => x.productAttributeOption.DisplayOrder);

            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            var productAttributeOptionDtos = queryResult.Select(x =>
            {
                var productAttributeOptionDto = x.productAttributeOption;
                return productAttributeOptionDto;
            }).ToList();

            return new ListResultDto<ProductAttributeLookDto>(
                ObjectMapper.Map<List<ProductAttributeOption>, List<ProductAttributeLookDto>>(productAttributeOptionDtos)
            );
        }
    }
}
