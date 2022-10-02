using System;

namespace ORMs.Domain.Entities
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Guid FolderId { get; set; }

        public Folder Folder { get; set; }
    }
}
