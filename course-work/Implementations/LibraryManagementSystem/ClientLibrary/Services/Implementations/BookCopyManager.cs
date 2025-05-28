using BaseLibrary.Entities;
using ClientLibrary.Services.Interfaces;
using System.Net.Http.Json;

namespace ClientLibrary.Services.Implementations
{
    public class BookCopyManager : IBookCopyManager
    {
        private readonly HttpClient _httpClient;

        public BookCopyManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Book>> GetAllReservations()
        {
            var allReservations = await _httpClient.GetFromJsonAsync<List<Book>>("api/bookcopies/getall");
            return allReservations;
        }
    }
}
