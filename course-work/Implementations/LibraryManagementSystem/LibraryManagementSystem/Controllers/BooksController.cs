//using LibraryManagementSystem.API.Data;
//using LibraryManagementSystem.API.Entities;
//using log4net;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace LibraryManagementSystem.API.Controllers
//{

//    //localhost:xxxx/api/books
//    //[Authorize]
//    [Route("api/[controller]")]
//    [ApiController]
//    public class BooksController : ControllerBase
//    {
//        //private readonly IBookRepository<Book> _bookRepository;
//        private ApplicationDbContext dbContext;
//        private static readonly ILog log = LogManager.GetLogger(typeof(BookController));

//        //public BookController(IBookRepository<Book> bookRepository)
//        //{
//        //    _bookRepository = bookRepository;
//        //}


//        public BooksController(ApplicationDbContext dbContext) {
//            this.dbContext = dbContext;
//        }
//        [HttpGet]
//        public IActionResult GetAllBooks()
//        {
//            log.Info("Fetching all books...");
//            var books = await _bookRepository.GetAllAsync();
//            if (books == null)
//            {
//                log.Warn("No books found");
//                return NotFound();
//            }
//            log.Info("Books retrieved successfully");
//            return Ok(books);
//            //var allBooks = dbContext.Books.ToList();
//            //return Ok(allBooks);
//        }

//        [HttpPost]
//        public IActionResult AddBook([FromBody] Book book)
//        {
//            if (book == null)
//            {
//                return BadRequest("Book cannot be null");
//            }
//            dbContext.Books.Add(book);
//            dbContext.SaveChanges();
//            return CreatedAtAction(nameof(GetAllBooks), new { id = book.Id }, book);
//        }
//    }
//}


//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using BaseLibrary.Entities;
//using BaseLibrary.DTOs;
//using ServerLibrary.Data;

//[ApiController]
//[Route("api/[controller]")]
//public class BookController : ControllerBase
//{
//    private readonly ApplicationDbContext _context;
//    private readonly IMapper _mapper;

//    public BookController(ApplicationDbContext context, IMapper mapper)
//    {
//        _context = context;
//        _mapper = mapper;
//    }

//    // GET: api/Book
//    [HttpGet]
//    public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks()
//    {
//        var books = await _context.Books
//            .Include(b => b.Author)
//        .Include(b => b.Genre)
//            .ToListAsync();

//        return Ok(_mapper.Map<IEnumerable<BookDTO>>(books));
//    }

//    // GET: api/Book/5
//    [HttpGet("{id}")]
//    public async Task<ActionResult<BookDTO>> GetBook(int id)
//    {
//        var book = await _context.Books
//            .Include(b => b.Author)
//            .Include(b => b.Genre)
//            .FirstOrDefaultAsync(b => b.Id == id);

//        if (book == null)
//        {
//            return NotFound();
//        }

//        return Ok(_mapper.Map<BookDTO>(book));
//    }

//    // POST: api/Book
//    [HttpPost]
//    public async Task<ActionResult<BookDTO>> CreateBook(BookDTO bookDto)
//    {
//        var book = _mapper.Map<Book>(bookDto);
//        _context.Books.Add(book);
//        await _context.SaveChangesAsync();

//        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, _mapper.Map<BookDTO>(book));
//    }

//    // PUT: api/Book/5
//    [HttpPut("{id}")]
//    public async Task<IActionResult> UpdateBook(int id, BookDTO bookDto)
//    {
//        if (id != bookDto.Id)
//        {
//            return BadRequest();
//        }

//        var book = await _context.Books.FindAsync(id);
//        if (book == null)
//        {
//            return NotFound();
//        }

//        _mapper.Map(bookDto, book);

//        try
//        {
//            await _context.SaveChangesAsync();
//        }
//        catch (DbUpdateConcurrencyException)
//        {
//            if (!_context.Books.Any(b => b.Id == id))
//            {
//                return NotFound();
//            }

//            throw;
//        }

//        return NoContent();
//    }

//    // DELETE: api/Book/5
//    [HttpDelete("{id}")]
//    public async Task<IActionResult> DeleteBook(int id)
//    {
//        var book = await _context.Books.FindAsync(id);
//        if (book == null)
//        {
//            return NotFound();
//        }

//        _context.Books.Remove(book);
//        await _context.SaveChangesAsync();

//        return NoContent();
//    }
//}


using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Services.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetAllBooks()
        {
            var products = await _bookService.GetAllAsync();

            if (products == null)
            {
                return BadRequest("No books found");
            }
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            var product = await _bookService.GetByIdAsync(id);

            if (product == null)
            {
                return BadRequest("There is no book with that ID");
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> CreateBook([FromBody] BookDTO bookToAdd)
        {
            var response = await _bookService.CreateAsync(bookToAdd);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook([FromRoute] int id, [FromBody] BookDTO productToUpdate)
        {
            var response = await _bookService.UpdateAsync(id, productToUpdate);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteBook(int id)
        {
            var response = await _bookService.DeleteAsync(id);
            return Ok(response);
        }

        [HttpGet("search/{searchText}")]
        public async Task<ActionResult<ServiceResponse<List<Book>>>> SearchBooks(string searchText)
        {
            var result = await _bookService.SearchBooks(searchText);
            return Ok(result);
        }

        [HttpGet("searchsuggestions/{searchText}")]
        public async Task<ActionResult<ServiceResponse<List<Book>>>> GetBookSearchSuggestions(string searchText)
        {
            var result = await _bookService.GetBookSearchSuggestions(searchText);
            return Ok(result);
        }
    }
}
