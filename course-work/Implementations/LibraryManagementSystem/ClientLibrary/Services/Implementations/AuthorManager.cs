using BaseLibrary.Entities;
using ClientLibrary.Services.Interfaces;
using System.Net.Http.Json;

namespace ClientLibrary.Services.Implementations
{
    public class AuthorManager : IAuthorManager
    {
        private readonly HttpClient _httpClient;

        public AuthorManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            var authors = await _httpClient.GetFromJsonAsync<List<Author>>("api/authors");
            if (authors == null)
                return null;
            return authors;
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            var author = await _httpClient.GetFromJsonAsync<Author>($"api/authors/{id}");
            if (author == null)
                return null;
            return author;
        }

        public async Task<string> AddAuthor(Author authorToAdd)
        {
            var result = await _httpClient.PostAsJsonAsync("api/authors", authorToAdd);
            if (result.IsSuccessStatusCode)
                return await result.Content.ReadAsStringAsync();
            return null;
        }

        public async Task UpdateAuthor(Author authorToUpdate)
        {
            await _httpClient.PutAsJsonAsync($"api/authors/{authorToUpdate.Id}", authorToUpdate);
        }

        public async Task DeleteAuthorAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/authors/{id}");
        }
    }
}
