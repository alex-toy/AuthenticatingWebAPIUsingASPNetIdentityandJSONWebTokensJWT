using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AuthDemo.Api.Models
{
    public class LoginUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public string GenerateTokenString(byte[] key, string issuer, string audience)
        {                                             
            List<Claim> claims = GetClaims();

            var securityKey = new SymmetricSecurityKey(key);

            var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signingCred);

            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
        }

        private List<Claim> GetClaims()
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.Email, UserName),
                new Claim(ClaimTypes.Role, "Admin"),
            };
        }
    }
}
