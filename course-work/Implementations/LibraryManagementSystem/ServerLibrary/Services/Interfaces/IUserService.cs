using Azure;
using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Services.Interfaces
{
    public interface IUserService 
    {
        //Task<Response<RegisterDTO>> RegisterUserAsync(RegisterDTO register, CancellationToken cancellationToken = default);
        //Task<(string accessToken, string refreshToken)> LoginUserAsync(LoginDTO login, CancellationToken cancellationToken = default);
        //Task UpdateUserInformationAsync(UserDTO model, int id);
        //Task<string> ChangePasswordAsync(UpdateDTO editPassword);
        //Task ConfirmEmailAsync(Guid userId, CancellationToken cancellationToken = default);
        //Task<User> GetUserByEmailAsync(string userEmail);
        //Task<List<User>> GetUsersBySearchAsync(string searchText);
        //Task<List<User>> GetMembersAsync();
        //Task<string> GetUserIdAsync(LoginDTO user);
        //Task<(string accessToken, string refreshToken)> RefreshTokenAsync(string token, CancellationToken cancellationToken = default);
        //Task<UserDTO> DeactivateUserAsync(int id);
        //Task<UserDTO> ActivateUserAsync(int id);
        //string GenerateJwtToken(string Role, int Id);
        //Task<User> GetUserByEmail(string userEmail);
        //Task<User> GetUserByUsername(string username);
        Task<ServiceResponse<User>> UpdateUser(UserDTO model, int id);
        Task<ServiceResponse<string>> ChangePassword(UpdateDTO editPassword);
        Task<List<User>> GetEmployees();
        Task<List<User>> GetMembers();
        Task<List<User>> SearchUsers(string searchText);
        Task<string> Login(LoginDTO model);
        Task<string> Register(RegisterDTO model);
        Task<string> GetUserByEmail(string email);
        Task<List<User>> GetAllUser();
        Task<User> GetUser(int id);
        Task DeleteUserFromAuthDbContext(string email);
        Task<ServiceResponse<string>> DeleteUserFromDb(int id);
        //Task RegisterAdmin(RegisterDTO model);

    }
}

