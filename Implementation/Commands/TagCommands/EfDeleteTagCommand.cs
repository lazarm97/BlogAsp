using Application.Commands.TagCommands;
using Application.DataTransfer;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.TagCommands
{
    public class EfDeleteTagCommand : IDeleteTagCommand
    {
        private readonly EfContext _context;

        public EfDeleteTagCommand(EfContext context)
        {
            _context = context;
        }

        public int Id => 14;

        public string Name => "Delete tag";

        public void Execute(int request)
        {
            var tag = _context.Tags.Find(request);

            if (tag == null)
                throw new EntityNotFoundException(request, typeof(TagDto));

            _context.Tags.Remove(tag);
            _context.SaveChanges();
        }
    }
}
