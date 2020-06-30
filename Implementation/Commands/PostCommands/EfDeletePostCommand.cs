using Application.Commands.PostCommands;
using Application.Exceptions;
using Domain;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.PostCommands
{
    public class EfDeletePostCommand : IDeletePostCommand
    {
        private readonly EfContext _context;

        public EfDeletePostCommand(EfContext context)
        {
            _context = context;
        }

        public int Id => 22;

        public string Name => "Delete post";

        public void Execute(int request)
        {
            var post = _context.Posts.Find(request);

            if (post == null)
                throw new EntityNotFoundException(request, typeof(Post));

            _context.Posts.Remove(post);
            _context.SaveChanges();
        }
    }
}
