using Application.Commands.CategoryCommands;
using Application.Exceptions;
using Domain;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.CategoryCommands
{
    public class EfDeleteCategoryCommand : IDeleteCategoryCommand
    {
        private readonly EfContext _context;

        public EfDeleteCategoryCommand(EfContext context)
        {
            _context = context;
        }

        public int Id => 3;

        public string Name => "Delete category";

        public void Execute(int request)
        {
            var category = _context.Categories.Find(request);

            if (category == null)
                throw new EntityNotFoundException(request, typeof(Category));

            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}
