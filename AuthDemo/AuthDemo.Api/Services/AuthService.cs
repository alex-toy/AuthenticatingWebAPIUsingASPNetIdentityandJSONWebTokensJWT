using AuthDemo.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthDemo.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;

        public AuthService(UserManager<IdentityUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        public async Task<string> RegisterUser(LoginUser user)
        {
            var identityUser = new IdentityUser
            {
                UserName = user.UserName,
                Email = user.UserName
            };

            IdentityResult result = await _userManager.CreateAsync(identityUser, user.Password);

            if (result.Errors.Any()) return result.Errors.Select(e => e.Description).Aggregate((current, next) => current + ", " + next);

            return "user registered";
        }

        public async Task<bool> Login(LoginUser user)
        {
            var identityUser = await _userManager.FindByEmailAsync(user.UserName);

            if (identityUser is null) return false;

            return await _userManager.CheckPasswordAsync(identityUser, user.Password);
        }
    }
}
