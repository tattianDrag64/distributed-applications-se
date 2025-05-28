using BaseLibrary.Entities;
using BaseLibrary.Responses;
using ClientLibrary.Services.Interfaces;
using System.Net.Http.Json;

namespace ClientLibrary.Services.Implementations
{
    public class SearchManager : ISearchManager
    {
        private readonly HttpClient _httpClient;

        public SearchManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<Book> Books { get; set; } = new List<Book>();
        public List<User> Users { get; set; } = new List<User>();
        public string Message { get; set; } = "Loading Books..";

        public event Action BooksChanged;

        public async Task<ServiceResponse<Book>> GetBookById(int bookId)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<Book>>($"api/book/{bookId}");
            return result;
        }

        public async Task GetAllBooks()
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Book>>>("api/book");

            Books = result.Data;

            BooksChanged?.Invoke();
        }

        public async Task<List<string>> GetBookSearchSuggestions(string searchText)
        {
            var result = await _httpClient
               .GetFromJsonAsync<ServiceResponse<List<string>>>($"api/books/searchsuggestions/{searchText}");
            return result.Data;
        }

        public async Task SearchBooks(string searchText)
        {
            var result = await _httpClient
                 .GetFromJsonAsync<ServiceResponse<List<Book>>>($"api/books/search/{searchText}");
            if (result != null && result.Data != null)
            {
                Books = result.Data;
            }
            if (Books.Count == 0) Message = "No books found.";
            BooksChanged?.Invoke();
        }

        public async Task<List<User>> SearchMember(string searchText)
        {
            var result = await _httpClient
                 .GetFromJsonAsync<ServiceResponse<List<User>>>($"api/user/users?searchText={searchText}");
            if (result != null && result.Data != null)
            {
                Users = result.Data;
                return Users;
            }
            return null;
        }
    }
}
