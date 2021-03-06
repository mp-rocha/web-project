﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Backend.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

    }
}
