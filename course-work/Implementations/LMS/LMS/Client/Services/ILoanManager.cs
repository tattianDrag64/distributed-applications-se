
namespace BibliotekBoklusen.Client.Services
{
    public interface ILoanManager
    {
        Task<string> AddLoan(int productId, int userId);
        Task<List<Loan>> GetAllLoansAsync();
        Task<Loan> GetLoanByIdAsync(int id);
        Task<bool> ReturnLoanAsync(int id);
        Task UpdateLoan(Loan loanToUpdate);
        Task<List<Loan>> GetLoansByUserId(int userId);

        Task<bool> ReturnLoanByNameAsync(string productName);
        Task<List<string>> GetAvailableLoanedProductNamesAsync();

        Task<List<Product>> GetTopProducts();
    }
}