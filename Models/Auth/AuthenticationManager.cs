using FoodStoreAPI.Models.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FoodStoreAPI.Models.Auth
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration configuration;
        private IdentityUser _user;

        public AuthenticationManager(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }
        /*
         Check if the logged in user is valid
         */
        public async Task<bool> ValidateUser(LoginData loginData)
        {
            _user =  await userManager.FindByNameAsync(loginData.Username);
            return _user !=null && await userManager.CheckPasswordAsync(_user,loginData.Password);
        }
        public async Task<string> CreateToken(LoginData loginData)
        {
            var jwtSettings = configuration.GetSection("JwtSecret");
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginData.Username)
            };

            var roles = await userManager.GetRolesAsync(_user);
            foreach (var role in roles) {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            

            SecurityKey key =
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings["secret"]));
            SigningCredentials signingCredentials =
                new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken
                (   claims: claims,
                    issuer: jwtSettings["validIssuer"],
                    audience: jwtSettings["validAudience"],
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: signingCredentials
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        public async Task<bool> Logout()
        {
            try
            {
                await signInManager.SignOutAsync();
                return true;
            }
            catch
            {
                return false;
            }
            
        }
    }
}
