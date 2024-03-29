﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Vote : BaseEntity
    {
        public decimal Value { get; set; }

        public int UserId { get; set; }

        public int PostId { get; set; }

        public User User { get; set; }

        public Post Post { get; set; }
    }
}
