using AAA.src.Domain.Model;
using CommonDll.Dto;

namespace AAA.src.Application.Mapper
{
    public static class UserMapper
    {
        public static User ToUserFromRegisterDto(this RegisterDto registerDto) 
        {
            return new User
            {
                Username = registerDto.Username,
                PasswordHash = HashPassword(registerDto.Password)
            };
        }

        private static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
