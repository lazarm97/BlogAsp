using Application.DataTransfer;
using Application.Exceptions;
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
    public class EfGetUserQuery : IGetUserQuery
    {
        private readonly EfContext _context;

        public EfGetUserQuery(EfContext context)
        {
            _context = context;
        }

        public int Id => 15;

        public string Name => "Get user";

        public UserShowDto Execute(int request)
        {
            var user = _context.Users
                .Include(r => r.Role)
                .Where(u => u.Id == request)
                .FirstOrDefault();

            if (user == null)
                throw new EntityNotFoundException(request, typeof(UserShowDto));

            var userDto = new UserShowDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                RoleName = user.Role.Name
            };

            return userDto;
        }
    }
}
