using FoodStoreAPI.Models.Auth;
using FoodStoreAPI.Models.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FoodStoreAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationManager authManager;
        private readonly ILogger<AuthenticationController> logger;

        public AuthenticationController(IAuthenticationManager auth,
            ILogger<AuthenticationController> logger)
        {
            authManager = auth;
            this.logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginData loginData)
        {
            bool validated = await authManager.ValidateUser(loginData);
            if (!validated)
            {
                logger.LogError("Invalid login data from client");
                return Unauthorized(new { success = false });
            }
            string token = await authManager.CreateToken(loginData);
            return Ok(new { success = true, token });

        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            bool success = await authManager.Logout();
            if (!success)
            {
                return Unauthorized();
            }
            return Ok(true);
        }
    }
}
