using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries.RoleQueries;
using Application.Searches;
using Domain;
using EfDataAccess;
using FluentValidation;
using Implementation.Validators.RoleValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries.RoleQueries
{
    public class EfGetRoleQuery : IGetRoleQuery
    {
        private readonly EfContext _context;

        public EfGetRoleQuery(EfContext context)
        {
            _context = context;
        }

        public int Id => 7;

        public string Name => "Get role";

        public RoleDto Execute(int search)
        {
            var role = _context.Roles.Where(r => r.Id == search).FirstOrDefault();

            if (role == null)
                throw new EntityNotFoundException(search, typeof(RoleDto));

            var roleDto = new RoleDto
            {
                Id = role.Id,
                Name = role.Name
            };

            return roleDto;
        }
    }
}
