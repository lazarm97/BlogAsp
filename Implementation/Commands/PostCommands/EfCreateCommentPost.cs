using Application.Commands.PostCommands;
using Application.DataTransfer;
using Domain;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.PostCommands
{
    public class EfCreateCommentPost : ICreateCommentCommand
    {
        private readonly EfContext _context;

        public EfCreateCommentPost(EfContext context)
        {
            _context = context;
        }

        public int Id => 23;

        public string Name => "Create comment";

        public void Execute(CommentDto request)
        {
            var comment = new Comment
            {
                PostId = request.PostId,
                UserId = request.UserId,
                Text = request.Text
            };

            _context.Comments.Add(comment);
            _context.SaveChanges();
        }
    }
}
