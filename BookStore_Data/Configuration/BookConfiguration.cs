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
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
          builder.HasData
                 (
                new Book
                 {
                     Id = 1,
                     Title = "White Teeth",
                     Genres = "Fiction",
                     YearOfPublication = 2000,
                    Price=10,
                    NumberOfPages=448,
                    AuthorId = 1
                },
                new Book
                 {
                     Id = 2,
                     Title = "Atonement",
                     Genres = "Fiction",
                     YearOfPublication = 2001,
                     Price =10,
                        NumberOfPages= 331,
                    AuthorId = 2
                }
                );
        }
    }
}
