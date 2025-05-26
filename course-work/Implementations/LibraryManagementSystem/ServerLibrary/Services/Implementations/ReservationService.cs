using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Utility;
using ServerLibrary.Repositories.Interfaces;
using ServerLibrary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Services.Implementations
{
    public class ReservationService : ServicesBase<Reservation, ReservationDTO>, IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;

        public ReservationService(IBookRepository bookRepository, IUserRepository userRepository, IReservationRepository reservationRepository)
            : base(reservationRepository)
        {
            _bookRepository = bookRepository;
            _userRepository = userRepository;
            _reservationRepository = reservationRepository;
        }

        public async Task<Reservation> Create(int bookId, int userId)
        {
            // Fetch the book copy that is not loaned and matches the bookId  
            var availableBookCopy = await _bookRepository.GetAllAsync();
            var bookCopy = availableBookCopy.FirstOrDefault(bc => bc.Id == bookId && bc.AvailableCopies > 0);

            if (bookCopy == null)
            {
                return null; // No available book copy found  
            }

            // Update the book's available copies  
            bookCopy.AvailableCopies--;
            _bookRepository.Update(bookCopy);

            // Create a new reservation  
            var reservation = new Reservation
            {
                BookCopyId = bookId,
                UserId = userId,
                ReservationDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(14), 
                Status = SD.ReservationStatus.Active,
                IsReturned = false
            };

            await _reservationRepository.AddAsync(reservation);
            return reservation;
        }

        public async Task<List<Book>> GetTopBooks()
        {
            var reservations = await _reservationRepository.GetAllAsync();
            var topBooksIDs = reservations
                .GroupBy(r => r.BookCopy.Id) 
                .OrderByDescending(g => g.Count())
                .Take(5)
                .Select(g => g.Key)
                .ToList();

            var topBooks = await _bookRepository.GetAllAsync();
            return topBooks.Where(b => topBooksIDs.Contains(b.Id)).ToList();
        }

        public async Task<bool> ReturnReservation(int bookCopyId)
        {
            var reservations = await _reservationRepository.GetAllAsync();
            var reservation = reservations.FirstOrDefault(r => r.BookCopyId == bookCopyId && !r.IsReturned);

            if (reservation == null) return false;

            reservation.IsReturned = true;
            reservation.Status = SD.ReservationStatus.Returned;   

            var book = await _bookRepository.GetByIdAsync(reservation.BookCopyId);
            if (book != null)
            {
                book.AvailableCopies++;
                _bookRepository.Update(book);  
            }

            _reservationRepository.Update(reservation);
            return true;
        }
    }
}
