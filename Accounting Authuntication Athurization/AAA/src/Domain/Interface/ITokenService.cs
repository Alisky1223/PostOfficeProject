using AAA.src.Domain.Model;
using System.Security.Claims;

namespace AAA.src.Domain.Interface
{
    public interface ITokenService
    {
        string GenerateJwtToken(User user);   
        void DeleteToken(string token);
        void ChangeTokenExpireDate(string token, out ClaimsPrincipal? claimsPrincipal);
        void RefreshToken(string token);
    }
}
