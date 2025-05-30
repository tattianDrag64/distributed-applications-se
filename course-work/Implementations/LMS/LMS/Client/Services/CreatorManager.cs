namespace BibliotekBoklusen.Client.Services
{
    public class CreatorManager : ICreatorManager
    {
        private readonly HttpClient _httpClient;

        public CreatorManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Creator>> GetAllCreatorsAsync()
        {
            var creators = await _httpClient.GetFromJsonAsync<List<Creator>>("api/creators");
            if (creators == null)
                return null;
            return creators;
        }

        public async Task<Creator> GetCreatorByIdAsync(int id)
        {
            var creator = await _httpClient.GetFromJsonAsync<Creator>($"api/creators/{id}");
            if (creator == null)
                return null;
            return creator;
        }

        public async Task<string> AddCreator(Creator creatorToAdd)
        {
            var result = await _httpClient.PostAsJsonAsync("api/creators", creatorToAdd);
            if (result.IsSuccessStatusCode)
                return await result.Content.ReadAsStringAsync();
            return null;
        }

        public async Task UpdateCreator(Creator creatorToUpdate)
        {
            await _httpClient.PutAsJsonAsync($"api/creators/{creatorToUpdate.Id}", creatorToUpdate);

        }

        public async Task DeleteCreatorAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/creators/{id}");
        }

    }
}
