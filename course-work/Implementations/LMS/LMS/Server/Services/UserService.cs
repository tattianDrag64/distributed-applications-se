using BibliotekBoklusen.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace BibliotekBoklusen.Server.Services
{
    public class UserService : IUserService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;

        public UserService(SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<List<User>> GetAllUser()
        {
            var users = await _context.Users.ToListAsync();
            if (users != null)
            {
                return users;
            }
            else
            {
                return null;
            }
        }
        public async Task<User> GetUser(int id)
        {
            return await _context.Users
           .Where(x => x.Id == id)
           .FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByEmail(string userEmail)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
        }      
                
        public async Task<ServiceResponse<User>> UpdateUser(UpdatedUserDto user, int id)
        {
            var dbUser = _context.Users.Where(x => x.Id == id).FirstOrDefault();
            var authUser = _signInManager.UserManager.Users.FirstOrDefault(x => x.Email == dbUser.Email);

            if (dbUser == null || authUser == null)
            {
                return new ServiceResponse<User>
                {
                    Success = false,
                    Message = "User not found."
                };
            }
            authUser.FirstName = user.FirstName;
            authUser.LastName = user.LastName;

            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;
            dbUser.IsActive = user.IsActive;
            dbUser.UserRole = user.UserRole;

            if (user.UserRole.Equals(UserRole.Admin))
            {
                await SetUserToAdmin(dbUser, authUser);
            }
            else if (user.UserRole.Equals(UserRole.Librarian))
            {
                await SetUserToLibrarian(dbUser, authUser);
            }
            else
            {
                await SetUserToMember(dbUser, authUser);
            }

            _context.Update(dbUser);
            await _context.SaveChangesAsync();
            await _signInManager.UserManager.UpdateAsync(authUser);
            return new ServiceResponse<User> { Data = dbUser };
        }
        public async Task<ServiceResponse<string>> DeleteUserFromDb(int id)
        {
            var userDb = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (userDb == null)
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "User not found"
                };
            }

            _context.Remove(userDb);
            await _context.SaveChangesAsync();

            await DeleteUserFromAuthDbContext(userDb.Email);
            return new ServiceResponse<string> { Success = true };
        }

        public async Task DeleteUserFromAuthDbContext(string email)
        {
            var user = _signInManager.UserManager.Users.FirstOrDefault(x => x.Email == email);
            if (user != null)
            {
                await _signInManager.UserManager.DeleteAsync(user);
            }
        }

        public async Task<ServiceResponse<string>> ChangePassword(PasswordDto editPassword)
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

        public async Task<List<User>> SearchUsers(string searchText)
        {
            var result = await _context.Users.Where(u => u.FirstName.ToLower().Contains(searchText.ToLower()) || u.LastName.ToLower().Contains(searchText.ToLower())).ToListAsync();
            return result;
        }

        public async Task<List<User>> GetEmployees()
        {
            return await _context.Users.Where(u => u.UserRole.Equals(UserRole.Librarian) || u.UserRole.Equals(UserRole.Admin)).ToListAsync();
        }

        public async Task<List<User>> GetMembers()
        {
            return await _context.Users.Where(u => u.UserRole.Equals(UserRole.Member)).ToListAsync();
        }

        private async Task SetUserToAdmin(User dbUser, ApplicationUser authUser)
        {

            await _signInManager.UserManager.RemoveFromRoleAsync(authUser, UserRoles.Librarian);
            await _signInManager.UserManager.AddToRoleAsync(authUser, UserRoles.Admin);
        }

        private async Task SetUserToLibrarian(User dbUser, ApplicationUser authUser)
        {

            await _signInManager.UserManager.RemoveFromRoleAsync(authUser, UserRoles.Admin);
            await _signInManager.UserManager.AddToRoleAsync(authUser, UserRoles.Librarian);
        }

        private async Task SetUserToMember(User dbUser, ApplicationUser authUser)
        {
    
            await _signInManager.UserManager.RemoveFromRoleAsync(authUser, UserRoles.Admin);
            await _signInManager.UserManager.RemoveFromRoleAsync(authUser, UserRoles.Librarian);
            await _signInManager.UserManager.AddToRoleAsync(authUser, UserRoles.Member);
        }
    }
}

