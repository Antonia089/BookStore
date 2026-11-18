using BookStore_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Core.Interfaces
{
    public interface IAuthorService
    {
        Task<List<Author>> GetAllAuthorsAsync(string? search = null, string? specialty = null, string? sortOrder = null);
        Task<Author> GetAuthorByIdAsync(int id);
        Task AddAuthorAsync(Author a);
        Task UpdateAuthorAsync(Author a);
        Task DeleteAuthorAsync(int id);
    }
}
