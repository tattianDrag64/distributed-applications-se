
using BaseLibrary.Entities;
using ClientLibrary.Services.Interfaces;
using System.Net.Http.Json;

namespace ClientLibrary.Services.Implementations
{
    public class BookManager : IBookManager
    {
        private readonly HttpClient _httpClient;
        public BookManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public event Action BooksChanged;

        public async Task<List<Book>> GetAllBooks()
        {
            List<Book> books = new();

            books = await _httpClient.GetFromJsonAsync<List<Book>>("api/books");
            return books;
        }

        public async Task<Book> GetBookById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Book>($"api/books/{id}");
        }

        public async Task<string> CreateBook(Book bookToAdd)
        {
            var result = await _httpClient.PostAsJsonAsync("api/books", bookToAdd);
            var message = result.IsSuccessStatusCode ? "Book wasnt created" : null;
            return message;
        }

        public async Task<string> UpdateBook(int id, Book book)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/books/{id}", book);
            var message = result.IsSuccessStatusCode ? "Book wasnt update" : null;
            return message;
        }

        public async Task DeleteBook(int id)
        {
            await _httpClient.DeleteAsync($"api/books/{id}");
        }

        public async Task<List<Genre>> GetAllGenres()
        {
            return await _httpClient.GetFromJsonAsync<List<Genre>>("api/genres");
        }
    }
}

