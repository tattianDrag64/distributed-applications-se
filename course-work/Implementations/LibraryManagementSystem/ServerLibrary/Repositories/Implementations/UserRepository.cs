using BaseLibrary.Entities;
using BaseLibrary.Utility;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Implementations
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<User>> GetEmployees()
        {
            return await _context.Users.Where(u => u.role == SD.Role.Librarian || u.role == SD.Role.Admin).ToListAsync();
        }

        public async Task<List<User>> GetMembers()
        {
            return await _context.Users.Where(u => u.role == SD.Role.Member).ToListAsync();
        }

        public async Task<User> GetUserByEmail(string userEmail)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }

        //public async Task<User> ChangeRoleAsync(int id)
        //{
        //    var user = await _dbSet.FindAsync(id);
        //    if (user == null)
        //        throw new Exception("User not found");

        //    user.Role = user.Role == "User" ? "Admin" : "User";

        //    await _context.SaveChangesAsync();
        //    return user;
        //}


        public async Task<User?> GetUserWithReservationsAsync(int userId)
        {
            return await _dbSet
                .Include(u => u.Reservations)
                .ThenInclude(r => r.BookCopy)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
