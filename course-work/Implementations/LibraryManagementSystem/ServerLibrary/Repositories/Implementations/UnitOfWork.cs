using BaseLibrary.Entities;
using ServerLibrary.Data.AppDbCon;
using ServerLibrary.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IBookRepository Book { get; private set; }
        public IAuthorRepository Author { get; private set; }
        public IGenreRepository Genre { get; private set; }
        public IUserRepository User { get; private set; }
        public IReviewRepository Review { get; private set; }
        public IReservationRepository Reservation { get; private set; }
        public IEventRepository Event { get; private set; }
        public IPenaltyRepository Penalty { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Book = new BookRepository(_context);
            Author = new AuthorRepository(_context);
            Genre = new GenreRepository(_context);
            User = new UserRepository(_context);
            Review = new ReviewRepository(_context);
            Reservation = new ReservationRepository(_context);
            Event = new EventRepository(_context);
            Penalty = new PenaltyRepository(_context);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
