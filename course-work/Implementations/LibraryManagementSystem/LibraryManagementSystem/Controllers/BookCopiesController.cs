using BaseLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Implementations;
using System;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookCopiesController : ControllerBase
    {
        private readonly BookCopyRepository _bookCopiesRepository;

        public BookCopiesController(BookCopyRepository bookCopiesRepository)
        {
            _bookCopiesRepository = bookCopiesRepository;
        }

        [HttpGet("available")]
        public async Task<ActionResult<List<Book>>> GetAvailableBooks()
        {
            var availableBookCopies = await _bookCopiesRepository.FindAsync(bc => bc.IsAvailable);
            List<Book> availableBooks = new();

            foreach (var bookCopy in availableBookCopies)
            {
                if (bookCopy.Book != null)
                {
                    availableBooks.Add(bookCopy.Book);
                }
            }

            return Ok(availableBooks);
        }
    }
}
