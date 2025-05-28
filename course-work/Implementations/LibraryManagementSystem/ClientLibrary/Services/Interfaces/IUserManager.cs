using BaseLibrary.DTOs;
using BaseLibrary.Entities;

namespace ClientLibrary.Services.Interfaces
{
    public interface IUserManager
    {
        Task<List<User>> GetAllUser();
        Task<User> GetUser(int id);
        Task UpdateUserinformation(UserDTO model, int id);
        Task<string> ChangePassword(UpdateDTO editPassword);
        Task DeleteUser(int id);
        Task<string> Login(LoginDTO model);
        Task<string> Register(RegisterDTO model);
        Task RegisterAdmin(RegisterDTO model);
        Task<User> GetUserByEmail(string userEmail);
        Task<List<User>> GetUsersBySearch(string searchText);
        Task<List<User>> GetEmployees();
        Task<List<User>> GetMembers();
        //public Task<User?> GetUserAsync();
    }
}
