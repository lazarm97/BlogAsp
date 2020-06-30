using Application.Commands.RoleCommands;
using Application.DataTransfer;
using Domain;
using EfDataAccess;
using FluentValidation;
using Implementation.Validators.RoleValidators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.RoleCommands
{
    public class EfCreateRoleCommand : ICreateRoleCommand
    {
        private readonly EfContext _context;
        private readonly CreateRoleValidator _validator;

        public EfCreateRoleCommand(EfContext context, CreateRoleValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 5;

        public string Name => "Create role";

        public void Execute(RoleDto request)
        {
            _validator.ValidateAndThrow(request);

            var Role = new Role
            {
                Name = request.Name
            };

            _context.Roles.Add(Role);
            _context.SaveChanges();
        }
    }
}
