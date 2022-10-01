using ORMs.Domain.Enums;
using System;
using System.Collections.Generic;

namespace ORMs.Domain.Entities
{
    public class ProductParameters
    {
        public ProductParameters()
        {
            Orders = new List<Order>();
            Carts = new List<Cart>();
        }

        public Guid Id { get; set; }
        public Color Color { get; set; }
        public Memory Memory { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Guid ProductId { get; set; }

        public Product Product { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Cart> Carts { get; set; }
    }
}
