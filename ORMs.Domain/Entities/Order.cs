using System;

namespace ORMs.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Guid ProductParametersId { get; set; }
        public Guid UserId { get; set; }

        public User User { get; set; }
        public ProductParameters ProductParameters { get; set; }
    }
}
