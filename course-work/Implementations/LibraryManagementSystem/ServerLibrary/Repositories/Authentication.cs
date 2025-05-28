using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories
{
    public class AuthenticationManager : IAuthentication
    {
        private readonly UserManager<User> _userManager;
        public IConfiguration _configuration;

        private User _user;

        public AuthenticationManager(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<bool> ValidateUser(LoginDTO userForAuth)
        {
            _user = await _userManager.FindByNameAsync(userForAuth.Email);
            return _user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password);
        }

        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_configuration["SecretKey"]);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        // Fix for CS1061: Replace 'UserName' with 'Username' as per the 'User' class definition.  
        // Fix for CS1503: This error seems unrelated to the provided code. If it persists, additional context is needed to identify the source.  

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
           {
               new Claim(JwtRegisteredClaimNames.Sub, _user.Username), // Fixed 'UserName' to 'Username'  
               new Claim(JwtRegisteredClaimNames.Email, _user.Email)
           };
            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var tokenOptions = new JwtSecurityToken(
                issuer: jwtSettings.GetSection("validIssuer").Value,
                audience: jwtSettings.GetSection("validAudience").Value,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("expires").Value)),
                signingCredentials: signingCredentials
                );
            return tokenOptions;
        }
    }
}
