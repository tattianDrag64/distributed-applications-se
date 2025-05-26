using BaseLibrary.Entities;
using ClientLibrary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

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
            var categoryList = await _httpClient.GetFromJsonAsync<List<Author>>("api/authors");
            if (categoryList == null)
                return null;
            return categoryList;
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            var category = await _httpClient.GetFromJsonAsync<Author>($"api/authors/{id}");
            if (category == null)
                return null;
            return category;
        }
        public async Task<string> AddAuthor(Author category)
        {
            var result = await _httpClient.PostAsJsonAsync("api/authors", category);
            if (result.IsSuccessStatusCode)
                return await result.Content.ReadAsStringAsync();
            return null;
        }
        public async Task UpdateAuthor(Author category)
        {
            await _httpClient.PutAsJsonAsync($"api/authors/{category.Id}", category);
        }
        public async Task DeleteAuthorAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/authors/{id}");
        }
    }
}
