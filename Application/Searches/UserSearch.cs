﻿using Application.Queries.Paginations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Searches
{
    public class UserSearch : PagedSearch
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string RoleName { get; set; }
    }
}
