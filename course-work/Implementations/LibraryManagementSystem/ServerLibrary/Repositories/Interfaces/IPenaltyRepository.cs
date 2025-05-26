using BaseLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Interfaces
{
    public interface IPenaltyRepository : IRepository<Penalty>
    {
        Task<IEnumerable<Penalty>> GetPenaltiesByUserIdAsync(int userId);
    }
}
