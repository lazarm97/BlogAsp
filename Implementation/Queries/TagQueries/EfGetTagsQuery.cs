using Application.DataTransfer;
using Application.Queries.Tags;
using Application.Searches;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries.TagQueries
{
    public class EfGetTagsQuery : IGetTagsQuery
    {
        private readonly EfContext _context;

        public EfGetTagsQuery(EfContext context)
        {
            _context = context;
        }

        public int Id => 12;

        public string Name => "Get tags";

        public IEnumerable<TagDto> Execute(TagSearch search)
        {
            var query = _context.Tags.AsQueryable();

            if (!string.IsNullOrEmpty(search.Name) || !string.IsNullOrWhiteSpace(search.Name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(search.Name.ToLower()));
            }

            return query.Select(x => new TagDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

        }
    }
}
