using Microsoft.EntityFrameworkCore;
using ORM.EntityFrameworkCore.Configurations;
using ORMs.Domain.Entities;

namespace ORM.EntityFrameworkCore
{
    public class MessengerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserFolder> UsersFolders { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<Message> Messages { get; set; }

        public MessengerDbContext(DbContextOptions options) : base(options) 
        { 

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new FolderConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
