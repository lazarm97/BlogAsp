using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer
{
    public class PostShowDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public string Text { get; set; }

        public string ImageAlt { get; set; }

        public string ImagePath { get; set; }

        public string Username { get; set; }

        public string CategoryName { get; set; }

    }
}
