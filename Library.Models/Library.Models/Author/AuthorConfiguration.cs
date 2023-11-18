using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace Library.Models
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder) 
        {
            builder.ToTable("Author");
            builder.HasKey(i => i.ID);
            builder.Property(i => i.ID)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Property(i => i.Name)
                .HasMaxLength(500).IsRequired();
        }
    }
}
