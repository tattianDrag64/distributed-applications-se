using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotekBoklusen.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILoanService _loanService;

        public LoansController(AppDbContext context, ILoanService loanService)
        {
            _context = context;
            _loanService = loanService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Loan>>> GetAllLoans()
        {
            var loans = await _context.Loans
                .Include(l => l.User)
                .Include(l => l.ProductCopy)
                .ThenInclude(pc => pc.product)
                .OrderBy(l => l.ReturnDate)
                .ToListAsync();

            if (loans == null)
            {
                return NotFound("There are no loans");
            }
            return Ok(loans);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetLoansById(int id)
        {
            var productUserHas = _loanService.GetLoansById(id);

            if (productUserHas == null)
            {
                return NotFound("There is no loan with that ID");
            }
            return Ok(productUserHas);
        }

        [HttpPost("{productId}")]
        public async Task<ActionResult<string>> CreateLoan([FromRoute] int productId, [FromBody] int userId)
        {
            Loan loan = await _loanService.CreateLoan(productId, userId);

            if (loan != null)
            {
                await _context.Loans.AddAsync(loan);
                _context.SaveChangesAsync();
                return Ok("Product borrowed");
            }
            else
            {
                return NotFound("Failed");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoan(int id)
        {
            var loan = _context.Loans.Where(x => x.Id == id).FirstOrDefault();

            if (loan == null)
            {
                return BadRequest("There is no loan with that ID");
            }
            else
            {
                _context.Loans.Remove(loan);
                await _context.SaveChangesAsync();
            }
            return Ok("Loan has been deleted");
        }

        [HttpPut]
        public async Task<IActionResult> ReturnLoan([FromBody] int productCopyId)
        {
            var response = await _loanService.ReturnLoan(productCopyId);

            if (response == true)
            {
                return Ok("Loan has been updated");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("TopProducts")]
        public async Task<ActionResult<List<Product>>> GetTopProducts()
        {
            var result = await _loanService.GetTopProducts();
            return Ok(result);
        }
    }
}
