
namespace BibliotekBoklusen.Client.Services
{
    public class CategoryManager : ICategoryManager
    {
        private readonly HttpClient _httpClient;

        public CategoryManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            var categoryList = await _httpClient.GetFromJsonAsync<List<Category>>("api/categories");
            if (categoryList == null)
                return null;
            return categoryList;
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var category = await _httpClient.GetFromJsonAsync<Category>($"api/categories/{id}");
            if (category == null)
                return null;
            return category;
        }
        public async Task<string> AddCategory(Category category)
        {
            var result = await _httpClient.PostAsJsonAsync("api/categories", category);
            if (result.IsSuccessStatusCode)
                return await result.Content.ReadAsStringAsync();
            return null;
        }
        public async Task UpdateCategory(Category category)
        {
            await _httpClient.PutAsJsonAsync($"api/categories/{category.Id}", category);
        }
        public async Task DeleteCategoryAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/categories/{id}");
        }
    }
}
