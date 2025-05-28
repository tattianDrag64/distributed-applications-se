using BaseLibrary.Entities;
using ServerLibrary.Data.AppDbCon;
using ServerLibrary.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Implementations
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
