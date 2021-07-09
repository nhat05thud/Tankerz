using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankerz.TankerzEntities.ProductAttributeOptions;
using Tankerz.TankerzEntities.ProductAttributes;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Tankerz.ProductAttributeOptions
{
    public class ProductAttributeOptionAppService :
        CrudAppService<
            ProductAttributeOption,
            ProductAttributeOptionDto,
            int,
            GetProductAttributeOptionListInput,
            CreateUpdateProductAttributeOptionDto>,
        IProductAttributeOptionAppService
    {
        private readonly IRepository<ProductAttribute, int> _productAttributesRepository;
        public ProductAttributeOptionAppService(IRepository<ProductAttributeOption, int> repository, 
            IRepository<ProductAttribute, int> productAttributesRepository) : base(repository)
        {
            _productAttributesRepository = productAttributesRepository;
        }
        public override async Task<ProductAttributeOptionDto> GetAsync(int id)
        {
            //Get the IQueryable<Book> from the repository
            var queryable = await Repository.GetQueryableAsync();

            //Prepare a query to join books and authors
            var query = from productAttributeOption in queryable
                        join productAttribute in _productAttributesRepository on productAttributeOption.ProductAttributeId equals productAttribute.Id
                        where productAttributeOption.Id == id
                        select new { productAttributeOption, productAttribute };

            //Execute the query and get the book with author
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(ProductAttributeOption), id);
            }

            var productAttributeOptionDto = ObjectMapper.Map<ProductAttributeOption, ProductAttributeOptionDto>(queryResult.productAttributeOption);

            return productAttributeOptionDto;
        }

        public override async Task<PagedResultDto<ProductAttributeOptionDto>> GetListAsync(GetProductAttributeOptionListInput input)
        {
            //Get the IQueryable<Book> from the repository
            var queryable = await Repository.GetQueryableAsync();

            //Prepare a query to join books and authors
            var query = from productAttributeOption in queryable
                        join productAttribute in _productAttributesRepository on productAttributeOption.ProductAttributeId equals productAttribute.Id
                        where input.AttributeId > 0 && input.AttributeId == productAttribute.Id
                        select new { productAttributeOption, productAttribute };

            //Paging
            query = query
                .OrderBy(x => x.productAttributeOption.DisplayOrder)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            //Convert the query result to a list of BookDto objects
            var productAttributeOptionDtos = queryResult.Select(x =>
            {
                var productAttributeOptionDto = ObjectMapper.Map<ProductAttributeOption, ProductAttributeOptionDto>(x.productAttributeOption);
                return productAttributeOptionDto;
            }).ToList();

            var totalCount = productAttributeOptionDtos.Count();

            return new PagedResultDto<ProductAttributeOptionDto>(
                totalCount,
                productAttributeOptionDtos
            );
        }
    }
}
