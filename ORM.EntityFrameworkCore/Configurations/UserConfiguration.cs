using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ORMs.Domain.Entities;
using System;

namespace ORM.EntityFrameworkCore.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .HasDefaultValue(Guid.NewGuid());

            builder.Property(x => x.Username)
                .HasColumnName("username")
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.HasKey(x => x.Id);

            builder.HasAlternateKey(x => x.Username);

            builder.HasIndex(x => x.Id)
                .IsClustered()
                .IsUnique();

            builder.HasIndex(x => x.Username)
                .IsClustered(false)
                .IsUnique();

            builder.HasMany(x => x.Folders)
                .WithMany(x => x.Users)
                .UsingEntity<UserFolder>(
                x => x
                .HasOne(x => x.Folder)
                .WithMany(x => x.UserFolders)
                .HasForeignKey(x => x.FolderId),
                x => x
                .HasOne(x => x.User)
                .WithMany(x => x.UserFolders)
                .HasForeignKey(x => x.UserId),
                x =>
                {
                    x.ToTable("users_folders");

                    x.Property(x => x.UserId)
                        .HasColumnName("user_id");

                    x.Property(x => x.FolderId).
                        HasColumnName("folder_id");

                    x.HasKey(x => new { x.UserId, x.FolderId });

                    x.HasIndex(x => new { x.UserId, x.FolderId })
                        .IsClustered()
                        .IsUnique();
                });
        }
    }
}
