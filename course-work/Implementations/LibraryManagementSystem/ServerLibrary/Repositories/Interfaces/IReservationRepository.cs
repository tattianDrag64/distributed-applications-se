using BaseLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Interfaces
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        Task<IEnumerable<Reservation>> GetActiveReservationsByUser(int userId);
        Task<IEnumerable<Reservation>> GetReservationHistoryOfUser(int UserId);
        Task<Reservation> RenewReservation(int Id);
        Task<IEnumerable<Reservation>> GetAllReturnedReservations(int userId);
        Task<IEnumerable<Reservation>> GetAllLostedReservations();
        Task<Reservation> ChangeStatus(int id);
    }
}
