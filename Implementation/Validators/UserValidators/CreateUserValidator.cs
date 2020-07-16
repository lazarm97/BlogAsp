using Application.DataTransfer;
using EfDataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators.UserValidators
{
    public class CreateUserValidator : AbstractValidator<UserDto>
    {
        public CreateUserValidator(EfContext _context)
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MinimumLength(3)
                .WithMessage("First name must have minimum 3 characters");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MinimumLength(3)
                .WithMessage("Last name must have minimum 3 characters");

            RuleFor(x => x.Username)
                .NotEmpty()
                .MinimumLength(3)
                .Must(username => !_context.Users.Any(u => u.Username == username))
                .WithMessage("Username must have minimum 3 characters and must be unique");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(5)
                .WithMessage("Password must have minimum 5 characters");

            RuleFor(x => x.RoleId)
                .NotEmpty()
                .Must(id => _context.Roles.Any(r => r.Id == id))
                .WithMessage("Role with this id does not exist");

            RuleFor(x => x.Email)
                .NotEmpty()
                .Must(email => !_context.Users.Any(u => u.Email == email))
                .WithMessage("Email does not exist or not unique");
        }
    }
}
