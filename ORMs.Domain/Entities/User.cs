using System;
using System.Collections.Generic;

namespace ORMs.Domain.Entities
{
    public class User
    {
        public User()
        {
            Comments = new List<Comment>();
            Orders = new List<Order>();
            Carts = new List<Cart>();
        }

        public Guid Id { get; set; }
        public string Username { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Cart> Carts { get; set; }
    }
}
