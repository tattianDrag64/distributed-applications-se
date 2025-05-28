using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data.AppDbCon;
using ServerLibrary.Repositories.Interfaces;


namespace ServerLibrary.Repositories.Implementations
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        private readonly ApplicationDbContext _context;
        public AuthorRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Author?> GetAuthorByBookId(int bookId)
        {
            return await _context.Books
                .Where(b => b.Id == bookId)
                .Select(b => b.Author)
                .FirstOrDefaultAsync();
        }

        public async Task<Author?> GetAuthorWithBooksAsync(int authorId)
        {
            return await _context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == authorId);
        }
    }
}
