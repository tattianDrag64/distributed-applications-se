using static System.Net.WebRequestMethods;

namespace BibliotekBoklusen.Client.Services
{

    public class LoanManager : ILoanManager
    {
        private readonly HttpClient _httpClient;
        public LoanManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Loan>> GetAllLoansAsync()
        {
            var loans = await _httpClient.GetFromJsonAsync<List<Loan>>("api/loans");
            if (loans == null)
            {
                return null;
            }
            return loans;
        }

        public async Task<Loan> GetLoanByIdAsync(int id)
        {
            var loan = await _httpClient.GetFromJsonAsync<Loan>($"api/loans/{id}");
            if (loan == null)
            {
                return null;
            }
            return loan;
        }

        public async Task<List<Loan>> GetLoansByUserId(int userId)
        {
            return await _httpClient.GetFromJsonAsync<List<Loan>>($"api/loansByUser/{userId}");
        }

        public async Task<string> AddLoan(int productId, int userId)
        {
            var result = await _httpClient.PostAsJsonAsync($"api/loans/{productId}", userId);
            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadAsStringAsync();
            }
            return null;
        }

        public async Task UpdateLoan(Loan loanToUpdate)
        {
            await _httpClient.PutAsJsonAsync($"api/loans/{loanToUpdate.Id}", loanToUpdate);
        }
        public async Task<bool> ReturnLoanAsync(int id)
        {            
           var response = await _httpClient.PutAsJsonAsync($"api/loans", id);
            if(response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<List<Product>> GetTopProducts()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Product>>("api/Loans/TopProducts");
            return result;
        }

        public async Task<List<string>> GetAvailableLoanedProductNamesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<string>>("api/productcopies/available-names");
            return response ?? new List<string>();
        }

        public async Task<bool> ReturnLoanByNameAsync(string productName)
        {
            var response = await _httpClient.PostAsJsonAsync("api/productcopies/return-by-name", productName);
            return await response.Content.ReadFromJsonAsync<bool>();
        }

    }
}
