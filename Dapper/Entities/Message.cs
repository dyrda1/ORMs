using System;

namespace ORM.Dapper.Entities
{
    public class Message
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
