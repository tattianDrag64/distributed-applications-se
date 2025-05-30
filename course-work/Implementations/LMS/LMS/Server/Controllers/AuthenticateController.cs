using BibliotekBoklusen.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BibliotekBoklusen.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IUserService _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _appDbContext;

        public AuthenticateController(IUserService userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, AppDbContext appDbContext)
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

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            var user =  await _signInManager.UserManager.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
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
                    new Claim(ClaimTypes.Name, user.FirstName),
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
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var userExistsInAuthDb = await _signInManager.UserManager.FindByNameAsync(model.Email);
            var userExistsInDbContext = _appDbContext.Users.FirstOrDefault(x => x.Email == model.Email);

            if (userExistsInAuthDb != null || userExistsInDbContext != null)
                return BadRequest("User already exists.");

            if (model != null)
            {
                ApplicationUser user = new()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Email
                };

                User userModel = new User()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    IsActive = true,
                    UserRole = model.UserRole
                };

                var result = await _signInManager.UserManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                    return BadRequest("Something went wrong, try again.");

                if (model.UserRole.Equals(UserRole.Librarian))
                {
                    if (!await _roleManager.RoleExistsAsync(UserRoles.Librarian))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(UserRoles.Librarian));
                    }

                    if (await _roleManager.RoleExistsAsync(UserRoles.Librarian))
                    {
                        await _signInManager.UserManager.AddToRoleAsync(user, UserRoles.Librarian);
                    }
                }
                else
                {
                    if (!await _roleManager.RoleExistsAsync(UserRoles.Member))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(UserRoles.Member));
                    }
                    if (await _roleManager.RoleExistsAsync(UserRoles.Member))
                    {
                        await _signInManager.UserManager.AddToRoleAsync(user, UserRoles.Member);
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

