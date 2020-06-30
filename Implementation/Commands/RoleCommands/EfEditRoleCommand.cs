using Application.Commands.RoleCommands;
using Application.DataTransfer;
using Application.Exceptions;
using EfDataAccess;
using FluentValidation;
using Implementation.Validators.RoleValidators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.RoleCommands
{
    public class EfEditRoleCommand : IEditRoleCommand
    {
        private readonly EfContext _context;
        private readonly EditRoleValidator _validator;

        public EfEditRoleCommand(EfContext context, EditRoleValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 9;

        public string Name => "Edit role";

        public void Execute(RoleDto request)
        {
            _validator.ValidateAndThrow(request);

            var role = _context.Roles.Find(request.Id);

            if (role == null)
                throw new EntityNotFoundException(request.Id, typeof(RoleDto));

            role.Name = request.Name;
            _context.SaveChanges();
        }
    }
}
