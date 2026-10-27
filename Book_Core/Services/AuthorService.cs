using Book_Core.Interfaces;
using BookStore_Data;
using BookStore_Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Core.Services
{
    public class AuthorService : IAuthorService
    {
        protected readonly BookStoreDataDbContext context;

        public AuthorService(BookStoreDataDbContext dbContext)
        {
            context = dbContext;
        }

        public Task AddAuthorAsync(Author a)
        {
            context.Authors.Add(a);
           return context.SaveChangesAsync();
        }

        public async Task DeleteAuthorAsync(int id)
        {
            var author = await context.Authors.FindAsync(id);
            if (author != null)
            {
                author.IsDeleted = true;
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            return await context.Authors.Where(h => !h.IsDeleted).ToListAsync();
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            return await context.Authors.FindAsync(id);
        }

        public async Task UpdateAuthorAsync(Author a)
        {
            await context.SaveChangesAsync();
        }
    }
}
