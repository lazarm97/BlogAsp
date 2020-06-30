using Application.Commands.UserCommands;
using Application.DataTransfer;
using Application.Email;
using Domain;
using EfDataAccess;
using FluentValidation;
using Implementation.Validators.UserValidators;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Implementation.Commands.UserCommands
{
    public class EfCreateUserCommand : ICreateUserCommand
    {
        private readonly EfContext _context;
        private readonly CreateUserValidator _validator;
        private readonly IEmailSender _sender;

        public EfCreateUserCommand(EfContext context, CreateUserValidator validator, IEmailSender sender)
        {
            _context = context;
            _validator = validator;
            _sender = sender;
        }

        public int Id => 14;

        public string Name => "Create user";

        public void Execute(UserDto request)
        {
            _validator.ValidateAndThrow(request);

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
                RoleId = request.RoleId
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            _sender.Send(new SendEmailDto
            {
                Content = "<h1>Successfull registration!</h1>",
                SendTo = request.Email,
                Subject = "Registration"
            });

        }

    }
}
