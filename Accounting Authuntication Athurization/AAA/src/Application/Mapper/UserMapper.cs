using AAA.src.Domain.Model;
using CommonDll.Dto;

namespace AAA.src.Application.Mapper
{
    public static class UserMapper
    {
        public static UserDto ToDto(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Role = user.Role?.ToDto() ?? null,
            };
        }
        
        public static User ToUserFromRegisterDto(this RegisterDto registerDto) 
        {
            return new User
            {
                Username = registerDto.Username,
                PasswordHash = HashPassword(registerDto.Password),
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                UserEmail = registerDto.UserEmail,
                UserPhone = registerDto.UserPhone
            };
        }

        public static UserPersonalInformationDto ToUserPersonalInformationDto(this User user) 
        {
            return new UserPersonalInformationDto 
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserEmail = user.UserEmail,
                Username = user.Username,
                UserPhone = user.UserPhone
            };
        }

        private static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }


    }
}
