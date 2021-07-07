using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankerz.BlogCategories;
using Tankerz.TankerzEntities.BlogCategories;
using Tankerz.TankerzEntities.Blogs;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Tankerz.Blogs
{
    [Authorize]
    public class BlogAppService : 
        CrudAppService<
            Blog,
            BlogDto,
            int,
            GetBlogListInput,
            CreateUpdateBlogDto>,
        IBlogAppService
    {
        private readonly IRepository<BlogCategory, int> _blogCategoriesRepository;
        public BlogAppService(IRepository<Blog, int> repository, IRepository<BlogCategory, int> blogCategoriesRepository) : base(repository)
        {
            _blogCategoriesRepository = blogCategoriesRepository;
        }
        public override async Task<BlogDto> GetAsync(int id)
        {
            //Get the IQueryable<Book> from the repository
            var queryable = await Repository.GetQueryableAsync();

            //Prepare a query to join books and authors
            var query = from blog in queryable
                        join blogCategory in _blogCategoriesRepository on blog.CategoryId equals blogCategory.Id
                        where blog.Id == id
                        select new { blog, blogCategory };

            //Execute the query and get the book with author
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Blog), id);
            }

            var blogDto = ObjectMapper.Map<Blog, BlogDto>(queryResult.blog);

            return blogDto;
        }

        public override async Task<PagedResultDto<BlogDto>> GetListAsync(GetBlogListInput input)
        {
            //Get the IQueryable<Book> from the repository
            var queryable = await Repository.GetQueryableAsync();

            //Prepare a query to join books and authors
            var query = from blog in queryable
                        join blogCategory in _blogCategoriesRepository on blog.CategoryId equals blogCategory.Id
                        where input.CateId > 0 && input.CateId == blogCategory.Id
                        select new { blog, blogCategory };

            //Paging
            query = query
                .OrderBy(x => x.blog.DisplayOrder)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            //Convert the query result to a list of BookDto objects
            var blogDtos = queryResult.Select(x =>
            {
                var blogDto = ObjectMapper.Map<Blog, BlogDto>(x.blog);
                blogDto.CategoryName = x.blogCategory.Name;
                return blogDto;
            }).ToList();

            var totalCount = blogDtos.Count();

            return new PagedResultDto<BlogDto>(
                totalCount,
                blogDtos
            );
        }
        //public async Task<ListResultDto<BlogCategoryLookupDto>> GetBlogCategoryLookupAsync()
        //{
        //    var blogCategories = await _blogCategoriesRepository.GetListAsync();

        //    return new ListResultDto<BlogCategoryLookupDto>(
        //        ObjectMapper.Map<List<BlogCategory>, List<BlogCategoryLookupDto>>(blogCategories)
        //    );
        //}
    }
}
