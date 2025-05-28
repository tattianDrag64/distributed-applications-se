using BaseLibrary.Entities;
using ServerLibrary.Data.AppDbCon;
using ServerLibrary.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BaseLibrary.Utility.SD;

namespace ServerLibrary.Repositories.Implementations
{
    public class ReservationsByUserRepository : IReservationsByUserRepository
    {
        private readonly ApplicationDbContext _context; // Added field for DbContext  

        public ReservationsByUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reservation>> GetActiveReservationsByUser(int userId)
        {
            return await _context.Reservations
                .Where(r => r.UserId == userId && r.Status == ReservationStatus.Active)
                .Include(r => r.BookCopy)
                .ToListAsync();
        }
    }
}
