using BaseLibrary.Entities;
using ClientLibrary.Services.Interfaces;
using System.Net.Http.Json;

namespace ClientLibrary.Services.Implementations
{
    public class GenreManager : IGenreManager
    {
        private readonly HttpClient _httpClient;

        public GenreManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Genre>> GetAllGenresAsync()
        {
            var genreList = await _httpClient.GetFromJsonAsync<List<Genre>>("api/genres");
            if (genreList == null)
                return null;
            return genreList;
        }

        public async Task<Genre> GetGenreByIdAsync(int id)
        {
            var genre = await _httpClient.GetFromJsonAsync<Genre>($"api/genres/{id}");
            if (genre == null)
                return null;
            return genre;
        }
        public async Task<string> AddGenre(Genre genre)
        {
            var result = await _httpClient.PostAsJsonAsync("api/genres", genre);
            if (result.IsSuccessStatusCode)
                return await result.Content.ReadAsStringAsync();
            return null;
        }
        public async Task UpdateGenre(Genre genre)
        {
            await _httpClient.PutAsJsonAsync($"api/genres/{genre.Id}", genre);
        }
        public async Task DeleteGenreAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/genres/{id}");
        }
    }
}
