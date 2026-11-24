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
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.Genres)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.YearOfPublication)
                .IsRequired();

            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_Book_YearOfPublication",
                    "[YearOfPublication] >= 1900 AND [YearOfPublication] <= 2026");
            });

            builder.Property(b => b.Price)
                .IsRequired()
                .HasColumnType("decimal(8,2)");

            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_Book_Price",
                    "[Price] >= 1 AND [Price] <= 1000");
            });

            builder.Property(b => b.NumberOfPages)
                .IsRequired()
                .HasDefaultValue(2);

            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_Book_NumberOfPages",
                    "[NumberOfPages] >= 2 AND [NumberOfPages] <= 1000");
            });

            builder.HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            
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
