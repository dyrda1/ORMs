using System;

namespace ORMs.Domain.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public byte Rating { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }

        public User User { get; set; }
        public Product Product { get; set; }
    }
}
