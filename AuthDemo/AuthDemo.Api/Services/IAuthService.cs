using AuthDemo.Api.Models;

namespace AuthDemo.Api.Services
{
    public interface IAuthService
    {
        Task<bool> Login(LoginUser user);
        Task<string> RegisterUser(LoginUser user);
    }
}