using Application.Commands.PostCommands;
using Application.DataTransfer;
using Domain;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.PostCommands
{
    public class EfAddVoteCommand : IAddVoteCommand
    {
        private readonly EfContext _context;

        public EfAddVoteCommand(EfContext context)
        {
            _context = context;
        }

        public int Id => 24;

        public string Name => "Add vote";

        public void Execute(VoteDto request)
        {
            var vote = new Vote
            {
                UserId = request.UserId,
                PostId = request.PostId,
                Value = request.Value
            };

            _context.Votes.Add(vote);
            _context.SaveChanges();
        }
    }
}
