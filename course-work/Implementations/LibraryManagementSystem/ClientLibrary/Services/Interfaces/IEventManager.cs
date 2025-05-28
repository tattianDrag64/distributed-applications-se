using BaseLibrary.Entities;

namespace ClientLibrary.Services.Interfaces
{
    public interface IEventManager
    {
        Task<List<EventLibrary>> GetAllEvents();
        Task<EventLibrary> GetEventById(int id);
        Task<string> CreateEvent(EventLibrary eventItem);
        Task DeleteEvent(int id);
        Task UpdateEvent(int id, EventLibrary eventItem);
    }
}