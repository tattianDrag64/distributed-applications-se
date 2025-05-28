
using BaseLibrary.Entities;

namespace ClientLibrary.Services.Interfaces
{
    public interface IReservationManager
    {
        Task<string> AddReservation(int productId, int userId);
        Task<List<Reservation>> GetAllReservationsAsync();
        Task<Reservation> GetReservationByIdAsync(int id);
        Task<bool> ReturnReservationAsync(int id);
        Task UpdateReservation(Reservation reservationToUpdate);
        Task<List<Reservation>> GetReservationsByUserId(int userId);

        Task<List<Book>> GetTopBooks();
    }
}