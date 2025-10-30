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
        private readonly ILoginAttemptRepository _loginAttemptRepository;
        private readonly IConfiguration _configuration;

        // Configurable thresholds
        private readonly int MaxAttempts;
        private readonly TimeSpan LockoutDuration;

        public UserRepository(ApplicationDBContext dBContext, ITokenService tokenService, ILoginAttemptRepository loginAttemptRepository, IConfiguration configuration)
        {
            _context = dBContext;
            _tokenService = tokenService;
            _loginAttemptRepository = loginAttemptRepository;
            _configuration = configuration;

            MaxAttempts = _configuration.GetValue<int>("Lockout:MaxAttempts", 5);
            LockoutDuration = TimeSpan.FromMinutes(configuration.GetValue<int>("Lockout:Minutes", 2));
        }


        public async Task<User?> ChangeUserRole(int id, int RoleId)
        {
            var targetUser = await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (targetUser == null)  return null;

            targetUser.RoleId = RoleId;

            await _context.SaveChangesAsync();
            return targetUser;
        }

        public async Task<LoginResultDto> LoginAsync(LoginDto loginDto, string ipAddress)
        {
            var user = await _context.Users
                .Include(c => c.Role)
                .FirstOrDefaultAsync(u => u.Username == loginDto.Username);

            //if user not found
            if (user == null)
            {
                await _loginAttemptRepository.UnSuccessfullAttempt(loginDto.ToLoginAttemptFromLoginDto(ipAddress));
                return new LoginResultDto { StatusCode = 401, Message = "Invalid credentials" };
            }

            //if currently locked
            if (user.LockedUntil.HasValue && user.LockedUntil.Value > DateTime.Now)
            {
                if (user.FailedLoginAttempts != 0)
                {
                    user.FailedLoginAttempts = 0;
                    await _context.SaveChangesAsync();
                }

                return new LoginResultDto
                {
                    StatusCode = 403,
                    Message = $"Account locked until {user.LockedUntil:HH:mm:ss}.",
                    LockedUntil = user.LockedUntil
                };
            }

            if (!VerifyPassword(loginDto.Password, user.PasswordHash))
            {
                user.FailedLoginAttempts++;

                if (user.FailedLoginAttempts >= MaxAttempts)
                {
                    user.LockedUntil = DateTime.Now.Add(LockoutDuration);
                    await _context.SaveChangesAsync();

                    return new LoginResultDto
                    {
                        StatusCode = 403,
                        Message = "Too many failed attempts. Account locked",
                        LockedUntil = user.LockedUntil
                    };
                }

                await _context.SaveChangesAsync();

                await _loginAttemptRepository.UnSuccessfullAttempt(loginDto.ToLoginAttemptFromLoginDto(ipAddress));
                int left = MaxAttempts - user.FailedLoginAttempts;
                return new LoginResultDto
                {
                    StatusCode = 401,
                    Message = left > 0 ? $"{left} attempt(s) remaining." : "Last attempt before lockout."
                };
            }

            await _loginAttemptRepository.SuccessfullAttempt(loginDto.ToLoginAttemptFromLoginDto(ipAddress));

            //Reset FailedAttempt
            user.FailedLoginAttempts = 0;
            user.LockedUntil = null;
            await _context.SaveChangesAsync();

            var token = _tokenService.GenerateJwtToken(user);

            return new LoginResultDto
            {
                StatusCode = 200,
                Token = token,
                Message = "Login successful"
            };
        }

        public async Task<string?> RegisterAsync(RegisterDto registerDto)
        {
            if (await _context.Users.AnyAsync(u => u.Username == registerDto.Username))
                return null;

            var userRole = await _context.Role.FirstOrDefaultAsync(x => x.Name == "User");

            if (userRole == null) return null;

            var user = _context.Users.Add(registerDto.ToUserFromRegisterDto());

            user.Entity.RoleId = userRole.Id;

            await _context.SaveChangesAsync();
            return "User registered successfully";
        }

        private bool VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
