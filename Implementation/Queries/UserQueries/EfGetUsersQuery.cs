using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries.Paginations;
using Application.Queries.UserQueries;
using Application.Searches;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries.UserQueries
{
    public class EfGetUsersQuery : IGetUsersQuery
    {
        private readonly EfContext _context;

        public EfGetUsersQuery(EfContext context)
        {
            _context = context;
        }

        public int Id => 16;

        public string Name => "Get users";

        public PagedResponse<UserShowDto> Execute(UserSearch search)
        {
            var users = _context.Users
                .Include(r => r.Role)
                .AsQueryable();

            if (search.FirstName != null)
                users = users.Where(u => u.FirstName.ToLower().Contains(search.FirstName.ToLower()));

            if (search.LastName != null)
                users = users.Where(u => u.LastName.ToLower().Contains(search.LastName.ToLower()));

            if (search.Username != null)
                users = users.Where(u => u.Username.ToLower().Contains(search.Username.ToLower()));

            if (search.RoleName != null)
                users = users.Where(u => u.Role.Name.ToLower().Contains(search.RoleName.ToLower()));


            var skipCount = search.PerPage * (search.Page - 1);

            var reponse = new PagedResponse<UserShowDto>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = users.Count(),
                Items = users.Skip(skipCount).Take(search.PerPage).Select(x => new UserShowDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Username = x.Username,
                    RoleName = x.Role.Name
                }).ToList()
            };

            return reponse;

        }

    }
}
