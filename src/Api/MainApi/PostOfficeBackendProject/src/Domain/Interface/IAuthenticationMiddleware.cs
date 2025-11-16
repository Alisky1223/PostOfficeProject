using CommonDll.Dto;

namespace PostOfficeProject.Core.src.Domain.Interface
{
    public interface IAuthenticationMiddleware
    {
        Task<ApiResponse<string>> Login(LoginDto loginDto);
        Task<ApiResponse<string>> Register(RegisterDto registerDto);
        Task<ApiResponse<UserDto>> ChangeUserRole(int id, int newRoleId);
    }
}
