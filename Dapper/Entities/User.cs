using System;
using System.Collections.Generic;

namespace ORM.Dapper.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
