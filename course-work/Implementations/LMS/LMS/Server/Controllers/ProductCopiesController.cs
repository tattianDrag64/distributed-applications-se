using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotekBoklusen.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCopiesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductCopiesController(AppDbContext context)
        {
            _context = context;
        }
   
       
        [HttpGet("{getall}")]

        public async Task<ActionResult<List<Product>>> GetAllLoans()
        {
            var allLoans = _context.productCopies.Where(p =>p.IsLoaned ==false).GroupBy(p => p.ProductId).Select(g => g.OrderBy(p => p.Id).FirstOrDefault()).ToList();/*_context.productCopies.Where(p => p.IsLoaned == false).Take(1).ToList();*/
            List<Product> availableProducts = new();

            foreach (var productId in allLoans)
            {
                var availableProduct = _context.Products.Include(p => p.Creators)
                .Include(c => c.Category).FirstOrDefault(p => p.Id == productId.ProductId);
                availableProducts.Add(availableProduct);
            }


            return availableProducts;

        }

        [HttpGet("available-names")]
        public async Task<ActionResult<List<string>>> GetAvailableProductNames()
        {
            var products = await _context.productCopies
                .Where(pc => pc.IsLoaned)
                .Include(pc => pc.product)
                .Select(pc => pc.product.Title)
                .Distinct()
                .ToListAsync();

            return Ok(products);
        }

        [HttpPost("return-by-name")]
        public async Task<ActionResult<bool>> ReturnLoanByName([FromBody] string productName)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Title == productName);
            if (product == null) return NotFound(false);
            var loanedCopy = await _context.productCopies
                .Where(pc => pc.ProductId == product.Id && pc.IsLoaned)
                .OrderBy(pc => pc.Id) 
                .FirstOrDefaultAsync();

            if (loanedCopy == null) return NotFound(false);

            var loan = await _context.Loans
                .Where(l => l.ProductCopyId == loanedCopy.Id && !l.isReturned)
                .FirstOrDefaultAsync();

            if (loan != null)
            {
                loan.isReturned = true;
            }
            loanedCopy.IsLoaned = false;

            await _context.SaveChangesAsync();

            return Ok(true);
        }




    }
}
