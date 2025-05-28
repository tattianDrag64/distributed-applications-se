using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using Blazored.LocalStorage;
using ClientLibrary.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;
using System.Security.Claims;

namespace ClientLibrary.Services.Implementations
{
    public class UserManager : IUserManager
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorageService;
        //private readonly IHttpContextAccessor _httpContextAccessor; // Add this field

        public UserManager(HttpClient http, ILocalStorageService localStorageService, IHttpContextAccessor httpContextAccessor)
        {
            _http = http;
            _localStorageService = localStorageService;
            //_httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> ChangePassword(UpdateDTO editPassword)
        {
            var result = await _http.PutAsJsonAsync($"api/user/ChangePassword", editPassword);
            if (result.IsSuccessStatusCode)
            {
                return "Your password has been changed";
            }
            return "Please try again";
        }

        public async Task DeleteUser(int id)
        {
            await _http.DeleteAsync($"api/user/{id}");
        }

        public async Task<List<User>> GetAllUser()
        {
            var result = await _http.GetFromJsonAsync<List<User>>($"api/user/getallusers");

            if (result != null)
            {
                return result;
            }
            return null;
        }

        public async Task<User> GetUser(int id)
        {
            var result = await _http.GetFromJsonAsync<User>($"api/user/{id}");

            if (result != null)
            {
                return result;
            }
            return null;
        }

        public async Task<User> GetUserByEmail(string userEmail)
        {
            var result = await _http.GetFromJsonAsync<User>($"api/user/GetUserByEmail?userEmail={userEmail}");
            return result;
        }

        public async Task<string> Login(LoginDTO model)
        {
            var result = await _http.PostAsJsonAsync($"api/authenticate/login", model);

            if (result.IsSuccessStatusCode)
            {
                var token = await result.Content.ReadAsStringAsync();
                if (token != null)
                {
                    await _localStorageService.SetItemAsync("authToken", token);
                    await _localStorageService.SetItemAsync("email", model.Email);
                    return "You have been signed in";
                }
            }
            return null;
        }

        public async Task<string> Register(RegisterDTO model)
        {
            var result = await _http.PostAsJsonAsync("api/authenticate/register", model);

            if (result.IsSuccessStatusCode)
            {
                return "User has been registered";
            }
            return await result.Content.ReadAsStringAsync();
        }

        public async Task RegisterAdmin(RegisterDTO model)
        {
            await _http.PostAsJsonAsync("api/authenticate/register-admin", model);
        }

        public async Task UpdateUserinformation(UserDTO model, int id)
        {
            await _http.PutAsJsonAsync($"api/user/{id}", model);
        }

        public async Task<List<User>> GetUsersBySearch(string searchText)
        {
            return await _http.GetFromJsonAsync<List<User>>($"api/user/usersbysearch?searchText={searchText}");
        }

        public async Task<List<User>> GetEmployees()
        {
            return await _http.GetFromJsonAsync<List<User>>("api/authenticate");
        }

        public async Task<List<User>> GetMembers()
        {
            return await _http.GetFromJsonAsync<List<User>>("api/authenticate/getmembers");
        }

        //public Task<User?> GetUserAsync()
        //{
        //    var httpContext = _httpContextAccessor.HttpContext;
        //    if (httpContext?.User?.Identity?.IsAuthenticated ?? false)
        //    {
        //        var name = httpContext.User.FindFirst(ClaimTypes.Name)?.Value;
        //        return Task.FromResult(new User { UserName = name });
        //    }

        //    return Task.FromResult<User?>(null);
        //}
    }

}
