namespace BibliotekBoklusen.Client.Services
{
    public interface IUserManager
    {
        Task<List<User>> GetAllUser();
        Task<User> GetUser(int id);
        Task UpdateUserinformation(UpdatedUserDto model, int id);
        Task<string> ChangePassword(PasswordDto editPassword);
        Task DeleteUser(int id);
        Task<string> Login(LoginDto model);
        Task<string> Register(RegisterDto model);
        Task RegisterAdmin(RegisterDto model);
        Task<User> GetUserByEmail(string userEmail);
        Task<List<User>> GetUsersBySearch(string searchText);
        Task<List<User>> GetEmployees();
        Task<List<User>> GetMembers();
    }
}
