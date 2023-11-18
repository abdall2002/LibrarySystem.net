using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public static class RelationshipsMappercs
    {
        public static void MappRelationships(this ModelBuilder builder)
        {
            builder.Entity<Book>()
                .HasOne(i => i.Author)
                .WithMany(i => i.Books)
                .HasForeignKey(i => i.AuthorID)
                .IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<BookImage>().HasOne(i => i.Book)
                .WithMany(i => i.BookImages)
                .HasForeignKey(i => i.BookID)
                .IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
