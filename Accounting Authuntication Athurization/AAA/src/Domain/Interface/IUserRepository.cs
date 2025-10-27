using CommonDll.Dto;

namespace AAA.src.Domain.Interface
{
    public interface IUserRepository
    {
        Task<string?> LoginAsync(LoginDto loginDto);
        Task<string?> RegisterAsync(RegisterDto registerDto);
    }
}
