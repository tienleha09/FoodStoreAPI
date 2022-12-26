using FoodStoreAPI.Models.Auth;
using FoodStoreAPI.Models.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace FoodStoreAPI.Models.Repository
{
    public class AccountsRepository
    {
        private IdentityContext _identityContext;
        private readonly IAuthenticationManager auth;

        public AccountsRepository(IdentityContext identityContext,
            IAuthenticationManager auth)
        {
            _identityContext = identityContext;
            this.auth = auth;
        }

        
    }
}
