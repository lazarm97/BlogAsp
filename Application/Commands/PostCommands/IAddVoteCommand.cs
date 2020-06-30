using Application.DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.PostCommands
{
    public interface IAddVoteCommand : ICommand<VoteDto>
    {
    }
}
