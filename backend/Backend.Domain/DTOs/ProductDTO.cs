using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Domain.DTOs
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Price { get; set; }
    }
}
