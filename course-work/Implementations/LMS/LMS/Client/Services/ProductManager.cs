namespace BibliotekBoklusen.Client.Services
{
    public class ProductManager : IProductManager
    {
        private readonly HttpClient _httpClient;
        public ProductManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public event Action ProductsChanged;

        public async Task<List<Product>> GetAllProducts()
        {
            List<Product> products = new();

            products = await _httpClient.GetFromJsonAsync<List<Product>>("api/products");
            return products;
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Product>($"api/products/{id}");
        }

        public async Task<string> CreateProduct(Product productToAdd)
        {
            var result = await _httpClient.PostAsJsonAsync("api/products", productToAdd);
            var message = result.IsSuccessStatusCode ? "Produkten tillagd" : null;
            return message;
        }

        public async Task<string> UpdateProduct(int id, Product product)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/products/{id}", product);
            var message = result.IsSuccessStatusCode ? "Produkten har uppdaterats" : null;
            return message;
        }

        public async Task DeleteProduct(int id)
        {
            await _httpClient.DeleteAsync($"api/products/{id}");
        }

        public Task<List<Category>> GetAllCategories()
        {
            throw new NotImplementedException();
        }

    }
}

