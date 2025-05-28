using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Interfaces
{
    public interface IBookService 
    {
        Task<ServiceResponse<List<Book>>> SearchBooks(string searchText);
        Task AddBookCopiesAsync(int bookId, int copiesToAdd);
        Task<bool> CheckOutBookAsync(int bookId, int userId);
        Task<ServiceResponse<List<string>>> GetBookSearchSuggestions(string searchText);
        Task<List<Book>> GetAllBooks();
        Task<Book> GetBookById(int id);
        Task<ActionResult<string>> CreateBook(Book bookToAdd);
        Task<string> UpdateBook(int id, Book bookToUpdate);
        Task<string> DeleteBook(int id);
    }
}
