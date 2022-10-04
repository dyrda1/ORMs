using System;
using System.Collections.Generic;

namespace ORMs.Domain.Entities
{
    public class User
    {
        public User()
        {
            UserFolders = new List<UserFolder>();
            Folders = new List<Folder>();
        }

        public Guid Id { get; set; }
        public string Username { get; set; }

        public ICollection<UserFolder> UserFolders { get; set; }
        public ICollection<Folder> Folders { get; set; }
    }
}
