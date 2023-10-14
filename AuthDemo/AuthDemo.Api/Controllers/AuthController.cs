using AuthDemo.Api.Models;
using AuthDemo.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace AuthDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _config;

        public AuthController(IAuthService authService, IConfiguration config)
        {
            _authService = authService;
            _config = config;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(LoginUser user)
        {
            return Ok(await _authService.RegisterUser(user));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUser user)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (!await _authService.Login(user)) return BadRequest();

            byte[] key = Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value);
            string issuer = _config.GetSection("Jwt:Issuer").Value;
            string audience = _config.GetSection("Jwt:Audience").Value;
            string tokenString = user.GenerateTokenString(key, issuer, audience);
            return Ok(tokenString);
        }
    }
}
