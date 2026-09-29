using BookStore_Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_Data
{
    public class BookStoreDataDbContext:DbContext
    {
        public BookStoreDataDbContext(DbContextOptions<BookStoreDataDbContext> options):base(options)
        {
            
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
