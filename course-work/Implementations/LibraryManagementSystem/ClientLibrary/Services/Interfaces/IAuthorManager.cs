using BaseLibrary.Entities;

namespace ClientLibrary.Services.Interfaces
{
    public interface IAuthorManager
    {
        Task<List<Author>> GetAllAuthorsAsync();
        Task<Author> GetAuthorByIdAsync(int id);
        Task<string> AddAuthor(Author authorToAdd);
        Task UpdateAuthor(Author author);
        Task DeleteAuthorAsync(int id);
    }
}
