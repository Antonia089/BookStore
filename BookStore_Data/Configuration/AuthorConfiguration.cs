using BookStore_Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_Data.Configuration
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasData
                 (
                new Author
                 {
                     Id = 1,
                     FirstName = "Zadie",
                     LastName = "Smith",
                     Country = "UK",
                     CreatedOn = DateTime.Now,
                     IsDeleted = false
                 },
                new Author
                 {
                     Id = 2,
                     FirstName = "Ian",
                     LastName = "McEwan",
                     Country = "UK",
                     CreatedOn = DateTime.Now,
                     IsDeleted = false
                 }
                );
        }
    }
}
