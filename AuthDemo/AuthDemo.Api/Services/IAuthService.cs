using AuthDemo.Api.Models;

namespace AuthDemo.Api.Services
{
    public interface IAuthService
    {
        Task<bool> Login(LoginUser user);
        Task<bool> RegisterUser(LoginUser user);
    }
}