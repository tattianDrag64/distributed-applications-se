

namespace BibliotekBoklusen.Client.Services
{
    public class SearchManager: ISearchManager
    {
        private readonly HttpClient _httpClient;

        public SearchManager(HttpClient httpClient)
        {
         
            _httpClient = httpClient;
        }

        public List<Product> Products { get; set; } = new List<Product>();
        public List<User> Users { get; set; } = new List<User>();
        public string Message { get; set; } = "Loading Products..";

        public event Action ProductsChanged;
        public async Task<ServiceResponse<Product>> GetProductById(int productId)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<Product>>($"api/product/{productId}");
            return result;
        }

        public async Task GetAllProducts()
        {
           var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product");

            Products = result.Data;

            ProductsChanged.Invoke();
        }

        public async Task<List<string>> GetProductSearchSuggestions(string searchText)
        {
            var result = await _httpClient
               .GetFromJsonAsync<ServiceResponse<List<string>>>($"api/products/searchsuggestions/{searchText}");
            return result.Data;
        }

        public async Task SearchProducts(string searchText)
        {
            var result = await _httpClient
                 .GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/products/search/{searchText}");
            if (result != null && result.Data != null)
            {
                Products = result.Data;

            }
            if (Products.Count == 0) Message = "No products found.";
            ProductsChanged?.Invoke();
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
