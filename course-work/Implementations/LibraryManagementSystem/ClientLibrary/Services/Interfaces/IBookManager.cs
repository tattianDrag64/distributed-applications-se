using BaseLibrary.Entities;

namespace ClientLibrary.Services.Interfaces
{
    public interface IBookManager
    {
        Task<List<Book>> GetAllBooks();
        Task<Book> GetBookById(int id);
        Task<string> CreateBook(Book book);
        Task<string> UpdateBook(int id, Book book);
        Task DeleteBook(int id);
        Task<List<Genre>> GetAllGenres();
        IList<string> Types => new List<string>();
    }
}