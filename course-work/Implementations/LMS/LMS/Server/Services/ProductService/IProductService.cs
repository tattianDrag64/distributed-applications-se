
using Microsoft.AspNetCore.Mvc;

namespace BibliotekBoklusen.Server.Services.ProductService
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<ActionResult<string>> CreateProduct(Product productToAdd);
        Task<string> UpdateProduct(int id, Product productToUpdate);
        Task<string> DeleteProduct(int id);
        Task<ServiceResponse<List<Product>>> SearchProducts(string searchText);
        Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText);
        Task CreateProductCopies(Product product);
    }
}
