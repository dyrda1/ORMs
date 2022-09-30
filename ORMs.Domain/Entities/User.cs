using System;
using System.Collections.Generic;

namespace ORMs.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Cart> Carts { get; set; } = new List<Cart>();
    }
}
