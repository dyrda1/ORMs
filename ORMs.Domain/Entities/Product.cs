using System;
using System.Collections.Generic;

namespace ORMs.Domain.Entities
{
    public class Product
    {
        public Product()
        {
            Comments = new List<Comment>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ProductParameters Parameters { get; set; }
    }
}
