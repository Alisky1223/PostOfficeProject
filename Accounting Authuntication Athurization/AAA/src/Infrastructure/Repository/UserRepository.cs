using AAA.src.Application.Mapper;
using AAA.src.Domain.Interface;
using AAA.src.Domain.Model;
using AAA.src.Infrastructure.Data;
using CommonDll.Dto;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AAA.src.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly ITokenService _tokenService;
        private readonly ILoginAttemptRepository _loginAttemptRepository;

        public UserRepository(ApplicationDBContext dBContext, ITokenService tokenService, ILoginAttemptRepository loginAttemptRepository)
        {
            _context = dBContext;
            _tokenService = tokenService;
            _loginAttemptRepository = loginAttemptRepository;
        }

        public async Task<string?> LoginAsync(LoginDto loginDto, string ipAddress)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == loginDto.Username);

            if (user == null || !VerifyPassword(loginDto.Password, user.PasswordHash))
            {
                await _loginAttemptRepository.UnSuccessfullAttempt(loginDto.ToLoginAttemptFromLoginDto(ipAddress));
                return null;
            }

            await _loginAttemptRepository.SuccessfullAttempt(loginDto.ToLoginAttemptFromLoginDto(ipAddress));

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
