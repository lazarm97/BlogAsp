using Application.DataTransfer;
using EfDataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators.PostValidators
{
    public class CreatePostValidator : AbstractValidator<PostDto>
    {
        public CreatePostValidator(EfContext context)
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .Must(title => !context.Posts.Any(t => t.Title == title))
                .WithMessage("Title must be unique");

            RuleFor(x => x.Summary)
                .NotEmpty()
                .WithMessage("Summary mustn`t be empty");

            RuleFor(x => x.Text)
                .NotEmpty()
                .WithMessage("Text mustn`t be empty");

        }
    }
}
