using AAA.src.Domain.Model;
using CommonDll.Dto;

namespace AAA.src.Application.Mapper
{
    public static class LoginAttemptMapper
    {
        public static LoginAttempt ToLoginAttemptFromLoginDto(this LoginDto loginDto, string ipAddress) 
        {
            return new LoginAttempt 
            {
                Username = loginDto?.Username ?? "Unknown User",
                AttemptedAt = DateTime.UtcNow,
                IpAddress = ipAddress
            };
        }
    }
}
