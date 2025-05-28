using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data.AppDbCon;
using ServerLibrary.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Implementations
{
    public class EventRepository : Repository<EventLibrary>, IEventRepository
    {
        private readonly ApplicationDbContext _context;
        public EventRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<EventLibrary>> GetUpcomingEventsAsync()
        {
            return await _context.Events
            .Where(e => e.EventDate >= DateTime.UtcNow)
            .OrderBy(e => e.EventDate)
            .ToListAsync();
        }


    }
}
