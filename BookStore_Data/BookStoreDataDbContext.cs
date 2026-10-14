using BookStore_Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_Data
{
    public class BookStoreDataDbContext : DbContext
    {
        public BookStoreDataDbContext(DbContextOptions<BookStoreDataDbContext> options) : base(options)
        {

        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Purchases> Purchases { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Purchases>()
                .HasKey(p => new { p.ClientId, p.BookId });
            modelBuilder.Entity<Purchases>()
                .HasOne(p => p.Client)
                .WithMany(c => c.Purchases)
                .HasForeignKey(p => p.ClientId);
            modelBuilder.Entity<Purchases>()
                .HasOne(p => p.Book)
                .WithMany(b => b.Purchases)
                .HasForeignKey(p => p.BookId);
        }
    }
}
