using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Book");
            builder.HasKey(i => i.ID);
            builder.Property(i => i.ID)
                .ValueGeneratedOnAdd();
            builder.Property(i => i.Title)
                .IsRequired().HasMaxLength(1000);
            builder.Property(i => i.AuthorID)
                .IsRequired();
            builder.Property(i => i.Description)
                .IsRequired().HasMaxLength(1500);
        }
    }
}
