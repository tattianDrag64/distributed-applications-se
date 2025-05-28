using BaseLibrary.Entities;

namespace ClientLibrary.Services.Interfaces
{
    public interface IBookCopyManager
    {
        Task<List<Book>> GetAllReservations();
    }
}