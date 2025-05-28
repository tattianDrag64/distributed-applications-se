using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Interfaces
{
    public interface IEventService
    {
        Task<List<EventLibrary>> GetAllEvents();
        Task<EventLibrary> GetEventById(int id);
        Task<EventLibrary> CreateEvent(EventLibrary eventToAdd);
        Task<EventLibrary> UpdateEvent(int id, EventLibrary eventToUpdate);
        Task<string> DeleteEvent(int id);
    }
}
