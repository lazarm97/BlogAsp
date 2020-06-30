using Application.Commands.UserCommands;
using Application.DataTransfer;
using Application.Exceptions;
using Domain;
using EfDataAccess;
using FluentValidation;
using Implementation.Validators.UserValidators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.UserCommands
{
    public class EfEditUserCommand : IEditUserCommand
    {
        private readonly EfContext _context;
        private readonly EditUserValidator _validator;

        public EfEditUserCommand(EfContext context, EditUserValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 17;

        public string Name => "Edit user";

        public void Execute(UserDto request)
        {
            _validator.ValidateAndThrow(request);

            var user = _context.Users.Find(request.Id);

            if (user == null)
                throw new EntityNotFoundException(request.Id, typeof(UserDto));

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Username = request.Username;
            user.Password = request.Password;
            user.RoleId = request.RoleId;

            _context.SaveChanges();

        }

    }
}
