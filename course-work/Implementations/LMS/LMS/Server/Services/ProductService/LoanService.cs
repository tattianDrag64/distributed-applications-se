using Microsoft.EntityFrameworkCore;

namespace BibliotekBoklusen.Server.Services.ProductService
{
    public class LoanService : ILoanService
    {
        private readonly AppDbContext _context;
        public LoanService(AppDbContext context)
        {
            _context = context;
        }
        public ProductCopy ProductCopy { get; set; }

        public async Task<Loan> CreateLoan(int productId, int userId)
        {
            var prodcop = _context.productCopies.Where(pc => pc.ProductId == productId && pc.IsLoaned == false).ToList();
            ProductCopy = prodcop.FirstOrDefault(pc => pc.ProductId == productId);

            if (ProductCopy != null)
            {
                ProductCopy.IsLoaned = true;

                _context.productCopies.Update(ProductCopy);
                _context.SaveChanges();

                Loan loan = new();
                loan.ProductCopyId = ProductCopy.Id;
                loan.UserId = userId;
                return loan;
            }
            return null;
        }

        public async Task<List<Loan>> GetLoansById(int userId)
        {
            List<Product> products = new();
            var productCopies = _context.Loans.Include(p => p.ProductCopy).ThenInclude(p => p.product).Where(pc => pc.UserId == userId).ToList();
            return productCopies;
        }

        public async Task<List<Product>> GetTopProducts()
        {
            var topProductsIDs = _context.Loans // table with a row for each loan of a product
                .Include(p => p.ProductCopy).ThenInclude(p => p.product.Creators)
                .GroupBy(p => p.ProductCopy.ProductId) //group all rows with same product id together
                .OrderByDescending(g => g.Count()) // move products with highest loan to the top
                .Take(5) // take top 5
                .Select(x => x.Key) // get id of products
                .ToList(); // execute query and convert it to a list

            var topProducts = _context.Products.Include(p => p.Creators).Include(p => p.Category)  // table with products information
                .Where(x => topProductsIDs.Contains(x.Id)).ToList(); // get info of products that their Ids are retrieved in previous query

            return topProducts;
        }

        public async Task<bool> ReturnLoan(int productCopyId)
        {
            Loan loanToUpdate = new();

            var personLoans = _context.productCopies.FirstOrDefault(pc => pc.Id == productCopyId);
            var loans = _context.Loans.ToList();
            foreach (var loan in loans)
            {
                if (loan.ProductCopyId == productCopyId && loan.isReturned == false)
                {
                    loanToUpdate = loan;
                }
            }

            if (personLoans != null && personLoans.IsLoaned == true)
            {
                loanToUpdate.isReturned = true;
                _context.Loans.Update(loanToUpdate);
                personLoans.IsLoaned = false;
                _context.productCopies.Update(personLoans);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
