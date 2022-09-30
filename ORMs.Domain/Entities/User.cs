using System;
using System.Collections.Generic;

namespace ORMs.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Cart> Carts { get; set; }
    }
}
