using AAA.src.Application.Mapper;
using AAA.src.Domain.Interface;
using AAA.src.Domain.Model;
using AAA.src.Infrastructure.Data;
using CommonDll.Dto;
using Microsoft.EntityFrameworkCore;

namespace AAA.src.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly ITokenService _tokenService;

        public UserRepository(ApplicationDBContext dBContext, ITokenService tokenService)
        {
            _context = dBContext;
            _tokenService = tokenService;
        }

        public async Task<string?> LoginAsync(LoginDto loginDto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == loginDto.Username);

            if (user == null || !VerifyPassword(loginDto.Password, user.PasswordHash))
                return null;

            return _tokenService.GenerateJwtToken(user);
        }

        public async Task<string?> RegisterAsync(RegisterDto registerDto)
        {
            if (await _context.Users.AnyAsync(u => u.Username == registerDto.Username))
                return null;

            
            _context.Users.Add(registerDto.ToUserFromRegisterDto());
            await _context.SaveChangesAsync();
            return "User registered successfully";
        }

        private bool VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
