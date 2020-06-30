using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries.PostQueries;
using AutoMapper;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries.PostQueries
{
    public class EfGetPostQuery : IGetPostQuery
    {
        private readonly EfContext _context;
        private readonly IMapper _mapper;

        public EfGetPostQuery(EfContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 21;

        public string Name => "Get post";

        public PostShowDto Execute(int search)
        {
            var post = _context.Posts
                .Include(u => u.User)
                .Include(c => c.Category)
                .Include(i => i.Image)
                .Where(p => p.Id == search)
                .FirstOrDefault();

            if (post == null)
                throw new EntityNotFoundException(search, typeof(PostShowDto));

            var postDto = _mapper.Map<PostShowDto>(post);

            return postDto;
        }
    }
}
