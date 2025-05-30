

namespace BibliotekBoklusen.Client.Services
{
    public interface ISearchManager
    {
        event Action ProductsChanged;
        List<Product> Products { get; set; }
        string Message { get; set; }
        /*if we have no categoryUrl given then we will display all the products or recive all the products
         * from the request(all the products from the server)
         */
        Task GetAllProducts();
        Task<ServiceResponse<Product>> GetProductById(int productId);

        Task SearchProducts(string searchText);
        Task<List<string>> GetProductSearchSuggestions(string searchText);
        Task<List<User>> SearchMember(string searchText);

        
    }

}

