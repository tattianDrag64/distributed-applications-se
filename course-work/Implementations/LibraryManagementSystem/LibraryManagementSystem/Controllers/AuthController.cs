using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using Microsoft.EntityFrameworkCore;
using static BaseLibrary.Utility.SD;
using BaseLibrary.Utility;
using ServerLibrary.Data.AppDbCon;
using ServerLibrary.Services.Interfaces;


namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IUserService _userManager;
        private readonly SignInManager<MyApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _appDbContext;

        public AuthenticateController(IUserService userManager, 
            SignInManager<MyApplicationUser> signInManager, 
            RoleManager<IdentityRole> roleManager, 
            IConfiguration configuration, 
            ApplicationDbContext appDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var result = await _userManager.GetEmployees();
            return Ok(result);
        }

        [HttpGet]
        [Route("getmembers")]
        public async Task<IActionResult> GetMembers()
        {
            var result = await _userManager.GetMembers();
            return Ok(result);
        }

        // Fix for CS1503: Argument 2: cannot convert from 'string' to 'System.Security.Claims.ClaimsIdentity?'

        // The issue is that `ClaimTypes` does not have a `FullName` property. Instead, you can use a custom claim type or an existing one like `ClaimTypes.Name`.

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO request)
        {
            var user = await _signInManager.UserManager.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user != null)
            {
                var result = await _signInManager.UserManager.CheckPasswordAsync(user, request.Password);

                if (!result)
                {
                    return Unauthorized("Wrong password.");
                }

                var userRoles = await _signInManager.UserManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
               {
                   new Claim(ClaimTypes.NameIdentifier, user.Email),
                   new Claim("FullName", user.FullName), 
                   new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
               };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return NotFound("User not found.");
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            var userExistsInAuthDb = await _signInManager.UserManager.FindByNameAsync(model.Email);
            var userExistsInDbContext = _appDbContext.Users.FirstOrDefault(x => x.Email == model.Email);

            if (userExistsInAuthDb != null || userExistsInDbContext != null)
                return BadRequest("User already exists.");

            if (model != null)
            {
                MyApplicationUser user = new()
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Email
                };

                User userModel = new User()
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    IsActive = true,
                    role = model.role
                };

                var result = await _signInManager.UserManager.CreateAsync(user, model.Password);


                if (!result.Succeeded)
                    return BadRequest("Something went wrong, please try again.");

                if (model.role == Role.Librarian)
                {
                    if (!await _roleManager.RoleExistsAsync(Role.Librarian.ToString()))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(Role.Librarian.ToString()));
                    }

                    if (await _roleManager.RoleExistsAsync(Role.Librarian.ToString()))
                    {
                        await _signInManager.UserManager.AddToRoleAsync(user, Role.Librarian.ToString());
                    }
                }
                else
                {
                    if (!await _roleManager.RoleExistsAsync(Role.Member.ToString()))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(Role.Member.ToString()));
                    }
                    if (await _roleManager.RoleExistsAsync(Role.Member.ToString()))
                    {
                        await _signInManager.UserManager.AddToRoleAsync(user, Role.Member.ToString());
                    }
                }

                _appDbContext.Users.Add(userModel);
                var dbContextResult = await _appDbContext.SaveChangesAsync();

                if (dbContextResult != 0)
                    return Ok("User created");
            }
            return BadRequest();
        }
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
