using System;

namespace ORMs.Domain.Entities
{
    public class Cart
    {
        public Guid UserId { get; set; }
        public Guid ProductParametersId { get; set; }
        public int Quantity { get; set; }

        public User User { get; set; }
        public ProductParameters ProductParameters { get; set; }
    }
}
