using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ORMs.Domain.Entities;
using System;

namespace ORM.EntityFrameworkCore.Configurations
{
    public class FolderConfiguration : IEntityTypeConfiguration<Folder>
    {
        public void Configure(EntityTypeBuilder<Folder> builder)
        {
            builder.ToTable("folders");

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .HasDefaultValue(Guid.NewGuid());

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Id)
                .IsClustered()
                .IsUnique();
        }
    }
}
