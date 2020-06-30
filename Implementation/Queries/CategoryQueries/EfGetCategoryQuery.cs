using Application;
using Application.DataTransfer;
using Application.Queries;
using Application.Searches;
using AutoMapper;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries
{
    public class EfGetCategoryQuery : IGetCategoriesQuery
    {
        private readonly EfContext context;
        private readonly IMapper _mapper;

        public EfGetCategoryQuery(EfContext context, IMapper mapper)
        {
            this.context = context;
            _mapper = mapper;
        }

        public int Id => 2;

        public string Name => "Category search";

        public IEnumerable<CategoryDto> Execute(CategorySearch search)
        {
            var query = context.Categories.AsQueryable();

            if (!string.IsNullOrEmpty(search.Name) || !string.IsNullOrWhiteSpace(search.Name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(search.Name.ToLower()));
            }

            return _mapper.Map<IEnumerable<CategoryDto>>(query.ToList());

        }

    }
}
