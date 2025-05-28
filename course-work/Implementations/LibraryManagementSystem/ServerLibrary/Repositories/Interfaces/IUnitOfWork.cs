using BaseLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IBookRepository Book { get; }
        IAuthorRepository Author { get; }
        IGenreRepository Genre { get; }
        IUserRepository User { get; }
        IReviewRepository Review { get; }
        IReservationRepository Reservation { get; }
        IEventRepository Event { get; }
        IPenaltyRepository Penalty { get; }

        public Task<bool> SaveChangesAsync();
    }
}
