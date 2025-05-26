using ClientLibrary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Services.Implementations
{
    public class BaseManager<T> : IBaseManager<T> where T : class
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;

        public BaseManager(HttpClient httpClient, string endpoint)
        {
            _httpClient = httpClient;
            _endpoint = endpoint.TrimEnd('/');
        }

        public async Task<string> Add(T entity)
        {
            var result = await _httpClient.PostAsJsonAsync($"{_endpoint}", entity);
            if (result.IsSuccessStatusCode)
                return await result.Content.ReadAsStringAsync();
            return null;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"{_endpoint}/{id}");
        }

        public async Task<List<T>> GetAllAsync()
        {
            var entityList = await _httpClient.GetFromJsonAsync<List<T>>($"{_endpoint}");
            return entityList ?? new List<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _httpClient.GetFromJsonAsync<T>($"{_endpoint}/{id}");
            return entity;
        }

        public async Task Update(T entity)
        {
            await _httpClient.PutAsJsonAsync($"{_endpoint}", entity);
        }
    }
}
