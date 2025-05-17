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
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<User> ChangeStatus(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUserByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUserWithReservationsAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public string HashPassword(string password)
        {
            throw new NotImplementedException();
        }
    }
}
