namespace BibliotekBoklusen.Client.Services
{
    public interface IProductManager
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<string> CreateProduct(Product product);
        Task<string> UpdateProduct(int id, Product product);
        Task DeleteProduct(int id);
        Task<List<Category>> GetAllCategories();
        IList<string> Types => new List<string>();
    }
}