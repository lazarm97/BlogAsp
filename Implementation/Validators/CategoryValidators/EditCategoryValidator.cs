using Application.DataTransfer;
using EfDataAccess;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators.Category
{
    public class EditCategoryValidator : AbstractValidator<CategoryDto>
    {
        public EditCategoryValidator(EfContext context)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Must(name => !context.Categories.Any(c => c.Name == name))
                .WithMessage("Category name must be unique");
        }
    }
}
