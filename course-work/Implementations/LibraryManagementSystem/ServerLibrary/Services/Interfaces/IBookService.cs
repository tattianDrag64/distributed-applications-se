using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Services.Interfaces
{
    public interface IBookService : IServiceBase<BookDTO>
    {
        Task<ServiceResponse<List<Book>>> SearchBooks(string searchText);
        Task AddBookCopiesAsync(int bookId, int copiesToAdd);
        Task<bool> CheckOutBookAsync(int bookId, int userId);
        Task<ServiceResponse<List<string>>> GetBookSearchSuggestions(string searchText);
    }
}
