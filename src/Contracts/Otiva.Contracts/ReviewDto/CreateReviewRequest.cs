﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Contracts.ReviewDto
{
    public class CreateReviewRequest
    {
        public Guid SellerId { get; set; }

        public string Content { get; set; }
    }
}
