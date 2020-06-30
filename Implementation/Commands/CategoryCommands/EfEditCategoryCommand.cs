using Application.Commands.CategoryCommands;
using Application.DataTransfer;
using Application.Exceptions;
using Domain;
using EfDataAccess;
using FluentValidation;
using Implementation.Validators.Category;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.CategoryCommands
{
    public class EfEditCategoryCommand : IEditCategoryCommand
    {
        private readonly EfContext _context;
        private readonly EditCategoryValidator _validator;
        

        public EfEditCategoryCommand(EfContext context, EditCategoryValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 4;

        public string Name => "Edit category";

        public void Execute(CategoryDto request)
        {
            _validator.ValidateAndThrow(request);

            var category = _context.Categories.Find(request.Id);

            if (category == null)
                throw new EntityNotFoundException(request.Id, typeof(Category));
            

            category.Name = request.Name;
            _context.SaveChanges();
        }
    }
}
