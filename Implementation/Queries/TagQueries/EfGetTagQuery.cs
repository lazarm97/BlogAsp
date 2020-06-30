using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries.Tags;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries.TagQueries
{
    public class EfGetTagQuery : IGetTagQuery
    {
        private readonly EfContext _context;

        public EfGetTagQuery(EfContext context)
        {
            _context = context;
        }

        public int Id => 11;

        public string Name => "Get tag";

        public TagDto Execute(int search)
        {
            var tag = _context.Tags.Where(t => t.Id == search).FirstOrDefault();

            if (tag == null)
                throw new EntityNotFoundException(search, typeof(TagDto));

            var tagDto = new TagDto
            {
                Id = tag.Id,
                Name = tag.Name
            };

            return tagDto;
        }
    }
}
