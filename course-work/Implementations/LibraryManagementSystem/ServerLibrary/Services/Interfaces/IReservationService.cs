using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Services.Interfaces
{
    public interface IReservationService : IServiceBase<ReservationDTO>
    {
        //        Task<List<Loan>> GetLoansByUserId(int userId);

        //        Task<IEnumerable<BorrowDetailsDTO>> GetBorrowHistoryOfAUser(int userId1);
        //        Task<List<BorrowDetailsDTO>> GetCurrentBorrowsOfAUser(int userId2);
        //        Task<BorrowDetailsDTO> GetBorrow(int id);
        //        Task<BorrowDetailsDTO> AddBorrow(BorrowToAddDTO borrowToAddDTO);
        //        Task<BorrowDetailsDTO> RemoveBorrow(int id);
        //        Task<BorrowDetailsDTO> ReturnBorrow(int id, BorrowDetailsDTO borrowDetailsDTO);
        //        Task<List<Product>> GetTopProducts();
        //        Task<IEnumerable<ReservationDto>> GetByUserIdAsync(int userId);
        //        Task<bool> UpdateStatusAsync(int reservationId, string status);

        ////        Продлить бронирование

        ////Сменить статус(возврат, потеря)

        ////Получить активные брони, историю, потерянные

        ////Проверка правил бронирования(время, статус и т.д.)
        ///
        Task<bool> ReturnReservation(int bookCopyId);
        Task<List<Book>> GetTopBooks();
        Task<Reservation> Create(int bookId, int UserId);
    }
}
