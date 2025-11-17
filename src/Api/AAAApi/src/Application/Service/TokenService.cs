using AAA.src.Domain.Interface;
using AAA.src.Domain.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AAA.src.Application.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ChangeTokenExpireDate(string token, out ClaimsPrincipal? claimsPrincipal)
        {
            throw new NotImplementedException();
        }

        public string GenerateJwtToken(User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            if (user.Role == null)
            {
                throw new InvalidOperationException("User role is null.");
            }


            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.Name ),
            };

            var jwtKey = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("jwt key is not set in configuration");

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public void DeleteToken(string token)
        {
            throw new NotImplementedException();
        }

        public void RefreshToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
