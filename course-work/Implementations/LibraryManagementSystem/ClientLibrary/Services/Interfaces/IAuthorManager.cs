using BaseLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Services.Interfaces
{
    public interface IAuthorManager 
    {
        Task<List<Author>> GetAllAuthorsAsync();
        Task<Author> GetAuthorByIdAsync(int id);
        Task<string> AddAuthor(Author author);
        Task UpdateAuthor(Author author);
        Task DeleteAuthorAsync(int id);
    }
}
