using Book_Core.Interfaces;
using BookStore_Data;
using BookStore_Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Book_Core.Services
{
    public class BookService : IBookService
    {
        protected readonly BookStoreDataDbContext context;

        public BookService(BookStoreDataDbContext context)
        {
            this.context = context;
        }

       public async Task  AddBookAsync(Book book)
        {
            context.Books.Add(book);
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

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await context.Books.Include(d => d.Author).ToListAsync();
        }

       public async Task<Book> GetBookByIdAsync(int id)
        {
            return await context.Books.FindAsync(id);
        }

        public async Task<Book> GetBookDetailsAsync(int id)
        {
            return await context.Books.Include(d => d.Author).
     Include(d => d.Purchases).
     ThenInclude(dp => dp.Book).
     FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task UpdateBookAsync(Book book)
        {
            await context.SaveChangesAsync();
        }
    }
}
