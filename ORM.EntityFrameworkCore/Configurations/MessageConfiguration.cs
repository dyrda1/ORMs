using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ORMs.Domain.Entities;
using System;

namespace ORM.EntityFrameworkCore.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("messages");

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .HasDefaultValue(Guid.NewGuid());

            builder.Property(x => x.Text)
                .HasColumnName("text")
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.Property(x => x.FolderId)
                .HasColumnName("folder_id")
                .IsRequired();

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Id)
                .IsClustered()
                .IsUnique();

            builder.HasOne(x => x.Folder)
                .WithMany(x => x.Messages)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(x => x.FolderId);
        }
    }
}
