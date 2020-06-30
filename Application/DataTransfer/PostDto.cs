using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer
{
    public class PostDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public string Text { get; set; }

        public int ImageId { get; set; }

        public IFormFile Image { get; set; }

        public int CategoryId { get; set; }

        public int UserId { get; set; }

        public List<int> AddTagsInPost { get; set; }
    }
}
