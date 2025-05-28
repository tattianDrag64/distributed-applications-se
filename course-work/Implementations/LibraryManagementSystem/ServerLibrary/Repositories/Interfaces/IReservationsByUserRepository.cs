using BaseLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Interfaces
{
    public interface IReservationsByUserRepository
    {
        Task<IEnumerable<Reservation>> GetActiveReservationsByUser(int userId);
    }
}
