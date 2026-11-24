using Book_Core.Interfaces;
using BookStore_Data;
using BookStore_Data.Entities;
using Microsoft.Data.SqlClient;
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
            var а = await context.Authors.FindAsync(id);
            if (а == null)
            {
                return;
            }
            bool HasBooks = await context.Books.AnyAsync(d => d.AuthorId == id);
            if (HasBooks)
            {
                throw new InvalidOperationException("Cannot delete author with associated books.");
            }
            а.IsDeleted = true;
            await context.SaveChangesAsync();
        }

        public async Task<List<Author>> GetAllAuthorsAsync(string? search = null, string? speciality = null, string? SortOrder = null)
        {
            var query = context.Authors.Include(d => d.Books).Where(a=>!a.IsDeleted).AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(d => d.FirstName.Contains(search) || d.LastName.Contains(search));
            }
            if (!string.IsNullOrEmpty(speciality))
            {
                query = query.Where(d => d.Country == speciality);
            }

            switch (SortOrder)
            {
                case "name_asc":
                    query = query.OrderByDescending(d => d.FirstName).ThenBy(d => d.LastName);
                    break;
                case "name_desc":
                    query = query.OrderByDescending(d => d.FirstName).ThenByDescending(d => d.LastName);
                    break;
                case "specialty":
                    query = query.OrderByDescending(d => d.Country).ThenBy(d => d.FirstName);
                    break;
                default:
                    query = query.OrderBy(d => d.FirstName).ThenBy(d => d.LastName);
                    break;
            }
            return await query.ToListAsync();
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            return await context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task UpdateAuthorAsync(Author a)
        {
            await context.SaveChangesAsync();
        }
    }
}
