using Application.Commands.PostCommands;
using Application.DataTransfer;
using Application.Helpers;
using Domain;
using EfDataAccess;
using FluentValidation;
using Implementation.Validators.PostValidators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Implementation.Commands.PostCommands
{
    public class EfCreatePostCommand : ICreatePostCommand
    {
        private readonly EfContext _context;
        private readonly CreatePostValidator _validator;

        public EfCreatePostCommand(EfContext context, CreatePostValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 19;

        public string Name => "Create post";

        public void Execute(PostDto request)
        {
            _validator.ValidateAndThrow(request);

            var ext = Path.GetExtension(request.Image.FileName);
            if (!FileUpload.AllowedExtensions.Contains(ext))
            {
                throw new Exception("File extension is not ok");
            }

            var newFileName = Guid.NewGuid().ToString() + "_" + request.Image.FileName;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", newFileName);
            request.Image.CopyTo(new FileStream(filePath, FileMode.Create));

            var image = new Image
            {
                Alt = request.Title,
                Path = newFileName
            };

            _context.Images.Add(image);

            var post = new Post
            {
                Title = request.Title,
                Summary = request.Summary,
                Text = request.Text,
                CategoryId = request.CategoryId,
                UserId = request.UserId,
                Image = image
            };
            _context.Posts.Add(post);


            if (request.AddTagsInPost != null)
            {
                foreach (var tag in request.AddTagsInPost)
                {
                    _context.PostTags.Add(new PostTag
                    {
                        Post = post,
                        TagId = tag
                    });
                }
            }

            _context.SaveChanges();
        }

    }
}
