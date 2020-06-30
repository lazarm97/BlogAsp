using Application.DataTransfer;
using EfDataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators.RoleValidators
{
    public class EditRoleValidator : AbstractValidator<RoleDto>
    {
        public EditRoleValidator(EfContext _context)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Must(name => !_context.Roles.Any(r => r.Name == name))
                .WithMessage("Name must be unique");
        }
    }
}
