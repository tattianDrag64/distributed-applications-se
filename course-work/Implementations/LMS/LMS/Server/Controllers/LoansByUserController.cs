using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotekBoklusen.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansByUserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LoansByUserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<ActionResult<List<Loan>>> GetLoansByCurrentUser(int userId)
        {
            var currentUser = _context.Loans.Include(l=>l.ProductCopy).Where(p => p.UserId == userId).ToList();
            return currentUser;
        }
    }
}
