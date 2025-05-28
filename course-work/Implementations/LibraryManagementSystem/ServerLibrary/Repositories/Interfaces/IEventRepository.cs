using BaseLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Interfaces
{
    public interface IEventRepository : IRepository<EventLibrary>
    {
        Task<IEnumerable<EventLibrary>> GetUpcomingEventsAsync();
    }
}
