using Application.Commands.TagCommands;
using Application.DataTransfer;
using Domain;
using EfDataAccess;
using FluentValidation;
using Implementation.Validators.TagValidators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.TagCommands
{
    public class EfCreateTagCommand : ICreateTagCommand
    {
        private readonly EfContext _context;
        private readonly CreateTagValidator _validator;

        public EfCreateTagCommand(EfContext context, CreateTagValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 10;

        public string Name => "Create tag";

        public void Execute(TagDto request)
        {
            _validator.ValidateAndThrow(request);

            var Tag = new Tag
            {
                Name = request.Name
            };

            _context.Tags.Add(Tag);
            _context.SaveChanges();
        }
    }
}
