using BaseLibrary.Entities;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Implementations
{
    public class BookCopyRepository : Repository<BookCopy>, IBookCopyRepository
    {
        private readonly ApplicationDbContext _context;
        public BookCopyRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<string> GenerateBookCodeFromTitleAsync(string title)
        {
            var shortCode = new string(title
            .Where(char.IsLetter)
            .Take(3)
            .ToArray())
            .ToUpper();

            var randomNumber = new Random().Next(100, 999);
            return await Task.FromResult($"{shortCode}{randomNumber}");
        }

    }
}
