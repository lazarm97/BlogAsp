using Application.Commands.PostCommands;
using Application.DataTransfer;
using Application.Exceptions;
using Domain;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.PostCommands
{
    public class EfEditPostCommand : IEditPostCommand
    {
        private readonly EfContext _context;

        public EfEditPostCommand(EfContext context)
        {
            _context = context;
        }

        public int Id => 23;

        public string Name => "Edit post";

        public void Execute(PostDto request)
        {
            var post = _context.Posts.Find(request.Id);

            if (post == null)
                throw new EntityNotFoundException(request.Id, typeof(Post));

            post.Title = request.Title;
            post.Summary = request.Summary;
            post.Text = request.Text;
            post.CategoryId = request.CategoryId;

            _context.SaveChanges();
        }
    }
}
