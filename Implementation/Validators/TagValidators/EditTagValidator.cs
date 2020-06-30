using Application.DataTransfer;
using EfDataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators.TagValidators
{
    public class EditTagValidator : AbstractValidator<TagDto>
    {
        public EditTagValidator(EfContext _context)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Must(name => !_context.Tags.Any(t => t.Name == name))
                .WithMessage("Name must be unique");
        }
    }
}
