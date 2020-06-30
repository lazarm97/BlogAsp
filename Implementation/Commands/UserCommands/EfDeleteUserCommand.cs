using Application.Commands.UserCommands;
using Application.DataTransfer;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.UserCommands
{
    public class EfDeleteUserCommand : IDeleteUserCommand
    {
        private readonly EfContext _context;

        public EfDeleteUserCommand(EfContext context)
        {
            _context = context;
        }

        public int Id => 18;

        public string Name => "Delete user";

        public void Execute(int request)
        {
            var user = _context.Users.Find(request);

            if (user == null)
                throw new EntityNotFoundException(request, typeof(UserDto));

            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
