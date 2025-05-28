using AutoMapper;
using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Server.Services.Interfaces;
using ServerLibrary.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Services.Implementations
{
    public class BookService(
           IBookRepository bookRepository,
           IUserRepository userRepository,
           IUnitOfWork unitOfWork,
           IMapper mapper) : IBookService
    {
        private readonly IBookCopyRepository _bookCopyRepository;

        public async Task AddBookCopiesAsync(int bookId, int copiesToAdd)
        {
            var book = await bookRepository.GetByIdAsync(bookId);
            if (book == null)
                throw new Exception("BookNotFound");

            var existingCopies = await _bookCopyRepository.GetAllAsync();

            for (int i = 1; i <= copiesToAdd; i++)
            {
                var newCopy = new BookCopy
                {
                    BookId = book.Id,
                    BookCode = await _bookCopyRepository.GenerateBookCodeFromTitleAsync(book.Title),
                    IsAvailable = true,
                };

                await _bookCopyRepository.AddAsync(newCopy);
            }

            book.TotalCopies += copiesToAdd;
            book.AvailableCopies += copiesToAdd;

            bookRepository.Update(book);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> CheckOutBookAsync(int bookId, int userId)
        {
            var book = await bookRepository.GetByIdAsync(bookId);
            if (book == null || book.AvailableCopies == 0)
                return false;

            var availableCopy = (await _bookCopyRepository
                .FindAsync(c => c.BookId == bookId && c.IsAvailable))
                .FirstOrDefault();

            if (availableCopy == null)
                return false;

            availableCopy.IsAvailable = false;
            book.AvailableCopies--;

            _bookCopyRepository.Update(availableCopy);
            bookRepository.Update(book);
            await unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<ServiceResponse<List<Book>>> SearchBooks(string searchText)
        {
            var books = await bookRepository.FindAsync(b =>
                b.Title.ToLower().Contains(searchText.ToLower()) ||
                b.Author.Name.ToLower().Contains(searchText.ToLower()) ||
                b.Genre.Name.ToLower().Contains(searchText.ToLower()));

            return new ServiceResponse<List<Book>>
            {
                Data = books.ToList(),
                Success = true,
                Message = "Books retrieved successfully."
            };
        }

        public async Task<ServiceResponse<List<string>>> GetBookSearchSuggestions(string searchText)
        {
            var suggestions = await bookRepository.FindAsync(b =>
                b.Title.ToLower().Contains(searchText.ToLower()) ||
                b.Author.Name.ToLower().Contains(searchText.ToLower()) ||
                b.Genre.Name.ToLower().Contains(searchText.ToLower()));

            var suggestionTitles = suggestions.Select(b => b.Title).ToList();

            return new ServiceResponse<List<string>>
            {
                Data = suggestionTitles,
                Success = true,
                Message = "Suggestions retrieved successfully."
            };
        }

        public async Task<List<Book>> GetAllBooks()
        {
            var books = await bookRepository.GetBooksWithAuthorsGenresAsync();
            return books.ToList();
        }

        public async Task<Book> GetBookById(int id)
        {
            var book = await bookRepository.GetByIdAsync(id);
            if (book == null)
                throw new Exception("Book not found");

            return book;
        }

        public async Task<ActionResult<string>> CreateBook(Book bookToAdd)
        {
            var bookExists = await bookRepository.FindAsync(b => b.Title.ToLower() == bookToAdd.Title.ToLower());

            if (bookExists.Any())
                return "Book already exists";

            await bookRepository.AddAsync(bookToAdd);
            await unitOfWork.SaveChangesAsync();

            return "Book has been added";
        }

        public async Task<string> UpdateBook(int id, Book bookToUpdate)
        {
            var existingBook = await bookRepository.GetByIdAsync(id);
            if (existingBook == null)
                return "Book not found";

            existingBook.Title = bookToUpdate.Title;
            existingBook.Description = bookToUpdate.Description;
            existingBook.ISBN = bookToUpdate.ISBN;
            existingBook.PublishedDate = bookToUpdate.PublishedDate;
            existingBook.PageCount = bookToUpdate.PageCount;
            existingBook.Language = bookToUpdate.Language;
            existingBook.CoverImageUrl = bookToUpdate.CoverImageUrl;
            existingBook.AuthorId = bookToUpdate.AuthorId;
            existingBook.GenreId = bookToUpdate.GenreId;

            bookRepository.Update(existingBook);
            await unitOfWork.SaveChangesAsync();

            return "Book has been updated";
        }

        public async Task<string> DeleteBook(int id)
        {
            var book = await bookRepository.GetByIdAsync(id);
            if (book == null)
                return "Book not found";

            bookRepository.Remove(book);
            await unitOfWork.SaveChangesAsync();

            return "Book has been deleted";
        }
    }
}
