using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.AspNetCore.Identity;
using ServerLibrary.Repositories.Interfaces;
using ServerLibrary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BaseLibrary.Utility.SD;

namespace ServerLibrary.Services.Implementations
{
    public class UserService : ServicesBase<User, UserDTO>, IUserService
    {

        private readonly SignInManager<User> _signInManager;
        private readonly IUserRepository _userRepository;

        public UserService(SignInManager<User> signInManager, IUserRepository userRepository) : base(userRepository)
        {
            _signInManager = signInManager;
            _userRepository = userRepository;
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
            var user = new User
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
                                     u.UserName.ToLower().Contains(searchText.ToLower())).ToList();
        }

        public async Task<ServiceResponse<User>> UpdateUser(UserDTO model, int id)
        {
            var dbUser = await _userRepository.GetByIdAsync(id);
            if (dbUser == null)
            {
                return new ServiceResponse<User>
                {
                    Success = false,
                    Message = "User not found."
                };
            }

            dbUser.FullName = model.FullNmae;
            dbUser.UserName = model.UserName;
            dbUser.Email = model.Email;
            dbUser.Address = model.Address;
            dbUser.PhoneNumber = model.PhoneNumber;
            dbUser.role = model.role;


            //if (user.UserRole.Equals(Role.Admin))
            //{
            //    await SetUserToAdmin(dbUser);
            //}
            //else if (user.UserRole.Equals(Role.Librarian))
            //{
            //    await SetUserToLibrarian(dbUser,);
            //}

            _userRepository.Update(dbUser);

            return new ServiceResponse<User>
            {
                Success = true,
                Data = dbUser
            };
        }

        //private async Task SetUserToAdmin(User user)
        //{
        //    await _signInManager.UserManager.RemoveFromRoleAsync(user, Role.Librarian.ToString());
        //    await _signInManager.UserManager.AddToRoleAsync(user, Role.Admin.ToString());
        //}

        //private async Task SetUserToLibrarian(User user)
        //{
        //    await _signInManager.UserManager.RemoveFromRoleAsync(user, Role.Admin.ToString());
        //    await _signInManager.UserManager.AddToRoleAsync(user, Role.Librarian.ToString());
        //}
    }
}
