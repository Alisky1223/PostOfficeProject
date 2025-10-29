using CommonDll.Dto;

namespace AAA.src.Domain.Interface
{
    public interface IUserRepository
    {
        Task<LoginResultDto> LoginAsync(LoginDto loginDto, string ipAddress);
        Task<string?> RegisterAsync(RegisterDto registerDto);
    }
}
