using System.Net.Http.Json;

namespace BibliotekBoklusen.Client.Services
{
    public class ProductCopyManager : IProductCopyManager
    {
        private readonly HttpClient _httpClient;

        public ProductCopyManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Product>> GetAllLoans()
        {
            
            var allLoans= await  _httpClient.GetFromJsonAsync<List<Product>>("api/productcopies/getall");
            return allLoans;
        }

        
    }
}
