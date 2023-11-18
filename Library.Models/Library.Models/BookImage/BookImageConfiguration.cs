using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class BookImageConfiguration : IEntityTypeConfiguration<BookImage>
    {
        public void Configure(EntityTypeBuilder<BookImage> builder)
        {
            builder.ToTable("BookImage");
            builder.HasKey(i => i.ID);
            builder.Property(i => i.ID)
                .ValueGeneratedOnAdd();
            builder.Property(i => i.BookID)
                .IsRequired();
            builder.Property(i => i.Path)
                .IsRequired().HasMaxLength(1000);
        }
    }
}
