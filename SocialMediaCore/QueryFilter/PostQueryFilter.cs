﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMediaCore.QueryFilter
{
    public class PostQueryFilter
    {
        public int? UserId { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }
    }
}
