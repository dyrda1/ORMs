using System;
using System.Collections.Generic;

namespace ORMs.Domain.Entities
{
    public class Folder
    {
        public Folder()
        {
            UserFolders = new List<UserFolder>();
            Messages = new List<Message>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<UserFolder> UserFolders { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
