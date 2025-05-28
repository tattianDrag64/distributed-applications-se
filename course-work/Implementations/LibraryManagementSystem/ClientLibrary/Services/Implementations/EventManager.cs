using BaseLibrary.Entities;
using ClientLibrary.Services.Interfaces;
using System.Net.Http.Json;

namespace ClientLibrary.Services.Implementations
{
    public class EventManager : IEventManager
    {
        private readonly HttpClient _httpClient;
        public EventManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<EventLibrary>> GetAllEvents()
        {
            var events = await _httpClient.GetFromJsonAsync<List<EventLibrary>>("api/events");
            return events;
        }

        public async Task<EventLibrary> GetEventById(int id)
        {
            return await _httpClient.GetFromJsonAsync<EventLibrary>($"api/events/{id}");
        }

        public async Task<string> CreateEvent(EventLibrary eventItem)
        {
            var result = await _httpClient.PostAsJsonAsync("api/events", eventItem);
            var message = result.IsSuccessStatusCode ? $"Event \"{eventItem.Title}\" has been added" : "Failed to add event";
            return message;
        }

        public async Task UpdateEvent(int id, EventLibrary eventItem)
        {
            await _httpClient.PutAsJsonAsync($"api/events/{id}", eventItem);
        }

        public async Task DeleteEvent(int id)
        {
            await _httpClient.DeleteAsync($"api/events/{id}");
        }
    }
}
