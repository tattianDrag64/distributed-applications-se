using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data.AppDbCon;
using ServerLibrary.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Implementations
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Review>> GetReviewsByBookIdAsync(int bookId)
        {
            return await _context.Reviews
                .Where(r => r.BookId == bookId && !r.IsDeleted)
                .Include(r => r.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewsByUserIdAsync(int userId)
        {
            return await _context.Reviews
                 .Where(r => r.UserId == userId && !r.IsDeleted)
                 .Include(r => r.Book)
                 .ToListAsync();
        }
    }
}
