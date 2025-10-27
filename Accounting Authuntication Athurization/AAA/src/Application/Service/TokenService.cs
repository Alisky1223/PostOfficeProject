using AAA.src.Domain.Interface;
using AAA.src.Domain.Model;
using AAA.src.Infrastructure.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AAA.src.Application.Service
{
    public class TokenService : ITokenService
    {
        private readonly ApplicationDBContext _context;
        private readonly IConfiguration _configuration;

        public TokenService(ApplicationDBContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }

        public void ChangeTokenExpireDate(string token, out ClaimsPrincipal? claimsPrincipal)
        {
            throw new NotImplementedException();
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
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
