using BaseLibrary.Entities;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Interfaces
{
    public class PenaltyRepository : Repository<Penalty>, IPenaltyRepository
    {
        private readonly ApplicationDbContext _context;

        public PenaltyRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<IEnumerable<Penalty>> GetPenaltiesByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
