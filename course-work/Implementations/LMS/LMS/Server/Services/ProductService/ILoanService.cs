
using Microsoft.AspNetCore.Mvc;

namespace BibliotekBoklusen.Server.Services.ProductService
{
    public interface ILoanService
    {
        Task<Loan> CreateLoan(int ProductId, int UserId);
        Task<List<Loan>> GetLoansById(int userId);
        Task<bool> ReturnLoan(int productCopyId);
        Task<List<Product>> GetTopProducts();

    }
}