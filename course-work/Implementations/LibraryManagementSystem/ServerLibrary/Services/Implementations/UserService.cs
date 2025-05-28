using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using BaseLibrary.Utility;
using Microsoft.AspNetCore.Identity;
using ServerLibrary.Data.AppDbCon;
using ServerLibrary.Data.AuthDbCon;
using ServerLibrary.Repositories.Interfaces;
using ServerLibrary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BaseLibrary.Utility.SD;

namespace Server.Services.Implementations
{
    public class UserService : IUserService
    {

        private readonly SignInManager<MyApplicationUser> _signInManager;
        private readonly IUserRepository _userRepository;
        private readonly ApplicationDbContext _context;

        public UserService(SignInManager<MyApplicationUser> signInManager, IUserRepository userRepository, ApplicationDbContext context) : base()
        {
            _signInManager = signInManager;
            _userRepository = userRepository;
            _context = context;
        }
        public async Task<ServiceResponse<string>> ChangePassword(UpdateDTO editPassword)
        {
            var user = _signInManager.UserManager.Users.FirstOrDefault(u => u.Email == editPassword.Email);

            if (user != null)
            {
                var result = await _signInManager.UserManager.ChangePasswordAsync(user, editPassword.OldPassword, editPassword.NewPassword);
                if (result.Succeeded)
                {
                    return new ServiceResponse<string> { Success = true };
                }
            }
            return null;
        }

        public async Task DeleteUserFromAuthDbContext(string email)
        {
            var user = _signInManager.UserManager.Users.FirstOrDefault(x => x.Email == email);
            if (user != null)
            {
                await _signInManager.UserManager.DeleteAsync(user);
            }
        }

        public async Task<ServiceResponse<string>> DeleteUserFromDb(int id)
        {
            var userDb = await _userRepository.GetByIdAsync(id);

            if (userDb == null)
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "User not found"
                };
            }

            _userRepository.Remove(userDb);
            await _context.SaveChangesAsync();

            await DeleteUserFromAuthDbContext(userDb.Email);
            return new ServiceResponse<string> { Success = true };
        }

        public async Task<List<User>> GetAllUser()
        {
            var users = await _userRepository.GetAllAsync();
            if (users != null)
            {
                return (List<User>)users;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<User>> GetEmployees()
        {
            var employees = await _userRepository.GetEmployees();
            return employees;
        }

        public async Task<List<User>> GetMembers()
        {
            var members = await _userRepository.GetMembers();
            return members;
        }

        public async Task<User> GetUser(int id)
        {
            var users = await _userRepository.GetByIdAsync(id);
            if (users != null)
            {
                return users;
            }
            else
            {
                return null;
            }
        }

        public async Task<string> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                return "User not found.";
            }
            return user.Email;
        }

        public async Task<string> Login(LoginDTO model)
        {
            var user = await _signInManager.UserManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return "Invalid email or password.";
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (!result.Succeeded)
            {
                return "Invalid email or password.";
            }

            return "Login successful.";
        }

        public async Task<string> Register(RegisterDTO model)
        {
            var user = new MyApplicationUser
            {
                FullName = model.FullName,
                UserName = model.Username,
                Email = model.Email,
                PasswordHash = _userRepository.HashPassword(model.Password)
            };

            var result = await _signInManager.UserManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return "Registration failed.";
            }

            return "Registration successful.";
        }
        public async Task<List<User>> SearchUsers(string searchText)
        {
            var result = await _userRepository.GetAllAsync();
            return result.Where(u => u.FullName.ToLower().Contains(searchText.ToLower()) ||
                                     u.Username.ToLower().Contains(searchText.ToLower())).ToList();
        }

        public async Task<ServiceResponse<User>> UpdateUser(UserDTO model, int id)
        {
            var dbUser = await _userRepository.GetByIdAsync(id);
            var authUser = _signInManager.UserManager.Users.FirstOrDefault(x => x.Email == dbUser.Email);
            if (dbUser == null)
            {
                return new ServiceResponse<User>
                {
                    Success = false,
                    Message = "User not found."
                };
            }

            authUser.FullName = model.FullName;

            dbUser.Username = model.UserName;
            dbUser.Email = model.Email;
            dbUser.role = model.role;


            if (model.role.Equals(Role.Admin.ToString()))
            {
                await SetUserToAdmin(dbUser, authUser);
            }
            else if (model.role.Equals(Role.Librarian.ToString()))
            {
                await SetUserToLibrarian(dbUser, authUser);
            }
            else
            {
                await SetUserToMember(dbUser, authUser);
            }

            _userRepository.Update(dbUser);

            return new ServiceResponse<User>
            {
                Success = true,
                Data = dbUser
            };
        }


        private async Task SetUserToAdmin(User dbUser, MyApplicationUser authUser)
        {

            await _signInManager.UserManager.RemoveFromRoleAsync(authUser, Role.Librarian.ToString());
            await _signInManager.UserManager.AddToRoleAsync(authUser, Role.Admin.ToString());
        }

        private async Task SetUserToLibrarian(User dbUser, MyApplicationUser authUser)
        {

            await _signInManager.UserManager.RemoveFromRoleAsync(authUser, Role.Admin.ToString());
            await _signInManager.UserManager.AddToRoleAsync(authUser, Role.Librarian.ToString());
        }

        private async Task SetUserToMember(User dbUser, MyApplicationUser authUser)
        {

            await _signInManager.UserManager.RemoveFromRoleAsync(authUser, Role.Admin.ToString());
            await _signInManager.UserManager.RemoveFromRoleAsync(authUser, Role.Librarian.ToString());
            await _signInManager.UserManager.AddToRoleAsync(authUser, Role.Member.ToString());
        }
    }
}
