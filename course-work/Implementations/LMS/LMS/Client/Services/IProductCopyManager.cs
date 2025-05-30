
namespace BibliotekBoklusen.Client.Services
{
    public interface IProductCopyManager
    {
        Task<List<Product>> GetAllLoans();
    }
}