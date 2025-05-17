using BaseLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserWithReservationsAsync(Guid userId);
        Task<User?> GetUserByCodeAsync(string code);
        Task<User> ChangeStatus(int id);
        string HashPassword(string password);
    }
}
