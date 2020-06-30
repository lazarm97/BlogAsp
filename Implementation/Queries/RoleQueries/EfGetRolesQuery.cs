using Application.DataTransfer;
using Application.Queries.RoleQueries;
using Application.Searches;
using EfDataAccess;
using FluentValidation;
using Implementation.Validators.RoleValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries.RoleQueries
{
    public class EfGetRolesQuery : IGetRolesQuery
    {
        private readonly EfContext _context;

        public EfGetRolesQuery(EfContext context)
        {
            _context = context;
        }

        public int Id => 6;

        public string Name => "Get roles";

        public IEnumerable<RoleDto> Execute(RoleSearch search)
        {
            var query = _context.Roles.AsQueryable();

            if (!string.IsNullOrEmpty(search.Name) || !string.IsNullOrWhiteSpace(search.Name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(search.Name.ToLower()));
            }

            return query.Select(x => new RoleDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

        }
    }
}
