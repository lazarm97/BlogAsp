using Application.DataTransfer;
using Application.Queries.Paginations;
using Application.Queries.PostQueries;
using Application.Searches;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries.PostQueries
{
    public class EfGetPostsQuery : IGetPostsQuery
    {
        private readonly EfContext _context;

        public EfGetPostsQuery(EfContext context)
        {
            _context = context;
        }

        public int Id => 20;

        public string Name => "Get posts";

        public PagedResponse<PostShowDto> Execute(PostSearch search)
        {
            var posts = _context.Posts
                .Include(u => u.User)
                .Include(c => c.Category)
                .Include(i => i.Image)
                .AsQueryable();

            if (search.Summary != null)
                posts = posts.Where(p => p.Summary.ToLower().Contains(search.Summary.ToLower()));

            if (search.Text != null)
                posts = posts.Where(p => p.Text.ToLower().Contains(search.Text.ToLower()));

            if (search.Title != null)
                posts = posts.Where(p => p.Title.ToLower().Contains(search.Title.ToLower()));

            if (search.Username != null)
                posts = posts.Where(p => p.User.Username.ToLower().Contains(search.Username.ToLower()));

            if (search.CategoryName != null)
                posts = posts.Where(p => p.Category.Name.ToLower().Contains(search.CategoryName.ToLower()));


            var skipCount = search.PerPage * (search.Page - 1);

            var reponse = new PagedResponse<PostShowDto>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = posts.Count(),
                Items = posts.Skip(skipCount).Take(search.PerPage).Select(x => new PostShowDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Summary = x.Summary,
                    Text = x.Text,
                    CategoryName = x.Category.Name,
                    Username = x.User.Username,
                    ImageAlt = x.Image.Alt,
                    ImagePath = x.Image.Path
                }).ToList()
            };

            return reponse;

        }
    }
}
