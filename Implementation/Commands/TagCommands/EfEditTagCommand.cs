using Application.Commands.TagCommands;
using Application.DataTransfer;
using Application.Exceptions;
using EfDataAccess;
using FluentValidation;
using Implementation.Validators.RoleValidators;
using Implementation.Validators.TagValidators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.TagCommands
{
    public class EfEditTagCommand : IEditTagCommand
    {
        private readonly EfContext _context;
        private readonly EditTagValidator _validator;

        public EfEditTagCommand(EfContext context, EditTagValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 13;

        public string Name => "Edit tag";

        public void Execute(TagDto request)
        {
            _validator.ValidateAndThrow(request);

            var tag = _context.Tags.Find(request.Id);

            if (tag == null)
                throw new EntityNotFoundException(request.Id, typeof(TagDto));

            tag.Name = request.Name;
            _context.SaveChanges();
        }
    }
}
