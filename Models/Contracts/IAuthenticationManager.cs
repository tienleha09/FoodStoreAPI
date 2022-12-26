using FoodStoreAPI.Models.Auth;
using System.Threading.Tasks;

namespace FoodStoreAPI.Models.Contracts
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(LoginData loginData);
        Task<string> CreateToken(LoginData loginData);
        Task<bool> Logout();
    }
}
