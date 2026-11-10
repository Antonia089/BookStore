using Book_Core.Interfaces;
using BookStore_Data;
using BookStore_Data.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Book_Core.Services
{
    public class BookService : IBookService
    {
        protected readonly BookStoreDataDbContext context;

        public BookService(BookStoreDataDbContext context)
        {
            this.context = context;
        }

        public async Task AddBookAsync(Book book)
        {
            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            context.Books.Update(book);
            await context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = context.Books.Include(d => d.Purchases).FirstOrDefault(d => d.Id == id);
            if (book != null)
            {

                context.Purchases.RemoveRange(book.Purchases);
                context.Books.Remove(book);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Book>> GetAllBooksAsync(string? search = null, decimal? minBeds = null, string? sortOrder = null)
        {
            var query = context.Books.Include(b => b.Author).AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(h => h.Title.Contains(search));
            }
            if (minBeds.HasValue)
            {
                query = query.Where(h => h.Price > minBeds.Value);
            }
            switch (sortOrder)
            {
                case "title":
                    query = query.OrderBy(h => h.Title);
                    break;
                case "price_asc":
                    query = query.OrderBy(h => h.Price);
                    break;
                case "price_desc":
                    query = query.OrderByDescending(h => h.Price);
                    break;
                default:
                    query = query.OrderBy(h => h.Title);
                    break;
            }
            return await query.ToListAsync();
        }

       public async Task<Book> GetBookByIdAsync(int id)
        {
            return await context.Books.FindAsync(id);
        }

        public async Task<Book> GetBookDetailsAsync(int id)
        {
            return await context.Books.Include(b => b.Author)
.Include(b => b.Purchases).ThenInclude(p => p.Client)
.FirstOrDefaultAsync(d => d.Id == id);
        }

       
    }
}
