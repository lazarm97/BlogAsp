using Application.Commands.RoleCommands;
using Application.DataTransfer;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.RoleCommands
{
    public class EfDeleteRoleCommand : IDeleteRoleCommand
    {
        private readonly EfContext _context;

        public EfDeleteRoleCommand(EfContext context)
        {
            _context = context;
        }

        public int Id => 8;

        public string Name => "Delete role";

        public void Execute(int request)
        {
            var role = _context.Roles.Find(request);

            if (role == null)
                throw new EntityNotFoundException(request, typeof(RoleDto));

            _context.Roles.Remove(role);
            _context.SaveChanges();
        }
    }
}
