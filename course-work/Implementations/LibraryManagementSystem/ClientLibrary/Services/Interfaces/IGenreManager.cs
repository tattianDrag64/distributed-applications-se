using BaseLibrary.Entities;

namespace ClientLibrary.Services.Interfaces
{
    public interface IGenreManager
    {
        Task<List<Genre>> GetAllGenresAsync();
        Task<Genre> GetGenreByIdAsync(int id);
        Task<string> AddGenre(Genre genre);
        Task UpdateGenre(Genre genre);
        Task DeleteGenreAsync(int id);
    }
}
