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
        Task<User?> GetUserWithReservationsAsync(int userId);
        //Task<User> ChangeRoleAsync(int id);
        string HashPassword(string password);
        Task<List<User>> GetEmployees();

        Task<List<User>> GetMembers();

        Task<User> GetUserByEmail(string userEmail);
        Task<User> GetUserByUsername(string username);

    }
}
