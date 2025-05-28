using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data.AppDbCon;
using ServerLibrary.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BaseLibrary.Utility.SD;

namespace ServerLibrary.Repositories.Implementations
{
    public class ReservationRepository : Repository<Reservation>, IReservationRepository
    {
        private readonly ApplicationDbContext _context;

        public ReservationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reservation>> GetReservationHistoryOfUser(int userId)
        {
            return await _context.Reservations
                .Where(r => r.UserId == userId)
                .Include(r => r.BookCopy)
                .ToListAsync();
        }

        public async Task<Reservation> RenewReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation == null || reservation.Status != ReservationStatus.Cancelled)
                throw new InvalidOperationException("Reservation not found or cannot be renewed.");

            reservation.DueDate = reservation.DueDate.AddDays(7);
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task<IEnumerable<Reservation>> GetAllReturnedReservations(int userId)
        {
            return await _context.Reservations
                .Where(r => r.UserId == userId && r.Status == ReservationStatus.Returned)
                .Include(r => r.BookCopy)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetAllLostedReservations()
        {
            return await _context.Reservations
                .Where(r => r.Status == ReservationStatus.Lost)
                .Include(r => r.BookCopy)
                .Include(r => r.User)
                .ToListAsync();
        }
    }
}
