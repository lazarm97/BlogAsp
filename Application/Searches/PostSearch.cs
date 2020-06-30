using Application.Queries.Paginations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Searches
{
    public class PostSearch : PagedSearch
    {
        public string Title { get; set; }

        public string Summary { get; set; }

        public string Text { get; set; }

        public string Username { get; set; }

        public string CategoryName { get; set; }

    }
}
